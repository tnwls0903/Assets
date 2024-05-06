using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public AudioClip clip;

    public void Change()
    {
        SceneManager.LoadScene("BG0");

        SoundManager.instance.SFXPlay("Tuto", clip);
    }

    public void OnClick()
    {
        SoundManager.instance.SFXPlay("ClickFail", clip);
    }
}
