namespace badlife
{
    internal class Cell
    {
        public static bool StateChanged = false;
        public Cell(long row, long col,int state, int neighbours)
        {
            Row = row;
            Col = col;
            State = state;
            Neighbours = neighbours;
        }

        public string Key 
        {
            get
            {
                return $"{Row}#{Col}";
            }
        }
        public long Row { get; set; }
        public long Col { get; set; }

        private int _state;
        public int State 
        {
            get 
            {
                return _state;
            }
            set
            {
                if(!StateChanged && _state != value)
                    StateChanged = true;

                _state=value;
            }
        }
        public int Neighbours { get; set; }

        public Cell Update(int neighbours)
        {
            Neighbours += neighbours;
            return this;
        }
    }
}
