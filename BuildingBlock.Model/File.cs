using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelExecuter.Model
{
    public class File
    {
        public File()
        {
            DateAdded = DateTime.Now;
        }
        public int Id { get; set; }
        public string FileName { get; set; }
        public int TypeId { get; set; }
        public int SubTypeId { get; set; }
        public int RelatedId { get; set; }
        public int Index { get; set; }

        public DateTime DateAdded { get; set; }
        public Guid? AddedByUserId { get; set; }


    }
}
