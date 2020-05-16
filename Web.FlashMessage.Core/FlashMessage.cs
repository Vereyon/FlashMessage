using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Vereyon.Web
{
    public class FlashMessage : IFlashMessage
    {


        public ITempDataDictionary _tempData;

        public ITempDataDictionary TempData
        {
            protected set { _tempData = value; }
            get
            {
                if (_tempData == null)
                    _tempData = _tempDataFactory.GetTempData(_httpContextAccessor.HttpContext);

                return _tempData;
            }
        }

        private ITempDataDictionaryFactory _tempDataFactory;

        private IHttpContextAccessor _httpContextAccessor;

        private IFlashMessageSerializer _messageSerializer;

        public FlashMessage(ITempDataDictionaryFactory tempDataFactory, IHttpContextAccessor httpContextAccessor, 
            IFlashMessageSerializer messageSerializer)
        {

            _tempDataFactory = tempDataFactory;
            _httpContextAccessor = httpContextAccessor;
            _messageSerializer = messageSerializer;
        }

        public static string KeyName { get; set; } = "_FlashMessage";

        


        /// <summary>
        /// Queues the passed flash message for display.
        /// </summary>
        /// <param name="message"></param>
        public void Queue(FlashMessageModel message)
        {

            List<FlashMessageModel> messages;

            // Retrieve the currently queued cookies.
            messages = Queued(TempData);

            // Append the new message.
            messages.Add(message);

            // Store the messages.
            Store(messages);
        }

        /// <summary>
        /// Queues the passed flash messages for display, replacing any queued messages.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="messages"></param>
        private void Store(IList<FlashMessageModel> messages)
        {

            // Serialize the messages.
            var data = _messageSerializer.Serialize(messages);

            // Set the data without doing any further securing or transformations.
            TempData[KeyName] = data;
        }

        /// <summary>
        /// Retrieves the queued flash messages for display and clears them.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public List<FlashMessageModel> Retrieve(ITempDataDictionary dictionary)
        {

            // Retrieve the data from the session store, guard for cases where it does not exist.
            var data = dictionary[KeyName];
            if (data == null)
                return new List<FlashMessageModel>();

            // Clear the data and return.
            dictionary.Remove(KeyName);
            return _messageSerializer.Deserialize((string)data);
        }

        /// <summary>
        /// Retrieves the queued messages for the current response without clearing them.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public List<FlashMessageModel> Queued(ITempDataDictionary dictionary)
        {

            // For this transport Peek() and Queued() have equal implementations.
            return Peek(dictionary);
        }

        public List<FlashMessageModel> Peek(ITempDataDictionary dictionary)
        {

            // Retrieve the data from the session store, guard for cases where it does not exist.
            var data = dictionary.Peek(KeyName);
            if (data == null)
                return new List<FlashMessageModel>();

            // Deserialize messages and return them.
            return _messageSerializer.Deserialize((string)data);
        }
    }
}
