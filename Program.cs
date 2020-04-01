using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Antlr4.Runtime;

namespace ParsingSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            // standard ANTLR code
            // we get the input
            ICharStream chars = CharStreams.fromPath(args[0]);
            // we set up the lexer
            SQLLexer lexer = new SQLLexer(chars);            
            // we use the lexer
            CommonTokenStream stream = new CommonTokenStream(lexer);
            // we set up the parser
            SQLParser parser = new SQLParser(stream);

            // we find the root node of our parse tree             
            var tree = parser.statements();                                    
            
            // we create our visitor
            CreateVisitor createVisitor = new CreateVisitor();   
            List<ClassDescriptor> classes = createVisitor.VisitStatements(tree);

            // we choose our code generator...
            ICodeGenerator generator;

            // ...depending on the command line argument
            if(args.Count() > 1 && args[1] == "kotlin")
                generator = new KotlinCodeGenerator();
            else
                generator = new CSharpCodeGenerator();
            
            Console.WriteLine(generator.ToSourceCode("SQLDataTypes", classes));
        }
    }
}
