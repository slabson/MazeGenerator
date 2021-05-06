namespace MazeGenerator
{
    public struct Coordinates
    {
        public int x;
        public int y;

        public Coordinates(int x = 0, int y = 0)
        {
            this.x = x;
            this.y = y;
        }

        public void Set(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", x, y);
        }

        public override bool Equals(object obj)
        {
            if (obj is Coordinates coords)
            {
                if (x == coords.x && y == coords.y)
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }
    }
}