using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pirate : MonoBehaviour
{
    public Button[] buttons;
    public Image[] images;
    public Image[] heartImages;
    public Animator successAnimator;
    public Animator failAnimator;
    public float initialFailChance = 0.3f;
    public float failChanceIncreasePerSuccess = 0.1f; // 성공 애니메이션 횟수당 실패 확률 증가량

    private bool isGameEnded = false;
    private List<int> activeImageIndices = new List<int>();
    private List<int> activeHeartImageIndices = new List<int>();
    private DataController dataController;
    public InputField goldInputField;

    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;

    private float failChance;
    private int goldToAddMultiplier = 1;
    private int maxGoldToAddMultiplier = 8;
    private bool[] buttonClicked;
    private int currentGoldAmount;
    private int initialGold; // 처음 배팅하기 전의 소지금을 저장하는 변수
    private int bettingAmount; // 입력한 배팅금을 저장하는 변수

    private void Start()
    {
        dataController = FindObjectOfType<DataController>();
        HideAllImages();
        HideAllHeartImages();
        failChance = initialFailChance;

        buttonClicked = new bool[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
        }

        initialGold = dataController.GetGold(); // 처음 배팅하기 전의 소지금 저장
    }

    private void HideAllImages()
    {
        foreach (var image in images)
        {
            image.gameObject.SetActive(false);
        }
    }

    private void HideAllHeartImages()
    {
        foreach (var heartImage in heartImages)
        {
            heartImage.gameObject.SetActive(false);
        }
    }

    private void OnButtonClick(int buttonIndex)
    {
        if (isGameEnded || buttonClicked[buttonIndex])
            return;

        buttonClicked[buttonIndex] = true;

        if (!int.TryParse(goldInputField.text, out bettingAmount) || bettingAmount <= 0)
        {
            StartCoroutine(PlayFailAnimation());
            SoundManager.instance.SFXPlay("Fail", clip1);
            SoundManager.instance.SFXPlay("PungDung", clip3);
            ResetFailChance();
            ResetGoldToAddMultiplier();
            return;
        }

        bettingAmount = Mathf.Max(0, bettingAmount); // 음수인 경우 0으로 설정

        int currentGold = dataController.GetGold();

        if (bettingAmount <= currentGold)
        {
            ShowNewImage(buttonIndex);

            foreach (var button in buttons)
            {
                button.interactable = false;
            }

            if (Random.value <= failChance)
            {
                StartCoroutine(PlayFailAnimation());
                int successAnimationCount = activeImageIndices.Count;
                SoundManager.instance.SFXPlay("Fail", clip1);
                SoundManager.instance.SFXPlay("PungDung", clip3);

                // 실패한 경우, 현재 소지금을 초기 소지금 값에서 배팅금을 차감한 값으로 설정
                currentGold = Mathf.Max(0, initialGold - bettingAmount);

                if (currentGold < 0)
                {
                    dataController.SetGold(0);
                }
                else
                {
                    dataController.SetGold(currentGold);
                }

                ResetFailChance();
                ResetGoldToAddMultiplier();
            }
            else
            {
                StartCoroutine(PlaySuccessAnimation());
                int goldToAdd = bettingAmount * goldToAddMultiplier;
                dataController.AddGold(goldToAdd);
                SoundManager.instance.SFXPlay("Success", clip2);
                IncreaseFailChance();
                IncreaseGoldToAddMultiplier();
            }
        }
        else
        {
            StartCoroutine(PlayFailAnimation());
            SoundManager.instance.SFXPlay("Fail", clip1);
            SoundManager.instance.SFXPlay("PungDung", clip3);
            ResetFailChance();
            ResetGoldToAddMultiplier();
        }
    }

    private void ShowNewImage(int imageIndex)
    {
        activeImageIndices.Add(imageIndex);
        images[imageIndex].gameObject.SetActive(true);
    }

    private IEnumerator PlayFailAnimation()
    {
        isGameEnded = true;

        if (failAnimator != null)
        {
            SoundManager.instance.SFXPlay("Eat", clip4);
            failAnimator.SetTrigger("Fail");
            yield return new WaitForSeconds(failAnimator.GetCurrentAnimatorStateInfo(0).length);
        }

        int successAnimationCount = activeImageIndices.Count;
        int goldAmount;
        if (!int.TryParse(goldInputField.text, out goldAmount) || goldAmount <= 0)
        {
            HideAllImages();
            activeImageIndices.Clear();
            HideAllHeartImages();
            activeHeartImageIndices.Clear();

            foreach (var button in buttons)
            {
                button.interactable = true;
            }

            for (int i = 0; i < buttonClicked.Length; i++)
            {
                buttonClicked[i] = false;
            }

            ResetFailChance();
            ResetGoldToAddMultiplier();
            isGameEnded = false;
            yield break;
        }

        HideAllImages();
        activeImageIndices.Clear();
        HideAllHeartImages();
        activeHeartImageIndices.Clear();

        foreach (var button in buttons)
        {
            button.interactable = true;
        }

        for (int i = 0; i < buttonClicked.Length; i++)
        {
            buttonClicked[i] = false;
        }

        // 현재 금액을 성공 애니메이션 재생 횟수가 0회일 때의 값으로 변경
        if (successAnimationCount == 0)
        {
            goldAmount = currentGoldAmount;
        }

        ResetFailChance();
        ResetGoldToAddMultiplier();
        isGameEnded = false;

        // InputField 값을 초기화합니다.
        goldInputField.text = string.Empty;
    }

    private IEnumerator PlaySuccessAnimation()
    {
        isGameEnded = true;

        if (successAnimator != null)
        {
            successAnimator.SetTrigger("Success");
            yield return new WaitForSeconds(successAnimator.GetCurrentAnimatorStateInfo(0).length);
        }

        HideAllImages();
        foreach (int index in activeImageIndices)
        {
            images[index].gameObject.SetActive(true);
            heartImages[index].gameObject.SetActive(true);
            activeHeartImageIndices.Add(index);
        }
        isGameEnded = false;

        foreach (var button in buttons)
        {
            button.interactable = true;
        }
    }

    private void IncreaseFailChance()
    {
        failChance += failChanceIncreasePerSuccess;
    }

    private void ResetFailChance()
    {
        failChance = initialFailChance;
    }

    private void IncreaseGoldToAddMultiplier()
    {
        if (goldToAddMultiplier < maxGoldToAddMultiplier)
        {
            goldToAddMultiplier++;
        }
    }

    private void ResetGoldToAddMultiplier()
    {
        goldToAddMultiplier = 1;
    }
}
