using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickNPC3 : MonoBehaviour
{
    public AudioClip clip;

    public void OnClick()
    {
        SoundManager.instance.SFXPlay("NPC3", clip);
    }
}
