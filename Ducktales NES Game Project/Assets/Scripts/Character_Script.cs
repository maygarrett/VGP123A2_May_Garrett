using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Script : MonoBehaviour
{

    Rigidbody2D rb;   // establish a rigidbody element

    public Rigidbody2D rb2;      // establish public rigidbody go to unity viewer and click and drag rigid body into this script to access it

    public float speed;          // speed variable declaration

    public float jumpForce;      //variables for jump declared
    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;          // variables for jump + animations


    public float climbForce;
    public bool isLadder;
    public LayerMask isTriggerLadder;         //variables for ladder collision detection
    public Transform ladderCheck;
    public float ladderCheckRadius;

    public LayerMask isDeathBoxLayer;                // deathbox variables

    public bool isCrouching;              // crouching variable
    public bool isClimbing;                 // climbing variable

    Animator anim;                          // sets up variable anim with data type Animator which is a class from Unity Engine

    public bool isAttacking;


    public Transform attackSpawnPoint;
    public Transform attackSpawnPointLeft;
    public GameObject attackPrefab;

    

    





    // character flipping variable
    public bool isFacingRight = true;


    // Use this for initialization
    void Start()
    {
        


        rb = GetComponent<Rigidbody2D>();             // make sure that this script and variable rb refers to the Rigidbody2D of the elements that this script is assigned to

        if (!rb)
        {
            Debug.LogWarning("No Rigidbody2D found.");
        }

        if (speed <= 0)                          // check to make sure variable is properly set, if not put in corrections to make sure
        {
            speed = 5.0f;

            Debug.LogWarning("default speeding to " + speed);
        }

        if (jumpForce <= 0)
        {
            jumpForce = 6.0f;
            Debug.LogWarning("default jumpForce to " + jumpForce);
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.01f;
            Debug.LogWarning("default groundCheckRadius to " + groundCheckRadius);
        }

        if (!groundCheck)
        {
            Debug.LogWarning("no groundCheck found");
        }



        anim = GetComponent<Animator>();             // make sure that this script and variable anim refers to the animator and if not returns error message
        if (!anim)
        {
            Debug.LogWarning("No Animator found.");
        }

        if (isClimbing == true)
        {
            isClimbing = false;
        }



        if (ladderCheckRadius <= 0)
        {
            ladderCheckRadius = 0.1f;
            Debug.LogWarning("default groundCheckRadius to " + groundCheckRadius);
        }

        if (!ladderCheck)
        {
            Debug.LogWarning("no ladderCheck found");
        }
        if (climbForce <= 0)
        {
            jumpForce = 1.0f;
            Debug.LogWarning("default jumpForce to " + jumpForce);
        }



        // Checks if projectileSpawnPoint GameObject is connected
        if (!attackSpawnPoint)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogError("No attackSpawnPoint found on GameObject");
        }

        if (!attackSpawnPointLeft)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogError("No attackSpawnPointLeft found on GameObject");
        }

        // Checks if projectileSpawnPoint GameObject is connected
        if (!attackPrefab)
        {
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogError("No attackPrefab found on GameObject");
        }


        

    }

    // Update is called once per frame
    void Update()
    {

        float moveValue = Input.GetAxisRaw("Horizontal");          // gives numbers based in input left = -1  no key = 0  right = 1          raw meaning you get the hard numbers rather than a gradual progression up to 1 or -1 from 0
        rb.velocity = new Vector2(speed * moveValue, rb.velocity.y);       // Vector2 is a 2d unit vector that is given x and y coordinates and then is then written into rb.velocity

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);     //using the OverlapCircle(pisition, radius, condition layermask) to return a true or false condition to isGrounded
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)                                     // if condition that states if the player presses jump and is grounded add a jumpforce vector to their rigidbody
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            SoundManager.instance.playSound(SoundManager.instance.playerJump);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))     // uf control is pressed print pew pew to the console
        {
            anim.SetBool("AttackButton", true);         //if update detects control being pressed then set attack button to true
            isAttacking = true;
        }
        else { anim.SetBool("AttackButton", false); isAttacking = false; }     // otherwise continue setting attack button to false

        anim.SetFloat("MoveValue", Mathf.Abs(moveValue));                       // Setting float value of condition "MoveValue" established in Unity to the absolute value of moveValue the variable (which is 1 when moving and 0 when stationary)


        if ((isFacingRight && moveValue < 0) || (!isFacingRight && moveValue > 0))   // if the character is facing one way and moves the other, execute the flip function
        {
            flip();
        }

        if (isGrounded)                                                // if the player is grounded set condition "IsGrounded" to True, otherwise set it as false
        {                                                              // using for jumping animations
            anim.SetBool("IsGrounded", true);
        }
        else
        {
            anim.SetBool("IsGrounded", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !isGrounded)
        {
            anim.SetBool("AirAttack", true);
            
        }
        else if (isGrounded)
        {
            anim.SetBool("AirAttack", false);
        }







        isLadder = Physics2D.OverlapCircle(ladderCheck.position, ladderCheckRadius, isTriggerLadder);     //using the OverlapCircle(pisition, radius, condition layermask) to return a true or false condition to isLadder
        if (isLadder)
        {
            if (Input.GetKey(KeyCode.UpArrow))                                     // if condition that states if the player presses jump and is touching a ladder add a climbforce vector to their rigidbody
            {
                rb.velocity = new Vector2(moveValue, climbForce);
                isClimbing = true;
                
            }
            else
            {
                isClimbing = false;
                
            }


        }

        if (!isLadder)
        {
            isClimbing = false;
        }



        if (isClimbing)
        {
            anim.SetBool("IsClimbing", true);
            SoundManager.instance.playMusic2();
        }
        else if (!isClimbing)
        {
            anim.SetBool("IsClimbing", false);
            SoundManager.instance.stopMusic2();
        }

        




        if (Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetBool("IsCrouching", true);
            rb.velocity = new Vector2(0, 0);
        }
        else
        {
            anim.SetBool("IsCrouching", false);
        }


        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            

            // Call function to make pew pew
            attack();
        }







    }


    void flip()                 // flips character direction
    {
        isFacingRight = !isFacingRight;             // sets isFacingRight to the opposite of whatever isFacingRight is at the time


        Vector3 scaleFactor = transform.localScale;                 // establishing a new vactor 3 named scaleFactor and setting it equal to the local scale of this object

        scaleFactor.x *= -1;          // scalefactor.x = -scalefactor.x;                          //making the scale factor negative

        transform.localScale = scaleFactor;               // these three lines mace a copy of the scale factor, make it negative and then overwrite the original with the copy's value
    }

    void attack()
    {
        SoundManager.instance.playSound(SoundManager.instance.playerAttack);
        if (isFacingRight)
        {
            GameObject temp = Instantiate(attackPrefab, attackSpawnPoint.position, attackSpawnPoint.rotation);
        }
        else
        {
            GameObject temp = Instantiate(attackPrefab, attackSpawnPointLeft.position, attackSpawnPoint.rotation);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Collectable")
        {
            GameManager.instance.score += 1;
            Destroy(collision.gameObject);
            SoundManager.instance.playSound2(SoundManager.instance.coinCollected);

        }
        if (collision.gameObject.tag == "Treasure")
        {
            GameManager.instance.score += 5;
            Destroy(collision.gameObject);
            SoundManager.instance.playSound2(SoundManager.instance.chestCollected);
        }


    }


    

}
