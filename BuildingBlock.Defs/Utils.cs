using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModelExecuter.Defs
{
    public static class Utils
    {

        public static void ShallowConvert<T, U>(this T parent, U child)
        {
            foreach (PropertyInfo property in parent.GetType().GetProperties())
            {
                if (property.CanWrite)
                {
                    property.SetValue(child, property.GetValue(parent, null), null);
                }
            }
        }

        public static Stream DownloadRemoteImageFile(string uri)
        {
            Stream result = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Check that the remote file was found. The ContentType
                // check is performed since a request for a non-existent
                // image file might be redirected to a 404-page, which would
                // yield the StatusCode "OK", even though the image was not
                // found.
                if ((response.StatusCode == HttpStatusCode.OK ||
                    response.StatusCode == HttpStatusCode.Moved ||
                    response.StatusCode == HttpStatusCode.Redirect) &&
                    response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
                {
                    // if the remote file was found, download it
                    result = response.GetResponseStream();
                }
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        public static T ReadAppSetting<T>(string key, T defaultValue)
        {
            string value = ConfigurationManager.AppSettings[key];

            if(value == null)
                return defaultValue;

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static string BuildCodeFromName(string name)
        {
            string result = name.ToLower().Trim();

            result = RemoveSpecialCharacters(result);

            return result
                .Replace("  ", " ")
                .Replace(" ", "_")
                ;
        }

        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '_' || c == ' ')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string FirstCharToUpper(string input)
        {
            string result = input;

            if (result.Length > 0)
            {
                result = input.First().ToString().ToUpper() + input.Substring(1);
            }

            return result;
        }


    }
}
