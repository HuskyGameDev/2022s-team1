using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShapePuzzleMovement : MonoBehaviour
{

    public GameObject correctForm;
    private bool moving;
    private bool finish;

    private float startPosX;
    private float startPosy;

    private Vector3 resetPosition;

    // Start is called before the first frame update
    void Start()
    {
        resetPosition = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(finish == false){
            if(moving){
                Vector3 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosy, this.transform.localPosition.z);
            }
        }
        
    }

    private void OnMouseDown(){

        if(Input.GetMouseButtonDown(0)){
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosy = mousePos.y - this.transform.localPosition.y;

            moving = true;
        }

    }
    private void OnMouseUp(){
        
        moving = false;

        if(Mathf.Abs(this.transform.position.x - correctForm.transform.position.x) <= 5.0f && 
        Mathf.Abs(this.transform.position.y - correctForm.transform.position.y) <= 5.0f){

            this.transform.position = new Vector3(correctForm.transform.position.x, correctForm.transform.position.y, correctForm.transform.position.z);

            finish = true;

            GameObject.Find("CompletionHandler").GetComponent<PuzzleComplete>().addPoints();

        }else{
            this.transform.localPosition = new Vector3(resetPosition.x, resetPosition.y, resetPosition.z);
        }

    }
}
