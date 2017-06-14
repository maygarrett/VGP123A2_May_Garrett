using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Pit : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Character")
        {
            SoundManager.instance.playSound2(SoundManager.instance.playerDeath);
            Destroy(col.gameObject);
            GameManager.instance.spawnPlayer();
        }
    }
}
