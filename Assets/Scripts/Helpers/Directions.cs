using System.Collections.Generic;
using UnityEngine;

namespace MazeGenerator
{
    public enum Directions
    {
        North = 0,
        South = 1,
        East = 2,
        West = 3
    }

    public class Direction
    {
        private List<int> previousDirections;

        public Direction()
        {
            previousDirections = new List<int>();
        }

        //The same directions won't be returned
        public Directions GetRandomAndNew()
        {
            if(previousDirections.Count >= 4)
            {
                Reset();
            }

            int rand;

            do
            {
                rand = Random.Range(0, 4);
                
            }
            while (previousDirections.Contains(rand));

            previousDirections.Add(rand);
            return (Directions)rand;
        }

        public void Reset()
        {
            previousDirections.Clear();
        }

        public static Directions GetRandom()
        {
            int rand = Random.Range(0, 4);
            return (Directions)rand;
        }
    }
}