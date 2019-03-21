using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace PendriveSyncer.Console.Core.Model
{
    public class CommandOptions
    {
        [Option('s', "silent",Default = false, Required = false, HelpText = "Run Console Application in silent mode (NO GUI)")]
        public bool Silent { get; set; }
    }
}
