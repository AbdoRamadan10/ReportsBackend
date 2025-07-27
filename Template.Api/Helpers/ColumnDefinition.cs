namespace ReportsBackend.Api.Helpers
{
    public class ColumnDefinition<T>
    {
        public string Header { get; set; }
        public Func<T, string> ValueSelector { get; set; }
    }
}
