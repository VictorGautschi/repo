using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;


// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class IAPManager : MonoBehaviour, IStoreListener
{
    //public static IAPManager Instance { set; get; }

    private bool processPurchaseCalled;
    public Button removeAdsButton;
    public Button restorePurchasesButton1;
    public Text priceText;
    //public Button removeBuyTherapyModeButton;
    //public Button restorePurchasesButton2;
    LevelManager levelManager;
    OnShopCanvas onShopCanvas;

    bool searchCheck;

    //public string buyNoAdsPrice;

    [HideInInspector]
    public static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    // Product identifiers for all products capable of being purchased: 
    // "convenience" general identifiers for use with Purchasing, and their store-specific identifier 
    // counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers 
    // also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

    // General product identifiers for the consumable, non-consumable, and subscription products.
    // Use these handles in the code to reference which product to purchase. Also use these values 
    // when defining the Product Identifiers on the store. Except, for illustration purposes, the 
    // kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
    // specific mapping to Unity Purchasing's AddProduct, below.
#if UNITY_ANDROID
    public static string PRODUCT_NO_ADS = "remove_ads";
    //public static string PRODUCT_THERAPY_MODE = "buy_therapy_mode";
#else 
    public static string PRODUCT_NO_ADS = "com.vormikestudios.ItsPebbleTime.remove_ads";
    //public static string PRODUCT_THERAPY_MODE = "com.vormikestudios.ItsPebbleTime.buy_therapy_mode";

#endif


    private static IAPManager iAPManager;

    public static IAPManager Instance()
    {
        if (!iAPManager)
        {
            iAPManager = FindObjectOfType(typeof(IAPManager)) as IAPManager;
            if (!iAPManager)
            {
                Debug.LogError("There needs to be one active IAPManager script on a GameObject in your scene.");
            }
        }
        return iAPManager;
    }

    private void Awake()
    {
        // Instance = this;
        GameControl.control.Load();

#if UNITY_ANDROID
            restorePurchasesButton1.gameObject.SetActive(false);
            //restorePurchasesButton2.gameObject.SetActive(false);
#endif

        levelManager = LevelManager.Instance();
        onShopCanvas = OnShopCanvas.Instance();
    }

    private void Start()
    {
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }

        //Debug.Log("Disable ads " + GameControl.control.adsDisabled);

        if (GameControl.control.adsDisabled)
        {
            removeAdsButton.gameObject.SetActive(false);
            restorePurchasesButton1.gameObject.SetActive(false);
        }

        //if(GameControl.control.therapyModePurchased)
        //{
        //    removeBuyTherapyModeButton.gameObject.SetActive(false);
        //    restorePurchasesButton2.gameObject.SetActive(false);
        //}

        if (GameControl.control.purchasesRestoredChecked)
        {
            restorePurchasesButton1.gameObject.SetActive(false);
            //restorePurchasesButton2.gameObject.SetActive(false);
        }

        // make sure that the setactive on th buttons only happens once and not every fram in Update.
        searchCheck = true;
    }

    private void Update()
    {
        // It will only disable the buttons if the restorePurchases has not been checked. 
        if (searchCheck == true && !GameControl.control.purchasesRestoredChecked)
        {
            if (priceText.text == "Searching...")
            {
                removeAdsButton.gameObject.SetActive(false);
                restorePurchasesButton1.gameObject.SetActive(false);
            }
            else
            {
                removeAdsButton.gameObject.SetActive(true);
#if UNITY_IOS
                restorePurchasesButton1.gameObject.SetActive(true);
#endif
                searchCheck = false;
            }
        }
    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Continue adding the non-consumable product.
        builder.AddProduct(PRODUCT_NO_ADS, ProductType.NonConsumable);
        //builder.AddProduct(PRODUCT_THERAPY_MODE, ProductType.NonConsumable);

        // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
        // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
        UnityPurchasing.Initialize(this, builder);
    }

    public bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyNoAds()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(PRODUCT_NO_ADS);

        // NBNBNB Victor: This is called by the button which buys the IAP
    }

    //public void BuyTherapyMode()
    //{
    //    BuyProductID(PRODUCT_THERAPY_MODE);

    //}

    private void BuyProductID(string productId)
    {
        onShopCanvas.ShowThatItsPurchasing();

        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                //Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);

            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                //Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                onShopCanvas.PriceToNormal();
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            //Debug.Log("BuyProductID FAIL. Not initialized.");
            onShopCanvas.PriceToNormal();
        }
    }

    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        processPurchaseCalled = false;

        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            //Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer    ||
            Application.platform == RuntimePlatform.OSXEditor)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");
            onShopCanvas.ShowThatItsRestoring();

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) => {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                //Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");

                if (result)
                {
                    restorePurchasesButton1.gameObject.SetActive(false); // disable Restore button
                    //restorePurchasesButton2.gameObject.SetActive(false); // disable Restore button

                    removeAdsButton.gameObject.SetActive(false); 

                    GameControl.control.purchasesRestoredChecked = true;
                    GameControl.control.Save();

                    if (!processPurchaseCalled)
                    {
                        // No previous purchases to restore, so re-enable Purchase button. Victor NBNBNB
                        removeAdsButton.gameObject.SetActive(true);
                        onShopCanvas.PriceToNormal();
                        //removeBuyTherapyModeButton.gameObject.SetActive(true);
                        //Debug.Log("ProcessPurchases not called");
                    }
                }
                else
                {
                    // Don't know what to do here. Something went wrong with RestoreTransactions?
                    Debug.Log("RestorePurchases FAIL. Not supported on this platform.");
                }
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            //Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    //  
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        // Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;

        onShopCanvas.RunGetPrice();
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        //Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);

    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        processPurchaseCalled = true;

        //Debug.Log("processPurchaseCalled " + processPurchaseCalled);

        // a non-consumable product has been purchased by this user.
        if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_NO_ADS, StringComparison.Ordinal))
        {
            //Debug.Log("You have just removed the ads");

            // TODO: The non-consumable item has been successfully purchased, grant this item to the player. NBNBNBNB Victor
            // Remove the purchase button that says "remove ads" here
            //if (!buttonRemoved){
            if(removeAdsButton != null)
                removeAdsButton.gameObject.SetActive(false);
            //    buttonRemoved = true;
            //}

            // Remove the ads here
            GameControl.control.adsDisabled = true;
            GameControl.control.Save();

            // Then if the result is successful, we need to go back to the start menu
            levelManager.LoadLevel("01 Start");

        }

        //if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_THERAPY_MODE, StringComparison.Ordinal))
        //{
        //    //Debug.Log("You have just removed the ads");

        //    // TODO: The non-consumable item has been successfully purchased, grant this item to the player. NBNBNBNB Victor
        //    // Remove the purchase button that says "remove ads" here
        //    //if (!buttonRemoved){
        //    if (removeBuyTherapyModeButton != null)
        //        removeBuyTherapyModeButton.gameObject.SetActive(false);
        //    //    buttonRemoved = true;
        //    //}

        //    // Remove the ads here
        //    GameControl.control.therapyModePurchased = true;
        //    GameControl.control.Save();

        //}

        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        //else
        //{
        //    //Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        //}

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        //Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        onShopCanvas.PriceToNormal();
    }
}

