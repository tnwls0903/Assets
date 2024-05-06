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

    private DataController dataController; // DataController 컴포넌트 참조 변수

    private void Start()
    {
        StartCoroutine(DisplayTexts());
        dataController = FindObjectOfType<DataController>(); // DataController 컴포넌트를 찾아 참조 변수에 할당
    }

    IEnumerator DisplayTexts()
    {
        while (true)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].gameObject.SetActive(i == currentTextIndex);
            }

            yield return new WaitForSeconds(1f); // 필요에 따라 시간 간격을 조정하세요.

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

        // DataController 클래스의 m_gold 멤버를 SetGold 메서드를 사용하여 설정
        if (dataController != null)
        {
            dataController.SetGold(dataController.m_gold);
        }
    }
}