using Common.Exceptions;

namespace Common.Utils
{
    /// <summary>
    /// Represent a result for operation that returns value or can throw exception
    /// </summary>
    /// <typeparam name="T">Result return type</typeparam>
    public class Result<T>
    {
        private readonly T _value;
        private readonly string _failureMessage;

        public T Value => IsSuccess ? _value : throw new ResultFailureException();
        public bool IsSuccess { get; }
        public string FailureMessage => IsSuccess ? throw new ResultSuccessException() : _failureMessage;

        /// <summary>
        /// Returns new succesful result with given value
        /// </summary>
        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }

        public static implicit operator T(Result<T> result)
        {
            return result.Value;
        }

        public static implicit operator Result<T>(T value)
        {
            return Success(value);
        }

        /// <summary>
        /// Returns new failed result, with specific message
        /// </summary>
        public static Result<T> Failure(string failureMessage)
        {
            return new Result<T>(failureMessage);
        }

        private Result(T value)
        {
            _value = value;
            IsSuccess = true;
        }

        private Result(string message)
        {
            _failureMessage = message;
            IsSuccess = false;
        }
    }

    /// <summary>
    /// Represent a result for action that can throw exception
    /// </summary>
    public class Result
    {
        private readonly string _failureMessage;

        public bool IsSuccess { get; }
        public string FailureMessage => IsSuccess ? throw new ResultSuccessException() : _failureMessage;

        /// <summary>
        /// Returns new succesful result without values
        /// </summary>
        public static Result Success()
        {
            return new Result();
        }

        /// <summary>
        /// Returns new failed result, with specific message
        /// </summary>
        public static Result Failure(string failureMessage)
        {
            return new Result(failureMessage);
        }

        private Result()
        {
            IsSuccess = true;
        }

        private Result(string message)
        {
            _failureMessage = message;
            IsSuccess = false;
        }
    }
}
