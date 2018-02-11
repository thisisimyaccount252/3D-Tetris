using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private float gameTimer;
    private float gameCeiling;
    private List<GameObject> FloorTiles;
    private Vector3 PieceStartingPosition;

    // TODO: Make a class that inherits from GameObject that includes this boolean 
    // so we can assign it to every Tetromino instead of setting it back and forth on the active one
    bool tetrominoCanMove = true;


    #region Controls
    private KeyCode RotateBlockLeftKey = KeyCode.A;
    private KeyCode RotateBlockRightKey = KeyCode.D;
    #endregion Controls

    // TODO: Public or private?
    public GameObject Tetromino;
    public float CurrentIncrement; // The tick systesm for how often a block should move

    // Use this for initialization
    void Start()
    {
        gameTimer = 0;
        gameCeiling = 7.5f;
        PieceStartingPosition = new Vector3(-0.5f, gameCeiling, -4.5f);

        GenerateTetromino();

        // TODO: Do I need this?
        FloorTiles = new List<GameObject>();
        GenerateFloorTiles();
    }

    // Update is called once per frame
    void Update()
    {
        RotateTetromino();
    }

    void FixedUpdate()
    {
        gameTimer++;

        if (gameTimer >= CurrentIncrement)
        {
            // Move the Tetronimo down by the fixed distance
            DropPiece();
            gameTimer = 0;
        }
    }

    private void RotateTetromino()
    {
        if (Input.GetKeyDown(RotateBlockLeftKey))
        {
            Tetromino.transform.Rotate(Vector3.forward, 90, Space.Self);
        }

        if (Input.GetKeyDown(RotateBlockRightKey))
        {
            Tetromino.transform.Rotate(Vector3.forward, -90, Space.Self);
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
                cube.transform.position = new Vector3(tileX, 0, tileZ);
                cube.name = "FloorTileCube";
                cube.transform.SetParent(transform);

                // Add some physics so pieces can't phase through the floor
                var tilePhysics = cube.AddComponent<Rigidbody>();
                tilePhysics.useGravity = true; // TODO: Do I need to stop this on the Y-axis so it isn't pushed down?
                tilePhysics.isKinematic = true;

                // Apply the script so we can pass the collider
                cube.AddComponent<FloorTileController>();

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
        Tetromino.name = "Tetromino";

        // Set it at the top of the board
        Tetromino.transform.position = PieceStartingPosition;

        // Add a texture
        Tetromino.GetComponent<Renderer>().material.mainTexture = Resources.Load("Textures/CageFace") as Texture2D;
        
        tetrominoCanMove = true;
    }

    void DropPiece()
    {
        if (tetrominoCanMove)
        {
            var floorLevel = 0.5f;

            var newY = Tetromino.transform.position.y - 1;
            if (newY <= floorLevel)
            {
                newY = floorLevel;
            }

            Tetromino.transform.position = new Vector3(Tetromino.transform.position.x, newY, Tetromino.transform.position.z);
        }
        else
        {
            // We're done here. Make a new one.
            GenerateTetromino();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Floor Collider Hit, bruh.");
        tetrominoCanMove = false;
    }
}
