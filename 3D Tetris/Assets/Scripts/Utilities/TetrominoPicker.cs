using Assets.Scripts.Enum;
using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;

namespace Assets.Scripts.Utilities
{
    public static class TetrominoPicker
    {
        /// <summary>
        /// Grabs a random tetromino piece.
        /// </summary>
        /// <returns>A random tetromino piece.</returns>
        public static Tetromino GetRandom(Vector3 startPosition)
        {
            var tetrominoTypeUpperBound = (int)System.Enum.GetValues(typeof(TetrominoType)).Cast<TetrominoType>().Max();

            System.Random random = new System.Random();
            int randomNumber = random.Next(0, tetrominoTypeUpperBound + 1); // Adding 1 to make the upper bound inclusive (otherwise we would never get "L" blocks

            return GetTetromino((TetrominoType)randomNumber, startPosition);
        }

        /// <summary>
        /// Grabs a specific tetromino piece.
        /// </summary>
        /// <param name="type">The type of tetromino piece being requested.</param>
        /// <returns>A specific tetromino piece whose type is <paramref name="type"/>.</returns>
        public static Tetromino GetTetromino(TetrominoType type, Vector3 startPosition)
        {
            Tetromino tetromino;
            switch (type)
            {
                //case TetrominoType.I:
                //    tetromino = new StraightPolyomino(startPosition);
                //    break;
                //case TetrominoType.O:
                //    tetromino = new SquarePolyomino(startPosition);
                //    break;
                //case TetrominoType.Z:
                //    tetromino = new SkewPolyomino(startPosition);
                //    break;
                //case TetrominoType.T:
                //    tetromino = new TPolyomino(startPosition);
                //    break;
                //case TetrominoType.L:
                //    tetromino = new LPolyomino(startPosition);
                //    break;
                //default:
                //    tetromino = new SkewPolyomino(startPosition);
                //    break;
                default:
                    tetromino = new TPolyomino(startPosition);
                    break;
            }

            return tetromino;
        }
    }
}
