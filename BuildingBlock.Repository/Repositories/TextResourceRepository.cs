using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ModelExecuter.Repository.Contracts;
using ModelExecuter.Model;
using System.Reflection;

namespace ModelExecuter.Repository
{
    public class TextResourceRepository : EFRepository<TextResource>, ITextResourceRepository
    {
        public TextResourceRepository(DbContext context) : base(context) { }

        public string Get(string type, Language language, int relatedId, string defaultValue = "")
        {
            string result = defaultValue;

            TextResource textResource = DbSet.Where(p => p.Type == type && p.LanguageId == language.Id && p.RelatedId == relatedId).FirstOrDefault();

            if (textResource != null)
                result = textResource.Value;

            return result;
        }


        public T LocalizeObject<T>(T localizableObject, Language language) where T : class
        {
            LocalizableObjectKeyPropertyAttribute localizableObjectKeyPropertyAttribute = typeof(T).GetCustomAttribute<LocalizableObjectKeyPropertyAttribute>(true);

            if (localizableObjectKeyPropertyAttribute == null)
                throw new Exception(string.Format("An object of type {0} is being localized, but no LocalizableObjectKeyPropertyAttribute has been defined for that type", typeof(T).ToString()));

            int localizableObjectKeyValue = -1;

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name == localizableObjectKeyPropertyAttribute.localizableObjectKeyProperty)
                {
                    localizableObjectKeyValue = Convert.ToInt32(property.GetValue(localizableObject));
                    break;
                }
            }

            foreach (PropertyInfo property in properties)
            {
                LocalizationCodeAttribute localizationCodeAttribute = property.GetCustomAttribute<LocalizationCodeAttribute>(true);
                if (localizationCodeAttribute != null)
                {
                    object notLocalizedObject = property.GetValue(localizableObject);
                    if (notLocalizedObject != null)
                    {
                        string localizedValue = Get(localizationCodeAttribute.localizationCode, language, localizableObjectKeyValue, notLocalizedObject.ToString());

                        property.SetValue(localizableObject, localizedValue);
                    }
                }
            }

            return localizableObject;
        }


    }
}
