using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickNPC2 : MonoBehaviour
{
    public AudioClip clip;

    public void OnClick()
    {
        SoundManager.instance.SFXPlay("NPC3", clip);
    }
}
