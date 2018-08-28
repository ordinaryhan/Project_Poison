using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class C_ScoreUpdate : MonoBehaviour {
    public int score = 0;
    public Image TimeBar;
    float timeLeft;
    public float maxTime = 3f;
    public Text scoreLabel;
    public Image ScoreBar ;
    public int accumulate = 0;
    public static C_ScoreUpdate instance = null;
    public bool isItem = false;

    //모래시계
    public Slider hourglassA, hourglassB, hourglassC;
    public int playerMaxHP = 600;
    int playerHP;
    public Animator BreakHourglass;
    public bool moveSand = false, breakHourglass = false;
    float diffHP;
    private Transform transformC;

    // UIScreen
    public GameObject TimeStop_Screen, GameOver_Screen, Result_Screen, Good, Bad;
    public GameObject RemoveCanvas;

    public AudioSource bgm, Audio;
    public AudioClip bgm3, good, soso, bad, buttonClick;
    public GameObject story, talk, talkingEnemy, player, storyback;
    private bool[] grade = new bool[3];

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        playerHP = playerMaxHP;
        hourglassA.value = playerHP;
        hourglassB.value = playerMaxHP - playerHP;
        hourglassC.value = 0;
        transformC = hourglassC.GetComponent<Transform>();
        TimeBar = GetComponent<Image>();
        timeLeft = maxTime;

    }

    public void ScoreReset()
    {
        accumulate = 0;
        ScoreBar.fillAmount = 0;
    }
    public void ScoreSet()
    {
        accumulate += 1;
        if (!isItem)
        {
            ScoreBar.fillAmount += 1 / 60f;
        }
        score += 50;
        scoreLabel.text = score.ToString();
    }
    void Update()
    {
        
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

    // 플레이어 체력 관련
    public void HitPlayer(int power)
    {
        moveSand = true;
        playerHP -= power;
        s_variable.score[2] = playerHP;
    }

    // 게임 오버 창 뜨게 하기
    public void GameOver()
    {
        Time.timeScale = 0;
        RemoveCanvas.SetActive(false);
        GameOver_Screen.SetActive(true);
    }

    // 게임 결과 창 뜨게 하기
    public void Result()
    {
        Time.timeScale = 0;
        RemoveCanvas.SetActive(false);
        Result_Screen.SetActive(true);

        StartCoroutine(ending());
    }

    // 게임 일시 정지
    public void TimeStop()
    {
        Audio.clip = buttonClick;
        Audio.Play();
        if (!TimeStop_Screen.activeSelf)
        {
            TimeStop_Screen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    // 일시 정지 취소
    public void TimeGo()
    {
        Audio.clip = buttonClick;
        Audio.Play();
        TimeStop_Screen.SetActive(false);
        GameOver_Screen.SetActive(false);
        Time.timeScale = 1; 
    }

    // 메인화면으로 가기
    public void Restart()
    {
        Audio.clip = buttonClick;
        Audio.Play();
        TimeGo();
        s_variable.score[2] = 0;
        SceneManager.LoadScene("4_main");
    }

    // 결과 화면에서 메인화면으로 가기 (
    public void Quit()
    {
        Audio.clip = buttonClick;
        Audio.Play();
        TimeGo();
        if (playerHP > 0) s_variable.finish3 = true;
        SceneManager.LoadScene("4_main");
    }

    // 앱 종료
    public void Exit()
    {
        TimeGo();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 종료
#endif
    }

    IEnumerator ending()
    {

        if (playerHP >= 150)
        {

            Good.SetActive(true);
            yield return new WaitForSecondsRealtime(3f);

            storyback.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/background");
            story.SetActive(true);
            talkingEnemy.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/three");
            yield return new WaitForSecondsRealtime(3f);

            for (int i = 1; i <= 6; i++)
            {
                talk.SetActive(true);
                talk.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/three_good" + i);
                yield return new WaitForSecondsRealtime(3f);
            }
        }
        else if (playerHP > 0)
        {
            Bad.SetActive(true);
            yield return new WaitForSecondsRealtime(3f);

            story.SetActive(true);
            talkingEnemy.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/three");
            yield return new WaitForSecondsRealtime(3f);


            for (int i = 1; i <= 6; i++)
            {
                talk.SetActive(true);
                talk.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/three_bad" + i);
                yield return new WaitForSecondsRealtime(3f);

            }
        }

        player.SetActive(false);
        talkingEnemy.SetActive(false);
        talk.SetActive(false);

        if (s_variable.score[0] > 420) grade[0] = true;
        if (s_variable.score[0] <= 420) grade[0] = false;
        if (s_variable.score[1] > 250) grade[1] = true;
        if (s_variable.score[1] <= 250) grade[1] = false;
        if (s_variable.score[2] > 150) grade[2] = true;
        if (s_variable.score[2] <= 150) grade[2] = false;

        if ((grade[0] && grade[1] && grade[2]) || (grade[0] && grade[1] && !grade[2]))
        {
            for (int i = 1; i <= 3; i++)
            {
                storyback.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ending" + i);
                yield return new WaitForSecondsRealtime(3f);
            }
            bgm.clip = good;
            bgm.Play();
            storyback.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ending_good1");
            yield return new WaitForSecondsRealtime(3f);
            storyback.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ending_good2");
            yield return new WaitForSecondsRealtime(3f);
        }
        if ((grade[0] && !grade[1] && grade[2]) || (grade[0] && !grade[1] && !grade[2]))
        {
            for (int i = 1; i <= 3; i++)
            {
                storyback.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ending" + i);
                yield return new WaitForSecondsRealtime(3f);
            }
            bgm.clip = soso;
            bgm.Play();
            storyback.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ending_soso1");
            yield return new WaitForSecondsRealtime(3f);
        }
        if ((!grade[0] && grade[1] && grade[2]) || (!grade[0] && grade[1] && !grade[2]))
        {
            for (int i = 1; i <= 3; i++)
            {
                storyback.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ending" + i);
                yield return new WaitForSecondsRealtime(3f);
            }
            bgm.clip = soso;
            bgm.Play();
            storyback.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ending_soso2");
            yield return new WaitForSecondsRealtime(3f);
        }
        if (!(grade[0] && grade[1] && grade[2]) || !(grade[0] && grade[1] && !grade[2]))
        {
            for (int i = 1; i <= 3; i++)
            {
                storyback.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ending" + i);
                yield return new WaitForSecondsRealtime(3f);
            }
            bgm.clip = bad;
            bgm.Play();
            storyback.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ending_bad");
            yield return new WaitForSecondsRealtime(3f);
        }

        SceneManager.LoadScene("4_main");
    }
}
