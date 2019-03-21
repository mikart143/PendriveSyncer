using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;
using PendriveSyncer.Console.Core.Model;

namespace PendriveSyncer.Console.Core.Utility
{
    public class CommandLineParser
    {
        public ParserResult<CommandOptions> ParseArguments(string[] args)
        {
            return Parser.Default.ParseArguments<CommandOptions>(args);
        }
    }
}
