using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_PlayerControl : MonoBehaviour
{
    public bool isItem = false;
    public GameObject Item;
   


    // Use this for initialization
    void Start()
    {
        Item.SetActive(false);
    }

    // Update is called once per frame
    public void ItemOn()
    {
        
        isItem = true;
        Invoke("ItemOff", 10f);
    }
    private void ItemOff()
    {

        isItem = false;

    } 
}
