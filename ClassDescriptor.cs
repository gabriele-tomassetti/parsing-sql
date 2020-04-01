using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParsingSQL
{    
    public class FieldDescriptor
    {
        public string Name { get; private set; }
        public TypeDescriptor Type { get; private set; }

        public FieldDescriptor(string name, TypeDescriptor type)
        {
            Name = name;
            Type = type;
        }
    }
    
    public class ClassDescriptor
    {
        public string Name { get; set; }
        public List<FieldDescriptor> Fields { get; set;}
    }
}