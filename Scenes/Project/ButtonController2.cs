using UnityEngine;
using UnityEngine.UI;

public class ButtonController2 : MonoBehaviour
{
    public GameObject panel; // ��Ȱ��ȭ�� �г�
    public GameObject text1; // ��Ȱ��ȭ�� ù ��° �ؽ�Ʈ
    public GameObject text2; // ��Ȱ��ȭ�� �� ��° �ؽ�Ʈ
    public Button button1; // ��ư ��ü
    public Button button2; // ��ư ��ü
    public AudioClip clip1;

    private void Start()
    {
        button1.interactable = false; // ��ư Ȱ��ȭ
        button2.interactable = true; // ��ư Ȱ��ȭ
        button1.GetComponentInChildren<Text>().enabled = false; // ��ư �ؽ�Ʈ ��Ȱ��ȭ
        button1.image.enabled = false; // ��ư �̹��� ��Ȱ��ȭ
    }

    public void OnButtonClick()
    {
        panel.SetActive(false); // �г� Ȱ��ȭ
        text1.SetActive(false); // ù ��° �ؽ�Ʈ Ȱ��ȭ
        text2.SetActive(false); // �� ��° �ؽ�Ʈ Ȱ��ȭ
        SoundManager.instance.SFXPlay("Walk", clip1);
    }
}