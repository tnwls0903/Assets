using UnityEngine;
using UnityEngine.UI;

public class ButtonController2 : MonoBehaviour
{
    public GameObject panel; // 비활성화된 패널
    public GameObject text1; // 비활성화된 첫 번째 텍스트
    public GameObject text2; // 비활성화된 두 번째 텍스트
    public Button button1; // 버튼 객체
    public Button button2; // 버튼 객체
    public AudioClip clip1;

    private void Start()
    {
        button1.interactable = false; // 버튼 활성화
        button2.interactable = true; // 버튼 활성화
        button1.GetComponentInChildren<Text>().enabled = false; // 버튼 텍스트 비활성화
        button1.image.enabled = false; // 버튼 이미지 비활성화
    }

    public void OnButtonClick()
    {
        panel.SetActive(false); // 패널 활성화
        text1.SetActive(false); // 첫 번째 텍스트 활성화
        text2.SetActive(false); // 두 번째 텍스트 활성화
        SoundManager.instance.SFXPlay("Walk", clip1);
    }
}