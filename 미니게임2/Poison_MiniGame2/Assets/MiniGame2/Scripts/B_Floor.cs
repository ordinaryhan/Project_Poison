using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Floor : MonoBehaviour {

    public GameObject targetFloor;
    public bool switchA = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!switchA)
        {
            if (collision.CompareTag("Player"))
            {
                if (collision.GetComponent<Rigidbody2D>().velocity.y == 0)
                {
                    switchA = true;
                    targetFloor.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switchA = false;
        }
    }

}
