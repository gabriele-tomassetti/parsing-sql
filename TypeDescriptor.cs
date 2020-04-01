using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParsingSQL
{    
    public enum BaseType
    {
        Integer,
        Floating,    
        Decimal,           
        Text,
        Binary,
        ArrayCharacters,        
        Year,
        DateTime
    }

    public class TypeDescriptor
    {
        public BaseType Type { get; protected set; }
        public bool Nullability { get; set; } = true;
    }

    public class IntegerTypeDescriptor : TypeDescriptor
    {
        public int Bytes { get; private set; }
        public bool Unsigned { get; private set;  }

        public IntegerTypeDescriptor(int bytes, bool unsigned = false)
        {
            Type = BaseType.Integer;
            Bytes = bytes;
            Unsigned = unsigned;
        }
    }

    public class FloatingTypeDescriptor : TypeDescriptor
    {
        public int Bytes { get; private set; }

        public FloatingTypeDescriptor(int bytes)
        {
            Type = BaseType.Floating;
            Bytes = bytes;            
        }
    }

    public class DecimalTypeDescriptor : TypeDescriptor
    {
        public (int Precision, int Scale) Precision { get; private set; }

        public DecimalTypeDescriptor(int precision, int scale)
        {
            Type = BaseType.Decimal;
            Precision = (precision, scale);
        }
    }

    public class StringTypeDescriptor : TypeDescriptor
    {        
        public StringTypeDescriptor()
        {
            Type = BaseType.Text;
        }
    }

    public class CharArrayTypeDescriptor : TypeDescriptor
    {
        public (int Min, int Max) Limits { get; private set; }

        public CharArrayTypeDescriptor(int min, int max)
        {
            Type = BaseType.ArrayCharacters;
            Limits = (min, max);
        }
    }

    public class BinaryTypeDescriptor : TypeDescriptor
    {
        public BinaryTypeDescriptor()
        {
            Type = BaseType.Binary;
        }
    }

    public class DateTimeTypeDescriptor : TypeDescriptor
    {        
        public DateTimeTypeDescriptor()
        {
            Type = BaseType.DateTime;
        }
    }
}