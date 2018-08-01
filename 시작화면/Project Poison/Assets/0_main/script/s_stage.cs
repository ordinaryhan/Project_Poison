using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_stage : MonoBehaviour {
    
    public enum Stage { one, two, three }
    public Stage stage;
    private int[] score = new int[2];
    public GameObject rock;

    
	void Start () {
        if(rock != null) rock.SetActive(true);
        for(int i=0; i<2; i++)
        {
            score[i] = 0;
        }
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
        if (stage == Stage.one)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if (stage == Stage.two && score[0] > 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        if (stage == Stage.three && score[1] > 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);

    }
}
