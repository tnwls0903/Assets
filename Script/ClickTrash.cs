using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTrash : MonoBehaviour
{
    public DataController dataController;
    public AudioClip clip;

    public void OnClick()
    {
        int goldPerClick = dataController.GetGoldPerClick();
        dataController.AddGold(goldPerClick);

        SoundManager.instance.SFXPlay("GetCoin", clip);
    }

}
