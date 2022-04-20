using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleComplete : MonoBehaviour
{
    
    private int total;
    private int current;
    public GameObject shapes;
    
    
    // Start is called before the first frame update
    void Start()
    {
        total = shapes.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(current >= total){
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);

            AudioManager.instance.Play("Ding");
            StartCoroutine(WaitAndLoad());
        }
    }

    public void addPoints(){
        current++;
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(3f);

        foreach (Transform child in GameManager.instance.transform)
        {
            child.gameObject.SetActive(true);
        }

        switch (GameManager.instance.currentLevel)
        {
            case GameManager.Level.VILLAGE:
                SceneManager.LoadScene("Village");
                break;
            case GameManager.Level.FOREST:
                SceneManager.LoadScene("Forest");
                break;
            case GameManager.Level.CAVE:
                SceneManager.LoadScene("Cave");
                break;
            case GameManager.Level.CASTLE_EXT:
                SceneManager.LoadScene("CastleExterior");
                break;
            case GameManager.Level.CASTLE_INT:
                SceneManager.LoadScene("CastleInterior");
                break;
        }

    }

}
