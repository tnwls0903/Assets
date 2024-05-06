using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    public AudioClip clip;

    public void Change()
    {
        SceneManager.LoadScene("BG1");

        SoundManager.instance.SFXPlay("Start", clip);
    }
}
