using System;

namespace MazeGenerator
{
    public abstract class MazeAlgorithm
    {
        protected MazeCellInfo[,] grid;
        protected IntPair size;

        public abstract MazeCellInfo[,] GetGridInfo();

        public MazeAlgorithm(int width, int height)
        {
            size.Set(width, height);
            InitGrid();
        }

        protected void InitGrid()
        {
            grid = new MazeCellInfo[size.x, size.y];

            for(int x = 0; x < size.x; x++)
            {
                for(int y = 0; y < size.y; y++)
                {
                    grid[x, y] = new MazeCellInfo();
                }
            }
        }

        public bool CellExists(int x, int y)
        {
            return x >= 0 && x < size.x
                 && y >= 0 && y < size.y;
        }

        public bool IsCellVisited(int x, int y)
        {
            return CellExists(x, y) && grid[x, y].Visited;
        }

        public bool IsCellUnvisited(int x, int y)
        {
            return CellExists(x, y) && !grid[x, y].Visited;
        }

        public bool ExistUnvisitedNeighbours(int x, int y)
        {
            return IsCellUnvisited(x, y + 1)
                 || IsCellUnvisited(x, y - 1)
                 || IsCellUnvisited(x + 1, y)
                 || IsCellUnvisited(x - 1, y);
        }

        public bool ExistVisitedNeightbours(int x, int y)
        {
            return IsCellVisited(x, y + 1)
                 || IsCellVisited(x, y - 1)
                 || IsCellVisited(x + 1, y)
                 || IsCellVisited(x - 1, y);
        }

        protected void ResetGrid()
        {
            int width = grid.GetLength(0);
            int height = grid.GetLength(1);

            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    grid[x, y].Reset();
                }
            }
        }

        public static Type GetMazeAlgorithmType(MazeAlgorithms algorithm)
        {
            switch(algorithm)
            {
                //Classes inherited from MazeAlgorithm
                case MazeAlgorithms.HuntAndKill: return typeof(MazeAlgorithmHuntAndKill);
            }

            return null;
        }
    }
}