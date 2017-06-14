using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Box : MonoBehaviour {

    public Rigidbody2D boxBody;

    public float flySpeed;

    

   
   


    // Use this for initialization
    void Start () {
		if(!boxBody)
        {
            Debug.LogWarning("no boxBody found");
        }

        if(flySpeed == 0)
        {
            flySpeed = 1;
            Debug.LogWarning("flySpeed updated to 1");
        }

        





        Physics2D.IgnoreLayerCollision(14, 13);
        Physics2D.IgnoreLayerCollision(14, 11);
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Character" && col.gameObject.tag != "Projectile")
        {
            SoundManager.instance.playSound2(SoundManager.instance.boxDestroyed);
            Destroy(gameObject);
        }
       
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            SoundManager.instance.playSound2(SoundManager.instance.boxDestroyed);
            Destroy(gameObject);
        }

        
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "AttackBox")
        {

            SoundManager.instance.playSound2(SoundManager.instance.boxHit);
            boxBody.velocity = new Vector2(flySpeed, flySpeed + 1);

        }
    }

    

}
