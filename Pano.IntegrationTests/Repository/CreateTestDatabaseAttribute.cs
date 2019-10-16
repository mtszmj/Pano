using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Pano.IntegrationTests.Repository
{
    public class CreateTestDatabaseAttribute : Attribute, ITestAction
    {
        public void BeforeTest(ITest test)
        {
            throw new NotImplementedException();
        }

        public void AfterTest(ITest test)
        {
            throw new NotImplementedException();
        }

        public ActionTargets Targets => ActionTargets.Test;
    }
}
