using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrandScript : MonoBehaviour
{
    public Image[] imageObjects; // 6개의 이미지 객체들을 배열로 저장
    public Animation successAnimation; // 성공 애니메이션
    public Animation failureAnimation; // 실패 애니메이션
    public Image successImage; // 성공 이미지
    public Image failureImage; // 실패 이미지

    private bool isAnimating; // 애니메이션 실행 중인지 여부 확인
    private int successAnimationCount; // 성공 애니메이션 재생 횟수

    private void Start()
    {
        // 이미지 객체들을 모두 비활성화
        foreach (Image image in imageObjects)
        {
            image.gameObject.SetActive(false);
        }

        // 성공 이미지와 실패 이미지도 비활성화
        successImage.gameObject.SetActive(false);
        failureImage.gameObject.SetActive(false);

        // 업데이트 버튼에 클릭 이벤트 연결
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
            // 애니메이션 실행 중이 아니면 애니메이션 재생
            isAnimating = true;

            // 성공 또는 실패 애니메이션 재생
            if (Random.value < 0.5f)
            {
                // 50% 확률로 성공 애니메이션 재생
                successAnimationCount++;
                successAnimation.Play();
            }
            else
            {
                // 50% 확률로 실패 애니메이션 재생
                failureAnimation.Play();
                successAnimationCount = 0; // 실패 시 성공 애니메이션 재생 횟수 초기화
            }

            // 애니메이션이 끝나면 애니메이션 상태 초기화
            StartCoroutine(ResetAnimationState());
        }
    }

    private IEnumerator ResetAnimationState()
    {
        // 애니메이션 종료까지 대기
        yield return new WaitForSeconds(Mathf.Max(successAnimation.clip.length, failureAnimation.clip.length));

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
        foreach (Image image in imageObjects)
        {
            image.gameObject.SetActive(false);
        }
        isAnimating = false;
    }
}