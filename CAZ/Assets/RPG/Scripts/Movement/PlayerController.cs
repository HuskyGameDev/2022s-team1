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
    private float speed; // Movement speed of player ~250 seems well paced

    public LayerMask BattleLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // player's Rigidbody2D
        anim = GetComponent<Animator>(); // player's Animatior
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
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
        
    }

    private void CheckForRandomEncounter() {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, BattleLayer) != null && isMoving)
        {
            if (Random.Range(1, 901) <= 10)
            {
                Debug.Log("Random Encounter");
                LoadRandomEncounter();
            }
        }
    }

    private void LoadRandomEncounter() {
        foreach (Transform child in GameManager.instance.transform)
        {
            child.gameObject.SetActive(false);
        }
        SceneManager.LoadScene("AIDevelopment");
    }
}
