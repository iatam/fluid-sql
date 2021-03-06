﻿// <license>
//     The MIT License (MIT)
// </license>
// <copyright company="TTRider, L.L.C.">
//     Copyright (c) 2014-2016 All Rights Reserved
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using TTRider.FluidSql;

namespace FluidSqlTests
{
    [TestClass]
    public class Delete
    {
        [TestMethod]
        public void Delete1()
        {
            var statement = Sql.Delete.Top(1).From(Sql.Name("foo.bar"));

            var command = Utilities.GetCommand(statement);

            Assert.IsNotNull(command);
            Assert.AreEqual("DELETE TOP ( 1 ) FROM [foo].[bar];", command.CommandText);
        }

        [TestMethod]
        public void Delete1P()
        {
            var statement = Sql.Delete.Top(1, true).From(Sql.NameAs("foo.bar", "f"));

            var command = Utilities.GetCommand(statement);

            Assert.IsNotNull(command);
            Assert.AreEqual("DELETE TOP ( 1 ) PERCENT [f] FROM [foo].[bar] AS [f];", command.CommandText);
        }

        [TestMethod]
        public void DeleteOutput()
        {
            var statement = Sql.Delete.From(Sql.Name("foo.bar")).Output(Sql.Name("INSERTED.*"), Sql.Name("DELETED.*"));

            var command = Utilities.GetCommand(statement);

            Assert.IsNotNull(command);
            Assert.AreEqual("DELETE FROM [foo].[bar] OUTPUT [INSERTED].*, [DELETED].*;", command.CommandText);
        }

        [TestMethod]
        public void DeleteOutput2()
        {
            var statement = Sql.Delete.From(Sql.NameAs("foo.bar", "s"))
                .Output(Sql.Name("INSERTED.*"), Sql.Name("DELETED.*"));

            var command = Utilities.GetCommand(statement);

            Assert.IsNotNull(command);
            Assert.AreEqual("DELETE [s] OUTPUT [INSERTED].*, [DELETED].* FROM [foo].[bar] AS [s];", command.CommandText);
        }

        [TestMethod]
        public void DeleteWhere()
        {
            var statement = Sql.Delete.From(Sql.Name("foo.bar")).Where(Sql.Name("f").IsEqual(Sql.Name("b")));

            var command = Utilities.GetCommand(statement);

            Assert.IsNotNull(command);
            Assert.AreEqual("DELETE FROM [foo].[bar] WHERE [f] = [b];", command.CommandText);
        }


        [TestMethod]
        public void DeleteJoin()
        {
            var sourceTable = Sql.NameAs("foo", "bar");

            var statement = Sql.Delete.From(sourceTable)
                .InnerJoin(Sql.NameAs("reftable", "ref"), Sql.Name("bar", "id").IsEqual(Sql.Name("ref", "id")))
                .Top(5)
                .Output(Sql.Name("deleted", "*"))
                .Where(Sql.Name("ref", "id").NotEqual(Sql.Scalar(10)));


            var command = Utilities.GetCommand(statement);

            Assert.IsNotNull(command);
            Assert.AreEqual(
                "DELETE TOP ( 5 ) [bar] OUTPUT [deleted].* FROM [foo] AS [bar] INNER JOIN [reftable] AS [ref] ON [bar].[id] = [ref].[id] WHERE [ref].[id] <> 10;",
                command.CommandText);
        }
    }
}