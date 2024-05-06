using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickpocket : MonoBehaviour
{
    public Fade fadeManager;
    public Image messagePanel;  // �޽��� �г��� Image ������Ʈ
    public Text messageText;
    public DataController dataController;
    public int stolenAmount = 500;
    public int GetAmount = 7000; //����
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;

    void Start()
    {
        StartCoroutine(CheckEvent());
    }

    IEnumerator CheckEvent()
    {
        // ���� ��尡 10 �̻��� ��쿡�� �̺�Ʈ�� �����մϴ�.
        if (dataController.GetGold() >= GetAmount)
        {
            if (Random.Range(0f, 1f) < 0.5f) //Ȯ�� ����
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

        // ���� ��尡 10 �̻��� ��쿡�� ��带 �����մϴ�.
        if (dataController.GetGold() >= GetAmount)
        {
            SoundManager.instance.SFXPlay("SubCoin", clip2);
            dataController.SubGold(stolenAmount);

            // �޽��� �г� Ȱ��ȭ
            messagePanel.gameObject.SetActive(true);

            // �޽��� �ؽ�Ʈ ���� �� ���̵� �� �ִϸ��̼�
            messageText.text = "�Ҹ�ġ�Ⱑ ����� ��带 ���ƽ��ϴ�!";
            yield return new WaitForSeconds(2f);

            // �޽��� �г� ��Ȱ��ȭ
            messagePanel.gameObject.SetActive(false);
        }

        yield return StartCoroutine(fadeManager.FadeIn());
    }

    IEnumerator FadeIn()
    {
        yield return StartCoroutine(fadeManager.FadeIn());
    }
}
