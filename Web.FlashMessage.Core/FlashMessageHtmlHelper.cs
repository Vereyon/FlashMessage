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
        /// Renders any queued flash messages as a Twitter Bootstrap alerta and returns the html code. 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="dismissable">Indicates if the messages should be dismissable</param>
        /// <returns></returns>
        public static IHtmlContent RenderFlashMessages(this IHtmlHelper html, bool dismissable = true)
        {

            // Retrieve queued messages.
            var messages = FlashMessage.Retrieve(html.ViewContext.TempData);
            var output = "";

            foreach (var message in messages)
            {
                output += RenderFlashMessage(message, dismissable);
            }

            return new HtmlString(output);
        }

        /// <summary>
        /// Renders the passed flash message as a Twitter Bootstrap alert component.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="dismissable">Indicates if this message should be dismissable</param>
        /// <returns></returns>
        public static HtmlString RenderFlashMessage(FlashMessageModel message, bool dismissable = true)
        {
            var cssClasses = message.Type.GetCssStyle();
            if (dismissable)
                cssClasses += " alert-dismissible";
            string result = $"<div class=\"{cssClasses}\" role=\"alert\">\r\n";

            if (dismissable)
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
    }
}
