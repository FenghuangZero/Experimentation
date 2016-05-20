using System;
using System.Runtime.Serialization;

namespace YGOShared
{
    [Serializable]
    internal class MonsterZoneFullException : Exception
    {
        public MonsterZoneFullException() : base()
        {
        }

        public MonsterZoneFullException(string message) : base(message)
        {
        }

        public MonsterZoneFullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MonsterZoneFullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    internal class NoMonsterinHandException : Exception
    {
        public NoMonsterinHandException() : base()
        {
        }

        public NoMonsterinHandException(string message) : base(message)
        {
        }

        public NoMonsterinHandException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoMonsterinHandException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}