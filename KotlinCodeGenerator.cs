using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsingSQL
{    
    public class KotlinCodeGenerator : ICodeGenerator
    {
        public string ToSourceCode(string idNamespace, List<ClassDescriptor> classes)
        {
            StringBuilder sourceCode = new StringBuilder();

            // declaring package
            if(!String.IsNullOrEmpty(idNamespace))
                sourceCode.AppendLine($"package {idNamespace}");  

            // adding imports
            sourceCode.AppendLine();
            sourceCode.AppendLine("import java.time.LocalDateTime");
            sourceCode.AppendLine("import java.math.BigDecimal");            
            sourceCode.AppendLine();
            
            foreach(var c in classes)
            {
                sourceCode.Append($"data class {c.Name}(");
                
                foreach(var f in c.Fields)
                {
                    switch(f.Type.Type)
                    {
                        case BaseType.Integer:
                            sourceCode.Append($"var {f.Name}: {GenerateInt(f.Type as IntegerTypeDescriptor)}");
                            break;
                        case BaseType.Text:
                            sourceCode.Append($"var {f.Name}: String");
                            break;
                        case BaseType.ArrayCharacters:
                            sourceCode.Append($"var {f.Name}: {GenerateCharArray(f.Type as CharArrayTypeDescriptor)}");
                            break;
                        case BaseType.DateTime:
                            sourceCode.Append($"var {f.Name}: LocalDateTime");
                            break;                            
                        case BaseType.Decimal:                        
                            sourceCode.Append($"var {f.Name}: BigDecimal");
                            break;
                        case BaseType.Binary:
                            sourceCode.Append($"var {f.Name}: ByteArray");
                            break;
                    }                    
                    
                    if(f != c.Fields.Last())
                        sourceCode.Append(", ");
                }
                
                sourceCode.AppendLine(")");
                sourceCode.AppendLine("");
            }

            return sourceCode.ToString();
        }

        private string GenerateCharArray(CharArrayTypeDescriptor descriptor)
        {            
            // is a fixed size array
            if(descriptor.Limits.Min == descriptor.Limits.Max)            
                return $"CharArray = CharArray({descriptor.Limits.Max}, {{x -> ' '}})";
            // we really have no limits on variable size arrays
            else
                return $"List<Char>";
        }
        
        private string GenerateInt(IntegerTypeDescriptor descriptor)
        {
            StringBuilder intType = new StringBuilder();            

            switch(descriptor.Bytes)
            {
                case 2:
                    intType.Append("Short");
                    break;
                case 4:
                    intType.Append("Int");
                    break;
                case 8:
                    intType.Append("Long");
                    break;
            }

            // we ignore unsigned, because it is not well supported in C#
            if(descriptor.Nullability == true)
                intType.Append("?");
           
            return intType.ToString();
        }
    }
}