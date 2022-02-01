using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactRange = 3f;

    //Draw interact range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }

    private void Update()
    {
        // If player is within range
        if (Vector2.Distance(gameObject.transform.position, GameManager.instance.player.position) < interactRange)
        {
            // Make interact icon visible
            GameManager.instance.player.gameObject.GetComponent<PlayerController>().interactIcon.SetActive(true);

            // Detect if the player presses Spacebar (interact key)
            if (Input.GetKeyDown(KeyCode.Space)) {
                Interact();
            }
        }
        else {
            // Make interact icon not visible
            GameManager.instance.player.gameObject.GetComponent<PlayerController>().interactIcon.SetActive(false);
        }
    }

    public virtual void Interact() {

        /* 
         
        !~ HOW TO USE INTERACT SYSTEM ~!
            Other scripts inherit from Interactable to implement this function.
            This allows interactable objects to perform different actions depending on what they are.
            To add a new interactable behavior, simply create a new script that derives form "Interactable" rather than "Monobehaviour".
            This will cause the new script to inherit the above code for proximity based interaction.
            Then add the following code to that new script:
        
            public override void Interact() {
                // new interactable behavior
            }
        
            Finally, implement the new behavior within the overridden Interact function and attach ONLY the new behavior script to a gameObject.
            Unity handles the inheritance to the "Interactable" script; therefore, it does not need to be attached as well.
        
        */

    }

}
