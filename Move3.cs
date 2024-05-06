using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move3 : MonoBehaviour
{
    public AudioClip clip;

    public void Change()
    {
        SoundManager.instance.SFXPlay("Door", clip);

        SceneManager.LoadScene("BG2");
    }
}
