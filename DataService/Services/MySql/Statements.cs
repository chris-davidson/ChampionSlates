using Models.DbCrud;

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

        private static readonly string CreateTableWorlds = @$"
                CREATE TABLE IF NOT EXISTS Worlds (
                    {nameof(WorldData.Id)} INT AUTO_INCREMENT PRIMARY KEY,
                    {nameof(WorldData.Name)} VARCHAR(255),
                    {nameof(WorldData.Abbr)} VARCHAR(50)
                );
            ";

        private static readonly string CreateTableAlignments = @$"
                CREATE TABLE IF NOT EXISTS Alignments (
                    {nameof(AlignmentData.Id)} INT AUTO_INCREMENT PRIMARY KEY,
                    {nameof(AlignmentData.Name)} VARCHAR(255) NOT NULL,
                    {nameof(AlignmentData.Abbr)} VARCHAR(50) NOT NULL,
                    {nameof(AlignmentData.WorldId)} INT,
                    FOREIGN KEY ({nameof(AlignmentData.WorldId)})
                        REFERENCES Worlds({nameof(WorldData.Id)})
                );
            ";

        private static readonly string CreateTableFactions = @$"
                CREATE TABLE IF NOT EXISTS Factions (
                    {nameof(FactionData.Id)} INT AUTO_INCREMENT PRIMARY KEY,
                    {nameof(FactionData.Name)} VARCHAR(255) NOT NULL,
                    {nameof(FactionData.Abbr)} VARCHAR(50) NOT NULL,
                    {nameof(FactionData.WorldId)} INT,
                    FOREIGN KEY ({nameof(FactionData.WorldId)})
                        REFERENCES Worlds({nameof(WorldData.Id)})
                );
            ";

        private static readonly string CreateTableCharacters = @$"
                CREATE TABLE IF NOT EXISTS Characters (
                    {nameof(ChampData.Id)} INT AUTO_INCREMENT PRIMARY KEY,
                    {nameof(ChampData.Title)} VARCHAR(255),
                    {nameof(ChampData.FirstName)} VARCHAR(255) NOT NULL,
                    {nameof(ChampData.LastName)} VARCHAR(255),
                    {nameof(ChampData.AlignmentId)} INT,
                    {nameof(ChampData.FactionId)} INT,
                    {nameof(ChampData.WorldId)} INT,
                    FOREIGN KEY ({nameof(ChampData.AlignmentId)}) 
                        REFERENCES Alignments({nameof(AlignmentData.Id)}),
                    FOREIGN KEY ({nameof(ChampData.FactionId)}) 
                        REFERENCES Factions({nameof(FactionData.Id)}),
                    FOREIGN KEY ({nameof(ChampData.WorldId)})
                        REFERENCES Worlds({nameof(WorldData.Id)})
                );
            ";

        private static string CreateTableStats = @$"
                CREATE TABLE IF NOT EXISTS StatNames (
                    {nameof(StatData.Id)} INT AUTO_INCREMENT PRIMARY KEY,
                    {nameof(StatData.Name)} VARCHAR(255) NOT NULL,
                    {nameof(StatData.Abbr)} VARCHAR(50) NOT NULL,
                    {nameof(StatData.WorldId)} INT,
                    FOREIGN KEY ({nameof(StatData.WorldId)})
                        REFERENCES Worlds({nameof(WorldData.Id)})
                );
            ";

        private static string CreateTableCharStats = @$"
                CREATE TABLE IF NOT EXISTS CharStats (
                    {nameof(CharStatData.CharId)} INT,
                    {nameof(CharStatData.StatId)} INT,
                    {nameof(CharStatData.Value)} VARCHAR(50),
                    FOREIGN KEY ({nameof(CharStatData.CharId)}) 
                        REFERENCES Characters({nameof(ChampData.Id)}),
                    FOREIGN KEY ({nameof(CharStatData.StatId)}) 
                        REFERENCES StatNames({nameof(StatData.Id)})
                );
            ";

        private static string CreateTableCharAlign = @$"
                CREATE TABLE IF NOT EXISTS CharAlign (
                    {nameof(CharAlignData.CharId)} INT,
                    {nameof(CharAlignData.AlignmentId)} INT,
                    FOREIGN KEY ({nameof(CharAlignData.CharId)}) 
                        REFERENCES Characters({nameof(ChampData.Id)}),
                    FOREIGN KEY ({nameof(CharAlignData.AlignmentId)}) 
                        REFERENCES Alignments({nameof(AlignmentData.Id)})
                );
            ";

        private static string CreateTableCharFaction = @$"
                CREATE TABLE IF NOT EXISTS CharFaction (
                    {nameof(CharFactionData.CharId)} INT,
                    {nameof(CharFactionData.FactionId)} INT,
                    FOREIGN KEY ({nameof(CharFactionData.CharId)}) 
                        REFERENCES Characters({nameof(ChampData.Id)}),
                    FOREIGN KEY ({nameof(CharFactionData.FactionId)}) 
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
    }
}
