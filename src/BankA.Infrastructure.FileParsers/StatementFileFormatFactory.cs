using BankA.Application.Interfaces;
using BankA.Domain.Enums;
using System;
using System.Linq;

namespace BankA.Infrastructure.FileParsers
{
    public class StatementFileFormatFactory : IStatementFileParserFactory
    {
        public IStatementFileParser Create(StatementFileFormatEnum accountFileFormat)
        {
            var parser = FindParser(accountFileFormat.ToString());
            var parserInstance = (IStatementFileParser)Activator.CreateInstance(parser);
            return parserInstance;
        }


        private Type FindParser(string fileFormat)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
            var interfaceType = typeof(IStatementFileParser);
            var types = assembly.Where(p => interfaceType.IsAssignableFrom(p));

            foreach (var t in types)
            {
                var attrs = (StatementFileFormatAttribute)Attribute.GetCustomAttribute(t, typeof(StatementFileFormatAttribute));
                if (attrs != null)
                {
                    if (attrs.GetFileFormat() == fileFormat)
                    {
                        return t;
                    }
                }
            }
            throw new ApplicationException($"Couldn't find Parser. Invalid file format '{fileFormat}'");
        }
    }
}
