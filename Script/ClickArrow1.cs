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
        // �̹��� �����
        imageToShow.enabled = false;
    }

    public void OnButtonClick()
    {
        clickCount++; // Ŭ�� Ƚ���� ������ŵ�ϴ�.

        if (clickCount % 3 == 0)
        {
            imageToShow.enabled = false; // 3��° Ŭ�� �� �̹����� ����ϴ�.
        }
        else
        {
            imageToShow.enabled = true; // 1��°�� 2��° Ŭ�� �� �̹����� ���Դϴ�.
        }
    }
}
