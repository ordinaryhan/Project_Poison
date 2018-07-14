using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_DestroyInTime : MonoBehaviour {

    [SerializeField]
    float destroyTime = 1f;
    public int power = 30;
    Vector2 Dir;
    float speed, delay;
    Rigidbody2D thisbody;
    string ThisTag;

    // Use this for initialization
    void Start () {
        Invoke("Delete", destroyTime);
        thisbody = GetComponent<Rigidbody2D>();
        ThisTag = transform.tag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;

        if ((tag.Equals("floor") || tag.Equals("floor_middle")) && ThisTag.Equals("waterbullet"))
            Delete();
        if(tag.Equals("wall"))
            Delete();
        if ((tag.Equals("Player")) && ThisTag.Equals("item"))
        {
            collision.GetComponent<B_PlayerControl>().ItemOn();
            Delete();
        }
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

    private void Delete()
    {
        gameObject.SetActive(false);
    }

}
