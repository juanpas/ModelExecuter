using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelExecuter.Model
{
    public class ParameterCategory
    {
        public ParameterCategory()
        {
        }

        public int Id { get; set; }
        public String Code { get; set; }
        public int? ParentCategoryId { get; set; }
        public ParameterCategory ParentCategory { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

        public ICollection<Parameter> Parameters { get; set; }

    }
}
