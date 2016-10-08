using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class ImmutableGame
    {
        protected readonly int[,] frame;
        protected readonly Coordinates[] currentLocation;
        
        public ImmutableGame(params int[] tiles)
        {
            if (tiles.Length < 4)
                throw new ArgumentException("Too few arguments.");
            else
            {
                double sideDouble = Math.Sqrt(tiles.Length);
                int side = Convert.ToInt32(sideDouble);
                if (sideDouble != side)
                    throw new ArgumentException("Field isn't squared.");
                else
                {

                    currentLocation = new Coordinates[tiles.Length];
                    frame = new int[side, side];
                    for (int i = 0; i < side; i++)
                        for (int j = 0; j < side; j++)
                            if (tiles[i * side + j] >= tiles.Length
                                || currentLocation[tiles[i * side + j]] != null)
                                throw new ArgumentException("Number on some tile isn't correct.");
                            else
                            {
                                frame[i, j] = tiles[i * side + j];
                                currentLocation[tiles[i * side + j]] =
                                        new Coordinates { X = i, Y = j };
                            }
                }
            }
        }

        protected ImmutableGame(ImmutableGame previousState, int value)
        {
            frame = new int[previousState.frame.GetLength(0), previousState.frame.GetLength(1)];
            currentLocation = new Coordinates[previousState.currentLocation.Length];
            for (int i = 0; i < frame.GetLength(0); i++)
                for (int j = 0; j < frame.GetLength(1); j++)
                    frame[i, j] = previousState.frame[i, j];
            for (int i = 0; i < currentLocation.Length; i++)
                currentLocation[i] = previousState.currentLocation[i];
            frame[currentLocation[value].X, currentLocation[value].Y] = 0;
            frame[currentLocation[0].X, currentLocation[0].Y] = value;
            Coordinates temp = currentLocation[value];
            currentLocation[value] = currentLocation[0];
            currentLocation[0] = temp;
        }

        public Coordinates GetLocation(int value)
        {
            return currentLocation[value];
        }

        public int this[int x, int y]
        {
            get
            {
                return frame[x, y];
            }
        }

        public ImmutableGame Shift(int value)
        {
            if (currentLocation[value].isAdjacent(currentLocation[0]))
                return new ImmutableGame(this, value);
            else
                throw new ArgumentException("Element not adjacent to zero.");
        }
    }
}
