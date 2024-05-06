using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameControl : MonoBehaviour
{
    public static event Action HandlePulled = delegate { };

    public DataController dataController;

    [SerializeField]
    private Text prizeText;

    [SerializeField]
    private Row[] rows;

    [SerializeField]
    private Transform handle;

    [SerializeField]
    private Text coinText;

    private int prizeValue;

    private bool resultChecked = false;

    private bool handlePulled = false; // 핸들이 당겨졌는지 여부를 저장하는 변수

    public AudioClip clip;

    // Update is called once per frame
    void Update()
    {
        if (!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped)
        {
            prizeValue = 0;
            prizeText.enabled = false;
            resultChecked = false;
        }

        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultChecked)
        {
            CheckResults();
            prizeText.enabled = true;
            prizeText.text = "점수판";

            // 결과에 따라 소지금 갱신
            int gold = dataController.GetGold(); // 현재 소지금 가져오기
            int score = prizeValue / 10;

            if (prizeValue != 0 && handlePulled)
            {
                if (gold >= 10)
                {
                    gold = (gold - 10) + (prizeValue / 10); // 점수가 0이 아닌 경우
                    prizeText.text = prizeValue + "점이므로\n 얻은 금액은 " + score + "골드입니다!!";
                }
            }
            else if (handlePulled)
            {
                if (gold >= 10)
                {
                    gold -= 10; // 점수가 0인 경우
                    prizeText.text = "아이고~~~\n아쉬워라~~~";
                }
            }
            else if(gold == 0)
            {
                prizeText.text = "금액이 부족합니다.";
            }

            dataController.SetGold(gold); // 수정된 소지금을 업데이트합니다.
            resultChecked = true;
        }
    }

    private void OnMouseDown() //핸들
    {
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
        {
            StartCoroutine("PullHandle");
            handlePulled = true; // 핸들이 당겨졌음을 표시
            SoundManager.instance.SFXPlay("Pull", clip);
        }
    }

    private IEnumerator PullHandle()
    {
        for (int i = 0; i < 15; i += 5)
        {
            handle.Rotate(0f, 0f, i);
            yield return new WaitForSeconds(0.1f);
        }

        HandlePulled();

        for (int i = 0; i < 15; i += 5)
        {
            handle.Rotate(0f, 0f, -i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void CheckResults()
    {
        if (rows[0].stoppedSlot == "Diamond" && rows[1].stoppedSlot == "Diamond" && rows[2].stoppedSlot == "Diamond")
            prizeValue = 200;
        else if (rows[0].stoppedSlot == "Crown" && rows[1].stoppedSlot == "Crown" && rows[2].stoppedSlot == "Crown")
            prizeValue = 400;
        else if (rows[0].stoppedSlot == "Melon" && rows[1].stoppedSlot == "Melon" && rows[2].stoppedSlot == "Melon")
            prizeValue = 600;
        else if (rows[0].stoppedSlot == "Bar" && rows[1].stoppedSlot == "Bar" && rows[2].stoppedSlot == "Bar")
            prizeValue = 800;
        else if (rows[0].stoppedSlot == "Seven" && rows[1].stoppedSlot == "Seven" && rows[2].stoppedSlot == "Seven")
            prizeValue = 1500;
        else if (rows[0].stoppedSlot == "Cherry" && rows[1].stoppedSlot == "Cherry" && rows[2].stoppedSlot == "Cherry")
            prizeValue = 3000;
        else if (rows[0].stoppedSlot == "Lemon" && rows[1].stoppedSlot == "Lemon" && rows[2].stoppedSlot == "Lemon")
            prizeValue = 5000;
        else if (((rows[0].stoppedSlot == rows[1].stoppedSlot) && (rows[0].stoppedSlot == "Diamond")) || ((rows[0].stoppedSlot == rows[2].stoppedSlot) && (rows[0].stoppedSlot == "Diamond")) ||
            ((rows[1].stoppedSlot == rows[2].stoppedSlot) && (rows[1].stoppedSlot == "Diamond")))
            prizeValue = 100;
        else if (((rows[0].stoppedSlot == rows[1].stoppedSlot) && (rows[0].stoppedSlot == "Crown")) || ((rows[0].stoppedSlot == rows[2].stoppedSlot) && (rows[0].stoppedSlot == "Crown")) ||
            ((rows[1].stoppedSlot == rows[2].stoppedSlot) && (rows[1].stoppedSlot == "Crown")))
            prizeValue = 300;
        else if (((rows[0].stoppedSlot == rows[1].stoppedSlot) && (rows[0].stoppedSlot == "Melon")) || ((rows[0].stoppedSlot == rows[2].stoppedSlot) && (rows[0].stoppedSlot == "Melon")) ||
            ((rows[1].stoppedSlot == rows[2].stoppedSlot) && (rows[1].stoppedSlot == "Melon")))
            prizeValue = 500;
        else if (((rows[0].stoppedSlot == rows[1].stoppedSlot) && (rows[0].stoppedSlot == "Bar")) || ((rows[0].stoppedSlot == rows[2].stoppedSlot) && (rows[0].stoppedSlot == "Bar")) ||
            ((rows[1].stoppedSlot == rows[2].stoppedSlot) && (rows[1].stoppedSlot == "Bar")))
            prizeValue = 700;
        else if (((rows[0].stoppedSlot == rows[1].stoppedSlot) && (rows[0].stoppedSlot == "Seven")) || ((rows[0].stoppedSlot == rows[2].stoppedSlot) && (rows[0].stoppedSlot == "Seven")) ||
            ((rows[1].stoppedSlot == rows[2].stoppedSlot) && (rows[1].stoppedSlot == "Seven")))
            prizeValue = 1000;
        else if (((rows[0].stoppedSlot == rows[1].stoppedSlot) && (rows[0].stoppedSlot == "Cherry")) || ((rows[0].stoppedSlot == rows[2].stoppedSlot) && (rows[0].stoppedSlot == "Cherry")) ||
            ((rows[1].stoppedSlot == rows[2].stoppedSlot) && (rows[1].stoppedSlot == "Cherry")))
            prizeValue = 2000;
        else if (((rows[0].stoppedSlot == rows[1].stoppedSlot) && (rows[0].stoppedSlot == "Lemon")) || ((rows[0].stoppedSlot == rows[2].stoppedSlot) && (rows[0].stoppedSlot == "Lemon")) ||
            ((rows[1].stoppedSlot == rows[2].stoppedSlot) && (rows[1].stoppedSlot == "Lemon")))
            prizeValue = 4000;
        else
            prizeValue = 0; // 결과 조건에 해당하지 않으면 0으로 설정

        resultChecked = true;
    }
}