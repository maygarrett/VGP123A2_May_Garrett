using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class octopus_enemy : MonoBehaviour {

    public float speed;

    public float flipRate;
    float timeSinceLastFlip = 0.0f;

    public Rigidbody2D rb;

    public Transform projectileSpawnPoint;
    public Projectile projectilePrefab;
    public float projectileForce;

    float timeSinceLastFire = 0.0f;
    public float projectileFireRate;

    private bool isFacingRight;

    Animator anim;

    public Transform playerPosition;


    // Use this for initialization
    void Start () {
		if(speed == 0)
        {
            Debug.Log("Octopus speed not set, defaulting to 2");
            speed = 2;
        }
        if (flipRate == 0)
        {
            Debug.Log("flipRate not set, defaulting to 2");
            flipRate = 2;
        }
        if(!rb)
        {
            Debug.Log("Rigidbody not set! Problem!");
        }

        if (!projectileSpawnPoint)
        {
            Debug.LogError("No projectileSpawnPoint found on GameObject");
        }

        if (!projectilePrefab)
        {
            Debug.LogError("No projectilePrefab found on GameObject");
        }

        if (projectileForce == 0)
        {

            Debug.Log("projectileForce was not set. Defaulting to " + projectileForce);
        }

        if (flipRate == 0)
        {
            Debug.Log("flipRate not set, defaulting to 2");
            flipRate = 3;
        }


        isFacingRight = false;
        rb.velocity = new Vector2(0, speed);
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        playerPosition = GameObject.FindGameObjectWithTag("Character").transform;

        anim.SetBool("IsAttacking", false);
        if (Time.time > timeSinceLastFlip + flipRate)
        {
            // Fire a projectile
            change();

            // Timestamp of last time the projectile was fired
            timeSinceLastFlip = Time.time;
        }

        if (playerPosition.position.y - transform.position.y >= -1 && playerPosition.position.y - transform.position.y <= 1)
        {
            if (playerPosition.position.x - transform.position.x >= -3 && playerPosition.position.x - transform.position.x <= 3)
            {
                if (Time.time > timeSinceLastFire + projectileFireRate)
                {
                    // Fire a projectile
                    fire();

                    // Timestamp of last time the projectile was fired
                    timeSinceLastFire = Time.time;
                }
            }
        }


        if (GameObject.FindGameObjectWithTag("Character").transform.position.x < transform.position.x && isFacingRight || !isFacingRight && GameObject.FindGameObjectWithTag("Character").transform.position.x >= transform.position.x)
        {
            flip();
        }
    }

    void change()                 // flips character direction
    {
        rb.velocity = rb.velocity * -1;
    }

    void fire()
    {
        anim.SetBool("IsAttacking", true);
        SoundManager.instance.playSound(SoundManager.instance.octopusAttack);

        Projectile temp =
            Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Character")
        {
            SoundManager.instance.playSound2(SoundManager.instance.playerDeath);
            Destroy(col.gameObject);
            GameManager.instance.spawnPlayer();
        }
        if(col.gameObject.tag == "Interactable")
        {
            SoundManager.instance.playSound(SoundManager.instance.enemyDeath);
            Destroy(gameObject);
        }

    }

    private void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "AttackBox")
        {
            SoundManager.instance.playSound(SoundManager.instance.enemyDeath);
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

}
