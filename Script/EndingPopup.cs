using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingPopup : MonoBehaviour
{
    public GameObject endingImage; // 엔딩 이미지 오브젝트
    public Button endingButton; // 엔딩 버튼
    public Button closeButton; // 닫기 버튼
    public AudioClip clip1;
    public AudioClip clip2;

    private void Start()
    {
        // 엔딩 이미지 및 닫기 버튼 초기 상태는 비활성화
        endingImage.SetActive(false);
        closeButton.gameObject.SetActive(false);
        // 엔딩 버튼의 초기 상태는 비활성화
        endingButton.gameObject.SetActive(false);

        // 엔딩 버튼에 클릭 이벤트 연결
        endingButton.onClick.AddListener(ShowEndingElements);

        // 닫기 버튼에 클릭 이벤트 연결
        closeButton.onClick.AddListener(HideEndingElements);
    }

    private void ShowEndingElements()
    {
        // 엔딩 이미지와 닫기 버튼을 활성화하여 보이도록 함
        endingImage.SetActive(true);
        closeButton.gameObject.SetActive(true);
        SoundManager.instance.SFXPlay("show", clip1);
    }

    private void HideEndingElements()
    {
        // 엔딩 이미지와 닫기 버튼을 비활성화하여 안보이도록 함
        endingImage.SetActive(false);
        closeButton.gameObject.SetActive(false);
        SoundManager.instance.SFXPlay("hide", clip2);
    }

    public void ShowEndingButton()
    {
        // 엔딩 버튼을 활성화하여 보이도록 함
        endingButton.gameObject.SetActive(true);
    }
}
