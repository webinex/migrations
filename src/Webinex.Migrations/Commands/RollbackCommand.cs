﻿using System.Linq;
using CommandLine;

namespace Webinex.Migrations.Commands
{
    [Verb("rollback", HelpText = "Rollbacks database")]
    internal class RollbackCommand
    {
        [Option(
            'c',
            "constr",
            Required = false,
            HelpText = "Connection string to be used for migrations")]
        public string ConnectionString { get; set; }

        [Option(
            's',
            "steps",
            Required = false,
            Default = 1,
            HelpText = "Migration steps to rollback")]
        public int Steps { get; set; }

        [Option(
            't',
            "tags",
            Required = false,
            HelpText =
                "Environment tags. Can be multiple, split by `,`. Migration would be applied if it has no tags or matches all tags provided. For example, migration with [Tag(\"A\", \"B\")] would be applied if tags \"A\" AND \"B\" provided.")]
        public string TagsValue { get; set; }

        public string[] Tags => TagsValue?.Split(",").ToArray();
    }
}