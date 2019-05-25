using Assets.Scripts.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class LPolyomino : Tetromino
    {
        private static List<TetrominoBlock> _blockList = new List<TetrominoBlock>()
        {
            new TetrominoBlock(1, 0, 0), // one block right of pivot
            new TetrominoBlock(-1, 0, 0), // one block left of pivot
            new TetrominoBlock(-1, -1, 0) // one block left of and one block below pivot
        };

        public LPolyomino(Vector3 startPosition)
            : base(Enum.TetrominoType.L, TextureNames.LPolyomino, _blockList, startPosition) { }
    }
}
