﻿using System;
using System.Linq;

namespace Ploeh.Albedo
{
    public class TypeElement : IReflectionElement
    {
        private readonly Type type;

        public TypeElement(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            this.type = type;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "The name follows a consistent pattern with the other reflection elements, and most users will understand the distiction between 'GetType()' and 'Type'.")]
        public Type Type
        {
            get { return this.type; }
        }

        public IReflectionVisitor<T> Accept<T>(IReflectionVisitor<T> visitor)
        {
            if (visitor == null) throw new ArgumentNullException("visitor");
            return visitor.Visit(this);
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Type == ((TypeElement)obj).Type;
        }

        public override int GetHashCode()
        {
            return this.type.GetHashCode();
        }
    }
}