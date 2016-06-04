using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BuildingBlock.Defs.Enums;

namespace BuildingBlock.Model
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LocalizationCodeAttribute : System.Attribute
    {
        public string localizationCode;
        public LocalizationCodeAttribute(string localizationCode)
        {
            this.localizationCode = localizationCode;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class LocalizableObjectKeyPropertyAttribute : System.Attribute
    {
        public string localizableObjectKeyProperty;
        public LocalizableObjectKeyPropertyAttribute(string localizableObjectKeyProperty)
        {
            this.localizableObjectKeyProperty = localizableObjectKeyProperty;
        }
    }
}
