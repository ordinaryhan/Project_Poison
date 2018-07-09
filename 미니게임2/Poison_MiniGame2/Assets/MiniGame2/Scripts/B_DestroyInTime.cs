using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_DestroyInTime : MonoBehaviour {

    [SerializeField]
    float destroyTime = 2f;

    // Use this for initialization
    void Start () {
        Destroy(gameObject, destroyTime);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag.Equals("floor") || collision.tag.Equals("floor_middle")) && transform.tag.Equals("waterbullet"))
            Destroy(gameObject);
        if(collision.tag.Equals("wall"))
            Destroy(gameObject);
    }

}
