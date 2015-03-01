using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;


namespace Vereyon.Web
{
    public class FlashMessage
    {

        public static string CookieName { get; set; }

        /// <summary>
        /// Gets / sets the size limit of the cookie in bytes. This is used to ensure the response complies with the cookie specification.
        /// </summary>
        public static int CookieSizeLimit { get; set; }

        static FlashMessage()
        {

            CookieName = "_FlashMessage";
            CookieSizeLimit = 1024 * 3;
        }

        /// <summary>
        /// Queues a flash message for display with the specified message for display as a info notification.
        /// </summary>
        /// <param name="message"></param>
        public static void Queue(string message)
        {
            Queue(message, string.Empty, FlashMessageType.Info, false);
        }

        /// <summary>
        /// Queues a flash message for display with the specified message.
        /// </summary>
        /// <param name="message"></param>
        public static void Queue(string message, FlashMessageType messageType)
        {
            Queue(message, string.Empty, messageType, false);
        }

        /// <summary>
        /// Queues a flash message for display with the specified message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="messageType"></param>
        /// <param name="isHtml"></param>
        public static void Queue(string message, string title, FlashMessageType messageType, bool isHtml)
        {

            FlashMessageModel flashMessage;
            List<FlashMessageModel> messages;
            HttpContextBase context;

            // Retrieve the currently queued cookies.
            context = new HttpContextWrapper(HttpContext.Current);
            messages = GetQueued(context);

            // Append the new message.
            flashMessage = new FlashMessageModel { IsHtml = isHtml, Message = message, Title = title, Type = messageType };
            messages.Add(flashMessage);

            Queue(context, messages);
        }

        /// <summary>
        /// Queues the passed message for display on the next response.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="messages"></param>
        public static void Queue(HttpContextBase context, List<FlashMessageModel> messages)
        {

            // Serialize messages and enforce cookie size limit.
            var serializedMessages = Serialize(messages);
            if (serializedMessages.Length > CookieSizeLimit)
                throw new InvalidOperationException("The flash messages cookie size limit exceeded the limit value.");

            var cookie = new HttpCookie(CookieName, serializedMessages);
            context.Response.SetCookie(cookie);
        }

        /// <summary>
        /// Retrieves the queued messages for the current response.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static List<FlashMessageModel> GetQueued(HttpContextBase context)
        {

            // Attempt to retrieve cookie.
            var cookie = context.Request.Cookies[CookieName];
            if (cookie == null)
                return new List<FlashMessageModel>();

            // Deserialize the message.
            return Deserialize(cookie.Value);
        }

        /// <summary>
        /// Retrieves the flash messages from the incoming request cookie and clears the cookie.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static List<FlashMessageModel> Retrieve(HttpContextBase context)
        {

            // Attempt to retrieve cookie.
            var cookie = context.Request.Cookies[CookieName];
            if (cookie == null)
                return new List<FlashMessageModel>();

            // Deserialize the message.
            var messages = Deserialize(cookie.Value);

            // Clear the cookie by setting it to expired.
            cookie.Value = null;
            cookie.Expires = DateTime.Now.AddDays(-1);
            context.Response.SetCookie(cookie);

            return messages;
        }

        public static List<FlashMessageModel> Deserialize(string serializedMessages)
        {

            // Decrypt messages string using the configured machine encryption.
            using (MemoryStream stream = new MemoryStream(MachineKey.Decode(serializedMessages, MachineKeyProtection.All)))
            {

                var messages = new List<FlashMessageModel>();
                int messageCount;

                // Check if there is any data to read.
                if (stream.Length == 0)
                    return messages;

                using (BinaryReader reader = new BinaryReader(stream))
                {

                    // Read the number of message in the stream and deserialize each message.
                    messageCount = reader.ReadInt32();
                    while (messageCount > 0)
                    {

                        var model = new FlashMessageModel();
                        model.IsHtml = reader.ReadBoolean();
                        model.Message = reader.ReadString();
                        model.Title = reader.ReadString();
                        model.Type = (FlashMessageType)reader.ReadByte();

                        // Store message and decrement message counter.
                        messages.Add(model);
                        messageCount--;
                    }

                    return messages;
                }
            }
        }

        public static string Serialize(List<FlashMessageModel> messages)
        {

            // Decrypt messages.
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {

                    // Write the number of message and serialize each message.
                    writer.Write(messages.Count);
                    foreach (var message in messages)
                    {
                        writer.Write(message.IsHtml);
                        writer.Write(message.Message);
                        writer.Write(message.Title);
                        writer.Write((byte)message.Type);
                    }

                    // Encrypt the message and return data as string.
                    writer.Flush();
                    return MachineKey.Encode(stream.ToArray(), MachineKeyProtection.All);
                }
            }
        }
    }
}
