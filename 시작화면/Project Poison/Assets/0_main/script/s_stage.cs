using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_stage : MonoBehaviour {
    
    public enum Stage { one, two, three }
    public Stage stage;
    private bool canStart2;
    private bool canStart3;
    public GameObject rock;
    
	void Start () {
        canStart2 = false;
        canStart3 = false;
        if(rock != null) rock.SetActive(true);
	}
	
	void Update () {

        StageClear();
    }
    public void StageClear()
    {
        //Stage Clear 결과 상, 하 > 클리어 조건 추가하기 (canStart)

        if (stage == Stage.two && canStart2)
        {
            if (rock != null) rock.SetActive(false);
        }
        if (stage == Stage.three && canStart3)
        {
            if (rock != null) rock.SetActive(false);
        }
    }
    
    public void StartGame()
    {
        if (stage == Stage.one)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if (stage == Stage.two && canStart2)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        if (stage == Stage.three && canStart3)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);

    }
}
