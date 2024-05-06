using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueTrigger : MonoBehaviour
{
    public Dialogue info;
    public void OnParticleTrigger()
    {
        var system = FindObjectOfType<DialogSystem>();
        system.Begin(info);
    }
}
