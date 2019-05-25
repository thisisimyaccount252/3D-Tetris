using Assets.Scripts.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class SquarePolyomino : Tetromino
    {
        private static List<TetrominoBlock> _blockList = new List<TetrominoBlock>()
        {
            new TetrominoBlock(1, 0, 0), // one block right of pivot
            new TetrominoBlock(0, -1, 0), // one block below pivot
            new TetrominoBlock(1, -1, 0) // one block below and one block right of pivot
        };
        public SquarePolyomino(Vector3 startPosition)
            : base(Enum.TetrominoType.O, TextureNames.SquarePolyomino, _blockList, startPosition) { }
    }
}
