using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Xunit;

namespace Vereyon.Web
{
    public class CookieTransportTests : IDisposable
    {

        public CookieTransportTests()
        {

            // Setup fake HTTP context.
            HttpContext.Current = new HttpContext(
                new HttpRequest(null, "http://www.vereyon.com", null),
                new HttpResponse(null));

            // Ensure that we are using the cookie transport.
            FlashMessage.Transport = new FlashMessageCookieTransport();
        }

        [Fact]
        public void SerializeSingleTest()
        {

            FlashMessageModel message = new FlashMessageModel
            {
                IsHtml = true,
                Message = "<p>test content</p> üñï€ode",
                Title = "Title",
                Type = FlashMessageType.Custom
            };

            // Serialize and deserialize.
            FlashMessage.Queue(message);
            var messages = FlashMessage.GetQueued();
            var deserialized = messages[0];

            // Compare the results.
            Assert.Equal(message.Type, deserialized.Type);
            Assert.Equal(message.Title, deserialized.Title);
            Assert.Equal(message.Message, deserialized.Message);
            Assert.Equal(message.IsHtml, message.IsHtml);
        }


        public void Dispose()
        {
            HttpContext.Current = null;
        }
    }
}
