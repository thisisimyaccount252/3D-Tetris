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
        private static readonly string _pivotBlockName = "Pivot";
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
            
            // Set the Tetromino's position
            gameObj.transform.position = startingPosition;

            // Create the block around which the shape will pivot when moving in three dimensions
            var pivot = GetTetrominoBlock(_pivotBlockName, gameObj);

            // Set it at the top of the board
            pivot.transform.position = gameObj.transform.position;

            // Set the rest of the blocks
            foreach (var blockInfo in Blocks)
            {
                // Get the block
                var block = GetTetrominoBlock(_blockName, gameObj);

                // Set the block's position by applying the block offset to the object's starting position. Offset is relative to the pivot block
                Vector3 position = new Vector3(startingPosition.x + blockInfo.PivotOffsetX, startingPosition.y + blockInfo.PivotOffsetY, startingPosition.z + blockInfo.PivotOffsetZ);
                block.transform.position = position;
            }

            return gameObj;
        }
        
        // TODO: Move this logic to the Tetromino Block itself
        private GameObject GetTetrominoBlock(string name, GameObject parent)
        {
            // Give it life and a name
            var block = GameObject.CreatePrimitive(PrimitiveType.Cube);
            block.name = name;

            // Set its teture
            block.GetComponent<Renderer>().material.mainTexture = Resources.Load("Textures/" + TextureName) as Texture2D;

            // Do the physics thing
            MakeItTetrisy(ref block);

            // Tell it whose its daddy
            block.transform.SetParent(parent.transform);

            return block;
        }

        private void MakeItTetrisy(ref GameObject block)
        {
            // Set the physics to be more Tetrisy
            // TBH I doubt this code does ANYTHING except setting bounciness to zero
            //var blockPhysics = pivot.GetComponent<Rigidbody>();
            var blockPhysics = block.AddComponent<Rigidbody>();
            blockPhysics.useGravity = false;
            block.GetComponent<Collider>().material.bounciness = 0;
        }
    }
}
