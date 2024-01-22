using BindOpen.Commands.Tests;
using NUnit.Framework;
using System.Diagnostics;

namespace BindOpen.Commands
{
    [TestFixture, Order(400)]
    public class HelpTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void FlatOptionsTest()
        {
            var options = OptionSetFaker.CreateFlat();

            var help = SystemData.Scope.GetHelpText(options);

            Trace.WriteLine(help);

            Assert.That(!string.IsNullOrEmpty(help), "Could not generate help text");
        }
    }
}
