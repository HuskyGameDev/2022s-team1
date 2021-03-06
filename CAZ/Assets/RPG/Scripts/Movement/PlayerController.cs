using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb; // player's Rigidbody2D
    private Animator anim; // player's Animatior
    public GameObject interactIcon;
    public bool isMoving;

    [SerializeField]
    public float speed; // Movement speed of player ~250 seems well paced

    public bool canMove = true;

    public LayerMask BattleLayer;

    public float battleCooldownTimer;
    public bool battlePrimed;
    public float battleCooldownTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // player's Rigidbody2D
        anim = GetComponent<Animator>(); // player's Animatior
    }

    // Update is called once per frame
    void Update()
    {
        if (battleCooldownTimer > 0)
        {
            battleCooldownTimer -= Time.deltaTime;
            Debug.Log("Cooldown: " + (int)battleCooldownTimer);
        }
        else {
            battlePrimed = true;
            battleCooldownTimer = 0;
        }

        if (canMove){
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.LeftShift))
            {
                speed = 400;
                GetComponent<Animator>().speed = 1.5f;
            }
            else {
                speed = 250;
                GetComponent<Animator>().speed = 1.0f;
            }
        } else {
            speed = 0;
        }

    }

    private void FixedUpdate()
    {

        //if (canMove){
        
            // Get input and move player
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed * Time.deltaTime;

            //check if moving
            if (rb.velocity.x != 0 || rb.velocity.y != 0)
                isMoving = true;
            else
                isMoving = false;

            // Set animations for player's direction
            anim.SetFloat("moveX", rb.velocity.x);
            anim.SetFloat("moveY", rb.velocity.y);

            // Control idle animation direction
            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
            {
                anim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                anim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }

            CheckForRandomEncounter();
       // }
        
    }

    private void CheckForRandomEncounter() {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, BattleLayer) != null && isMoving && battlePrimed)
        {
            if (Random.Range(1, 901) <= 6) // WAS 10 - changed to lower encounter rate. With 10 ~ 55% chance/sec. With 6 ~33% chance/sec
            {
                Debug.Log("Random Encounter");
                isMoving = false;
                battlePrimed = false;
                LoadRandomEncounter();
            }
        }
    }

    private void LoadRandomEncounter() {
        foreach (Transform child in GameManager.instance.transform)
        {
            child.gameObject.SetActive(false);
        }
        GameManager.instance.DisablePlayerMovement();
        SceneManager.LoadScene("AIDevelopment");
    }
}
