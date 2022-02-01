using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTest : Interactable
{
    public override void Interact() {
        // new interactable behavior
        Debug.Log("Interacted!");
     }
}
