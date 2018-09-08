using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class s_result : MonoBehaviour {
    public GameObject story, storyback, talkingEnemy, talk, player;
    public AudioSource AudioSource;
    public AudioClip bgm3, good, soso, bad;
    private bool[] grade = new bool[3];
    
    // Use this for initialization
    void Awake () {
        story.SetActive(true);
        StartCoroutine(ending());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator ending()
    {
        story.SetActive(true);
        Debug.Log(s_variable.score[2]);
        if (s_variable.score[2] >= 150)
        {
            storyback.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/background");
            talkingEnemy.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/three");
            yield return new WaitForSecondsRealtime(3f);

            for (int i = 1; i <= 6; i++)
            {
                talk.SetActive(true);
                talk.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/three_good" + i);
                yield return new WaitForSecondsRealtime(3f);
            }
        }
        else if (s_variable.score[2] > 0)
        {
            talkingEnemy.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/three");
            yield return new WaitForSecondsRealtime(3f);


            for (int i = 1; i <= 6; i++)
            {
                talk.SetActive(true);
                talk.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/three_bad" + i);
                yield return new WaitForSecondsRealtime(3f);

            }
        }

        player.SetActive(false);
        talkingEnemy.SetActive(false);
        talk.SetActive(false);

        if (s_variable.score[0] > 420) grade[0] = true;
        if (s_variable.score[0] <= 420) grade[0] = false;
        if (s_variable.score[1] > 250) grade[1] = true;
        if (s_variable.score[1] <= 250) grade[1] = false;
        if (s_variable.score[2] > 150) grade[2] = true;
        if (s_variable.score[2] <= 150) grade[2] = false;

        if ((grade[0] && grade[1] && grade[2]) || (grade[0] && grade[1] && !grade[2]))
        {
            AudioSource.clip = good;
            AudioSource.Play();
            yield return new WaitForSecondsRealtime(3f);

            for (int i = 1; i <= 3; i++)
            {
                storyback.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ending" + i);
                yield return new WaitForSecondsRealtime(3f);
            }
            storyback.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ending_good1");
            yield return new WaitForSecondsRealtime(3f);
            storyback.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ending_good2");
            yield return new WaitForSecondsRealtime(3f);
            SceneManager.LoadScene(4);
        }
        if ((grade[0] && !grade[1] && grade[2]) || (grade[0] && !grade[1] && !grade[2]))
        {
            AudioSource.clip = soso;
            AudioSource.Play();
            yield return new WaitForSecondsRealtime(3f);

            for (int i = 1; i <= 4; i++)
            {
                storyback.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ending" + i);
                yield return new WaitForSecondsRealtime(3f);
            }
            storyback.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ending_soso1");
            yield return new WaitForSecondsRealtime(3f);
            SceneManager.LoadScene(4);
        }
        if ((!grade[0] && grade[1] && grade[2]) || (!grade[0] && grade[1] && !grade[2]))
        {
            AudioSource.clip = soso;
            AudioSource.Play();
            yield return new WaitForSecondsRealtime(3f);

            for (int i = 1; i <= 4; i++)
            {
                storyback.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ending" + i);
                yield return new WaitForSecondsRealtime(3f);
            }
            storyback.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ending_soso2");
            yield return new WaitForSecondsRealtime(3f);
            SceneManager.LoadScene(4);
        }
        if (!(grade[0] && grade[1] && grade[2]) || !(grade[0] && grade[1] && !grade[2]))
        {
            AudioSource.clip = bad;
            AudioSource.Play();
            yield return new WaitForSecondsRealtime(3f);

            for (int i = 1; i <= 4; i++)
            {
                storyback.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ending" + i);
                yield return new WaitForSecondsRealtime(3f);
            }
            storyback.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ending_bad");
            yield return new WaitForSecondsRealtime(3f);
            SceneManager.LoadScene(4);
        }

    }
}
