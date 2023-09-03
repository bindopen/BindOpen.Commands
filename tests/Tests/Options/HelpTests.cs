using BindOpen.Labs.Commands.Tests;
using NUnit.Framework;
using System.Diagnostics;

namespace BindOpen.Labs.Commands
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

            Debug.WriteLine(help);

            Assert.That(!string.IsNullOrEmpty(help), "Could not generate help text");
        }
    }
}
