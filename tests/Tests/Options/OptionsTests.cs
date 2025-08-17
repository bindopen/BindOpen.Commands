using BindOpen.Commands.Tests;
using NUnit.Framework;

namespace BindOpen.Commands;

[TestFixture, Order(400)]
public class OptionsTests
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
    }

    [Test, Order(4)]
    public void FromClassTest()
    {
        var optionSet = BdoCommands.NewOptionFrom<OptionFake>();

        Assert.That(optionSet._Children?.Count == 3, "Bad argument parsing");
    }
}
