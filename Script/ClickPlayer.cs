using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickPlayer : MonoBehaviour
{
    public Image imageToShow;
    public TMP_Text textName;
    public TMP_Text[] textsToShow; // TMP �ؽ�Ʈ �迭
    private int textIndex = -1; // -1�� �ʱ�ȭ�Ͽ� ù ��° �ؽ�Ʈ�� ��µǵ��� ��
    private int buttonClickCount = 0; // ��ư Ŭ�� Ƚ���� ���

    private void Start()
    {
        // �̹����� �ؽ�Ʈ �����
        imageToShow.enabled = false;
        textName.gameObject.SetActive(false);
        foreach (TMP_Text text in textsToShow)
        {
            text.gameObject.SetActive(false);
        }
    }

    public void OnButtonClick()
    {
        // ��ư Ŭ�� Ƚ�� ���
        buttonClickCount++;

        if (buttonClickCount == 1)
        {
            // �̹����� ù ��° TMP �ؽ�Ʈ ���
            imageToShow.enabled = true;
            textName.gameObject.SetActive(true);
            textIndex = 0;
            textsToShow[textIndex].gameObject.SetActive(true);
            textName.text = textsToShow[textIndex].text; // �ؽ�Ʈ �̸� ����
        }
        else if (buttonClickCount == 3)
        {
            // �̹����� �ؽ�Ʈ ��� ����
            imageToShow.enabled = false;
            textName.gameObject.SetActive(false);
            foreach (TMP_Text text in textsToShow)
            {
                text.gameObject.SetActive(false);
            }
            buttonClickCount = 0;
            textIndex = -1;
        }
        else if (buttonClickCount == 2)
        {
            // �̹����� �� ��° TMP �ؽ�Ʈ ���
            imageToShow.enabled = true;
            textName.gameObject.SetActive(true);
            textsToShow[textIndex].gameObject.SetActive(false);
            textIndex = 1;
            textsToShow[textIndex].gameObject.SetActive(true);
            textName.text = textsToShow[textIndex].text; // �ؽ�Ʈ �̸� ����
        }
    }
}
