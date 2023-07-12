using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScrollingBG : MonoBehaviour
{
    public float scrollSpeed;

    private RectTransform imageRectTransform;
    private float offset;

    void Start()
    {
        imageRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 offset = new Vector2(scrollSpeed * Time.deltaTime, 0f);
        imageRectTransform.anchoredPosition += offset;

        if (imageRectTransform.anchoredPosition.x <= -imageRectTransform.rect.width)
        {
            imageRectTransform.anchoredPosition += new Vector2(imageRectTransform.rect.width, 0f);
        }
    }
}