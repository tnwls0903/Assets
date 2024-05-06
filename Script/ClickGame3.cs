using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickGame3 : MonoBehaviour
{
    public AudioClip clip;

    public void Change()
    {
        SoundManager.instance.SFXPlay("Click", clip);

        SceneManager.LoadScene("Pirate");
    }
}
