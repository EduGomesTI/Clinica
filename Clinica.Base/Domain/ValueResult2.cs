namespace Clinica.Base.Domain
{
    public readonly struct ValueResult<TResult>
    {
        public bool Succeeded { get; }

        public TResult? Value { get; }

        public IReadOnlyCollection<ValueErrorDetail>? ErrorDetails { get; }

        private ValueResult(bool succeeded, TResult? value, ValueErrorDetail[]? errorDetails)
        {
            Succeeded = succeeded;
            Value = value;
            ErrorDetails = errorDetails;
        }

        internal static ValueResult<TResult> Create(bool succeeded = true, TResult? value = default(TResult?), params ValueErrorDetail[]? errorDetails)
        {
            return new ValueResult<TResult>(succeeded, value, errorDetails);
        }

        public ValueResult AsValueResult()
        {
            if (!this)
            {
                ValueErrorDetail[] errorDetails = ErrorDetails!.ToArray();
                return Create(true, default(TResult), errorDetails);
            }
            else
            {
                return Create(true, Value);
            }
        }

        public static ValueResult<TResult> Success(TResult value = default(TResult?)!)
        {
            return Create(true, value);
        }

        public static ValueResult<TResult> Failure()
        {
            return Create(false, default(TResult));
        }

        public static ValueResult<TResult> Failure(IEnumerable<ValueErrorDetail>? errorDetails)
        {
            ValueErrorDetail[] errorDetailsArray = errorDetails?.ToArray()!;
            return Create(false, default(TResult), errorDetailsArray);
        }

        public static ValueResult<TResult> Failure(TResult? value, IEnumerable<ValueErrorDetail>? errorDetails)
        {
            return Create(false, value, errorDetails?.ToArray());
        }

        public static ValueResult<TResult> Failure(params string[] descriptions)
        {
            return Failure(descriptions?.Select((string description) => new ValueErrorDetail(description)));
        }

        public static implicit operator bool(ValueResult<TResult> result)
        {
            return result.Succeeded;
        }

        public static implicit operator ValueResult(ValueResult<TResult> value)
        {
            return value.AsValueResult();
        }
    }
}