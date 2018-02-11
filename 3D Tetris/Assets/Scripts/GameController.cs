using Assets.Scripts.Enum;
using Assets.Scripts.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private float gameTimer;
    private float gameCeiling;
    private List<GameObject> FloorTiles; // TODO: Remove Floor Tile Logic
    private Vector3 PieceStartingPosition;

    // TODO: Make a class that inherits from GameObject that includes this boolean 
    // so we can assign it to every Tetromino instead of setting it back and forth on the active one
    bool tetrominoCanMove = true;

    #region Controls
    // By default, these keys just rotate the tetromino
    private KeyCode UpKey = KeyCode.W;
    private KeyCode LeftKey = KeyCode.A;
    private KeyCode DownKey = KeyCode.S;
    private KeyCode RightKey = KeyCode.D;

    // holding shift and pressing one of these keys will move the tetromino
    private KeyCode MoveKey = KeyCode.LeftShift;

    private KeyCode DropIt = KeyCode.Space;
    #endregion Controls

    // TODO: Public or private?
    public GameObject Tetromino;
    public float MovementTick; // The tick systesm for how often a block should move

    // Use this for initialization
    void Start()
    {
        gameTimer = 0;
        gameCeiling = 7.5f;
        PieceStartingPosition = new Vector3(0.5f, gameCeiling, 0.5f);

        GenerateTetromino();

        // TODO: Do I need this?
        FloorTiles = new List<GameObject>();
        GenerateFloorTiles();
    }

    // Update is called once per frame
    void Update()
    {
        ManipulateTetromino();
    }

    void FixedUpdate()
    {
        gameTimer++;

        if (gameTimer >= MovementTick)
        {
            // Move the Tetromino down by the fixed distance
            DropPiece();
            gameTimer = 0;
        }
    }

    private void ManipulateTetromino()
    {
        if (Input.GetKeyDown(DropIt))
        {
            DropToFloor();
        }

        if (Input.GetKeyDown(UpKey))
        {
            if (Input.GetKey(MoveKey))
            {
                var newZ = Tetromino.transform.position.z + 1;
                Tetromino.transform.position = new Vector3(Tetromino.transform.position.x, Tetromino.transform.position.y, newZ);
            }
            else
            {
                Tetromino.transform.Rotate(Vector3.right, 90, Space.World);
                
            }
        }
        else if (Input.GetKeyDown(LeftKey))
        {
            if (Input.GetKey(MoveKey))
            {
                var newX = Tetromino.transform.position.x - 1;
                Tetromino.transform.position = new Vector3(newX, Tetromino.transform.position.y, Tetromino.transform.position.z);
            }
            else
            {
                Tetromino.transform.Rotate(Vector3.forward, 90, Space.World);
            }
        }
        else if (Input.GetKeyDown(DownKey))
        {
            if (Input.GetKey(MoveKey))
            {
                var newZ = Tetromino.transform.position.z - 1;
                Tetromino.transform.position = new Vector3(Tetromino.transform.position.x, Tetromino.transform.position.y, newZ);
            }
            else
            {
                Tetromino.transform.Rotate(Vector3.right, -90, Space.World);
            }
        }
        else if (Input.GetKeyDown(RightKey))
        {
            if (Input.GetKey(MoveKey))
            {
                var newX = Tetromino.transform.position.x + 1;
                Tetromino.transform.position = new Vector3(newX, Tetromino.transform.position.y, Tetromino.transform.position.z);
            }
            else
            {
                Tetromino.transform.Rotate(Vector3.forward, -90, Space.World);
            }
        }
        else
        {
            // Eh. fuck it.
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
                cube.transform.position = new Vector3(tileX, -0.01f, tileZ);
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

    // TODO: Store Game Object in the Tetromino and TetrominoBlock classes and do these manipulations on initialization, my dude.
    void GenerateTetromino()
    {
        // Determine which type of piece is created
        var tetro = TetrominoPicker.GetRandom();

        // Create the current piece
        Tetromino = new GameObject("Tetromino");
        Tetromino.transform.position = PieceStartingPosition;

        // Create tetromino's pivot block
        var pivot = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pivot.name = "Pivot";
        // Make it a child of the tetromino
        pivot.transform.SetParent(Tetromino.transform);
        // Apply texture
        pivot.GetComponent<Renderer>().material.mainTexture = Resources.Load("Textures/" + tetro.TextureName) as Texture2D;
        // Apply physics
        var pivotPhysics = pivot.AddComponent<Rigidbody>();
        pivotPhysics.useGravity = false;
        pivot.GetComponent<Collider>().material.bounciness = 0;
        // Set it at the top of the board
        // TODO: do I also have to move the Tetromino?
        pivot.transform.position = Tetromino.transform.position;

        // Set the rest of the blocks
        foreach (var blockInfo in tetro.Blocks)
        {
            var block = GameObject.CreatePrimitive(PrimitiveType.Cube);
            block.name = "Block";
            block.transform.SetParent(Tetromino.transform);
            block.GetComponent<Renderer>().material.mainTexture = Resources.Load("Textures/" + tetro.TextureName) as Texture2D;
            var blockPhysics = pivot.GetComponent<Rigidbody>();
            blockPhysics.useGravity = false;
            block.GetComponent<Collider>().material.bounciness = 0;

            // Apply the block offset to the pivot's position
            Vector3 position = new Vector3(PieceStartingPosition.x + blockInfo.PivotOffsetX, PieceStartingPosition.y + blockInfo.PivotOffsetY, PieceStartingPosition.z + blockInfo.PivotOffsetZ);
            block.transform.position = position;
        }

        tetrominoCanMove = true;
    }

    /// <summary>
    /// Moves Piece down the board by a tiny increment
    /// </summary>
    void DropPiece()
    {
        if (tetrominoCanMove)
        {
            var floorLevel = 0.0f;

            var newY = Tetromino.transform.position.y - 0.5f;
            if (newY < floorLevel)
            {
                Debug.Log("NewY : " + newY);
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

    /// <summary>
    /// Drops the piece to the floor of the board
    /// </summary>
    void DropToFloor()
    {
        Tetromino.transform.position = new Vector3(Tetromino.transform.position.x, 0.5f, Tetromino.transform.position.z);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Floor Collider Hit, bruh.");
        tetrominoCanMove = false;
    }
}
