using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ClickableButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite clickSprite;
    public float iconShrinkPercent = 0.8f;
    Image img;
    Transform childImg;
    Vector3 childImgScale = Vector3.one;
    Sprite originalSprite;

    private void Awake()
    {
        img = GetComponent<Image>();
        originalSprite = img.sprite;
        childImg = transform.GetChild(0);
        if (childImg != null) childImgScale = childImg.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        img.sprite = clickSprite;
        childImg.transform.localScale = childImgScale * iconShrinkPercent;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        img.sprite = originalSprite;
        childImg.transform.localScale = childImgScale;
    }
}

