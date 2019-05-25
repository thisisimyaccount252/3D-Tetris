using Assets.Scripts.Enum;
using Assets.Scripts.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class Tetromino
    {
        private static readonly string _tetrominoName = "Tetromino";
        private static readonly string _pviotBlockName = "Pivot";
        private static readonly string _blockName = "Block";

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="type">The Type of tetromino.</param>
        /// <param name="texture">The name of the texture file being applied to the blocks</param>
        public Tetromino(TetrominoType type, string texture, List<TetrominoBlock> blox, Vector3 startPosition)
        {
            Type = type;
            TextureName = IsGodPiece() ? TextureNames.TheOneTrueGod : texture;
            Blocks = blox;
            GameObject = SetupGameObject(startPosition);
        }
        
        public TetrominoType Type { get; private set; }
        public string TextureName { get; private set; }
        public List<TetrominoBlock> Blocks { get; protected set; }
        
        public GameObject GameObject { get; set; }
        public Vector3 StartingPosition { get; set; }

        private bool IsGodPiece()
        {
            System.Random random = new System.Random();
            int randomNumber = random.Next(0, 100);

            return randomNumber == 42;
        }

        private GameObject SetupGameObject(Vector3 startingPosition)
        {
            GameObject gameObj = new GameObject(_tetrominoName);

            // Create the block around which the shape will pivot when moving in three dimensions
            var pivot = GameObject.CreatePrimitive(PrimitiveType.Cube);
            pivot.name = _pviotBlockName;

            // Set the Tetromino's position
            gameObj.transform.position = startingPosition;

            // Make it a child of the tetromino
            pivot.transform.SetParent(gameObj.transform);
            // Apply texture
            pivot.GetComponent<Renderer>().material.mainTexture = Resources.Load("Textures/" + TextureName) as Texture2D;
            // Apply physics
            var pivotPhysics = pivot.AddComponent<Rigidbody>();
            pivotPhysics.useGravity = false;
            pivot.GetComponent<Collider>().material.bounciness = 0;
            // Set it at the top of the board
            pivot.transform.position = gameObj.transform.position;

            // Set the rest of the blocks
            foreach (var blockInfo in Blocks)
            {
                var block = GameObject.CreatePrimitive(PrimitiveType.Cube);
                block.name = _blockName;
                block.transform.SetParent(gameObj.transform);
                block.GetComponent<Renderer>().material.mainTexture = Resources.Load("Textures/" + TextureName) as Texture2D;
                var blockPhysics = pivot.GetComponent<Rigidbody>();
                blockPhysics.useGravity = false;
                block.GetComponent<Collider>().material.bounciness = 0;

                // Apply the block offset to the pivot's position
                Vector3 position = new Vector3(startingPosition.x + blockInfo.PivotOffsetX, startingPosition.y + blockInfo.PivotOffsetY, startingPosition.z + blockInfo.PivotOffsetZ);
                block.transform.position = position;
            }

            return gameObj;
        }
    }
}
