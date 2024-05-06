using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class gotoSlot : MonoBehaviour
{
    public void change()
    {
        SceneManager.LoadScene("SlotMachine");
    }
}
