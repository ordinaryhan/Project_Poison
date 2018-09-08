using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_skip : MonoBehaviour {
    public enum Stage { one, two, three }
    public Stage stage;
	
	public void StageSkip()
    {
        if(stage==Stage.one)
            SceneManager.LoadScene(1);

        if (stage == Stage.two)
            SceneManager.LoadScene(2);

        if (stage == Stage.three)
            SceneManager.LoadScene(3);
    }
}
