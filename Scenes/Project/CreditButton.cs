using UnityEngine;
using UnityEngine.UI;

public class CreditButton : MonoBehaviour
{
    public GameObject creditBackground;
    private Animator animator;

    private DataController dataController;

    private bool creditsShown = false;

    private void Start()
    {
        animator = creditBackground.GetComponent<Animator>();
        dataController = FindObjectOfType<DataController>();
        HideCredits();
    }

    public void OnButtonClick()
    {
        int currentGold = dataController.GetGold(); // 현재 골드 양을 가져옵니다.

        if (currentGold >= 7777) // 골드가 7777 이상일 때만 크레딧 동작
        {
            if (!creditsShown)
            {
                ShowCredits();
                // 현재 소지금 초기화
                dataController.SetGold(0);
            }
            else
            {
                HideCredits();

            }
        }
    }

    private void ShowCredits()
    {
        creditsShown = true;
        creditBackground.SetActive(true);
    }

    private void HideCredits()
    {
        creditsShown = false;
        creditBackground.SetActive(false);
    }
}