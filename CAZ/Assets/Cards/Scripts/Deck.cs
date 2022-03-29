using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{

    public List<Card> deck = new List<Card>();
    public int limit;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
