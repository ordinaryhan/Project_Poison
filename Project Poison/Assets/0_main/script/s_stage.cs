using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class s_stage : MonoBehaviour {
    
    public enum Stage { one, two, three }
    public Stage stage;
    private int[] score = new int[3];

    public GameObject rock;

    public GameObject background;
    public GameObject Player;
    public GameObject Enemy;
    public GameObject talk;
    public GameObject Skip;


    void Start () {
        if(rock != null) rock.SetActive(true);
        for(int i=0; i<3; i++)
        {
            score[i] = 0;
        }
	}
	
	void Update () {

        StageClear();

        //Debug.Log("1: " + score[0] + " 2: " + score[1] + " 3: " + score[2]);
    }
    public void StageClear()
    {
        //Stage 결과 score에 저장하기
        s_variable.score[0] = 600;
        score[0] = s_variable.score[0];
        score[1] = s_variable.score[1];
        score[2] = s_variable.score[2];

        if (stage == Stage.two && score[0]>0)
        {
            if (rock != null) rock.SetActive(false);
        }
        if (stage == Stage.three && score[1] > 0)
        {
            if (rock != null) rock.SetActive(false);
        }
    }

    public void StartGame()
    {
        StartCoroutine(started());
    }
    IEnumerator started()
    {
        Skip.SetActive(true);
        background.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        Player.SetActive(true);
        Enemy.SetActive(true);

        if (stage == Stage.one)
        {
            background.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/background");
            Enemy.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/one");
            yield return new WaitForSecondsRealtime(0.5f);
            talk.SetActive(true);

            for (int i = 1; i <= 4; i++)
            {
                talk.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/one" + i);
                yield return new WaitForSecondsRealtime(2f);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (stage == Stage.two)
        {
            background.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/background");
            Enemy.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/two");
            yield return new WaitForSecondsRealtime(0.5f);
            talk.SetActive(true);

            for (int i = 1; i <= 6; i++)
            {
                if (i != 3 && i != 6)
                {
                    talk.transform.position += new Vector3(-2f, 0, 0);
                    talk.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/two" + i);
                    yield return new WaitForSecondsRealtime(2f);
                    talk.transform.position += new Vector3(2f, 0, 0);
                }
                else
                {
                    talk.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/two" + i);
                    yield return new WaitForSecondsRealtime(2f);
                }
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
        if (stage == Stage.three)
        {
            background.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/background");
            Enemy.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/three");
            yield return new WaitForSecondsRealtime(0.5f);
            talk.SetActive(true);

            for (int i = 1; i <= 7; i++)
            {
                if (i != 2 && i != 3 && i != 7)
                {
                    talk.transform.position += new Vector3(-1.5f, 0, 0);
                    talk.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/three" + i);
                    yield return new WaitForSecondsRealtime(2f);
                    talk.transform.position += new Vector3(1.5f, 0, 0);
                }
                else
                {
                    talk.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/three" + i);
                    yield return new WaitForSecondsRealtime(2f);
                }
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
        }
        
        yield return null;
    }
}
