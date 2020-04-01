using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsingSQL
{    
    public class CSharpCodeGenerator : ICodeGenerator
    {
        public string ToSourceCode(string idNamespace, List<ClassDescriptor> classes)
        {
            StringBuilder sourceCode = new StringBuilder();

            // opening namespace
            sourceCode.AppendLine($"namespace {idNamespace}");
            sourceCode.AppendLine("{");
            
            foreach(var c in classes)
            {
                sourceCode.AppendLine($"\tpublic class {c.Name}");
                sourceCode.AppendLine("\t{");
                
                foreach(var f in c.Fields)
                {
                    switch(f.Type.Type)
                    {
                        case BaseType.Integer:
                            sourceCode.AppendLine($"\t\tpublic {GenerateInt(f.Type as IntegerTypeDescriptor)} {f.Name} {{ get; set; }}");
                            break;
                        case BaseType.Text:
                            sourceCode.AppendLine($"\t\tpublic string {f.Name} {{ get; set; }}");
                            break;
                        case BaseType.ArrayCharacters:
                            sourceCode.AppendLine($"\t\tpublic {GenerateCharArray(f.Name, f.Type as CharArrayTypeDescriptor)}");
                            break;
                        case BaseType.DateTime:
                            sourceCode.AppendLine($"\t\tpublic DateTime {f.Name} {{ get; set; }}");
                            break;                            
                        case BaseType.Decimal:                        
                            sourceCode.AppendLine($"\t\tpublic decimal {f.Name} {{ get; set; }}");
                            break;
                        case BaseType.Binary:
                            sourceCode.AppendLine($"\t\tpublic bytes[] {f.Name} {{ get; set; }}");
                            break;
                    }                    
                }
                
                sourceCode.AppendLine("\t}");
                sourceCode.AppendLine();
            }

            // closing namespace            
            sourceCode.AppendLine("}");

            return sourceCode.ToString();
        }

        private string GenerateCharArray(string name, CharArrayTypeDescriptor descriptor)
        {                        
            // is a fixed size array
            if(descriptor.Limits.Min == descriptor.Limits.Max)            
                return $"Array {name} {{ get; set; }} = Array.CreateInstance(typeof(Char), {descriptor.Limits.Max});";
            // we really have no limits on variable size arrays
            else            
                return $"char[] {name} {{ get; set; }}";
        }
        
        private string GenerateInt(IntegerTypeDescriptor descriptor)
        {
            StringBuilder intType = new StringBuilder();

            // we ignore nullability, because it is not well supported in C#
            if(descriptor.Unsigned == true)
                intType.Append("u");

            switch(descriptor.Bytes)
            {
                case 2:
                    intType.Append("short");
                    break;
                case 4:
                    intType.Append("int");
                    break;
                case 8:
                    intType.Append("long");
                    break;
            }
           
            return intType.ToString();
        }
    }
}