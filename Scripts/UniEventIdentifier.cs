using System;

/// <summary>
/// Custom Identify Atrributes of UniEvent
/// </summary>
namespace UniEvent.Attributes
{
    /// <summary>
    /// Used by the injection container to determine if a property or field should be injected.
    /// </summary>
    public class UniEventIdentifier : Attribute
    {
        public UniEventIdentifier(string identifier)
        {
            Identifier = identifier;
        }

        public string Identifier { get; set; }

        public UniEventIdentifier()
        {
        }
    }

    public class ComponentId : Attribute
    {
        public ComponentId(int identifier)
        {
            Identifier = identifier;
        }

        public int Identifier { get; set; }

        public ComponentId()
        {
        }
    }

    public class EventId : Attribute
    {
        public EventId(int identifier)
        {
            Identifier = identifier;
        }

        public int Identifier { get; set; }

        public EventId()
        {
        }
    }
}