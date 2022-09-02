using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KP.Core.Exceptions
{
    [Serializable]
    public class KFApplicationException : Exception
    {
        public KFApplicationException() : base()
        {

        }

        public KFApplicationException(string message) : base(message)
        {
        }

        public KFApplicationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected KFApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
