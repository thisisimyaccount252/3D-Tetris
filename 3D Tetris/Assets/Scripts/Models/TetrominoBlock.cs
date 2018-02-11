using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class TetrominoBlock
    {
        public TetrominoBlock(int xOffset, int yOffset, int zOffset)
        {
            PivotOffsetX = xOffset;
            PivotOffsetY = yOffset;
            PivotOffsetZ = zOffset;
        }

        public int PivotOffsetX { get; private set; }
        public int PivotOffsetY { get; private set; }
        public int PivotOffsetZ { get; private set; }
    }
}
