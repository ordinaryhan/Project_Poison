using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_HintManager : MonoBehaviour {

    public GameObject lights; //the parent gameobject of lights

    // Use this for initialization
    void Start () {
        lights.SetActive(false);       

        StartCoroutine("MyUpdate");
    }

    IEnumerator LightStart()
    {
        yield return new WaitForSeconds(38);
        MaskOff();
        yield return new WaitForSeconds(7);
        MaskOn();
    }

    IEnumerator MyUpdate()
    {
        float timer = 0f;
        float time = 60f;
        while (timer < time)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        MaskOn();
    }

    void MaskOn()
    {
        lights.SetActive(true);
        StartCoroutine("LightStart");
    }

    void MaskOff()
    {
        lights.SetActive(false);
    }
}
