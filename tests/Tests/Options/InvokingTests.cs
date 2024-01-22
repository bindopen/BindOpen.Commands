using BindOpen.Data;
using BindOpen.Commands.Tests;
using NUnit.Framework;
using System.Threading.Tasks;

namespace BindOpen.Commands
{
    [TestFixture, Order(400)]
    public class InvokingTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void InvokeArgumentsTest()
        {
            var st = "";

            var options = BdoCommands.NewOption(
                BdoCommands.NewOption()
                    .WithAliases("--version")
                    .WithDataType(DataValueTypes.Text)
                    .WithLabel(LabelFormats.NameSpaceValue)
                    .Execute(() => { st = "default"; })
                    .Execute(q => q.GetData<string>() == "1.0", () => { st = "1.0"; })
            );

            var scope = SystemData.Scope;

            var args = new[] { "--version", "1.2" };
            scope.ParseArguments(args, options).Invoke();
            Assert.That(st == "default", "Error with Invoke method");

            args = new[] { "--version", "1.0" };
            scope.ParseArguments(args, options).Invoke();
            Assert.That(st == "1.0", "Error with Invoke method");
        }

        [Test, Order(1)]
        public void CustomInvokeArgumentsTest()
        {
            var options = OptionSetFaker.CreateFlat();

            var args = new[] { "--version", "1.0", "-h", "-i 123" };

            var b = false;
            var scope = SystemData.Scope;

            var log = SystemData.CreateLog();

            scope.ParseArguments(args, options, log: log)
                .Invoke(q => q.GetData<string>("version") == "1.0", () => { b = true; });
            Assert.That(b, "Error with Invoke method");
        }

        [Test, Order(2)]
        public async Task InvokeAsyncArgumentsTest()
        {
            var options = OptionSetFaker.CreateFlat();

            var args = new[] { "--version", "1.0", "-h", "-i 123" };

            var b = false;
            var parameters = SystemData.Scope.ParseArguments(args, options);
            await parameters.InvokeAsync(q => q.GetData<string>("version") == "1.0", () => { b = true; return Task.CompletedTask; });
            Assert.That(b, "Error with Invoke method");
        }
    }
}
