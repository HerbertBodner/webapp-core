using System;

namespace WaCore.Common.ExceptionHandling
{
    public static class WacHandling
    {
        private static readonly HandlingResult<object> HandledResult = new HandlingResult(true);

        private static readonly HandlingResult<object> IgnoredResult = new HandlingResult(false);
        
        public static WacHandlingConfiguration<TParameter, TResult> Prepare<TParameter, TResult>()
        {
            return new WacHandlingConfiguration<TParameter, TResult>();
        }

        public static WacHandlingConfiguration Prepare()
        {
            return new WacHandlingConfiguration();
        }
        
        public static TResult Result<TResult>(this HandlingResult<object> result)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));

            return (TResult)result.Result;
        }

        public static TResult Parameter<TResult>(this HandlingInput<object> input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            return (TResult)input.Parameter;
        }

        public static HandlingResult<TResult> Handled<TResult>()
        {
            return new HandlingResult<TResult>(true);
        }

        public static HandlingResult<TResult> Handled<TResult>(TResult result)
        {
            return new HandlingResult<TResult>(true, result);
        }

        public static HandlingResult<object> Handled()
        {
            return HandledResult;
        }

        public static HandlingResult<object> Handled(object result)
        {
            return new HandlingResult(true, result);
        }

        public static HandlingResult<TResult> Ignore<TResult>()
        {
            return new HandlingResult<TResult>(false);
        }

        public static HandlingResult<object> Ignore()
        {
            return IgnoredResult;
        }
    }
}
