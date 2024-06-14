namespace BuildingBlocks.Behaviors
{
    public static class ValidationBehaviorConstants
    {
        public const string FIELD_REQUIRED = "Field is required";
        public const string FIELD_NOT_NULL = "Field cannot be null";
        public const string FIELD_GREATER_ZERO = "Field must be greater than zero";
        public static string FieldLength(int min, int max)
        {
            return string.Format(format: "Field must be between {0} and {1} characters", min, max);
        }
    }
}
