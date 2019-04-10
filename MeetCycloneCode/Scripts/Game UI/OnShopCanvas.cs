using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnShopCanvas : MonoBehaviour
{

    //IAPManager iAPManager;

    string buyNoAdsPrice;
    public Text priceText;

    private static OnShopCanvas onShopCanvas;

    public static OnShopCanvas Instance()
    {
        if (!onShopCanvas)
        {
            onShopCanvas = FindObjectOfType(typeof(OnShopCanvas)) as OnShopCanvas;
            if (!onShopCanvas)
            {
                Debug.LogError("There needs to be one active OnShopCanvas script on a GameObject in your scene.");
            }
        }
        return onShopCanvas;
    }

    private void Start()
    {
        GameControl.control.Load();

        // if the text is blank, that means the initializing has already run (it doesn't seem to run the second time you go into the Store screen) and so we populate the text with the saved price string
        if (priceText.text == "" || priceText.text == "Searching..." && GameControl.control.savedPrice != "")
        {
            priceText.text = GameControl.control.savedPrice;
        }
    }

    public void RunGetPrice()
    {
        // StartCoroutine(FindShopCanvasToSetUpLocalPrice());

        buyNoAdsPrice = IAPManager.m_StoreController.products.WithID(IAPManager.PRODUCT_NO_ADS).metadata.localizedPriceString.ToString();

        // save the price so it can be brought back quickly, but keep saving it everytime so if it changes then it will change
        GameControl.control.savedPrice = buyNoAdsPrice;
        GameControl.control.Save();

        priceText.text = buyNoAdsPrice;
    }

    public void ShowThatItsPurchasing()
    {
        priceText.text = "Purchasing...";
    }

    public void ShowThatItsRestoring()
    {
        priceText.text = "Searching...";
    }

    public void PriceToNormal()
    {
        priceText.text = GameControl.control.savedPrice;
    }
}
