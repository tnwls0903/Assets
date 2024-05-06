using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Talk : MonoBehaviour
{
    public Image imageToShow;
    public TMP_Text textName;
    public TMP_Text[] textsToShow; // �⺻ �ؽ�Ʈ �迭
    public TMP_Text[] specialTextsToShow; // 10 ��� �̻��� ����� Ư���� �ؽ�Ʈ �迭
    private int textIndex = 0;
    private bool isLastTextShown = false;

    private DataController dataController; // ������ ��Ʈ�ѷ� ���� �߰�

    public AudioClip clip1;
    public AudioClip clip2;

    private void Start()
    {
        dataController = FindObjectOfType<DataController>(); // ������ ��Ʈ�ѷ� ã�Ƽ� �Ҵ�

        // �̹����� �ؽ�Ʈ �����
        imageToShow.enabled = false;
        textName.enabled = false;
        foreach (TMP_Text text in textsToShow)
        {
            text.gameObject.SetActive(false);
        }
        foreach (TMP_Text specialText in specialTextsToShow)
        {
            specialText.gameObject.SetActive(false);
        }
    }

    public void OnButtonClick()
    {
        if (isLastTextShown)
        {
            // �̹����� �ؽ�Ʈ �����
            imageToShow.enabled = false;
            textName.enabled = false;
            foreach (TMP_Text text in textsToShow)
            {
                text.gameObject.SetActive(false);
            }
            foreach (TMP_Text specialText in specialTextsToShow)
            {
                specialText.gameObject.SetActive(false);
            }
            isLastTextShown = false;
            textIndex = 0; // ��� �ε����� �ʱ�ȭ�Ͽ� ó������ �ٽ� ����
        }
        else
        {
            if (textIndex > 0)
            {
                imageToShow.enabled = false;
                textName.enabled = false;
                // ���� �ؽ�Ʈ �����
                textsToShow[textIndex - 1].gameObject.SetActive(false);
                specialTextsToShow[textIndex - 1].gameObject.SetActive(false);
            }

            if (textIndex < textsToShow.Length)
            {
                imageToShow.enabled = true;
                textName.enabled = true;

                if (dataController.GetGold() >= 7777) //�ӽð��
                {
                    SoundManager.instance.SFXPlay("hide", clip1);
                    specialTextsToShow[textIndex].gameObject.SetActive(true);

                    // EndingPopup ��ũ��Ʈ���� endingButton�� ���̵��� ����
                    EndingPopup endingPopup = FindObjectOfType<EndingPopup>();
                    if (endingPopup != null)
                    {
                        endingPopup.ShowEndingButton();
                    }
                }
                else
                {
                    textsToShow[textIndex].gameObject.SetActive(true);
                    SoundManager.instance.SFXPlay("hide", clip2);
                }

                textIndex++;

                // ��� ��縦 ����� ��, ���� �ؽ�Ʈ ���
                if (textIndex == textsToShow.Length)
                {
                    isLastTextShown = true;
                }
            }
        }
    }
}
