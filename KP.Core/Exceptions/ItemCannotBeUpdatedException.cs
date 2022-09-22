﻿using System.Runtime.Serialization;

namespace KP.Core.Exceptions
{
    [Serializable]
    public class ItemCannotBeUpdatedException : KPApplicationException
    {
        public ItemCannotBeUpdatedException(string message) : base(message)
        {
        }
        public ItemCannotBeUpdatedException() : base()
        {
        }

        public ItemCannotBeUpdatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ItemCannotBeUpdatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
