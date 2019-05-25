﻿using Assets.Scripts.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class SquarePolyomino : Tetromino
    {
        public SquarePolyomino()
            : base(Enum.TetrominoType.O, TextureNames.SquarePolyomino)
        {
            Blocks = new List<TetrominoBlock>()
            {
                new TetrominoBlock(1, 0, 0), // one block right of pivot
                new TetrominoBlock(0, -1, 0), // one block below pivot
                new TetrominoBlock(1, -1, 0) // one block below and one block right of pivot
            };
        }
    }
}
