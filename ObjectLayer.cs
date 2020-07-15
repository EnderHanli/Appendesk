using System.Runtime.Serialization;

namespace Appendesk
{
    public abstract class ObjectLayer
    {
        [DataMember]
        public Response Response { get; set; }
    }
}
