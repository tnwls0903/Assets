using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brand : MonoBehaviour
{
    public InputField goldInputField; // ���ñ� �Է� �ʵ�

    public Image[] imageObjects; // 6���� �̹��� ��ü���� �迭�� ����
    public Animator successAnimator; // ���� �ִϸ��̼�
    public Animator failureAnimator; // ���� �ִϸ��̼�
    public Image successImage; // ���� �̹���
    public Image failureImage; // ���� �̹���
    public Button upgradeButton; // ������Ʈ ��ư

    private int goldToAddMultiplier = 1;
    private bool isAnimating; // �ִϸ��̼� ���� ������ ���� Ȯ��
    private int successAnimationCount; // ���� �ִϸ��̼� ��� Ƚ��
    private float successAnimationChance = 0.7f; // �ʱ� ���� �ִϸ��̼� ��� Ȯ��
    private float successAnimationChanceDecrease = 0.1f; // ���� �ִϸ��̼� ��� Ȯ�� ���ҷ�
    private int initialGold; // �����ϱ� ���� �ʱ� ������ ���¸� �����ϴ� ����
    private int maxGoldToAddMultiplier = 8;

    private DataController dataController;

    public AudioClip clip1;
    public AudioClip clip2;

    private void Start()
    {
        dataController = FindObjectOfType<DataController>();

        initialGold = dataController.GetGold(); // ���� ���� �� �ʱ� ������ ����

        // �̹��� ��ü���� ��� ��Ȱ��ȭ
        HideAllImages();

        // ���� �̹����� ���� �̹����� ��Ȱ��ȭ
        successImage.gameObject.SetActive(false);
        failureImage.gameObject.SetActive(false);

        // ������Ʈ ��ư�� Ŭ�� �̺�Ʈ ����
        upgradeButton.onClick.AddListener(OnUpdateButtonClick);
    }

    private void HideAllImages()
    {
        foreach (Image image in imageObjects)
        {
            image.gameObject.SetActive(false);
        }
    }

    private bool IsValidBettingAmount(string input)
    {
        int amount;
        if (int.TryParse(input, out amount))
        {
            // �Է°��� �����̰ų� 0�� ��� ��ȿ���� ����
            if (amount <= 0)
                return false;
            else
                return true;
        }
        else
        {
            return false;
        }
    }

    private void OnUpdateButtonClick()
    {
        if (!isAnimating)
        {
            // ���ñ� �Է� ���� �״�� ���
            string bettingAmountText = goldInputField.text;

            // ���ñ��� ��ȿ�� �������� Ȯ��
            if (IsValidBettingAmount(bettingAmountText))
            {
                int bettingAmount = int.Parse(bettingAmountText);

                // ���ñ��� ���� �����ݺ��� ũ�ų� ���� ��� ���� �ִϸ��̼� ���
                if (bettingAmount > initialGold)
                {
                    // ���� �ִϸ��̼� ���
                    failureAnimator.SetTrigger("Fail");

                    // �ִϸ��̼��� ������ �ִϸ��̼� ���� �ʱ�ȭ
                    StartCoroutine(ResetAnimationState());
                    return;

                    SoundManager.instance.SFXPlay("Click", clip2);
                }

                // ���ñ��� ���� �����ݺ��� �۰ų� ���� ��쿡�� �ִϸ��̼� ����
                if (bettingAmount <= initialGold)
                {
                    // �����ϱ� �� �����ݿ��� ������ ����ŭ ����
                   

                    // �ִϸ��̼� ���� ���� �ƴϸ� �ִϸ��̼� ���
                    isAnimating = true;

                    // ���� �Ǵ� ���� �ִϸ��̼� ���
                    if (Random.value < successAnimationChance)
                    {
                        // ���� �ִϸ��̼� ���
                        successAnimationCount++;
                        successAnimator.SetTrigger("Success");

                        // ������ ��
                        int goldToAdd = bettingAmount * goldToAddMultiplier;
                        goldToAddMultiplier++;
                        dataController.AddGold(goldToAdd);
                        IncreaseGoldToAddMultiplier();

                        // ���� Ƚ���� ���� ���� Ȯ�� ����
                        if (successAnimationCount >= 5)
                        {
                            successAnimationChance = 0.1f; // ���� Ȯ�� 90%
                        }

                        SoundManager.instance.SFXPlay("Click", clip1);
                    }
                    else
                    {
                        // ������ ���� ���� Ȯ���� ����
                        successAnimationChance += successAnimationChanceDecrease;
                        if (successAnimationChance > 1.0f)
                            successAnimationChance = 1.0f; // �ִ� Ȯ�� ����

                        // ���� �ִϸ��̼� ���
                        failureAnimator.SetTrigger("Fail");

                        successAnimationCount = 0; // ���� �� ���� �ִϸ��̼� ��� Ƚ�� �ʱ�ȭ                                 

                        // ���н� �̹��� ���¸� �ʱ�ȭ
                        ResetImageState();

                        // �����ϱ� �� �����ݿ��� ������ ����ŭ ����
                        dataController.SetGold(initialGold - bettingAmount);

                        SoundManager.instance.SFXPlay("Click", clip2);

                        ResetGoldToAddMultiplier();
                    }

                    // �ִϸ��̼��� ������ �ִϸ��̼� ���� �ʱ�ȭ
                    StartCoroutine(ResetAnimationState());
                }
            }
            else
            {               
                // ���ñ��� ��ȿ���� ���� ��� ���� �ִϸ��̼� ���
                failureAnimator.SetTrigger("Fail");

                // �ִϸ��̼��� ������ �ִϸ��̼� ���� �ʱ�ȭ
                StartCoroutine(ResetAnimationState());

                initialGold = dataController.GetGold(); // �ʱ� ��� �ʱ�ȭ

                SoundManager.instance.SFXPlay("Click", clip2);
            }
        }
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

    private IEnumerator ResetAnimationState()
    {
        // �ִϸ��̼� ������� ���
        yield return new WaitForSeconds(Mathf.Max(successAnimator.GetCurrentAnimatorStateInfo(0).length,
                                                  failureAnimator.GetCurrentAnimatorStateInfo(0).length));

        if (successAnimationCount > 0)
        {
            // ���� �̹��� ��� �� 3�� �ڿ� ���� �ִϸ��̼� ���Ƚ���� ���� ���������� �̹��� ���
            successImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            successImage.gameObject.SetActive(false);

            // �̹��� ��ü���� ���������� Ȱ��ȭ
            for (int i = 0; i < successAnimationCount; i++)
            {
                if (i < imageObjects.Length)
                {
                    // ���� �̹����� ��Ȱ��ȭ�ϰ�, �ش� �ε����� �̹����� Ȱ��ȭ
                    if (i > 0)
                        imageObjects[i - 1].gameObject.SetActive(false);

                    imageObjects[i].gameObject.SetActive(true);
                }
            }
        }
        else
        {
            // ���� �̹��� ��� �� 3�� �ڿ� ���� �ִϸ��̼� ���Ƚ�� �ʱ�ȭ
            failureImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            failureImage.gameObject.SetActive(false);
        }

        // �ִϸ��̼� ���� ���¸� �ʱ�ȭ
        isAnimating = false;
    }

    private void ResetImageState()
    {
        // �̹��� ��ü���� ��� ��Ȱ��ȭ
        HideAllImages();

        // ���� �̹����� ���� �̹����� ��Ȱ��ȭ
        successImage.gameObject.SetActive(false);
        failureImage.gameObject.SetActive(false);

        // ���� �ִϸ��̼� ��� Ȯ�� ����
        successAnimationChance -= successAnimationChanceDecrease;
    }

}
