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
        int currentGold = dataController.GetGold(); // ���� ��� ���� �����ɴϴ�.

        if (currentGold >= 7777) // ��尡 7777 �̻��� ���� ũ���� ����
        {
            if (!creditsShown)
            {
                ShowCredits();
                // ���� ������ �ʱ�ȭ
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