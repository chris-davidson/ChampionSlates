using Models.DbCrud;

namespace DataService.Services.PostgreSql
{
    public class CreateSql
    {
        public string SqlForCreate { get; set; } = string.Empty;
        public string TableName { get; set; } = string.Empty;
    }

    public class Statements
    {
        public static readonly string DbExists = "SELECT EXISTS(SELECT 1 FROM pg_database WHERE datname = '{0}');";
        public static readonly string CreateDatabase = "CREATE DATABASE \"{0}\"";

        private static readonly string CreateTableWorlds = @$"
                CREATE TABLE IF NOT EXISTS Worlds (
                    {nameof(WorldData.Id)} SERIAL PRIMARY KEY,
                    {nameof(WorldData.Name)} VARCHAR,
                    {nameof(WorldData.Abbr)} VARCHAR
                );
            ";
        
        private static readonly string CreateTableAlignments = @$"
                CREATE TABLE IF NOT EXISTS Alignments (
                    {nameof(AlignmentData.Id)} SERIAL PRIMARY KEY,
                    {nameof(AlignmentData.Name)} VARCHAR,
                    {nameof(AlignmentData.Abbr)} VARCHAR,
                    {nameof(AlignmentData.WorldId)} INTEGER
                        REFERENCES Worlds({nameof(WorldData.Id)})
                );
            ";

        private static readonly string CreateTableFactions = @$"
                CREATE TABLE IF NOT EXISTS Factions (
                    {nameof(FactionData.Id)} SERIAL PRIMARY KEY,
                    {nameof(FactionData.Name)} VARCHAR,
                    {nameof(FactionData.Abbr)} VARCHAR,
                    {nameof(FactionData.WorldId)} INTEGER
                        REFERENCES Worlds({nameof(WorldData.Id)})
                );
            ";

        private static readonly string CreateTableCharacters = @$"
                CREATE TABLE IF NOT EXISTS Characters (
                    {nameof(ChampData.Id)} SERIAL PRIMARY KEY,
                    {nameof(ChampData.Title)} VARCHAR,
                    {nameof(ChampData.FirstName)} VARCHAR,
                    {nameof(ChampData.LastName)} VARCHAR,
                    {nameof(ChampData.AlignmentId)} INTEGER 
                        REFERENCES Alignments({nameof(AlignmentData.Id)}),
                    {nameof(ChampData.FactionId)} INTEGER 
                        REFERENCES Factions({nameof(FactionData.Id)}),
                    {nameof(ChampData.WorldId)} INTEGER
                        REFERENCES Worlds({nameof(WorldData.Id)})
                );
            ";

        private static string CreateTableStats = @$"
                CREATE TABLE IF NOT EXISTS StatNames (
                    {nameof(StatData.Id)} SERIAL PRIMARY KEY,
                    {nameof(StatData.Name)} VARCHAR,
                    {nameof(StatData.Abbr)} VARCHAR,
                    {nameof(StatData.WorldId)} INTEGER
                        REFERENCES Worlds({nameof(WorldData.Id)})
                );
            ";

        private static string CreateTableCharStats = @$"
                CREATE TABLE IF NOT EXISTS CharStats (
                    {nameof(CharStatData.CharId)} INTEGER 
                        REFERENCES Characters({nameof(ChampData.Id)}),
                    {nameof(CharStatData.StatId)} INTEGER 
                        REFERENCES StatNames({nameof(StatData.Id)}),
                    {nameof(CharStatData.Value)} VARCHAR
                );
            ";

        private static string CreateTableCharAlign = @$"
                CREATE TABLE IF NOT EXISTS CharAlign (
                    {nameof(CharAlignData.CharId)} INTEGER 
                        REFERENCES Characters({nameof(ChampData.Id)}),
                    {nameof(CharAlignData.AlignmentId)} INTEGER 
                        REFERENCES Alignments({nameof(AlignmentData.Id)})
                );
            ";

        private static string CreateTableCharFaction = @$"
                CREATE TABLE IF NOT EXISTS CharFaction (
                    {nameof(CharFactionData.CharId)} INTEGER 
                        REFERENCES Characters({nameof(ChampData.Id)}),              
                    {nameof(CharFactionData.FactionId)} INTEGER 
                        REFERENCES Factions({nameof(FactionData.Id)})
                );
            ";

        public static readonly CreateSql[] TableList = new CreateSql[] {
            new CreateSql { SqlForCreate = CreateTableWorlds, TableName = "Worlds Table" },
            new CreateSql { SqlForCreate = CreateTableAlignments, TableName = "Alignments Table" },
            new CreateSql { SqlForCreate = CreateTableFactions, TableName = "Factions Table" },
            new CreateSql { SqlForCreate = CreateTableCharacters, TableName = "Characters Table" },
            new CreateSql { SqlForCreate = CreateTableStats, TableName = "StatNames Table" },
            new CreateSql { SqlForCreate = CreateTableCharStats, TableName = "CharStats Table" },
            new CreateSql { SqlForCreate = CreateTableCharAlign, TableName = "CharAlign Table" },
            new CreateSql { SqlForCreate = CreateTableCharFaction, TableName = "CharFaction Table" }
        };

        public static readonly string CreateTablesql = """
                CREATE TABLE IF NOT EXISTS Users (
                    Id SERIAL PRIMARY KEY,
                    Title VARCHAR,
                    FirstName VARCHAR,
                    LastName VARCHAR,
                    Email VARCHAR,
                    Role INTEGER,
                    PasswordHash VARCHAR
                );
            """;

        // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-10.0/constant_interpolated_strings
        //const string myWholeFilePath = $"{myRootPath}/README.md";
    }
}
