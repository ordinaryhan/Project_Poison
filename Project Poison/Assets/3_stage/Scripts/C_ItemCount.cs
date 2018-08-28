using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_ItemCount : MonoBehaviour
{
    int itemCount = 0;

    void GetItem()
    {
        itemCount++;

        Debug.Log("핸드폰 : " + itemCount);
    }

}