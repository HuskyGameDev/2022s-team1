using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcounterScript : MonoBehaviour
{

    public LayerMask grassLayer;   
    
    private void randomEcounter(){
        if(Physics2D.OverlapCircle(transform.position, 0.2f, grassLayer) != null){
            if(Random.Range(1, 101) <= 10){
                Debug.Log("Random Encounter");
            }
        }
    }

    private void FixedEcounter(){
        if(Physics2D.OverlapCircle(transform.position, 0.2f, grassLayer) != null){
            if(Random.Range(1, 11) <= 10){
                Debug.Log("Fixed Encounter");
            }
        }
    }
    


}
