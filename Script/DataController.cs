using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    public int m_gold = 0; // ���� ������ ���
    public int m_goldPerClick = 0; // Ŭ���� ȹ���ϴ� ��� ��

    void Awake()
    {
        m_gold = PlayerPrefs.GetInt("Gold"); // ����� ��� �ҷ�����
        m_goldPerClick = PlayerPrefs.GetInt("GoldPerClick", 1); // ����� Ŭ���� ��� �� �ҷ�����, �⺻���� 1
    }

    // ��� ���� �����մϴ�.
    public void SetGold(int newGold)
    {
        m_gold = newGold;
        PlayerPrefs.SetInt("Gold", m_gold); // ��� ���� �����մϴ�.
    }

    // ��带 �߰��մϴ�.
    public void AddGold(int newGold)
    {
        m_gold += newGold;
        SetGold(m_gold); // ��� ���� ������Ʈ�մϴ�.
    }

    // ��带 ���ҽ�ŵ�ϴ�.
    public void SubGold(int newGold)
    {
        m_gold -= newGold;
        SetGold(m_gold); // ��� ���� ������Ʈ�մϴ�.
    }

    // ���� ������ ��带 ��ȯ�մϴ�.
    public int GetGold()
    {
        return m_gold;
    }

    // Ŭ���� ȹ���ϴ� ��� ���� ��ȯ�մϴ�.
    public int GetGoldPerClick()
    {
        return m_goldPerClick;
    }

    // Ŭ���� ȹ���ϴ� ��� ���� �����մϴ�.
    public void SetGoldPerClick(int newGoldPerClick)
    {
        m_goldPerClick = newGoldPerClick;
        PlayerPrefs.SetInt("GoldPerClick", m_goldPerClick); // Ŭ���� ��� ���� �����մϴ�.
    }
}