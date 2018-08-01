using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_Anime : MonoBehaviour {

	public enum StageNum { one, two, three}
    public StageNum stage;
    public GameObject background;

    private bool isFinished = false;
    private bool isStarted = false;
    private int score=0;

	void Start () {
        //score가져오기
        background = GameObject.Find("background");
	}
	
	void Update () {

        if (!isStarted)
            StartCoroutine(started());

        if (isFinished)
            StartCoroutine(finished());
    }
    IEnumerator finished()
    {
        if (stage == StageNum.one)
        {
            if(score >420)
                for (int i = 0; i < 3; i++)
                {
                background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/one_great" + i);
                yield return new WaitForSeconds(1f);
                }
            else if(score>0)
                for (int i = 0; i < 4; i++)
                {
                    background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/one_good" + i);
                    yield return new WaitForSeconds(1f);
                }
            else
            {
                background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/one_bad");
                yield return new WaitForSeconds(1f);
            }
        }
        if (stage == StageNum.two)
        {
            if (score > 250)
                for (int i = 0; i < 5; i++)
                {
                    background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/two_great" + i);
                    yield return new WaitForSeconds(1f);
                }
            else if (score > 0)
                for (int i = 0; i < 4; i++)
                {
                    background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/two_good" + i);
                    yield return new WaitForSeconds(1f);
                }
            else
            {
                background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/two_bad");
                yield return new WaitForSeconds(1f);
            }
        }
        if (stage == StageNum.three)
        {
            if (score > 150)
                for (int i = 0; i < 5; i++)
                {
                    background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/three_great" + i);
                    yield return new WaitForSeconds(1f);
                }
            else if (score > 0)
                for (int i = 0; i < 7; i++)
                {
                    background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/three_good" + i);
                    yield return new WaitForSeconds(1f);
                }
            else
            {
                background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/three_bad");
                yield return new WaitForSeconds(1f);
            }
        }
        yield return null;
    }
    IEnumerator started()
    {
        if(stage==StageNum.one)
            for(int i=0; i<4; i++)
            {
                background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/one" + i);
                yield return new WaitForSeconds(1f);
            }
        if (stage == StageNum.two)
            for (int i = 0; i < 4; i++)
            {
                background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/two" + i);
                yield return new WaitForSeconds(1f);
            }
        if (stage == StageNum.three)
            for (int i = 0; i < 5; i++)
            {
                background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/three" + i);
                yield return new WaitForSeconds(1f);
            }

        isStarted = true;
        yield return null;
    }
}
