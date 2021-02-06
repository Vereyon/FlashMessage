using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vereyon.Web
{
    public class JsonFlashMessageSerializer : IFlashMessageSerializer
    {

        /// <summary>
        /// Deserializes a serialized collection of flash messages.
        /// </summary>
        /// <param name="serializedMessages"></param>
        /// <returns></returns>
        public List<FlashMessageModel> Deserialize(string data)
        {

            if(string.IsNullOrWhiteSpace(data))
                return new List<FlashMessageModel>();

            var messages = JsonSerializer.Deserialize<List<FlashMessageModel>>(data);
            return messages;
        }

        /// <summary>
        /// Serializes the passed list of messages to json format.
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public string Serialize(IList<FlashMessageModel> messages)
        {

            var data = JsonSerializer.Serialize(messages);
            return data;
        }
    }
}
