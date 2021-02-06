using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Vereyon.Web
{
    public abstract class SerializationTestsBase
    {

        protected IFlashMessageSerializer Serializer { get;set; }

        [Fact]
        public void SerializeEmptyTest()
        {

            List<FlashMessageModel> messages = new List<FlashMessageModel>();
            var data = Serializer.Serialize(messages);
            messages = Serializer.Deserialize(data);

            Assert.Empty(messages);
        }

        /// <summary>
        /// When no flash messages are queued, the deserializer may be passed an empty byte array and it must handle this correctly.
        /// </summary>
        [Fact]
        public void DeserializeNoDataTest()
        {

            var data = string.Empty;
            var messages = Serializer.Deserialize(data);

            Assert.Empty(messages);
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
                Type = FlashMessageType.Confirmation
            };

            // Serialize and deserialize.
            messages.Add(message);
            var data = Serializer.Serialize(messages);
            messages = Serializer.Deserialize(data);
            var deserialized = messages[0];

            // Compare the results.
            Assert.Equal(message.Type, deserialized.Type);
            Assert.Equal(message.Title, deserialized.Title);
            Assert.Equal(message.Message, deserialized.Message);
            Assert.Equal(message.IsHtml, message.IsHtml);
        }
    }
}
