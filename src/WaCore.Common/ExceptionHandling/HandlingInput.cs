using System;

namespace WaCore.Common.ExceptionHandling
{
    public class HandlingInput<TParameter>
    {
        public Exception Exception { get; }

        public TParameter Parameter { get; }

        public HandlingInput(Exception exception, TParameter parameter = default(TParameter))
        {
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
            Parameter = parameter;
        }
    }

    public class HandlingInput : HandlingInput<object>
    {
        public HandlingInput(Exception exception, object parameter = null) : base(exception, parameter)
        {

        }
    }
}
