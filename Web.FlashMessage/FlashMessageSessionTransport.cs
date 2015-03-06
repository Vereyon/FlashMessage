using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Vereyon.Web
{
    /// <summary>
    /// The FlashMessageTempDataTransport class implements transport for flash messages using the session state.
    /// </summary>
    /// <remarks>
    /// Note that for this to work reliably in a web farm (multiple processes) you need to make sure that a common session state store is used.
    /// </remarks>
    public class FlashMessageSessionTransport : IFlashMessageTransport
    {

        public string KeyName { get; set; }

        public FlashMessageSessionTransport()
        {

            KeyName = "_FlashMessage";
        }

        public void Queue(IList<FlashMessageModel> messages)
        {

            // Serialize the messages.
            var data = FlashMessage.Serialize(messages);

            // Set the data without doing any further securing or transformations.
            var context = new HttpContextWrapper(HttpContext.Current);
            context.Session[KeyName] = data;
        }

        public List<FlashMessageModel> Retrieve()
        {

            // Retrieve the data from the session store, guard for cases where it does not exist.
            var context = new HttpContextWrapper(HttpContext.Current);
            var data = context.Session[KeyName];
            if (data == null)
                return new List<FlashMessageModel>();

            // Clear the data and return.
            context.Session.Remove(KeyName);
            return FlashMessage.Deserialize((byte[])data);
        }

        public List<FlashMessageModel> GetQueued()
        {

            // For this transport Peek() and GetQueued() have equal implementations.
            return Peek();
        }

        public List<FlashMessageModel> Peek()
        {

            // Retrieve the data from the session store, guard for cases where it does not exist.
            var context = new HttpContextWrapper(HttpContext.Current);
            var data = context.Session[KeyName];
            if (data == null)
                return new List<FlashMessageModel>();

            // Deserialize messages and return them.
            return FlashMessage.Deserialize((byte[])data);
        }
    }
}
