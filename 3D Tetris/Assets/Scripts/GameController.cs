using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private float gameTimer;
    private float gameCeiling;
    private List<GameObject> FloorTiles;
    private Vector3 PieceStartingPosition;
    

    #region Controls
    private KeyCode RotateBlockLeftKey = KeyCode.A;
    private KeyCode RotateBlockRightKey = KeyCode.D;
    #endregion Controls

    // TODO: Public or private?
    public GameObject Tetromino;
    public float CurrentIncrement; // The tick systesm for how often a block should move

	// Use this for initialization
	void Start () {
        gameTimer = 0;
        gameCeiling = 7.5f;
        PieceStartingPosition = new Vector3(-0.5f, gameCeiling, -4.5f);

        GenerateTetromino();

        // TODO: Do I need this?
        FloorTiles = new List<GameObject>();
        GenerateFloorTiles();
	}

    // Update is called once per frame
    void FixedUpdate () {
        gameTimer++;

        RotateTetromino();

        if (gameTimer >= CurrentIncrement)
        {
            // Move the Tetronimo down by the fixed distance
            Debug.Log(gameTimer);
            DropPiece();
            gameTimer = 0;
        }
	}

    private void RotateTetromino()
    {
        if (Input.GetKeyDown(RotateBlockLeftKey))
        {
            Tetromino.transform.Rotate(Vector3.forward, -90, Space.Self);
            //Tetromino.transform.Rotate(Vector3.forward, -90, Space.World);
        }

        if (Input.GetKeyDown(RotateBlockRightKey))
        {
            Tetromino.transform.Rotate(Vector3.forward, 90, Space.Self);
            //Tetromino.transform.Rotate(Vector3.forward, 90, Space.World);
        }
    }

    void GenerateFloorTiles()
    {
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

    void GenerateTetromino()
    {
        // TODO: Create actual tetronimo pieces (pieces at random)
        Tetromino = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Tetromino.transform.position = new Vector3(-0.5f, gameCeiling, -4.5f);
        Tetromino.GetComponent<Renderer>().material.mainTexture = Resources.Load("Textures/CageFace") as Texture2D;
    }

    void DropPiece()
    {
        Tetromino.transform.position = new Vector3(Tetromino.transform.position.x, Tetromino.transform.position.y - 1, Tetromino.transform.position.z);
    }
}
