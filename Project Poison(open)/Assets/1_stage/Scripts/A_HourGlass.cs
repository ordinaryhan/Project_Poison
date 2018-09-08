using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class A_HourGlass : MonoBehaviour {
    //게임오버 불러오기 위한 변수
    public GameObject GameOn;
    public GameObject GameEnd;

    // 필요한 변수들
    public int playerMaxHP = 600;
    public float playerHP = 600;
    public Slider hourglassA, hourglassB, hourglassC;
    private Transform transformC;
    public Animator BreakHourglass;
    public bool moveSand = false, breakHourglass = false, isItem = false; //isItem은 임시로 추가함 뭔지 알아야함
    float diffHP;
    // 효과음 (2_stage -> Audio 폴더에 있을 거에요)
    public AudioClip breakGlass;
    // 효과음 나오게 할 AudioSource
    private AudioSource ThisAudio;

    private void Awake()
    {
        ThisAudio = GetComponent<AudioSource>();
        transformC = hourglassC.GetComponent<Transform>();
        hourglassA.value = playerHP;
        hourglassB.value = playerMaxHP - playerHP;
        hourglassC.value = 0;
    }

    void Update()
    {
        playerHP -= Time.deltaTime;

        // 모래시계 UI 관련
        if (playerHP > 0)
        {
            if (moveSand)
            {
                moveSand = false;
                StartCoroutine("MoveSand");
            }
            // 플레이어 체력 양과 모래시계 양이 차이가 나면 diffHP씩 맞춰간다.
            if (hourglassA.value > playerHP && !isItem)
            {
                diffHP = (hourglassA.value - playerHP) * 0.1f;
                hourglassA.value -= diffHP;
                hourglassB.value += diffHP;
            }
            else if (hourglassA.value < playerHP && isItem)
            {
                diffHP = (playerHP - hourglassA.value) * 0.1f;
                hourglassA.value += diffHP;
                hourglassB.value -= diffHP;
            }
        }
        // 플레이어 체력이 0이 되면 모래시계 깨지는 효과가 나오게 한다. (1회 실행)
        else if (!breakHourglass)
        {
            breakHourglass = true;
            BreakHourglass.SetTrigger("break");
            Invoke("BreakSound", 1.5f);
            Invoke("GameOver", 3f);
        }

    }

    public void GameOver()
    {
        GameEnd.SetActive(true);
        GameOn.SetActive(false);
    }

    //모래 시계 깨지는 소리
    private void BreakSound()
    {
        // 이 스크립트 붙일 곳에 AudioSource붙여 주시거나 AudioSource있는 곳 이용해주세요
        ThisAudio.clip = breakGlass;
        ThisAudio.Play();
    }

    //모래 쏟아져 내리는 효과 관련 (hourglassC가 모래 막대 부분임)
    IEnumerator MoveSand()
    {
        for (int i = 1; i <= 10; i++)
        {
            hourglassC.value += 0.1f;
            yield return new WaitForSecondsRealtime(0.02f);
        }
        Vector3 tempV = transformC.localScale;
        tempV.y *= -1;
        if (hourglassC.direction == Slider.Direction.TopToBottom)
        {
            hourglassC.SetDirection(Slider.Direction.BottomToTop, true);
            transformC.localScale = tempV;
        }
        else
        {
            hourglassC.SetDirection(Slider.Direction.TopToBottom, true);
            transformC.localScale = tempV;
        }
        for (int i = 1; i <= 10; i++)
        {
            hourglassC.value -= 0.1f;
            yield return new WaitForSecondsRealtime(0.02f);
        }
        tempV.y *= -1;
        if (hourglassC.direction == Slider.Direction.TopToBottom)
        {
            hourglassC.SetDirection(Slider.Direction.BottomToTop, true);
            transformC.localScale = tempV;
        }
        else
        {
            hourglassC.SetDirection(Slider.Direction.TopToBottom, true);
            transformC.localScale = tempV;
        }
    }
}
