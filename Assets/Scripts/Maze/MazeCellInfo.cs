namespace MazeGenerator
{
    public class MazeCellInfo
    {
        public bool Visited { get; private set; }

        ///<summary>True if the wall exist</summary>
        public bool wallN, wallS, wallE, wallW;
        private bool defaultValue;

        public MazeCellInfo(bool defaultValue = true)
        {
            this.defaultValue = defaultValue;
            ResetWalls(defaultValue);
        }

        public void Visit()
        {
            Visited = true;
        }

        public void Reset()
        {
            Visited = false;
            ResetWalls(defaultValue);
        }

        public void ResetWalls(bool defaultValue)
        {
            wallN = wallS = wallE = wallW = defaultValue;
        }

        public void SetWall(Directions direction, bool exists)
        {
            switch (direction)
            {
                case Directions.North: wallN = exists; break;
                case Directions.South: wallS = exists; break;
                case Directions.East: wallE = exists; break;
                case Directions.West: wallW = exists; break;
            }
        }

        public void AddWall(Directions direction)
        {
            SetWall(direction, true);
        }

        public void RemoveWall(Directions directions)
        {
            SetWall(directions, false);
        }
    }
}