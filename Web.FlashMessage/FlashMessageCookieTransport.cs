using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vereyon.Web
{
    public class FlashMessageCookieTransport : IFlashMessageTransport
    {

        public string CookieName { get; set; }

        /// <summary>
        /// Gets / sets the size limit of the cookie in bytes. This is used to ensure the response complies with the cookie specification.
        /// </summary>
        public int CookieSizeLimit { get; set; }
    }
}
