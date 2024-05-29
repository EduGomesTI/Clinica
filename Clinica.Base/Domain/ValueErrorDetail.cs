namespace Clinica.Base.Domain
{
    public readonly struct ValueErrorDetail
    {
        public string Tag { get; }

        public string? Description { get; }

        public ValueErrorDetail(string description, string? tag = null)
        {
            Tag = (string.IsNullOrWhiteSpace(tag) ? "__general__" : tag);
            Description = description;
        }

        public static ValueErrorDetail[] EmptyErrorDetails()
        {
            return [];
        }

        public static implicit operator ValueErrorDetail(string description)
        {
            return new ValueErrorDetail(description);
        }
    }
}