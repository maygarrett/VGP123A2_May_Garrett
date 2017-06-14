using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    public float lifeTime;

    // Use this for initialization
    void Start()
    {
        if (speed <= 0)
        {
            

            Debug.Log("speed was not set.");
        }

        
        if (lifeTime <= 0)
        {          
            lifeTime = 1.0f; 
            Debug.Log("lifeTime was not set. Defaulting to " + lifeTime);
        }

        // Take Rigidbody2D component and change its velocity to value passed
        if (GameObject.FindGameObjectWithTag("Character").transform.position.x < transform.position.x)
        GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(2, 0);
        }


        // Destroy gameObject after 'lifeTime' seconds
        Destroy(gameObject, lifeTime);

    }

    private void Update()
    {
       
    }

    // Check for collisions with other GameObjects
    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag != "Character" && c.gameObject.tag != "Interactable")
        {
            // Destory GameObject Script is attached to
            SoundManager.instance.playSound(SoundManager.instance.projectileDestroyed);
            Destroy(gameObject);
        }
        if(c.gameObject.tag == "Character")
        {
            SoundManager.instance.playSound2(SoundManager.instance.playerDeath);
            Destroy(c.gameObject);
            GameManager.instance.spawnPlayer();
        }
        
    }
}
