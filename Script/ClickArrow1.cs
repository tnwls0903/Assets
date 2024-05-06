using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickArrow1 : MonoBehaviour
{
    public Image imageToShow;
    private int clickCount = 0;

    private void Start()
    {
        // 이미지 숨기기
        imageToShow.enabled = false;
    }

    public void OnButtonClick()
    {
        clickCount++; // 클릭 횟수를 증가시킵니다.

        if (clickCount % 3 == 0)
        {
            imageToShow.enabled = false; // 3번째 클릭 시 이미지를 숨깁니다.
        }
        else
        {
            imageToShow.enabled = true; // 1번째와 2번째 클릭 시 이미지를 보입니다.
        }
    }
}
