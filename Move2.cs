using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move2 : MonoBehaviour
{
    public AudioClip clip;

    public void Change()
    {
        SoundManager.instance.SFXPlay("Door", clip);

        SceneManager.LoadScene("BG3");        
    }

}
