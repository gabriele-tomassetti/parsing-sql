using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using static SQLParser;

namespace ParsingSQL
{
    public class CreateVisitor
    {
        public  List<ClassDescriptor> VisitStatements(StatementsContext context)    
        {
            List<ClassDescriptor> tables = new List<ClassDescriptor>();
            
            foreach(var statement in context.statement())
            {
                tables.Add(VisitStmt(statement));
            }

            return tables;
        }

        public ClassDescriptor VisitStmt(StatementContext context)
        {                      
            return VisitCreateStmt(context.createStmt());
        }

        public ClassDescriptor VisitCreateStmt(CreateStmtContext context)
        {
            ClassDescriptor table = new ClassDescriptor();

            table.Name = context.tableName.NAME().GetText();
            table.Fields = new List<FieldDescriptor>();

            foreach (var el in context.element())
            {
                if(el.definition() != null)
                {
                    table.Fields.Add(VisitDefinition(el.definition()));
                }                 
            }        

            return table;
        }

        public FieldDescriptor VisitDefinition(DefinitionContext context)
        {
            string name = context.name().NAME().GetText();
            TypeDescriptor type;
            
            switch(context.type().GetType().Name)
            {
                case "IntegerTypeContext":
                    type = VisitIntegerType(context.type() as IntegerTypeContext);
                    break;
                case "SmallIntegerTypeContext":
                    type = VisitSmallIntegerType(context.type() as SmallIntegerTypeContext);
                    break;
                case "VarcharTypeContext":
                    type = VisitVarcharType(context.type() as VarcharTypeContext);
                    break;
                case "TimestampTypeContext":
                    type = VisitTimestampType(context.type() as TimestampTypeContext);
                    break;
                case "TextTypeContext":
                    type = VisitTextType(context.type() as TextTypeContext);
                    break;               
                case "DecimalTypeContext":
                    type = VisitDecimalType(context.type() as DecimalTypeContext);
                    break;  
                case "CharTypeContext":
                    type = VisitCharType(context.type() as CharTypeContext);
                    break; 
                case "BlobTypeContext":
                    type = VisitBlobType(context.type() as BlobTypeContext);
                    break; 
                default:                    
                    type = null;
                    break;
            }
            
            if(context.nullability() != null && context.nullability().NOT() != null)            
                type.Nullability = false;

            return new FieldDescriptor(name, type);
        }

        private TypeDescriptor VisitBlobType(BlobTypeContext blobTypeContext)
        {
            return new BinaryTypeDescriptor();
        }

        private TypeDescriptor VisitCharType(CharTypeContext context)
        {
            return new CharArrayTypeDescriptor(int.Parse(context.NUMBER().GetText()), int.Parse(context.NUMBER().GetText()));
        }

        private TypeDescriptor VisitDecimalType(DecimalTypeContext context)
        {
            int precision = 0;
            int scale = 0;

            if(context.precision != null)
                precision = int.Parse(context.precision.Text);

            if(context.scale != null)
                scale = int.Parse(context.scale.Text);

            return new DecimalTypeDescriptor(precision, scale);
        }

        private TypeDescriptor VisitTimestampType(TimestampTypeContext context)
        {
            return new DateTimeTypeDescriptor();
        }

        private TypeDescriptor VisitVarcharType(VarcharTypeContext context)
        {
            return new CharArrayTypeDescriptor(0, int.Parse(context.NUMBER().GetText()));
        }

        private TypeDescriptor VisitSmallIntegerType(SmallIntegerTypeContext context)
        {
           if(context.UNSIGNED() != null)
                return new IntegerTypeDescriptor(2, true);
            else
                return new IntegerTypeDescriptor(2);
        }

        private TypeDescriptor VisitIntegerType(IntegerTypeContext context)
        {
            if(context.UNSIGNED() != null)
                return new IntegerTypeDescriptor(4, true);
            else
                return new IntegerTypeDescriptor(4);
        }

        private TypeDescriptor VisitTextType(TextTypeContext context)
        {           
            return new StringTypeDescriptor();
        }
    }
}