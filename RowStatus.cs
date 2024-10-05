namespace ScenarioBasedVideoUploader
{
    public class RowStatus
    {
        public string   Text        { get; }
        public bool     IsExecuted  { get; }

        public RowStatus( string text, bool isExecuted )
        {
            Text        = text;
            IsExecuted  = isExecuted;
        }
    }
}