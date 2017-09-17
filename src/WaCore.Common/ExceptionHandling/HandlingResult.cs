namespace WaCore.Common.ExceptionHandling
{
    public class HandlingResult<TResult>
    {
        public HandlingResult()
        {
            Handled = true;
        }

        public HandlingResult(bool handled, TResult result = default(TResult))
        {
            Handled = handled;
            Result = result;
        }

        public bool Handled { get; }

        public TResult Result { get; }
    }

    public class HandlingResult : HandlingResult<object>
    {
        public HandlingResult()
        {
        }

        public HandlingResult(bool handled, object result = null) : base(handled, result)
        {
        }
    }
}
