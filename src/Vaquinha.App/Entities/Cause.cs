using System;
using Vaquinha.App.Base;

namespace Vaquinha.App.Entities
{
    public class Cause : Entity
    {
        public string Name { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }

        private Cause() { }

        public Cause(Guid id, string name, string city, string state)
        {
            Id = id;
            Name = name;
            City = city;
            State = state;
        }

        public override bool Valid()
        {
            return true;
        }
    }
}