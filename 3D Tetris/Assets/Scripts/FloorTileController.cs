using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTileController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Floor Tile Collider Hit, my dude.");
        this.transform.parent.GetComponent<Rigidbody>().SendMessage("OnCollisionEnter", other);
    }
}
