using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_DestroyInTime : MonoBehaviour {

    [SerializeField]
    float destroyTime = 2f;
    Vector2 Dir;
    float speed, delay;
    Rigidbody2D thisbody;

    // Use this for initialization
    void Start () {
        Destroy(gameObject, destroyTime);
        thisbody = GetComponent<Rigidbody2D>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag.Equals("floor") || collision.tag.Equals("floor_middle")) && transform.tag.Equals("waterbullet"))
            Destroy(gameObject);
        if(collision.tag.Equals("wall"))
            Destroy(gameObject);
    }

    public void MoveBullet(Vector2 Dir, float speed, float delay)
    {
        this.Dir = Dir;
        this.speed = speed;
        Invoke("Move", delay);
    }

    private void Move()
    {
        thisbody.AddForce(Dir.normalized * speed);
    }

}
