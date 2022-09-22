using System.Runtime.Serialization;

namespace KP.Core.Exceptions
{
    [Serializable]
    public class KPApplicationException : Exception
    {
        public KPApplicationException() : base()
        {

        }

        public KPApplicationException(string message) : base(message)
        {
        }

        public KPApplicationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected KPApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
