using Assets.Scripts.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class StraightPolyomino : Tetromino
    {
        private static List<TetrominoBlock> _blockList = new List<TetrominoBlock>()
        {
                new TetrominoBlock(-1, 0, 0), // one block left of pivot
                new TetrominoBlock( 1, 0, 0), // one block right of pivot
                new TetrominoBlock( 2, 0, 0) // two blocks right of pivot
        };

        public StraightPolyomino(Vector3 startPosition)
               : base(Enum.TetrominoType.I, TextureNames.StraightPolynomio, _blockList, startPosition) { }
    }
}
