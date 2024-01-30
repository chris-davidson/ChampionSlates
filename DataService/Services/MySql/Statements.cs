namespace DataService.Services.MySql
{
    public class CreateSql
    {
        public string SqlForCreate { get; set; } = string.Empty;
        public string TableName { get; set; } = string.Empty;
    }

    public class Statements
    {
        public static readonly string CreateDatabase = "CREATE DATABASE IF NOT EXISTS {0}";

        private static readonly string CreateTableWorlds = @"
                CREATE TABLE IF NOT EXISTS Worlds (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    Name VARCHAR(255),
                    Abbreviation VARCHAR(50)
                );
            ";

        private static readonly string CreateTableAlignments = @"
                CREATE TABLE IF NOT EXISTS Alignments (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    Name VARCHAR(255) NOT NULL,
                    Abbreviation VARCHAR(50) NOT NULL
                );
            ";

        private static readonly string CreateTableFactions = @"
                CREATE TABLE IF NOT EXISTS Factions (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    Name VARCHAR(255) NOT NULL,
                    Abbreviation VARCHAR(50) NOT NULL
                );
            ";

        private static readonly string CreateTableCharacters = @"
                CREATE TABLE IF NOT EXISTS Characters (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    FirstName VARCHAR(255) NOT NULL,
                    LastName VARCHAR(255),
                    AlignmentId INT,
                    FactionId INT,
                    FOREIGN KEY (AlignmentId) REFERENCES Alignments(Id),
                    FOREIGN KEY (FactionId) REFERENCES Factions(Id)
                );
            ";

        private static string CreateTableStats = @"
                CREATE TABLE IF NOT EXISTS StatNames (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    Name VARCHAR(255) NOT NULL,
                    Abbreviation VARCHAR(50) NOT NULL
                );
            ";

        private static string CreateTableCharStats = @"
                CREATE TABLE IF NOT EXISTS CharStats (
                    CharId INT,
                    StatId INT,
                    Value VARCHAR(50),
                    FOREIGN KEY (CharId) REFERENCES Characters(Id),
                    FOREIGN KEY (StatId) REFERENCES StatNames(Id)
                );
            ";

        private static string CreateTableCharAlign = @"
                CREATE TABLE IF NOT EXISTS CharAlign (
                    CharId INT,
                    AlignId INT,
                    FOREIGN KEY (CharId) REFERENCES Characters(Id),
                    FOREIGN KEY (CharId) REFERENCES Alignments(Id)
                );
            ";

        private static string CreateTableCharFaction = @"
                CREATE TABLE IF NOT EXISTS CharFaction (
                    CharId INT,
                    FactionId INT,
                    FOREIGN KEY (CharId) REFERENCES Characters(Id),
                    FOREIGN KEY (FactionId) REFERENCES Factions(Id)
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
    }
}
