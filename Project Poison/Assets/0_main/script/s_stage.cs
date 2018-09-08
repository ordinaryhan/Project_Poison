using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class s_stage : MonoBehaviour {
    
    public enum Stage { one, two, three }
    public Stage stage;
    private bool[] isFinish = new bool[3];

    public GameObject rock;

    public GameObject background;
    public GameObject Player;
    public GameObject Enemy;
    public GameObject talk;
    public GameObject Skip;

    public AudioSource Audio;
    public AudioClip one, two, three;


    void Start () {
        if(rock != null) rock.SetActive(true);
        isFinish[0] = s_variable.finish1;
        isFinish[1] = s_variable.finish2;
        isFinish[2] = s_variable.finish3;

    }

    private void Update()
    {
        Debug.Log(s_variable.score[0] + ", " + s_variable.score[1] + ", " + s_variable.score[2]);
        StageClear();
    }

    public void StageClear()
    {
        //Stage 결과 score에 저장하기

        if (stage == Stage.two && isFinish[0])
        {
            if (rock != null) rock.SetActive(false);
        }
        if (stage == Stage.three && isFinish[1])
        {
            if (rock != null) rock.SetActive(false);
        }
    }

    public void StartGame()
    {
        StartCoroutine(started());
    }
    IEnumerator started()
    {

        if (stage == Stage.one)
        {
            Audio.clip = one;
            Audio.Play();
            Skip.SetActive(true);
            background.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
            Player.SetActive(true);
            Enemy.SetActive(true);

            background.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/background");
            Enemy.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/one");
            yield return new WaitForSecondsRealtime(0.5f);
            talk.SetActive(true);

            for (int i = 1; i <= 4; i++)
            {
                talk.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/one" + i);
                yield return new WaitForSecondsRealtime(2f);
            }
            SceneManager.LoadScene(1);
        }
        if (stage == Stage.two && !rock.activeSelf)
        {

            Audio.clip = two;
            Audio.Play();
            Skip.SetActive(true);
            background.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
            Player.SetActive(true);
            Enemy.SetActive(true);

            background.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/background");
            Enemy.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/two");
            yield return new WaitForSecondsRealtime(0.5f);
            talk.SetActive(true);

            for (int i = 1; i <= 6; i++)
            {
                if (i != 3 && i != 6)
                {
                    talk.transform.position += new Vector3(-2f, 0, 0);
                    talk.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/two" + i);
                    yield return new WaitForSecondsRealtime(2f);
                    talk.transform.position += new Vector3(2f, 0, 0);
                }
                else
                {
                    talk.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/two" + i);
                    yield return new WaitForSecondsRealtime(2f);
                }
            }

            SceneManager.LoadScene(2);
        }
        if (stage == Stage.three && !rock.activeSelf)
        {

            Audio.clip = three;
            Audio.Play();
            Skip.SetActive(true);
            background.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
            Player.SetActive(true);
            Enemy.SetActive(true);

            background.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/background");
            Enemy.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/three");
            yield return new WaitForSecondsRealtime(0.5f);
            talk.SetActive(true);

            for (int i = 1; i <= 7; i++)
            {
                if (i != 2 && i != 3 && i != 7)
                {
                    talk.transform.position += new Vector3(-1.5f, 0, 0);
                    talk.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/three" + i);
                    yield return new WaitForSecondsRealtime(2f);
                    talk.transform.position += new Vector3(1.5f, 0, 0);
                }
                else
                {
                    talk.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/three" + i);
                    yield return new WaitForSecondsRealtime(2f);
                }
            }

            SceneManager.LoadScene(3);
        }
        
        yield return null;
    }
}
