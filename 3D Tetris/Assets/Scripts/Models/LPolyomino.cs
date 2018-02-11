using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class LPolyomino : Tetromino
    {
        public LPolyomino()
            : base(Enum.TetrominoType.L, "LPolyomino")
        {
            Blocks = new List<TetrominoBlock>()
            {
                new TetrominoBlock(1, 0, 0), // one block right of pivot
                new TetrominoBlock(-1, 0, 0), // one block left of pivot
                new TetrominoBlock(-1, -1, 0) // one block left of and one block below pivot
            };
        }
    }
}
