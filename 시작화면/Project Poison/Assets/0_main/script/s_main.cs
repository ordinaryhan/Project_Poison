using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_main : MonoBehaviour {

    public GameObject MainCanvas;
    public GameObject StartCanvas;
    public GameObject CollectionCanvas;


    void Start()
    {
        MainCanvas.SetActive(true);
        StartCanvas.SetActive(false);
        CollectionCanvas.SetActive(false);
    }

    void Update()
    {
    }
}
