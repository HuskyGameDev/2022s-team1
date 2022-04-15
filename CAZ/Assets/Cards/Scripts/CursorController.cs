using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CursorState { NORMAL, GRAB, ATTACK, EFFECT }

public class CursorController : MonoBehaviour
{

    public EncounterManager manager;
    //private SpriteRenderer rend;
    private Canvas canvas;
    public CursorState cursorState;
    public Sprite normalCursor;
    public Sprite grabCursor;
    public Sprite attackCursor;
    public Sprite effectCursor;
    public Image cursorImage;
    public int xOffset;
    public int yOffset;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        //rend = GetComponent<SpriteRenderer>();
        canvas = FindObjectOfType<Canvas>();
        //Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.ForceSoftware);
        cursorImage.transform.SetParent(canvas.transform);
        cursorImage.sprite = normalCursor;
        cursorState = CursorState.NORMAL;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 cursorPos = new Vector2(Input.mousePosition.x + xOffset, Input.mousePosition.y + yOffset);
        cursorImage.transform.position = cursorPos;

        if (manager.activeEffect == ActiveEffect.NONE || manager.state == BattleState.ENEMYTURN)
        {
            if (Input.GetMouseButtonDown(0))
            {
                cursorImage.sprite = grabCursor;
                cursorState = CursorState.GRAB;
            }
            if (Input.GetMouseButtonUp(0))
            {
                cursorImage.sprite = normalCursor;
                cursorState = CursorState.NORMAL;
            }
        }
        else {
            cursorImage.sprite = effectCursor;
            cursorState = CursorState.EFFECT;
        }

       

        cursorImage.gameObject.transform.SetAsLastSibling();
        //rend.sprite = normalCursor;
    }
}
