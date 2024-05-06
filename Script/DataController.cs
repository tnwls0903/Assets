using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    public int m_gold = 0; // 현재 소지한 골드
    public int m_goldPerClick = 0; // 클릭당 획득하는 골드 양

    void Awake()
    {
        m_gold = PlayerPrefs.GetInt("Gold"); // 저장된 골드 불러오기
        m_goldPerClick = PlayerPrefs.GetInt("GoldPerClick", 1); // 저장된 클릭당 골드 양 불러오기, 기본값은 1
    }

    // 골드 값을 설정합니다.
    public void SetGold(int newGold)
    {
        m_gold = newGold;
        PlayerPrefs.SetInt("Gold", m_gold); // 골드 값을 저장합니다.
    }

    // 골드를 추가합니다.
    public void AddGold(int newGold)
    {
        m_gold += newGold;
        SetGold(m_gold); // 골드 값을 업데이트합니다.
    }

    // 골드를 감소시킵니다.
    public void SubGold(int newGold)
    {
        m_gold -= newGold;
        SetGold(m_gold); // 골드 값을 업데이트합니다.
    }

    // 현재 소지한 골드를 반환합니다.
    public int GetGold()
    {
        return m_gold;
    }

    // 클릭당 획득하는 골드 양을 반환합니다.
    public int GetGoldPerClick()
    {
        return m_goldPerClick;
    }

    // 클릭당 획득하는 골드 양을 설정합니다.
    public void SetGoldPerClick(int newGoldPerClick)
    {
        m_goldPerClick = newGoldPerClick;
        PlayerPrefs.SetInt("GoldPerClick", m_goldPerClick); // 클릭당 골드 양을 저장합니다.
    }
}