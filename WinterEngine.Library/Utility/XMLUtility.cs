using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace WinterEngine.Library.Utility
{
    public static class XMLUtility
    {
        public static T DeserializeFile<T>(string filePath)
        {
            T result = default(T);

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        result = (T)serializer.Deserialize(reader);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Cannot deserialize object. See inner exception for more details.", ex);
            }

            return result;
        }

        public static void SerializeObjectToFile<T>(T objectToSerialize, string filePath)
        {
            try
            {
                using (StringWriter stringWriter = new StringWriter())
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    XmlWriterSettings settings = new XmlWriterSettings { Indent = true, Encoding = Encoding.ASCII };
                    XmlWriter writer = XmlWriter.Create(stringWriter, settings);
                    serializer.Serialize(writer, objectToSerialize);
                    string xmlOutput = stringWriter.ToString();

                    File.WriteAllText(filePath, xmlOutput);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error serializing object to file. See inner exception for more details.", ex);
            }

        }
    }
}
