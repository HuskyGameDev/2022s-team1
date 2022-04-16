using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact_Carrot : Interactable
{
    public GameManager manager;
    public DialogueBase kidConvoCarrotFound;
    public DialogueTrigger_Condition kid;
    public Sprite carrotSprite;

    private void Start()
    {
        //manager = FindObjectOfType<GameManager>();
        manager = GameManager.instance;
    }

    public override void Interact()
    {
        manager.holding = "Carrot";
        kid.DB = kidConvoCarrotFound;
        manager.holdingImage.sprite = carrotSprite;
        manager.InventoryUI.SetActive(true);
    }
}
