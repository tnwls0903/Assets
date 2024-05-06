using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text goldDisplayer;
    public DataController dataController;

    void Update()
    {
        goldDisplayer.text = " " + dataController.GetGold();
    }
}
