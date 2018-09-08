using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class s_initial : MonoBehaviour {

    public GameObject MainCanvas;
    public GameObject StartCanvas;
    public GameObject CollectionCanvas;
    public GameObject Anime;
    public GameObject Anime2;
    public GameObject light;
    public GameObject bgm;

    public float speed = 20f;

    void Start()
    {
        Anime2.GetComponent<SpriteRenderer>().sprite = null;
        light.SetActive(false);
        MainCanvas.SetActive(false);
        StartCanvas.SetActive(false);
        CollectionCanvas.SetActive(false);
        bgm.SetActive(false);

        StartCoroutine(StartAnime());
        
    }
    

    IEnumerator StartAnime()
    {
        Anime.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/anime1");
        yield return new WaitForSeconds(1f);
        Anime.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/anime2");
        yield return new WaitForSeconds(1f);
        for (int i=1; i <= 6; i++)
        {
            Anime.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/anime"+ i);
            if (i == 3)
            {
                Anime2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/anime7");
                yield return new WaitForSeconds(1f);
                Anime2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/anime8");
                yield return new WaitForSeconds(1f);
                Anime2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/anime9");
                yield return new WaitForSeconds(1f);
                Anime2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/anime10");
                yield return new WaitForSeconds(1f);
                Anime2.GetComponent<SpriteRenderer>().sprite = null;
            }
           else  if (i >= 4)
            {
                light.SetActive(true);
                light.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/light");
                for (int j = 0; j < 10; j++)
                {
                    light.transform.position += new Vector3(0.25f, 0.25f, 0);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else
                yield return new WaitForSeconds(1f);
        }
        MainCanvas.SetActive(true);
        bgm.SetActive(true);
        gameObject.SetActive(false);
        yield return null;
    }
}
