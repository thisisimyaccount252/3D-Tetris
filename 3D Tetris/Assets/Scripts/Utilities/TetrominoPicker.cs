using Assets.Scripts.Enum;
using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utilities
{
    public static class TetrominoPicker
    {
        /// <summary>
        /// Grabs a random tetromino piece.
        /// </summary>
        /// <returns>A random tetromino piece.</returns>
        public static Tetromino GetRandom()
        {
            var tetrominoTypeUpperBound = (int)System.Enum.GetValues(typeof(TetrominoType)).Cast<TetrominoType>().Max();

            Random random = new Random();
            int randomNumber = random.Next(0, tetrominoTypeUpperBound + 1); // Adding 1 to make the upper bound inclusive (otherwise we would never get "L" blocks

            return GetTetromino((TetrominoType)randomNumber);
        }

        /// <summary>
        /// Grabs a specific tetromino piece.
        /// </summary>
        /// <param name="type">The type of tetromino piece being requested.</param>
        /// <returns>A specific tetromino piece whose type is <paramref name="type"/>.</returns>
        public static Tetromino GetTetromino(TetrominoType type)
        {
            Tetromino tetromino;
            switch (type)
            {
                case TetrominoType.I:
                    tetromino = new StraightPolyomino();
                    break;
                case TetrominoType.O:
                    tetromino = new SquarePolyomino();
                    break;
                case TetrominoType.Z:
                    tetromino = new SkewPolyomino();
                    break;
                case TetrominoType.T:
                    tetromino = new TPolyomino();
                    break;
                case TetrominoType.L:
                    tetromino = new LPolyomino();
                    break;
                default:
                    tetromino = new SkewPolyomino();
                    break;
            }

            return tetromino;
        }
    }
}
