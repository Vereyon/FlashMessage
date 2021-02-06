using System.Collections.Generic;
using Xunit;
using Moq;
using System.Runtime.Serialization;
using Vereyon.Web;

namespace Vereyon.Web
{
    public class JsonContractFlashMessageSerializerTests : SerializationTestsBase
    {

        public JsonContractFlashMessageSerializerTests()
        {

            Serializer = new JsonContractFlashMessageSerializer();
        }
    }
}
