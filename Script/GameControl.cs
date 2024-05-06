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

    private bool handlePulled = false; // �ڵ��� ��������� ���θ� �����ϴ� ����

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
            prizeText.text = "������";

            // ����� ���� ������ ����
            int gold = dataController.GetGold(); // ���� ������ ��������
            int score = prizeValue / 10;

            if (prizeValue != 0 && handlePulled)
            {
                if (gold >= 10)
                {
                    gold = (gold - 10) + (prizeValue / 10); // ������ 0�� �ƴ� ���
                    prizeText.text = prizeValue + "���̹Ƿ�\n ���� �ݾ��� " + score + "����Դϴ�!!";
                }
            }
            else if (handlePulled)
            {
                if (gold >= 10)
                {
                    gold -= 10; // ������ 0�� ���
                    prizeText.text = "���̰�~~~\n�ƽ�����~~~";
                }
            }
            else if(gold == 0)
            {
                prizeText.text = "�ݾ��� �����մϴ�.";
            }

            dataController.SetGold(gold); // ������ �������� ������Ʈ�մϴ�.
            resultChecked = true;
        }
    }

    private void OnMouseDown() //�ڵ�
    {
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
        {
            StartCoroutine("PullHandle");
            handlePulled = true; // �ڵ��� ��������� ǥ��
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
            prizeValue = 0; // ��� ���ǿ� �ش����� ������ 0���� ����

        resultChecked = true;
    }
}