using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour {

    public float lifeTime;

    // Use this for initialization
    void Start () {

        if (lifeTime <= 0)
        {
            // Assign a default value if one is not set in the Inspector
            lifeTime = 0.3f;

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.Log("lifeTime was not set. Defaulting to " + lifeTime);
        }



    }
	
	// Update is called once per frame
	void Update () {

        Destroy(gameObject, lifeTime);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

}
