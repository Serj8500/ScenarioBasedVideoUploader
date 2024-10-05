namespace ScenarioBasedVideoUploader
{
    public class IsExecutedWrapper<T>
    {
        public T    Item        { get; }
        public bool IsExecuted  { get; set; }

        public IsExecutedWrapper( T item )
        {
            Item        = item;
            IsExecuted  = false;
        }
    }
}