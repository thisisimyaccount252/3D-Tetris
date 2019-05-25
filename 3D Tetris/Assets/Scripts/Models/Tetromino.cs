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
        private static readonly string tetrominoName = "Tetromino";

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="type">The Type of tetromino.</param>
        /// <param name="texture">The name of the texture file being applied to the blocks</param>
        public Tetromino(TetrominoType type, string texture)
        {
            Type = type;
            TextureName = IsGodPiece() ? TextureNames.TheOneTrueGod : texture;
            TetroObject = new GameObject(tetrominoName);
        }
        
        public TetrominoType Type { get; private set; }
        public string TextureName { get; private set; }
        public List<TetrominoBlock> Blocks { get; protected set; }
        public GameObject TetroObject { get; set; }

        private bool IsGodPiece()
        {
            System.Random random = new System.Random();
            int randomNumber = random.Next(0, 100);

            return randomNumber == 42;
        }
    }
}
