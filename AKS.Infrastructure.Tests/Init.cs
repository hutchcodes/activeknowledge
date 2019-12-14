using System;
using System.Collections.Generic;
using System.Text;
using AKS.Infrastructure;
using NUnit.Framework;

//Having this with no namespace means it runs only once for the whole assembly
[SetUpFixture]
public class MySetUpClass
{
    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        // ...
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
        // ...
    }
}
