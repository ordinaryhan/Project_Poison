using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_collection : MonoBehaviour {
    public enum Collect { one, two, three, four }
    public Collect collectNum;
    private bool[] iscollect = new bool[4];
    public GameObject rock;
    public GameObject image;


    void Start()
    {
        rock.SetActive(true);
        image.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            iscollect[i] = false;
        }
    }

    void Update() {
        Result();
    }

    public void Result()
    {  
        //스테이지 여부 따라iscollect 변수값 설정하기//

        if (collectNum == Collect.one && iscollect[0])
        {
            rock.SetActive(false);
        }
        if (collectNum == Collect.two && iscollect[1])
        {
            rock.SetActive(false);
        }
        if (collectNum == Collect.three && iscollect[2])
        {
            rock.SetActive(false);
        }
        if (collectNum == Collect.four && iscollect[3])
        {
            rock.SetActive(false);
        }

    }

    public void collection()
    {
        if (collectNum == Collect.one && iscollect[0])
            image.SetActive(true);
        if (collectNum == Collect.two && iscollect[1])
            image.SetActive(true);
        if (collectNum == Collect.three && iscollect[2])
            image.SetActive(true);
        if (collectNum == Collect.four && iscollect[3])
            image.SetActive(true);
    }
}
