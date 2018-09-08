using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class A_TotalManager : MonoBehaviour {
    //캔버스
    public GameObject Manager;
    public GameObject GameOn;
    public GameObject GameEnd;
    public GameObject Result;

    public GameObject Player;

    public GameObject RemoveCanvas, TimeStop_Screen;
    public AudioSource ThisAudio;
    public AudioClip buttonClick;

    public GameObject story, talk, talkingEnemy;
    public GameObject ResultBack, Good_Bad;

    //플레이어 체력
    float playerHP;

    public Image ResultWord;
    public Sprite Good;
    public Sprite Bad;

    void Awake()
    {
        Time.timeScale = 1;
        GameOn.SetActive(true);
        GameEnd.SetActive(false);
        Result.SetActive(false);
        Player.GetComponent<Transform>().position = new Vector3(3.2f, 3.3f, 0);
    }

    public void GameOver()
    {
        GameEnd.SetActive(true);
    }

    public void GameOver_Door()
    {
        playerHP = Manager.GetComponent<A_HourGlass>().playerHP;
        GameOn.SetActive(false);

        if (playerHP > 0)
        {
            s_variable.finish1 = true;
            s_variable.score[0] = playerHP;
        }
        StartCoroutine(ending());
    }
    IEnumerator ending()
    {
        Result.SetActive(true);

        if (playerHP >= 420)
        {

            ResultWord.sprite = Good;
            yield return new WaitForSecondsRealtime(3f);

            ResultBack.SetActive(false);
            Good_Bad.SetActive(false);
            story.SetActive(true);
            talkingEnemy.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/one");
            yield return new WaitForSecondsRealtime(3f);

            for (int i = 1; i <= 7; i++)
            {
                talk.SetActive(true);
                talk.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/one_good" + i);
                yield return new WaitForSecondsRealtime(3f);
            }
        }
        else if (playerHP > 0)
        {
            ResultWord.sprite = Bad;
            yield return new WaitForSecondsRealtime(3f);

            ResultBack.SetActive(false);
            Good_Bad.SetActive(false);
            story.SetActive(true);
            talkingEnemy.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/one");
            yield return new WaitForSecondsRealtime(3f);


            for (int i = 1; i <= 6; i++)
            {
                talk.SetActive(true);
                talk.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/one_bad" + i);
                yield return new WaitForSecondsRealtime(3f);

            }
        }

        SceneManager.LoadScene(4);
    }

    public void TimeStop()
    {
        RemoveCanvas.SetActive(false);
        ThisAudio.clip = buttonClick;
        ThisAudio.Play();
        if (!TimeStop_Screen.activeSelf)
        {
            TimeStop_Screen.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void Restart()
    {
        ThisAudio.clip = buttonClick;
        ThisAudio.Play();
        TimeGo();
        SceneManager.LoadScene(4);
    }
    public void TimeGo()
    {
        ThisAudio.clip = buttonClick;
        ThisAudio.Play();
        Time.timeScale = 1;
        TimeStop_Screen.SetActive(false);
        RemoveCanvas.SetActive(true);
    }

    public void Quit()
    {
        if (playerHP > 0)
        {
            s_variable.finish1 = true;
            s_variable.score[0] = playerHP;
        }
        SceneManager.LoadScene(4);
    }
}
