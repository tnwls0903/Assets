using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSpider : MonoBehaviour
{
    public AudioClip clip;

    public void OnClick()
    {
        SoundManager.instance.SFXPlay("SPIDER", clip);
    }
}
