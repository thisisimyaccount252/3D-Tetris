using Assets.Scripts.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class Tetromino
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="type">The Type of tetromino.</param>
        /// <param name="texture">The name of the texture file being applied to the blocks</param>
        public Tetromino(TetrominoType type, string texture)
        {
            Type = type;

            Random random = new Random();
            int randomNumber = random.Next(0, 500);

            if (randomNumber == 42)
            {
                TextureName = "CageFace";
            }
            else
            {
                TextureName = texture;
            }
        }
        
        public TetrominoType Type { get; private set; }

        public string TextureName { get; private set; }

        public List<TetrominoBlock> Blocks { get; protected set; }   
    }
}
