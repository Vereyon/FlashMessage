using System;
using System.Collections.Generic;
using System.Text;

namespace Vereyon.Web
{
    /// <summary>
    /// Defines a contract for serializing flash messages to and from a string based format.
    /// </summary>
    public interface IFlashMessageSerializer
    {

        List<FlashMessageModel> Deserialize(string data);

        string Serialize(IList<FlashMessageModel> messages);
    }
}
