using System;
using System.Collections.Generic;

namespace MazeGenerator
{
    public class TrapsAlgorithmFullRandom : TrapsAlgorithm
    {
        public TrapsAlgorithmFullRandom(int width, int height, int numberOfTraps) : base(width, height, numberOfTraps)
        {

        }

        //It finds positions for traps. Each trap has a separate cell.
        public override Coordinates[] GetTrapCoords()
        {
            Coordinates[] coordinates = new Coordinates[numberOfTraps];
            List<Coordinates> usedPositions = new List<Coordinates>();

            int lenght = coordinates.Length;
            Random rand = new Random();

            for(int i = 0; i < lenght; i++)
            {
                coordinates[i] = new Coordinates();
                int randX, randY;

                do
                {
                    randX = rand.Next(0, size.x);
                    randY = rand.Next(0, size.y);
                    coordinates[i].Set(randX, randY);
                }
                while (usedPositions.Contains(coordinates[i]));

                usedPositions.Add(coordinates[i]);
            }

            return coordinates;
        }
    }
}