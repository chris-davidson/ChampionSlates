using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DbCrud
{
    public class ChampData
    {
        public int Id { get; set; } = 0;
        public string Title { get; set; } = string.Empty;
        public required string Name { get; set; } = string.Empty;
        public required int FactionId { get; set; }
        public string FactionName { get; set; } = string.Empty;
        public required int AlignmentId { get; set; }
        public string AlignmentName { get; set; } = string.Empty;
    }
}
