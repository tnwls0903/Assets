using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move1 : MonoBehaviour
{
    public AudioClip clip;

    public void Change()
    {
        StartCoroutine(ChangeSceneAfterSound());
    }

    private IEnumerator ChangeSceneAfterSound()
    {
        SoundManager.instance.SFXPlay("Walk", clip);

        yield return new WaitForSecondsRealtime(0.05f);

        SceneManager.LoadScene("BG1");
    }
}
