//There is an explanation of how the algorithm works https://weblog.jamisbuck.org/2011/1/24/maze-generation-hunt-and-kill-algorithm

namespace MazeGenerator
{
    public class MazeAlgorithmHuntAndKill : MazeAlgorithm
    {
        public Coordinates CurentCellCoords { get { return cellCoords; } }
        public MazeCellInfo CurrentCell { get { return grid[cellCoords.x, cellCoords.y]; } }

        private Coordinates cellCoords = new Coordinates(); //coordinates of the current cell
        private bool allVisited = false; //if true, the algorithm has been finished

        public MazeAlgorithmHuntAndKill(int width, int height) : base (width, height)
        {

        }

        public override MazeCellInfo[,] GetGridInfo()
        {
            CurrentCell.Visit();

            while (!allVisited) //Repeat the cycle until all cells are visited
            {
                Walk();
                Hunt();
            }

            return grid;
        }

        public void Reset()
        {
            ResetGrid();
            allVisited = false;
            cellCoords.Set(0, 0);
        }

        //Go in a random direction where the next cell is unvisited until there are unvisited neighbours for the current cell
        private void Walk()
        {
            while (ExistUnvisitedNeighbours(cellCoords.x, cellCoords.y))
            {
                Directions direction = Direction.GetRandom();

                switch (direction)
                {
                    case Directions.North:
                        {
                            if (IsCellUnvisited(cellCoords.x, cellCoords.y + 1))
                            {
                                RemoveWalls(direction); //remove the walls between cells
                                cellCoords.y++;         //set the next cell position as current
                                CurrentCell.Visit();    //visit the current cell
                            }

                            break;
                        }
                    case Directions.South:
                        {
                            if (IsCellUnvisited(cellCoords.x, cellCoords.y - 1))
                            {
                                RemoveWalls(direction);
                                cellCoords.y--;
                                CurrentCell.Visit();
                            }

                            break;
                        }
                    case Directions.East:
                        {
                            if (IsCellUnvisited(cellCoords.x + 1, cellCoords.y))
                            {
                                RemoveWalls(direction);
                                cellCoords.x++;
                                CurrentCell.Visit();
                            }

                            break;
                        }
                    case Directions.West:
                        {
                            if (IsCellUnvisited(cellCoords.x - 1, cellCoords.y))
                            {
                                RemoveWalls(direction);
                                cellCoords.x--;
                                CurrentCell.Visit();
                            }

                            break;
                        }
                }
            }
        }

        //Find first unvisited cell, set the start position for the next walk and remove a random wall between the cell and visited neighbour to connect the path.
        private void Hunt()
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    if (!grid[x, y].Visited && ExistVisitedNeightbours(x, y))
                    {
                        cellCoords.Set(x, y);
                        CurrentCell.Visit();
                        RemoveAdditionalWallBetweenVisitedNeighbour();
                        return;
                    }
                }
            }

            allVisited = true;
        }

        private void RemoveAdditionalWallBetweenVisitedNeighbour()
        {
            bool removed = false;
            Direction dir = new Direction();

            while (!removed)
            {
                Directions direction = dir.GetRandomAndNew();

                switch (direction)
                {
                    case Directions.North:
                        {
                            if (IsCellVisited(cellCoords.x, cellCoords.y + 1))
                            {
                                RemoveWalls(direction);
                                removed = true;
                            }
                            break;
                        }
                    case Directions.South:
                        {
                            if (IsCellVisited(cellCoords.x, cellCoords.y - 1))
                            {
                                RemoveWalls(direction);
                                removed = true;
                            }
                            break;
                        }
                    case Directions.East:
                        {
                            if (IsCellVisited(cellCoords.x + 1, cellCoords.y))
                            {
                                RemoveWalls(direction);
                                removed = true;
                            }
                            break;
                        }
                    case Directions.West:
                        {
                            if (IsCellVisited(cellCoords.x - 1, cellCoords.y))
                            {
                                RemoveWalls(direction);
                                removed = true;
                            }
                            break;
                        }
                }
            }
        }

        //Remove two walls between the current and next cell. You have to check if the next cell exists, before.
        private void RemoveWalls(Directions direction)
        {
            switch(direction)
            {
                case Directions.North:
                    {
                        CurrentCell.RemoveWall(Directions.North);
                        grid[cellCoords.x, cellCoords.y + 1].RemoveWall(Directions.South);
                        break;
                    }
                case Directions.South:
                    {
                        CurrentCell.RemoveWall(Directions.South);
                        grid[cellCoords.x, cellCoords.y - 1].RemoveWall(Directions.North);
                        break;
                    }
                case Directions.East:
                    {
                        CurrentCell.RemoveWall(Directions.East);
                        grid[cellCoords.x + 1, cellCoords.y].RemoveWall(Directions.West);
                        break;
                    }
                case Directions.West:
                    {
                        CurrentCell.RemoveWall(Directions.West);
                        grid[cellCoords.x - 1, cellCoords.y].RemoveWall(Directions.East);
                        break;
                    }
            }
        }
    }
}