using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace GraphLib.Domain.Serializers
{
    public class JSonSerializer<T> : ISerializer<T>
        where T : class
    {
        private DataContractJsonSerializer _serializer = new DataContractJsonSerializer(typeof(T));

        public T Deserialize(string json)
        {
            using (var mmemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return (T)_serializer.ReadObject(mmemoryStream);
            }
        }

        public string Serialize(T obj)
        {
            string json = null;

            using (var memoryStream = new MemoryStream())
            {
                _serializer.WriteObject(memoryStream, obj);
                json = Encoding.UTF8.GetString(memoryStream.ToArray());
            }

            return json;
        }
    }
}