using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPath : MonoBehaviour {

    [SerializeField]
    Transform[] PathCenter;

    int i = 1;
    int pathCenterIndex = 0;
    bool isItem = false;// false면 itemOn함수 실행
    public GameObject item;
    public int score;


    // Use this for initialization
    void Start () {
        transform.position = PathCenter[pathCenterIndex].transform.position;
        for (i = 0; i < PathCenter.Length; i++)
        {
            item.transform.position = PathCenter[i].transform.position;
            item.SetActive(false);
        }

        StartCoroutine("ItemTime");
    }

    IEnumerator ItemTime()
    {
        while (true)
        {
            //Instantiate(생성시킬 오브젝트, 생성될 위치, 생성됐을때 회전값);
            //

            yield return new WaitForSeconds(5f);
            //Instantiate(item, PathCenter[i].transform.position, Quaternion.identity);
            item.SetActive(true);

            StartCoroutine("ItemTime");
        }
    }
        // Update is called once per frame
        void Update () {
		
	}
}
