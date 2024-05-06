using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickpocket : MonoBehaviour
{
    public Fade fadeManager;
    public Image messagePanel;  // 메시지 패널의 Image 컴포넌트
    public Text messageText;
    public DataController dataController;
    public int stolenAmount = 500;
    public int GetAmount = 7000; //조건
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;

    void Start()
    {
        StartCoroutine(CheckEvent());
    }

    IEnumerator CheckEvent()
    {
        // 현재 골드가 10 이상인 경우에만 이벤트를 실행합니다.
        if (dataController.GetGold() >= GetAmount)
        {
            if (Random.Range(0f, 1f) < 0.5f) //확률 설정
            {
                SoundManager.instance.SFXPlay("Event", clip1);
                //SoundManager.instance.SFXPlay("Event", clip3);
                yield return StartCoroutine(TriggerEvent());
            }
            else
            {
                yield return StartCoroutine(FadeIn());
            }
        }
    }

    IEnumerator TriggerEvent()
    {
        yield return StartCoroutine(fadeManager.FadeOut());

        // 현재 골드가 10 이상인 경우에만 골드를 차감합니다.
        if (dataController.GetGold() >= GetAmount)
        {
            SoundManager.instance.SFXPlay("SubCoin", clip2);
            dataController.SubGold(stolenAmount);

            // 메시지 패널 활성화
            messagePanel.gameObject.SetActive(true);

            // 메시지 텍스트 설정 및 페이드 인 애니메이션
            messageText.text = "소매치기가 당신의 골드를 훔쳤습니다!";
            yield return new WaitForSeconds(2f);

            // 메시지 패널 비활성화
            messagePanel.gameObject.SetActive(false);
        }

        yield return StartCoroutine(fadeManager.FadeIn());
    }

    IEnumerator FadeIn()
    {
        yield return StartCoroutine(fadeManager.FadeIn());
    }
}
