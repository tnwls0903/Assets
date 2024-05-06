using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickArrow2 : MonoBehaviour
{
    public Image[] imagesToShow;
    private int[] requiredClickCounts;
    private int clickCount;
    private int currentImageIndex;

    private void Start()
    {
        requiredClickCounts = new int[imagesToShow.Length];
        requiredClickCounts[0] = 2;
        requiredClickCounts[1] = 5;
        requiredClickCounts[2] = 7;
        requiredClickCounts[3] = 9;

        HideAllImages();
    }

    public void OnButtonClick()
    {
        clickCount++;

        if (clickCount == 12)
        {
            HideAllImages();
            clickCount = 0;
            return;
        }

        for (int i = 0; i < imagesToShow.Length; i++)
        {
            if (clickCount == requiredClickCounts[i])
            {
                HideAllImages();
                imagesToShow[i].enabled = true;
                currentImageIndex = i;
                break;
            }
        }
    }

    private void HideAllImages()
    {
        foreach (Image image in imagesToShow)
        {
            image.enabled = false;
        }
    }
}