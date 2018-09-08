using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_howto2 : MonoBehaviour {

    public GameObject detail;

    public void DetailOn()
    {
        detail.SetActive(true);
    }

    public void DetailOff()
    {
        detail.SetActive(false);
    }

}
