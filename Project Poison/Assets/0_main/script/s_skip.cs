using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_skip : MonoBehaviour {
    public enum Stage { one, two, three }
    public Stage stage;
    
    void Start () {
		
	}
	
	public void StageSkip()
    {
        if(stage==Stage.one)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        if (stage == Stage.two)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

        if (stage == Stage.three)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }
}
