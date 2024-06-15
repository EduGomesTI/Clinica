namespace Clinica.Base.Domain
{
    public readonly struct ValueResult
    {
        public bool Succeeded { get; }

        public IReadOnlyCollection<ValueErrorDetail>? ErrorDetails { get; }

        private ValueResult(bool succeeded, ValueErrorDetail[]? errorDetails)
        {
            Succeeded = succeeded;
            ErrorDetails = (IReadOnlyCollection<ValueErrorDetail>)(object)(errorDetails ?? ValueErrorDetail.EmptyErrorDetails());
        }

        public static ValueResult Success() => new(true, null);

        public static ValueResult Failure(IEnumerable<ValueErrorDetail> errorDetails) => new(false, errorDetails?.ToArray());

        public static ValueResult Failure() => new(false, null);

        public static ValueResult Failure(params string[] descriptions) => Failure(descriptions?.Select((string description) => new ValueErrorDetail(description))!);

        public static implicit operator bool(ValueResult valueResult) => valueResult.Succeeded;
    }
}