using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashEvent : MonoBehaviour
{
    public Image messagePanel;  // 메시지 패널의 Image 컴포넌트
    public Text messageText;
    public DataController dataController;
    public int resetGold;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;

    // UI 버튼 클릭 시 호출될 함수
    public void OnClick()
    {
        // 소지금이 100 이상인 경우에만 이벤트를 처리
        if (dataController.GetGold() >= 101 || dataController.GetGold()<0)
        {
            SoundManager.instance.SFXPlay("Event", clip1);
            SoundManager.instance.SFXPlay("Event", clip3);

            // 소지금을 초기화
            dataController.SetGold(0);

            // 메시지 패널 활성화
            messagePanel.gameObject.SetActive(true);

            // 메시지 텍스트 설정 및 페이드 인 애니메이션
            messageText.text = "옆에서 지켜보던 도박중독자가 지갑을 가져갔습니다!";
            StartCoroutine(DeactivateMessagePanel());
        }
    }

    // 일정 시간 후 메시지 패널 비활성화
    IEnumerator DeactivateMessagePanel()
    {
        yield return new WaitForSeconds(2f);
        //SoundManager.instance.SFXPlay("SubCoin", clip2);

        // 메시지 패널 비활성화
        messagePanel.gameObject.SetActive(false);
    }
}