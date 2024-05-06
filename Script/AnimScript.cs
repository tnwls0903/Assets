using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrandScript : MonoBehaviour
{
    public Image[] imageObjects; // 6���� �̹��� ��ü���� �迭�� ����
    public Animation successAnimation; // ���� �ִϸ��̼�
    public Animation failureAnimation; // ���� �ִϸ��̼�
    public Image successImage; // ���� �̹���
    public Image failureImage; // ���� �̹���

    private bool isAnimating; // �ִϸ��̼� ���� ������ ���� Ȯ��
    private int successAnimationCount; // ���� �ִϸ��̼� ��� Ƚ��

    private void Start()
    {
        // �̹��� ��ü���� ��� ��Ȱ��ȭ
        foreach (Image image in imageObjects)
        {
            image.gameObject.SetActive(false);
        }

        // ���� �̹����� ���� �̹����� ��Ȱ��ȭ
        successImage.gameObject.SetActive(false);
        failureImage.gameObject.SetActive(false);

        // ������Ʈ ��ư�� Ŭ�� �̺�Ʈ ����
        Button updateButton = GetComponent<Button>();
        if (updateButton != null)
        {
            updateButton.onClick.AddListener(OnUpdateButtonClick);
        }
    }

    private void OnUpdateButtonClick()
    {
        if (!isAnimating)
        {
            // �ִϸ��̼� ���� ���� �ƴϸ� �ִϸ��̼� ���
            isAnimating = true;

            // ���� �Ǵ� ���� �ִϸ��̼� ���
            if (Random.value < 0.5f)
            {
                // 50% Ȯ���� ���� �ִϸ��̼� ���
                successAnimationCount++;
                successAnimation.Play();
            }
            else
            {
                // 50% Ȯ���� ���� �ִϸ��̼� ���
                failureAnimation.Play();
                successAnimationCount = 0; // ���� �� ���� �ִϸ��̼� ��� Ƚ�� �ʱ�ȭ
            }

            // �ִϸ��̼��� ������ �ִϸ��̼� ���� �ʱ�ȭ
            StartCoroutine(ResetAnimationState());
        }
    }

    private IEnumerator ResetAnimationState()
    {
        // �ִϸ��̼� ������� ���
        yield return new WaitForSeconds(Mathf.Max(successAnimation.clip.length, failureAnimation.clip.length));

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
        foreach (Image image in imageObjects)
        {
            image.gameObject.SetActive(false);
        }
        isAnimating = false;
    }
}