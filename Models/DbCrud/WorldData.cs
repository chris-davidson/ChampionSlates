using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DbCrud
{
    public class WorldData
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Abbr { get; set; } = string.Empty;
    }
}
