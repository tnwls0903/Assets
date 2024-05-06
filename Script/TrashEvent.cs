using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashEvent : MonoBehaviour
{
    public Image messagePanel;  // �޽��� �г��� Image ������Ʈ
    public Text messageText;
    public DataController dataController;
    public int resetGold;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;

    // UI ��ư Ŭ�� �� ȣ��� �Լ�
    public void OnClick()
    {
        // �������� 100 �̻��� ��쿡�� �̺�Ʈ�� ó��
        if (dataController.GetGold() >= 101 || dataController.GetGold()<0)
        {
            SoundManager.instance.SFXPlay("Event", clip1);
            SoundManager.instance.SFXPlay("Event", clip3);

            // �������� �ʱ�ȭ
            dataController.SetGold(0);

            // �޽��� �г� Ȱ��ȭ
            messagePanel.gameObject.SetActive(true);

            // �޽��� �ؽ�Ʈ ���� �� ���̵� �� �ִϸ��̼�
            messageText.text = "������ ���Ѻ��� �����ߵ��ڰ� ������ ���������ϴ�!";
            StartCoroutine(DeactivateMessagePanel());
        }
    }

    // ���� �ð� �� �޽��� �г� ��Ȱ��ȭ
    IEnumerator DeactivateMessagePanel()
    {
        yield return new WaitForSeconds(2f);
        //SoundManager.instance.SFXPlay("SubCoin", clip2);

        // �޽��� �г� ��Ȱ��ȭ
        messagePanel.gameObject.SetActive(false);
    }
}