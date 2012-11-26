using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;

namespace AspxCommerce.Core.Controller
{
   public static class JSONHelper
    {
        public static T Deserialise<T>(string json)
       {
           using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
           {
               var serialiser = new DataContractJsonSerializer(typeof(T));
               return (T)serialiser.ReadObject(ms);
           }
       }

       public static string Serialize<T>(T obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                return Encoding.Default.GetString(ms.ToArray());
            }
        }
    }
}
