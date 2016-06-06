using System;
using System.Runtime.Serialization;

namespace YGOShared
{
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
        
    }
    
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
        
    }
}