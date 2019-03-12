using System;
using System.Collections.Generic;
using System.Text;

namespace PendriveSyncer.FileManagment
{
    public abstract class IStorageProvider
    {
        protected internal IStorageProvider() { }

        public abstract void BrutalSync();

        public abstract void NormalSync();

    }
}
