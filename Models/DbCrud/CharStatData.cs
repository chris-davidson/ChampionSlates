namespace Models.DbCrud
{
    public class CharStatData
    {
        public required int CharId { get; set; }
        public required int StatId { get; set; }
        public required string Value { get; set; } = string.Empty;
    }
}
