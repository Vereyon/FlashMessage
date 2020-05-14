using System;
using System.Collections.Generic;
using System.Text;

namespace Vereyon.Web
{
    public interface IFlashMessageSerializer
    {

        List<FlashMessageModel> Deserialize(string data);

        string Serialize(IList<FlashMessageModel> messages);
    }
}
