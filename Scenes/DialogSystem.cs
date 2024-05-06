using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public Text txtName;
    public Text txtSentence;

    Queue<string> sentences = new Queue<string>();

    public Animator anim;
    
    public void Begin(Dialogue info)
    {
        anim.SetBool("isOpen", true);
        

        sentences.Clear();

        txtName.text = info.name;

        foreach (var sentence in info.sentences)
        {
            sentences.Enqueue(sentence);

        }

        Next();

    }

    public void Next()
    {
        if(sentences.Count == 0)
        {
            End();
            return;
        }
        txtSentence.text = sentences.Dequeue();
    }

    private void End()
    {
        anim.SetBool("isOpen", false);
        txtSentence.text = string.Empty;
    }
}
