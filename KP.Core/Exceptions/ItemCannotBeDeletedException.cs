using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KP.Core.Exceptions
{
    public class ItemCannotBeDeletedException : KPApplicationException
    {
        public ItemCannotBeDeletedException(string message) : base(message)
        {
        }
        public ItemCannotBeDeletedException() : base()
        {
        }

        public ItemCannotBeDeletedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ItemCannotBeDeletedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
