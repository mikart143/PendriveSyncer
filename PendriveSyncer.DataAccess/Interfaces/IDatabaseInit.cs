using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using LiteDB;

namespace PendriveSyncer.DataAccess
{
    public abstract class IDatabaseInit
    {
        
        public abstract string GetConnectionString();

    }
}
