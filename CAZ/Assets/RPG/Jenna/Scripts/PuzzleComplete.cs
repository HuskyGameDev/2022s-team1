using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }

    public void addPoints(){
        current++;
    }

}
