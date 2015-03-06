using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Vereyon.Web
{
    /// <summary>
    /// Transports flash messages using a cookie. Flash messages are signed using the configured machine key in order to prevent tampering with them.
    /// </summary>
    public class FlashMessageCookieTransport : IFlashMessageTransport
    {

        public string CookieName { get; set; }

        /// <summary>
        /// Gets / sets the size limit of the cookie in bytes. This is used to ensure the response complies with the cookie specification. Set to zero to disable.
        /// </summary>
        public int CookieSizeLimit { get; set; }

        public FlashMessageCookieTransport()
        {

            CookieName = "_FlashMessage";
            CookieSizeLimit = 1024 * 3;
        }

        public bool Secure
        {
            get { return true; }
        }

        public void Queue(IList<FlashMessageModel> messages)
        {

            // Serialize and digitally sign the data.
            // Encoding/signing is done using the configured encryption and key in the machineKey element.
            // TODO: Replace with MachineKey.Protect for .Net Framework >= 4.5
            var serializedMessages = FlashMessage.Serialize(messages);
            var data = MachineKey.Encode(serializedMessages, MachineKeyProtection.All);

            // Serialize messages and enforce cookie size limit.
            if (data.Length > CookieSizeLimit && CookieSizeLimit > 0)
                throw new InvalidOperationException("The flash messages cookie size limit exceeded the limit value. Queue less messages.");

            // Set the cookie.
            var context = new HttpContextWrapper(HttpContext.Current);
            var cookie = new HttpCookie(CookieName, data);
            context.Response.SetCookie(cookie);
        }

        public List<FlashMessageModel> GetQueued()
        {

            // Attempt to retrieve cookie.
            var context = new HttpContextWrapper(HttpContext.Current);
            var cookie = context.Response.Cookies[CookieName];

            // If the cookie is non existent, return an empty message list.
            // If the cookie value is null, also return an empty list, as MachineKey.Decode does not like empty strings.
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
                return new List<FlashMessageModel>();

            // Decode and deserialize the data.
            var serializedMessages = MachineKey.Decode(cookie.Value, MachineKeyProtection.All);
            return FlashMessage.Deserialize(serializedMessages);
        }

        public List<FlashMessageModel> Retrieve()
        {

            // Attempt to retrieve cookie. If the cookie is non existent, return an empty message list.
            var context = new HttpContextWrapper(HttpContext.Current);
            var cookie = context.Request.Cookies[CookieName];
            if (cookie == null)
                return new List<FlashMessageModel>();

            var data = cookie.Value;

            // Clear the cookie by setting it to expired.
            cookie.Value = null;
            cookie.Expires = DateTime.Now.AddDays(-1);
            context.Response.SetCookie(cookie);

            // Decode and deserialize the data.
            var serializedMessages = MachineKey.Decode(data, MachineKeyProtection.All);
            return FlashMessage.Deserialize(serializedMessages);
        }

        public List<FlashMessageModel> Peek()
        {

            // Attempt to retrieve cookie. If the cookie is non existent, return an empty message list.
            var context = new HttpContextWrapper(HttpContext.Current);
            var cookie = context.Request.Cookies[CookieName];
            if (cookie == null)
                return new List<FlashMessageModel>();

            // Decode and deserialize the data.
            var serializedMessages = MachineKey.Decode(cookie.Value, MachineKeyProtection.All);
            return FlashMessage.Deserialize(serializedMessages);
        }
    }
}
