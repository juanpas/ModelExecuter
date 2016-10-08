using ModelExecuter.Defs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelExecuter.Model
{
    public class Photo : File
    {
        public Photo()
        {
            TypeId = Convert.ToInt32(Enums.FileType.Photo);
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Alt { get; set; }

    }
}
