using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Vereyon.Web
{
    public class SerializationTests
    {

        [Fact]
        public void SerializeEmptyTest()
        {

            List<FlashMessageModel> messages = new List<FlashMessageModel>();
            var data = FlashMessage.Serialize(messages);
            messages = FlashMessage.Deserialize(data);

            Assert.Equal(0, messages.Count);
        }

        /// <summary>
        /// When no flash messages are queued, the deserializer may be passed an empty byte array and it must handle this correctly.
        /// </summary>
        [Fact]
        public void DeserializeNoDataTest()
        {

            var data = new byte[] { };
            var messages = FlashMessage.Deserialize(data);

            Assert.Equal(0, messages.Count);
        }

        [Fact]
        public void SerializeSingleTest()
        {

            List<FlashMessageModel> messages = new List<FlashMessageModel>();
            FlashMessageModel message = new FlashMessageModel
            {
                IsHtml = true,
                Message = "<p>test content</p> üñï€ode",
                Title = "Title",
                Type = FlashMessageType.Custom
            };

            // Serialize and deserialize.
            messages.Add(message);
            var data = FlashMessage.Serialize(messages);
            messages = FlashMessage.Deserialize(data);
            var deserialized = messages[0];

            // Compare the results.
            Assert.Equal(message.Type, deserialized.Type);
            Assert.Equal(message.Title, deserialized.Title);
            Assert.Equal(message.Message, deserialized.Message);
            Assert.Equal(message.IsHtml, message.IsHtml);
        }
    }
}
