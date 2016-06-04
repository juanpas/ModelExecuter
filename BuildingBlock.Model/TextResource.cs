using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Model
{
    public class TextResource
    {
        public TextResource()
        {
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public int RelatedId { get; set; }  
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public string Value { get; set; }
    }
}
