using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickNPC4 : MonoBehaviour
{
    public AudioClip clip;

    public void OnClick()
    {
        SoundManager.instance.SFXPlay("NPC4", clip);
    }
}
