using System;
using System.Collections.Generic;
using System.Text;
using static ITLexiconAPI.DataAccessLayer.Models.Changelog;

namespace ITLexiconAPI.BusinessLayer.DTO
{
    public class ChangeLogDto
    {       
        public LogItemEnum Type { get; set; }
        public DateTime Date { get; set; }
    }
}
