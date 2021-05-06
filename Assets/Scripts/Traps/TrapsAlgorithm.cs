using System;

namespace MazeGenerator
{
    public abstract class TrapsAlgorithm
    {
        protected IntPair size;
        protected int numberOfTraps;

        public abstract Coordinates[] GetTrapCoords();

        public TrapsAlgorithm(int width, int height, int numberOfTraps)
        {
            size = new IntPair(width, height);
            this.numberOfTraps = Math.Min(size.x * size.y, numberOfTraps); //to be sure that the number of traps is not greater than the number of cells
        }

        public static Type GetTrapsAlgorithmType(TrapsAlgorithms algorithm)
        {
            switch(algorithm)
            {
                //Classes inherited from TrapsAlgorithm
                case TrapsAlgorithms.FullRandom: return typeof(TrapsAlgorithmFullRandom);
            }

            return null;
        }
    }
}