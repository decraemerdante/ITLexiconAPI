using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ITLexiconAPI.DataAccessLayer.Models
{
   public class Changelog
    {
        public enum LogItemEnum
        {
            CREATED,
            UPDATED
        }

        public int Id { get; set; }

        [DefaultValue(LogItemEnum.CREATED)]
        public LogItemEnum Type { get; set; }        
        public DateTime Date { get; set; }
        public virtual Article Article { get; set; }
    }
}
