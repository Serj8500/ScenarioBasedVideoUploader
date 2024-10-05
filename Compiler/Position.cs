namespace ScenarioBasedVideoUploader.Compiler
{
    internal interface IPosition 
    {
        int                 RowIndex                    { get; set; }
        int                 ColumnIndex                 { get; set; }

        void                SwitchToNextLine();
        void                GoToNextSymbol();
        
        IReadOnlyPosition   ToReadOnlyPosition();
    }
    
    public interface IReadOnlyPosition
    {
        int                 RowIndex                    { get; }
        int                 ColumnIndex                 { get; }
    }
    
    internal class Position : IPosition, IReadOnlyPosition
    {
        public int          RowIndex                    { get; set; }
        public int          ColumnIndex                 { get; set; }

        public Position()
        {
            RowIndex    = 0;
            ColumnIndex = 0;
        }

        private Position( int rowIndex, int columnIndex )
        {
            RowIndex    = rowIndex;
            ColumnIndex = columnIndex;
        }

        public override string ToString()
        {
            return $"({RowIndex},{ColumnIndex})";
        }

        public void SwitchToNextLine()
        {
            RowIndex   += 1;
            ColumnIndex = 0;
        }

        public void GoToNextSymbol()
        {
            ColumnIndex += 1;
        }

        protected bool Equals( IPosition other )
        {
            return RowIndex == other.RowIndex && ColumnIndex == other.ColumnIndex;
        }

        public override bool Equals( object obj )
        {
            if( obj is null ) return false;
            if( ReferenceEquals( this, obj ) ) return true;
            if( obj.GetType() != GetType() ) return false;
            return Equals( (IPosition)obj );
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public IReadOnlyPosition ToReadOnlyPosition()
        {
            return Clone();
        }

        private Position Clone() => new( RowIndex, ColumnIndex );
    }
}