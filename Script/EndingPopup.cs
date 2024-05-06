using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingPopup : MonoBehaviour
{
    public GameObject endingImage; // ���� �̹��� ������Ʈ
    public Button endingButton; // ���� ��ư
    public Button closeButton; // �ݱ� ��ư
    public AudioClip clip1;
    public AudioClip clip2;

    private void Start()
    {
        // ���� �̹��� �� �ݱ� ��ư �ʱ� ���´� ��Ȱ��ȭ
        endingImage.SetActive(false);
        closeButton.gameObject.SetActive(false);
        // ���� ��ư�� �ʱ� ���´� ��Ȱ��ȭ
        endingButton.gameObject.SetActive(false);

        // ���� ��ư�� Ŭ�� �̺�Ʈ ����
        endingButton.onClick.AddListener(ShowEndingElements);

        // �ݱ� ��ư�� Ŭ�� �̺�Ʈ ����
        closeButton.onClick.AddListener(HideEndingElements);
    }

    private void ShowEndingElements()
    {
        // ���� �̹����� �ݱ� ��ư�� Ȱ��ȭ�Ͽ� ���̵��� ��
        endingImage.SetActive(true);
        closeButton.gameObject.SetActive(true);
        SoundManager.instance.SFXPlay("show", clip1);
    }

    private void HideEndingElements()
    {
        // ���� �̹����� �ݱ� ��ư�� ��Ȱ��ȭ�Ͽ� �Ⱥ��̵��� ��
        endingImage.SetActive(false);
        closeButton.gameObject.SetActive(false);
        SoundManager.instance.SFXPlay("hide", clip2);
    }

    public void ShowEndingButton()
    {
        // ���� ��ư�� Ȱ��ȭ�Ͽ� ���̵��� ��
        endingButton.gameObject.SetActive(true);
    }
}
