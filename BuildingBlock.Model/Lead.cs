using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Model
{
    public class Lead
    {
        public Lead()
        {

        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }


        public int CategoryId { get; set; }
        public LeadCategory Category { get; set; }

        public float Price { get; set; }



    }
}
