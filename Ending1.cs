using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Ending1 : MonoBehaviour
{
    public AudioClip clip;
    public TextMeshProUGUI[] texts;
    private int currentTextIndex = 0;

    private DataController dataController; // DataController ������Ʈ ���� ����

    private void Start()
    {
        StartCoroutine(DisplayTexts());
        dataController = FindObjectOfType<DataController>(); // DataController ������Ʈ�� ã�� ���� ������ �Ҵ�
    }

    IEnumerator DisplayTexts()
    {
        while (true)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].gameObject.SetActive(i == currentTextIndex);
            }

            yield return new WaitForSeconds(1f); // �ʿ信 ���� �ð� ������ �����ϼ���.

            currentTextIndex++;
            if (currentTextIndex >= texts.Length)
            {
                currentTextIndex = 0;
            }
        }
    }

    private void Change()
    {
        SceneManager.LoadScene("BG1");
        SoundManager.instance.SFXPlay("Tuto", clip);

        // DataController Ŭ������ m_gold ����� SetGold �޼��带 ����Ͽ� ����
        if (dataController != null)
        {
            dataController.SetGold(dataController.m_gold);
        }
    }
}