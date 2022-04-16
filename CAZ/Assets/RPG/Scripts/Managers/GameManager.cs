using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject InventoryUI;
    public Image holdingImage;
    public Transform player;
    public string holding;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(false);
            }
            SceneManager.LoadScene("DeckBuilder");
        }
    }
}
