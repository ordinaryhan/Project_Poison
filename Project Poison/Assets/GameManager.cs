using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject player;

    Vector3 StartingPos;
    Quaternion StartingRotate;
    bool isStarted = false;

    void Awake()
    {
        Time.timeScale = 0f;
    }

	// Use this for initialization
	void Start () {
        StartingPos = GameObject.FindWithTag("Start").transform.position;
        StartingRotate = GameObject.FindWithTag("Start").transform.rotation;
    }
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.Label("It's GUI! :D");
    }
    public static void RestartStage()
    {
        //Stop Game
        Time.timeScale = 0f;

        //ReLoad Stage
       // SceneManager.LoadScene(stageLevel1, LoadSceneMode.Single);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
