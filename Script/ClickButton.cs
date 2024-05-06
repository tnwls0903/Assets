using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour
{
    public AudioClip clip;

    public void OnClick()
    {
        SoundManager.instance.SFXPlay("SubCoin", clip);
    }
}
