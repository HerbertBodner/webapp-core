using System;
using System.Collections.Generic;
using System.Linq;

namespace WaCore.Common.ExceptionHandling
{
    public class WacHandlingConfiguration<TParameter, TResult>
    {
        private readonly IList<Func<Exception, HandlingInput<TParameter>, HandlingResult<TResult>>> _handlers =
            new List<Func<Exception, HandlingInput<TParameter>, HandlingResult<TResult>>>();

        private Action<Exception, HandlingInput<TParameter>, HandlingResult<TResult>> _finalizationHandler;

        public HandlingMatchType MatchType { get; set; } = HandlingMatchType.Inheritance;

        private void AddHandler(Func<Exception, HandlingInput<TParameter>, HandlingResult<TResult>> handler)
        {
            _handlers.Add(handler);
        }

        private bool TryCastException<TException>(Exception sourceException, out TException exception) where TException: Exception
        {
            exception = null;
            switch (MatchType)
            {
                case HandlingMatchType.Inheritance:
                {
                    if (sourceException is TException)
                    {
                        exception = (TException) sourceException;
                        return true;
                    }
                    return false;
                }
                case HandlingMatchType.ExactType:
                {
                    if (sourceException.GetType() == typeof(TException))
                    {
                        exception = (TException)sourceException;
                        return true;
                    }
                    return false;
                }
                default:
                    throw new NotImplementedException($"Unrecognized HandlingMatchType value - '{MatchType}'");
            }
        }

        public bool ContainsHandler<TException>() where TException : Exception
        {
            foreach (var handler in _handlers)
            {
                var handlerType = handler.GetType();
                var handlerExceptionType = handlerType.GetGenericArguments().First();

                if (handlerExceptionType == typeof(TException))
                {
                    return true;
                }
            }

            return false;
        }

        public WacHandlingConfiguration<TParameter, TResult> On<TException>(
            Action<TException, HandlingInput<TParameter>> handler,
            Func<TException, HandlingInput<TParameter>, bool> condition = null)
            where TException : Exception
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            AddHandler((ex, i) =>
            {
                if (TryCastException(ex, out TException castedException))
                {
                    if (condition == null || condition(castedException, i))
                    {
                        handler(castedException, i);
                        return new HandlingResult<TResult>(true);
                    }
                }

                return new HandlingResult<TResult>(false);
            });

            return this;
        }

        public WacHandlingConfiguration<TParameter, TResult> On<TException>(
            Func<TException, HandlingInput<TParameter>, HandlingResult<TResult>> handler,
            Func<TException, HandlingInput<TParameter>, bool> condition = null)
            where TException : Exception
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            AddHandler((ex, i) =>
            {
                if (TryCastException(ex, out TException castedException))
                {
                    if (condition == null || condition(castedException, i))
                    {
                        return handler(castedException, i);
                    }
                }

                return new HandlingResult<TResult>(false);
            });

            return this;
        }

        public WacHandlingConfiguration<TParameter, TResult> On<TException>(
            Action<TException> handler,
            Func<TException, HandlingInput<TParameter>, bool> condition = null)
            where TException : Exception
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            AddHandler((ex, i) =>
            {
                if (TryCastException(ex, out TException castedException))
                {
                    if (condition == null || condition(castedException, i))
                    {
                        handler(castedException);
                        return new HandlingResult<TResult>(true);
                    }
                }

                return new HandlingResult<TResult>(false);
            });

            return this;
        }

        public WacHandlingConfiguration<TParameter, TResult> On<TException>(
            Func<TException, HandlingResult<TResult>> handler,
            Func<TException, HandlingInput<TParameter>, bool> condition = null)
            where TException : Exception
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            AddHandler((ex, i) =>
            {
                if (TryCastException(ex, out TException castedException))
                {
                    if (condition == null || condition(castedException, i))
                    {
                        return handler(castedException);
                    }
                }

                return new HandlingResult<TResult>(false);
            });

            return this;
        }

        public WacHandlingConfiguration<TParameter, TResult> FinalizeWith(
            Action<Exception, HandlingInput<TParameter>, HandlingResult<TResult>> finalizationHandler)
        {
            _finalizationHandler = finalizationHandler ?? throw new ArgumentNullException(nameof(finalizationHandler));
            return this;
        }

        public WacHandlingConfiguration<TParameter, TResult> FinalizeWith(
            Action<Exception, HandlingInput<TParameter>> finalizationHandler)
        {
            if (finalizationHandler == null) throw new ArgumentNullException(nameof(finalizationHandler));

            _finalizationHandler = (e, input, result) =>
            {
                finalizationHandler(e, input);
            };
            return this;
        }

        public WacHandlingConfiguration<TParameter, TResult> FinalizeWith(Action<Exception> finalizationHandler)
        {
            if (finalizationHandler == null) throw new ArgumentNullException(nameof(finalizationHandler));

            _finalizationHandler = (e, input, result) =>
            {
                finalizationHandler(e);
            };
            return this;
        }

        public WacHandlingConfiguration<TParameter, TResult> FinalizeWith(Action finalizationHandler)
        {
            if (finalizationHandler == null) throw new ArgumentNullException(nameof(finalizationHandler));

            _finalizationHandler = (e, input, result) =>
            {
                finalizationHandler();
            };
            return this;
        }

        public HandlingResult<TResult> Catch(
            Exception exception, TParameter parameter = default(TParameter), bool throwIfNotHandled = true)
        {
            if (exception == null) throw new ArgumentNullException(nameof(exception));

            var input = new HandlingInput<TParameter>(exception, parameter);
            HandlingResult<TResult> result = null;
            try
            {
                foreach (var handler in _handlers)
                {
                    result = handler(exception, input);
                    if (result.Handled)
                    {
                        return result;
                    }
                }

                result = WacHandling.Ignore<TResult>();
                if (throwIfNotHandled)
                {
                    throw new NoHandlerMatchedException(exception);
                }

                return result;
            }
            finally
            {
                _finalizationHandler?.Invoke(exception, input, result ?? WacHandling.Ignore<TResult>());
            }
        }
    }

    public class WacHandlingConfiguration : WacHandlingConfiguration<object, object>
    {
        
    }
}
