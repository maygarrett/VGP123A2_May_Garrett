using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duck_enemy : MonoBehaviour {

    public Transform playerPosition;
    public GameObject player;

    public Rigidbody2D duckBody;

    public float jumpRate;
    float timeSinceLastJump = 0.0f;

    bool isFacingRight;

    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;          // variables for jump + animations
    Animator anim;




    // Use this for initialization
    void Start() {

        isFacingRight = false;

        if (!playerPosition)
        {
            Debug.Log("no playerPosition found");
        }

        if (jumpRate == 0)
        {
            Debug.Log("no jump rate set, defaulting to 4");
            jumpRate = 4;
        }

        if (!duckBody)
        {
            Debug.Log("no duck Body set");
        }

        if(!player)
        {
            Debug.Log("No player reference found!");
        }

        anim = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {

        playerPosition = GameObject.FindGameObjectWithTag("Character").transform;


        if (playerPosition.position.y - transform.position.y >= -2 && playerPosition.position.y - transform.position.y <= 2)
        {
            if (playerPosition.position.x - transform.position.x >= -3 && playerPosition.position.x - transform.position.x <= 3)
            {
                if (Time.time > timeSinceLastJump + jumpRate)
                {
                    // Fire a projectile
                    jump();

                    // Timestamp of last time the projectile was fired
                    timeSinceLastJump = Time.time;
                }
            }
        }


        if (playerPosition.position.x - transform.position.x >= -0.4 && playerPosition.position.x - transform.position.x <= 0.4)
            {
                anim.SetBool("IsClose", true);
            }
            else anim.SetBool("IsClose", false);
        


        if (GameObject.FindGameObjectWithTag("Character").transform.position.x < transform.position.x && isFacingRight || !isFacingRight && GameObject.FindGameObjectWithTag("Character").transform.position.x >= transform.position.x)
        {
            flip();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        if (isGrounded)                                                // if the player is grounded set condition "IsGrounded" to True, otherwise set it as false
        {                                                              // using for jumping animations
            anim.SetBool("IsGroundedDuck", true);
        }
        else
        {
            anim.SetBool("IsGroundedDuck", false);
        }


    }

    void jump()
    {
        duckBody.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
        if(transform.position.x > playerPosition.position.x)
            duckBody.AddForce(new Vector2(-1,0), ForceMode2D.Impulse);
        else duckBody.AddForce(Vector2.right, ForceMode2D.Impulse);

        SoundManager.instance.playSound(SoundManager.instance.duckJump);
    }


    private void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "AttackBox")
        {
            SoundManager.instance.playSound2(SoundManager.instance.enemyDeath);
            Destroy(gameObject);

        }
    }

    void flip()
    {
        isFacingRight = !isFacingRight;             // sets isFacingRight to the opposite of whatever isFacingRight is at the time


        Vector3 scaleFactor = transform.localScale;                 // establishing a new vactor 3 named scaleFactor and setting it equal to the local scale of this object

        scaleFactor.x *= -1;          // scalefactor.x = -scalefactor.x;                          //making the scale factor negative

        transform.localScale = scaleFactor;               // these three lines mace a copy of the scale factor, make it negative and then overwrite the original with the copy's value
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            SoundManager.instance.playSound2(SoundManager.instance.playerDeath);
            Destroy(collision.gameObject);
            GameManager.instance.spawnPlayer();
        }
    }
}
