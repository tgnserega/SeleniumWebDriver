using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace csharp_example
{
    [TestFixture]
    public class RecycleBin : TestBase
    {
        [Test]
        public void RecycleBinTest()
        {
            app.AddToRecycleBin(3);
            app.RemoveFromRecycleBin(3);
        }
    }
}