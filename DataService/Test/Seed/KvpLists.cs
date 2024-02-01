using Models.DbCrud;

namespace DataService.Test.Seed
{
    public class InsertRecordsToTable
    {
        public string TableName { get; set; } = string.Empty;
        public List<KeyValuePair<string, string>[]> VarcharEntries { get; set; } = 
            new List<KeyValuePair<string, string>[]>();
        public List<KeyValuePair<string, int>[]> IntegerEntries { get; set; } = 
            new List<KeyValuePair<string, int>[]>();
    }

    public class KvpLists
    {
        public static readonly InsertRecordsToTable WorldsList = new InsertRecordsToTable
        {
            TableName = "Worlds",
            VarcharEntries = new List<KeyValuePair<string, string>[]>
            {
                new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>(nameof(WorldData.Name), "Dungeons & Dragons"),
                    new KeyValuePair<string, string>(nameof(WorldData.Abbr), "D&D")
                },
                new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>(nameof(WorldData.Name), "Raid Shadow Legends"),
                    new KeyValuePair<string, string>(nameof(WorldData.Abbr), "RSL")
                },
                new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>(nameof(WorldData.Name), "Watcher of Realms"),
                    new KeyValuePair<string, string>(nameof(WorldData.Abbr), "WoR")
                }            }
        };
    }
}
