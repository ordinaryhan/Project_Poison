using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class s_main : MonoBehaviour {

    public GameObject MainCanvas;
    public GameObject StartCanvas;
    public GameObject CollectionCanvas;
    public GameObject AnimeCanvas;
    private GameObject Anime;

    void Start()
    {
        Anime = GameObject.Find("Anime");
        //AnimeCanvas.SetActive(true);
        MainCanvas.SetActive(false);
        StartCanvas.SetActive(false);
        CollectionCanvas.SetActive(false);

        StartCoroutine(StartAnime());
        
    }

    void Update()
    {
    }

    IEnumerator StartAnime()
    {
        for(int i=1; i <= 6; i++)
        {
            Debug.Log("들어갔다");
            Anime.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/anime"+ i);
            yield return new WaitForSeconds(1f);
        }
        MainCanvas.SetActive(true);
        AnimeCanvas.SetActive(false);
        yield return null;
    }
}
