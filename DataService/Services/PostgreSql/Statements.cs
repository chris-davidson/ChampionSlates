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

        private static readonly string CreateTableWorlds = @"
                CREATE TABLE IF NOT EXISTS Worlds (
                    Id SERIAL PRIMARY KEY,
                    Name VARCHAR,
                    Abbreviation VARCHAR
                );
            ";
        
        private static readonly string CreateTableAlignments = @"
                CREATE TABLE IF NOT EXISTS Alignments (
                    Id SERIAL PRIMARY KEY,
                    Name VARCHAR,
                    Abbreviation VARCHAR
                );
            ";

        private static readonly string CreateTableFactions = @"
                CREATE TABLE IF NOT EXISTS Factions (
                    Id SERIAL PRIMARY KEY,
                    Name VARCHAR,
                    Abbreviation VARCHAR
                );
            ";

        private static readonly string CreateTableCharacters = @"
                CREATE TABLE IF NOT EXISTS Characters (
                    Id SERIAL PRIMARY KEY,
                    FirstName VARCHAR,
                    LastName VARCHAR,
                    AlignmentId INTEGER REFERENCES Alignments(Id),
                    FactionId INTEGER REFERENCES Factions(Id)
                );
            ";

        private static string CreateTableStats = @"
                CREATE TABLE IF NOT EXISTS StatNames (
                    Id SERIAL PRIMARY KEY,
                    Name VARCHAR,
                    Abbreviation VARCHAR
                );
            ";

        private static string CreateTableCharStats = @"
                CREATE TABLE IF NOT EXISTS CharStats (
                    CharId INTEGER REFERENCES Characters(Id),
                    StatId INTEGER REFERENCES StatNames(Id),
                    Value VARCHAR
                );
            ";

        private static string CreateTableCharAlign = @"
                CREATE TABLE IF NOT EXISTS CharAlign (
                    CharId INTEGER REFERENCES Characters(Id),
                    AlignId INTEGER REFERENCES Alignments(Id)
                );
            ";

        private static string CreateTableCharFaction = @"
                CREATE TABLE IF NOT EXISTS CharFaction (
                    CharId INTEGER REFERENCES Characters(Id),
                    FactionId INTEGER REFERENCES Factions(Id)
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
