using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class TPolyomino : Tetromino
    {
        public TPolyomino()
            : base(Enum.TetrominoType.T, "TPolyomino")
        {
            Blocks = new List<TetrominoBlock>()
            {
                new TetrominoBlock(-1, 0, 0), // one block left of pivot
                new TetrominoBlock(1, 0, 0), // one block right of pivot
                new TetrominoBlock(-1, -1, 0) // one block left of and one block below pivot
            };
        }
    }
}
