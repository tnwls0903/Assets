using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Tutorial_1 : MonoBehaviour
{
    public AudioClip clip;
    public TextMeshProUGUI[] texts;
    private int currentTextIndex = 0;

    private void Start()
    {
        StartCoroutine(DisplayTexts());
    }

    IEnumerator DisplayTexts()
    {
        while (true)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].gameObject.SetActive(i == currentTextIndex);
            }

            yield return new WaitForSeconds(1f); //�ʿ信 ���� �ð� ������ �����ϼ���.

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
    }
}