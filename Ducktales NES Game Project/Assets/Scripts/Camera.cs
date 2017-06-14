using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
		if(!player)
        {
            Debug.Log("No player gameObject assigned!");
        }

        offset.z = -10;
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = player.transform.position + offset;
       


	}
}
