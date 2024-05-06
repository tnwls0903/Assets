using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickPlayer : MonoBehaviour
{
    public Image imageToShow;
    public TMP_Text textName;
    public TMP_Text[] textsToShow; // TMP 텍스트 배열
    private int textIndex = -1; // -1로 초기화하여 첫 번째 텍스트가 출력되도록 함
    private int buttonClickCount = 0; // 버튼 클릭 횟수를 기록

    private void Start()
    {
        // 이미지와 텍스트 숨기기
        imageToShow.enabled = false;
        textName.gameObject.SetActive(false);
        foreach (TMP_Text text in textsToShow)
        {
            text.gameObject.SetActive(false);
        }
    }

    public void OnButtonClick()
    {
        // 버튼 클릭 횟수 기록
        buttonClickCount++;

        if (buttonClickCount == 1)
        {
            // 이미지와 첫 번째 TMP 텍스트 출력
            imageToShow.enabled = true;
            textName.gameObject.SetActive(true);
            textIndex = 0;
            textsToShow[textIndex].gameObject.SetActive(true);
            textName.text = textsToShow[textIndex].text; // 텍스트 이름 설정
        }
        else if (buttonClickCount == 3)
        {
            // 이미지와 텍스트 모두 숨김
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
            // 이미지와 두 번째 TMP 텍스트 출력
            imageToShow.enabled = true;
            textName.gameObject.SetActive(true);
            textsToShow[textIndex].gameObject.SetActive(false);
            textIndex = 1;
            textsToShow[textIndex].gameObject.SetActive(true);
            textName.text = textsToShow[textIndex].text; // 텍스트 이름 설정
        }
    }
}
