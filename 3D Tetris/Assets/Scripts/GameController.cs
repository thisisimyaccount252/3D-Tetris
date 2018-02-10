using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private float gameTimer;

    public float CurrentIncrement; // The tick systesm for how often a block should move

	// Use this for initialization
	void Start () {
        gameTimer = 0;

        // TODO: Do I need this?
        List<GameObject> FloorTiles = new List<GameObject>();

        float lowerBound = -4.5f;
        float upperBound = 5.5f;
        
        float tileHeight = 0.01f;


        for (float tileX = lowerBound; tileX < upperBound; tileX++)
        {
            for (float tileZ = lowerBound; tileZ < upperBound; tileZ++)
            {
                // Make a cube
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.localScale = new Vector3(1, tileHeight, 1);
                cube.transform.position = new Vector3(tileX, tileHeight, tileZ);
                // Apply a Texture, my dude!
                cube.GetComponent<Renderer>().material.mainTexture = Resources.Load("Textures/FloorTile") as Texture2D;

                // TODO: Unique Name?

                //add to the list of floor tiles (do I need to do this?)
                FloorTiles.Add(cube);
            }
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        gameTimer++;

        if (gameTimer >= CurrentIncrement)
        {
            // Move the Tetronimo down by the fixed distance
            Debug.Log(gameTimer);
            gameTimer = 0;
        }
	}
}
