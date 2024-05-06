using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Talk : MonoBehaviour
{
    public Image imageToShow;
    public TMP_Text textName;
    public TMP_Text[] textsToShow; // 기본 텍스트 배열
    public TMP_Text[] specialTextsToShow; // 10 골드 이상인 경우의 특별한 텍스트 배열
    private int textIndex = 0;
    private bool isLastTextShown = false;

    private DataController dataController; // 데이터 컨트롤러 변수 추가

    public AudioClip clip1;
    public AudioClip clip2;

    private void Start()
    {
        dataController = FindObjectOfType<DataController>(); // 데이터 컨트롤러 찾아서 할당

        // 이미지와 텍스트 숨기기
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
            // 이미지와 텍스트 숨기기
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
            textIndex = 0; // 대사 인덱스를 초기화하여 처음부터 다시 시작
        }
        else
        {
            if (textIndex > 0)
            {
                imageToShow.enabled = false;
                textName.enabled = false;
                // 이전 텍스트 숨기기
                textsToShow[textIndex - 1].gameObject.SetActive(false);
                specialTextsToShow[textIndex - 1].gameObject.SetActive(false);
            }

            if (textIndex < textsToShow.Length)
            {
                imageToShow.enabled = true;
                textName.enabled = true;

                if (dataController.GetGold() >= 7777) //임시골드
                {
                    SoundManager.instance.SFXPlay("hide", clip1);
                    specialTextsToShow[textIndex].gameObject.SetActive(true);

                    // EndingPopup 스크립트에서 endingButton을 보이도록 설정
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

                // 모든 대사를 출력한 후, 엔딩 텍스트 출력
                if (textIndex == textsToShow.Length)
                {
                    isLastTextShown = true;
                }
            }
        }
    }
}
