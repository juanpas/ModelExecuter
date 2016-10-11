using ModelExecuter.IdentityModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelExecuter.Model
{
    public class Model
    {
        public Model()
        {
            this.Input = new Collection<ViewInfo>();
            this.Output = new Collection<ViewInfo>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ViewInfo> Input { get; set; }
        public ICollection<ViewInfo> Output { get; set; }

    }

    public class ViewInfo
    {
        public ViewInfo()
        {
        }

        public int Order { get; set; }
        public View View { get; set; }
    }
}
