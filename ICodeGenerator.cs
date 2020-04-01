using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParsingSQL
{    
    public interface ICodeGenerator
    {
        string ToSourceCode(string idNamespace, List<ClassDescriptor> classes);        
    }
}