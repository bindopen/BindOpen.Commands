﻿using BindOpen.Commands.Tests;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Commands
{
    [TestFixture, Order(400)]
    public class ParsingTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void GetArgumentsNullTest()
        {
            string commandLine1 = null;
            var parameters1 = commandLine1.GetArguments();
            Assert.That(parameters1?.Any() == false, "Bad argument creation");
        }

        [Test, Order(2)]
        public void GetArgumentsEmptyTest()
        {
            var commandLine2 = "";
            var parameters2 = commandLine2.GetArguments();
            Assert.That(parameters2?.Any() == false, "Bad argument creation");
        }

        [Test, Order(2)]
        public void GetArgumentsTest()
        {
            var commandLine3 = @"version=1 -h ""toto"" p  'titi' --help";
            var parameters3 = commandLine3.GetArguments();

            Assert.That(
                parameters3.Count() == 6
                && parameters3[0] == "version=1"
                && parameters3[1] == "-h"
                && parameters3[2] == "\"toto\""
                && parameters3[3] == "p"
                && parameters3[4] == "'titi'"
                && parameters3[5] == "--help", "Bad argument creation");
        }

        [Test, Order(3)]
        public void ParseArgumentsWithBadLabelSpecTest()
        {
            var options = OptionSetFaker.CreateFlat();

            var args = new[] { "--version" };

            var scope = SystemData.Scope;
            var parameters = scope.ParseArguments(args, options);
            Assert.That(parameters?.Count == 1, "Bad argument parsing");
        }

        [Test, Order(4)]
        public void ParseArgumentsWithoutSpecTest()
        {
            var args = new[] { "--version", "1.0", "-h", "-i 123" };

            var scope = SystemData.Scope;
            var log = SystemData.CreateLog();

            var parameters = scope.ParseArguments(args, log: log);
            Assert.That(parameters?.Count == 4, "Bad argument parsing");
        }

        [Test, Order(5)]
        public void ParseArgumentsTest()
        {
            var options = OptionSetFaker.CreateFlat();

            var args = new[] { "--version", "1.0", "-h", "-i 123" };

            var scope = SystemData.Scope;
            var parameters = scope.ParseArguments(args, options);
            Assert.That(parameters?.Count == 3, "Bad argument parsing");
        }
    }
}
