using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Html;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vereyon.Web
{
    /// <summary>
    /// Twitter Bootstrap based HTML renderer for Flash Messages rendering the messages as alerts.
    /// </summary>
    public static class FlashMessageHtmlHelper
    {

        /// <summary>
        /// Renders any queued flash messages to and returns the html code. 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static IHtmlContent RenderFlashMessages(this IHtmlHelper html)
        {

            // Retrieve queued messages.
            var messages = FlashMessage.Retrieve(html.TempData);
            var output = "";

            foreach (var message in messages)
            {
                output += RenderFlashMessage(message);
            }

            return new HtmlString(output);
        }

        /// <summary>
        /// Renders the passed flash message as a Twitter Bootstrap alert component.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static HtmlString RenderFlashMessage(FlashMessageModel message)
        {
            string result = "<div class=\"" + message.Type.GetCssStyle() + " alert-dismissible\" role=\"alert\">\r\n";

            result += "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>\r\n";
            if (!string.IsNullOrWhiteSpace(message.Title))
                result += "<strong>" + WebUtility.HtmlEncode(message.Title) + "</strong> ";

            if (message.IsHtml)
                result += message.Message;
            else
                result += WebUtility.HtmlEncode(message.Message);

            result += "</div>";
            return new HtmlString(result);
        }

        /// <summary>
        /// Returns the Twitter bootstrap css style for the passed message type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetCssStyle(this FlashMessageType type)
        {
            switch (type)
            {
                case FlashMessageType.Danger:
                    return "alert alert-danger";
                default:
                case FlashMessageType.Info:
                    return "alert alert-info";
                case FlashMessageType.Warning:
                    return "alert alert-warning";
                case FlashMessageType.Confirmation:
                    return "alert alert-success";
            }
        }

        /*
         * http://forums.asp.net/t/1752236.aspx/1?ASP+NET+MVC+Multiple+Forms+Validation+Summaries
        public static void RenderValidationFlashMessage(this HtmlHelper html, string message)
        {
            if(html.ViewData.ModelState.IsValid
        }*/
    }

    
}
