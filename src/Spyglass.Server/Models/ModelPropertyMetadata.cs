namespace Spyglass.Server.Models
{
    public class ModelPropertyMetadata
    {
        public bool ConvertEmptyStringToNull { get; set; }

        public string Description { get; set; }

        public string DisplayFormatString { get; set; }

        public string DisplayName { get; set; }

        public string EditFormatString { get; set; }

        public bool HasNonDefaultEditFormat { get; set; }

        public bool HideSurroundingHtml { get; set; }

        public bool HtmlEncode { get; set; }

        public bool IsCollectionType { get; set; }

        public bool IsComplexType { get; set; }

        public bool IsEnum { get; set; }

        public bool IsEnumerableType { get; set; }

        public bool IsFlagsEnum { get; set; }

        public bool IsNullableValueType { get; set; }

        public bool IsReadOnly { get; set; }

        public bool IsReferenceOrNullableType { get; set; }

        public bool IsRequired { get; set; }

        public string ModelType { get; set; }

        public string NullDisplayText { get; set; }

        public int Order { get; set; }

        public string Placeholder { get; set; }

        public string PropertyName { get; set; }

        public bool ShowForDisplay { get; set; }

        public bool ShowForEdit { get; set; }

        public string TemplateHint { get; set; }

    }
}
