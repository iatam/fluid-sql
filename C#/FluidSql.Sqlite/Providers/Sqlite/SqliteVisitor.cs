﻿// <license>
//     The MIT License (MIT)
// </license>
// <copyright company="TTRider, L.L.C.">
//     Copyright (c) 2014-2016 All Rights Reserved
// </copyright>

using System;
using TTRider.FluidSql.Statements;

namespace TTRider.FluidSql.Providers.Sqlite
{
    internal partial class SqliteVisitor : Visitor
    {
        private static readonly string[] supportedDialects = { "sqlite", "ansi" };

        private readonly string[] DbTypeStrings =
        {
            "BIGINT", // BigInt = 0,
            "BINARY", // Binary = 1,
            "BIT", // Bit = 2,
            "CHAR", // Char = 3,
            "DATETIME", // DateTime = 4,
            "DECIMAL", // Decimal = 5,
            "FLOAT", // Float = 6,
            "IMAGE", // Image = 7,
            "INTEGER", // Int = 8,
            "MONEY", // Money = 9,
            "NCHAR", // NChar = 10,
            "NTEXT", // NText = 11,
            "NVARCHAR", // NVarChar = 12,
            "REAL", // Real = 13,
            "UNIQUEIDENTIFIER", // UniqueIdentifier = 14,
            "SMALLDATETIME", // SmallDateTime = 15,
            "SMALLINT", // SmallInt = 16,
            "SMALLMONEY", // SmallMoney = 17,
            "TEXT", // Text = 18,
            "TIMESTAMP", // Timestamp = 19,
            "TINYINT", // TinyInt = 20,
            "VARBINARY", // VarBinary = 21,
            "VARCHAR", // VarChar = 22,
            "SQL_VARIANT", // Variant = 23,
            "Xml", // Xml = 24,
            "DATE", // Date = 25,
            "TIME", // Time = 26,
            "DATETIME2", // DateTime2 = 27,
            "DateTimeOffset" // DateTimeOffset = 28,
        };

        public SqliteVisitor()
        {
            this.IdentifierOpenQuote = "\"";
            this.IdentifierCloseQuote = "\"";
            this.LiteralOpenQuote = "'";
            this.LiteralCloseQuote = "'";
            this.CommentOpenQuote = "/*";
            this.CommentCloseQuote = "*/";
        }

        protected override string[] SupportedDialects => supportedDialects;


        protected override void VisitJoinType(Joins join)
        {
            if (join == Joins.RightOuter || join == Joins.FullOuter)
            {
                throw new NotImplementedException("join " + join + " is not implemented on SQLite");
            }
            State.Write(JoinStrings[(int) join]);
        }


        protected override void VisitType(ITyped typedToken)
        {
            if (typedToken.DbType.HasValue)
            {
                State.Write(DbTypeStrings[(int) typedToken.DbType]);
            }
        }

        protected override void VisitStatement(IStatement statement)
        {
            if (statement is VacuumStatement)
            {
                VisitVacuumStatement((VacuumStatement) statement);
                return;
            }
            base.VisitStatement(statement);
        }


        void VisitVacuumStatement(VacuumStatement statement)
        {
            State.Write(SqliteSymbols.VACUUM);
        }


        protected class SqliteSymbols : Symbols
        {
            public const string hex = "hex";
            public const string localtime = "localtime";
            public const string now = "now";
            public const string randomblob = "randomblob";
            public const string utc = "utc";
            public const string current_timestamp = "current_timestamp";

            public const string VACUUM = "VACUUM";
        }
    }
}