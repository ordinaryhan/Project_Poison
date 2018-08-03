using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_DestroyInTime : MonoBehaviour {

    [SerializeField]
    float destroyTime = 1f;
    public int power = 30;
    Vector2 Dir;
    float speed;
    Rigidbody2D thisbody;

    // Use this for initialization
    void Start () {
        thisbody = GetComponent<Rigidbody2D>();
        if(!tag.Equals("leterbullet"))
            Invoke("Delete", destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string Ctag = collision.tag;
        
        if (Ctag.Equals("floor") && tag.Equals("waterbullet"))
            Delete();
        if(Ctag.Equals("wall"))
            Delete();
        if ((Ctag.Equals("Player")) && tag.Equals("item"))
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
