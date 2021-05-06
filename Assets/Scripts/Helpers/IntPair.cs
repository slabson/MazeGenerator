namespace MazeGenerator
{
    public struct IntPair
    {
        public int x;
        public int y;

        public IntPair(int x = 0, int y = 0)
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
            return string.Format("x = {0}, y = {1}", x, y);
        }
    }
}