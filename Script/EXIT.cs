using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EXIT : MonoBehaviour
{
    public AudioClip clip;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            GameQuit();
    }

    public void GameQuit()
    {
        SoundManager.instance.SFXPlay("Exit", clip);

        Application.Quit();
    }

    public void Change()
    {
        SoundManager.instance.SFXPlay("Exit", clip);

        SceneManager.LoadScene("BG3");
    }
}
