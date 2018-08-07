using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_FloorReset : MonoBehaviour {

    public GameObject[] floors;
    public B_Floor initialFloor;
    public GameObject door_active;
    private bool switchA = false;

    private void Awake()
    {
        for (int i = floors.Length - 1; i >= 0; i--)
        {
            floors[i].SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!switchA && !door_active.activeSelf)
        {
            if (collision.CompareTag("Player"))
            {
                if (collision.GetComponent<Rigidbody2D>().velocity.y == 0)
                {
                    switchA = true;
                    StartCoroutine("ResetFloor");
                }
            }
        }
    }

    IEnumerator ResetFloor()
    {
        yield return new WaitForSecondsRealtime(1f);
        for (int i = floors.Length - 1; i >= 0; i--)
        {
            if (initialFloor.switchA)
                break;
            if (floors[i].activeSelf)
            {
                floors[i].SetActive(false);
                yield return new WaitForSecondsRealtime(0.6f);
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
