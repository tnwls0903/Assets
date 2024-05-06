using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    private string[] dialogues;
    private int currentDialogueIndex = 0;

    private void Start()
    {
        dialogues = new string[]
        {
            "안녕하세요!",
            "반갑습니다.",
            "잘 지내시죠?",
            "즐거운 게임 되세요!"
        };

        ShowDialogue();
    }

    public void NextDialogue()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex >= dialogues.Length)
        {
            currentDialogueIndex = 0; // 대화가 모두 종료된 경우, 대화 인덱스를 0으로 재설정하여 순환
        }
        ShowDialogue();
    }

    private void ShowDialogue()
    {
        dialogueText.text = dialogues[currentDialogueIndex];
    }
}