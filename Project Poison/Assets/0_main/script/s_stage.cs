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


    void Start () {
        if(rock != null) rock.SetActive(true);
        //background = GameObject.Find("stroyanime");
        for(int i=0; i<3; i++)
        {
            score[i] = 0;
        }
        score[0] = 100;
        score[1] = 100;
	}
	
	void Update () {

        StageClear();
    }
    public void StageClear()
    {
        //Stage 결과 score에 저장하기
        
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
        background.SetActive(true);
        if (stage == Stage.one)
        {
            for (int i = 0; i < 4; i++)
            {
                background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/one" + i);
                yield return new WaitForSeconds(1f);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (stage == Stage.two)
        {
            for (int i = 0; i < 4; i++)
            {
                background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/two" + i);
                yield return new WaitForSeconds(1f);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
        if (stage == Stage.three)
        {
            for (int i = 0; i < 5; i++)
            {
                background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/three" + i);
                yield return new WaitForSeconds(1f);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
        }
        
        yield return null;
    }
}
