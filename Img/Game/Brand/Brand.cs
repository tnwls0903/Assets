using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brand : MonoBehaviour
{
    public InputField goldInputField; // 배팅금 입력 필드

    public Image[] imageObjects; // 6개의 이미지 객체들을 배열로 저장
    public Animator successAnimator; // 성공 애니메이션
    public Animator failureAnimator; // 실패 애니메이션
    public Image successImage; // 성공 이미지
    public Image failureImage; // 실패 이미지
    public Button upgradeButton; // 업데이트 버튼

    private int goldToAddMultiplier = 1;
    private bool isAnimating; // 애니메이션 실행 중인지 여부 확인
    private int successAnimationCount; // 성공 애니메이션 재생 횟수
    private float successAnimationChance = 0.7f; // 초기 성공 애니메이션 재생 확률
    private float successAnimationChanceDecrease = 0.1f; // 성공 애니메이션 재생 확률 감소량
    private int initialGold; // 배팅하기 전의 초기 소지금 상태를 저장하는 변수
    private int maxGoldToAddMultiplier = 8;

    private DataController dataController;

    public AudioClip clip1;
    public AudioClip clip2;

    private void Start()
    {
        dataController = FindObjectOfType<DataController>();

        initialGold = dataController.GetGold(); // 배팅 시작 전 초기 소지금 저장

        // 이미지 객체들을 모두 비활성화
        HideAllImages();

        // 성공 이미지와 실패 이미지도 비활성화
        successImage.gameObject.SetActive(false);
        failureImage.gameObject.SetActive(false);

        // 업데이트 버튼에 클릭 이벤트 연결
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
            // 입력값이 음수이거나 0인 경우 유효하지 않음
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
            // 배팅금 입력 값을 그대로 사용
            string bettingAmountText = goldInputField.text;

            // 배팅금이 유효한 숫자인지 확인
            if (IsValidBettingAmount(bettingAmountText))
            {
                int bettingAmount = int.Parse(bettingAmountText);

                // 배팅금이 현재 소지금보다 크거나 같을 경우 실패 애니메이션 재생
                if (bettingAmount > initialGold)
                {
                    // 실패 애니메이션 재생
                    failureAnimator.SetTrigger("Fail");

                    // 애니메이션이 끝나면 애니메이션 상태 초기화
                    StartCoroutine(ResetAnimationState());
                    return;

                    SoundManager.instance.SFXPlay("Click", clip2);
                }

                // 배팅금이 현재 소지금보다 작거나 같을 경우에만 애니메이션 실행
                if (bettingAmount <= initialGold)
                {
                    // 배팅하기 전 소지금에서 배팅한 값만큼 차감
                   

                    // 애니메이션 실행 중이 아니면 애니메이션 재생
                    isAnimating = true;

                    // 성공 또는 실패 애니메이션 재생
                    if (Random.value < successAnimationChance)
                    {
                        // 성공 애니메이션 재생
                        successAnimationCount++;
                        successAnimator.SetTrigger("Success");

                        // 성공할 때
                        int goldToAdd = bettingAmount * goldToAddMultiplier;
                        goldToAddMultiplier++;
                        dataController.AddGold(goldToAdd);
                        IncreaseGoldToAddMultiplier();

                        // 성공 횟수에 따라 성공 확률 조절
                        if (successAnimationCount >= 5)
                        {
                            successAnimationChance = 0.1f; // 실패 확률 90%
                        }

                        SoundManager.instance.SFXPlay("Click", clip1);
                    }
                    else
                    {
                        // 실패할 때는 실패 확률을 증가
                        successAnimationChance += successAnimationChanceDecrease;
                        if (successAnimationChance > 1.0f)
                            successAnimationChance = 1.0f; // 최대 확률 제한

                        // 실패 애니메이션 재생
                        failureAnimator.SetTrigger("Fail");

                        successAnimationCount = 0; // 실패 시 성공 애니메이션 재생 횟수 초기화                                 

                        // 실패시 이미지 상태를 초기화
                        ResetImageState();

                        // 배팅하기 전 소지금에서 배팅한 값만큼 차감
                        dataController.SetGold(initialGold - bettingAmount);

                        SoundManager.instance.SFXPlay("Click", clip2);

                        ResetGoldToAddMultiplier();
                    }

                    // 애니메이션이 끝나면 애니메이션 상태 초기화
                    StartCoroutine(ResetAnimationState());
                }
            }
            else
            {               
                // 배팅금이 유효하지 않은 경우 실패 애니메이션 재생
                failureAnimator.SetTrigger("Fail");

                // 애니메이션이 끝나면 애니메이션 상태 초기화
                StartCoroutine(ResetAnimationState());

                initialGold = dataController.GetGold(); // 초기 골드 초기화

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
        // 애니메이션 종료까지 대기
        yield return new WaitForSeconds(Mathf.Max(successAnimator.GetCurrentAnimatorStateInfo(0).length,
                                                  failureAnimator.GetCurrentAnimatorStateInfo(0).length));

        if (successAnimationCount > 0)
        {
            // 성공 이미지 출력 후 3초 뒤에 성공 애니메이션 재생횟수에 따라 순차적으로 이미지 출력
            successImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            successImage.gameObject.SetActive(false);

            // 이미지 객체들을 순차적으로 활성화
            for (int i = 0; i < successAnimationCount; i++)
            {
                if (i < imageObjects.Length)
                {
                    // 이전 이미지는 비활성화하고, 해당 인덱스의 이미지를 활성화
                    if (i > 0)
                        imageObjects[i - 1].gameObject.SetActive(false);

                    imageObjects[i].gameObject.SetActive(true);
                }
            }
        }
        else
        {
            // 실패 이미지 출력 후 3초 뒤에 성공 애니메이션 재생횟수 초기화
            failureImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            failureImage.gameObject.SetActive(false);
        }

        // 애니메이션 실행 상태를 초기화
        isAnimating = false;
    }

    private void ResetImageState()
    {
        // 이미지 객체들을 모두 비활성화
        HideAllImages();

        // 성공 이미지와 실패 이미지도 비활성화
        successImage.gameObject.SetActive(false);
        failureImage.gameObject.SetActive(false);

        // 성공 애니메이션 재생 확률 감소
        successAnimationChance -= successAnimationChanceDecrease;
    }

}
