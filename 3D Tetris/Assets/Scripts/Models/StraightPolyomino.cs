using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class StraightPolyomino : Tetromino
    {
        public StraightPolyomino() 
            : base(Enum.TetrominoType.I, "StraightPolyomino")
        {
            Blocks = new List<TetrominoBlock>()
            {
                new TetrominoBlock(-1, 0, 0), // one block left of pivot
                new TetrominoBlock( 1, 0, 0), // one block right of pivot
                new TetrominoBlock( 2, 0, 0) // two blocks right of pivot
            };
        }
    }
}
