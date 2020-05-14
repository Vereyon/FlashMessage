using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Vereyon.Web
{
    public class JsonContractFlashMessageSerializer : IFlashMessageSerializer
    {

        /// <summary>
        /// Deserializes a serialized collection of flash messages.
        /// </summary>
        /// <param name="serializedMessages"></param>
        /// <returns></returns>
        public List<FlashMessageModel> Deserialize(string data)
        {

            var serializer = new DataContractJsonSerializer(typeof(List<FlashMessageModel>));
            var stringData = Encoding.UTF8.GetBytes(data);
            using (var stream = new MemoryStream(stringData))
            {
                var messages = serializer.ReadObject(stream) as List<FlashMessageModel>;
                return messages;
            }
        }

        /// <summary>
        /// Serializes the passed list of messages to json format.
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public string Serialize(IList<FlashMessageModel> messages)
        {

            var serializer = new DataContractJsonSerializer(typeof(List<FlashMessageModel>));
            using (var stream = new MemoryStream(stringData))
            {
                var messages = serializer.WriteObject(stream, messages);
                var data = stream.ToArray();
                var json = Encoding.UTF8.GetString(data, 0, data.Length);
                return json;
            }
        }
    }
}
