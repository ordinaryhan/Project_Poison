using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_LightHint : MonoBehaviour {
    public Transform playerPos;
    Vector3 lightPos;

    public float speed;

    public GameObject changeFloor;

    public Sprite[] floor_img = new Sprite[4];
    int floor;

	// Use this for initialization
	void OnEnable(){
        lightPos = this.GetComponent<Transform>().position;
        if (playerPos.position.y < 10.2f)
                {
            lightPos.x = -9.5f;
            speed = Mathf.Abs(speed);
        }
        else if (playerPos.position.y < 19.2f)
        {
            lightPos.x = 67.5f;
            speed = -Mathf.Abs(speed);
        }
        else if (playerPos.position.y < 28.2f)
        {
            lightPos.x = -9.5f;
            speed = Mathf.Abs(speed);
        }
        else
        {
            lightPos.x = 67.5f;
            speed = -Mathf.Abs(speed);
        }

        this.GetComponent<Transform>().position = lightPos;

        //to change the sprite of floor which player is on
        floor = Random.Range(1, 5);
        Debug.Log(floor);
    }

    // Update is called once per frame
    void Update () {
        this.transform.Translate(speed * Time.deltaTime, 0, 0);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Main Camera")
        {
            //change scirpte
            switch (floor)
            {
                case (1):
                    if(playerPos.position.y > 10.2f)
                        changeFloor.GetComponent<SpriteRenderer>().sprite = floor_img[0];
                    break;

                case (2):
                    if(playerPos.position.y < 10.2f || playerPos.position.y > 19.2f)
                        changeFloor.GetComponent<SpriteRenderer>().sprite = floor_img[1];
                    break;

                case (3):
                    if (playerPos.position.y < 19.2f || playerPos.position.y > 28.2f)
                        changeFloor.GetComponent<SpriteRenderer>().sprite = floor_img[2];
                    break;

                case (4):
                    if (playerPos.position.y < 28.2f)
                        changeFloor.GetComponent<SpriteRenderer>().sprite = floor_img[3];
                    break;

                default:
                    break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Main Camera")
        {
            changeFloor.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}
