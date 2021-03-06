using System;
using System.IO;
using NUnit.Framework;

using static Epos.Utilities.Characters;

namespace Epos.CmdLine
{
    [TestFixture]
    public class CmdLineDefinitionTest
    {
        [Test]
        public void BasicsWithoutSubcommand() {
            var theConsoleOutput = new StringWriter();

            var theCmdLineDefinition = new CmdLineDefinition {
                HasDifferentiatedSubcommands = false,
                Configuration = new CmdLineConfiguration {
                    UsageTextWriter = theConsoleOutput,
                    ErrorAction = () => { }
                }
            };

            Assert.Throws<ArgumentNullException>(() => theCmdLineDefinition.Try(null!));

            Assert.Throws<ArgumentNullException>(() => theCmdLineDefinition.Try(new string[] { null! }));

            Assert.Throws<InvalidOperationException>(() => theCmdLineDefinition.Try(new[] { "non-existing-subcommand" }));

            bool isRun = false;
            theCmdLineDefinition.Subcommands.Add(
                new CmdLineSubcommand<object>(CmdLineSubcommand.DefaultName, "Default command") {
                    CmdLineFunc = (o, _) => { isRun = true; return 0; }
                }
            );

            theCmdLineDefinition.Try(new string[] { });

            Assert.That(theConsoleOutput.ToString(), Is.Empty);
            Assert.That(isRun, Is.True);
        }

        [Test]
        public void BasicsWithDifferentiatedCommands() {
            var theConsoleOutput = new StringWriter();

            var theCmdLineDefinition = new CmdLineDefinition {
                Name = "sample",
                Configuration = new CmdLineConfiguration {
                    UsageTextWriter = theConsoleOutput,
                    ErrorAction = () => { }
                }
            };

            Assert.That(theCmdLineDefinition.Name, Is.EqualTo("sample"));

            Assert.Throws<InvalidOperationException>(() => theCmdLineDefinition.Try(new string[] { }));

            theCmdLineDefinition.Subcommands.Add(
                new CmdLineSubcommand<object>("build", "Builds something.")
            );
            theCmdLineDefinition.Subcommands.Add(
                new CmdLineSubcommand<object>("test", "Tests something.")
            );

            Assert.That(theCmdLineDefinition.Subcommands[0].Name, Is.EqualTo("build"));
            Assert.That(theCmdLineDefinition.Subcommands[1].Description, Is.EqualTo("Tests something."));
            Assert.That(theCmdLineDefinition.Subcommands[0].Options, Is.Empty);
            Assert.That(theCmdLineDefinition.Subcommands[1].Parameters, Is.Empty);

            theCmdLineDefinition.Try(new string[] { });

            Assert.That(
                theConsoleOutput.ToString(),
                Is.EqualTo(
                    "Usage: sample <build | test>" + DbLf +
                    "Subcommands"                  + Lf +
                    "  build   Builds something."  + Lf +
                    "  test    Tests something."   + DbLf
                )
            );
        }

        [Test]
        public void Parameters() {
            var theConsoleOutput = new StringWriter();

            // decimal als Parametertyp ist beispielsweise nicht erlaubt:

            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<TypeInitializationException>(() => new CmdLineParameter<decimal>(null!, null!));

            BuildOptions? theBuildOptions = null;

            var theCmdLineDefinition = new CmdLineDefinition {
                Name = "sample",
                Configuration = new CmdLineConfiguration {
                    UsageTextWriter = theConsoleOutput,
                    ErrorAction = () => throw new CmdLineError()
                },
                Subcommands = {
                    new CmdLineSubcommand<BuildOptions>("build", "Builds something.") {
                        Parameters = {
                            new CmdLineParameter<int>("project-number", "Sets the project number."),
                            new CmdLineParameter<string>("memory", "Sets the used memory.") {
                                DefaultValue = "1 GB"
                            }
                        },
                        CmdLineFunc = (o, _) => {
                            theBuildOptions = o;
                            return 0;
                        }
                    }
                }
            };

            Assert.Throws<CmdLineError>(() => theCmdLineDefinition.Try(new[] { "test" }));

            Assert.That(
                theConsoleOutput.ToString(),
                Is.EqualTo(
                    "Usage: sample <build>" + DbLf +
                    "Error: Unknown subcommand: test" + DbLf +
                    "Subcommands" + Lf +
                    "  build   Builds something." + DbLf
                )
            );

            theConsoleOutput.GetStringBuilder().Clear();

            Assert.Throws<CmdLineError>(() => theCmdLineDefinition.Try(new[] { "build" }));

            Assert.That(
                theConsoleOutput.ToString(),
                Is.EqualTo(
                    "Usage: sample build <project-number:int> [<memory:string>=\"1 GB\"]" + DbLf +
                    "Error: Missing parameter: project-number"                            + DbLf +
                    "Parameters"                                                          + Lf +
                    "  project-number   Sets the project number."                         + Lf +
                    "  memory           Sets the used memory. >>> defaults to \"1 GB\""   + DbLf
                )
            );

            theConsoleOutput.GetStringBuilder().Clear();

            Assert.Throws<CmdLineError>(() => theCmdLineDefinition.Try(new[] { "build", "no-number" }));

            Assert.That(
                theConsoleOutput.ToString(),
                Is.EqualTo(
                    "Usage: sample build <project-number:int> [<memory:string>=\"1 GB\"]"       + DbLf +
                    "Error: Value \"no-number\" for parameter <project-number:int> is invalid." + DbLf +
                    "Parameters"                                                                + Lf +
                    "  project-number   Sets the project number."                               + Lf +
                    "  memory           Sets the used memory. >>> defaults to \"1 GB\""         + DbLf
                )
            );

            theConsoleOutput.GetStringBuilder().Clear();

            theCmdLineDefinition.Try(new[] { "build", "33" });
            Assert.That(theBuildOptions!.ProjectNumber, Is.EqualTo(33));
            Assert.That(theBuildOptions.Memory, Is.EqualTo("1 GB"));
            Assert.That(theBuildOptions.DummyParameter, Is.Null);
            Assert.That(theConsoleOutput.ToString(), Is.Empty);

            theCmdLineDefinition.Try(new[] { "build", "66", "2 GB" });
            Assert.That(theBuildOptions.ProjectNumber, Is.EqualTo(66));
            Assert.That(theBuildOptions.Memory, Is.EqualTo("2 GB"));
            Assert.That(theBuildOptions.DummyParameter, Is.Null);
            Assert.That(theConsoleOutput.ToString(), Is.Empty);

            Assert.Throws<CmdLineError>(() => theCmdLineDefinition.Try(new[] { "build", "33", "2 GB", "Too much!" }));

            Assert.That(
                theConsoleOutput.ToString(),
                Is.EqualTo(
                    "Usage: sample build <project-number:int> [<memory:string>=\"1 GB\"]"       + DbLf +
                    "Error: Too many parameters: Too much!" + DbLf +
                    "Parameters"                                                                + Lf +
                    "  project-number   Sets the project number."                               + Lf +
                    "  memory           Sets the used memory. >>> defaults to \"1 GB\""         + DbLf
                )
            );
        }

        [Test]
        public void ExclusionGroups() {
            var theConsoleOutput = new StringWriter();

            TestOptions? theTestOptions = null;

            var theCmdLineDefinition = new CmdLineDefinition {
                Name = "sample",
                Configuration = new CmdLineConfiguration {
                    UsageTextWriter = theConsoleOutput,
                    ErrorAction = () => throw new CmdLineError()
                },
                Subcommands = {
                    new CmdLineSubcommand<TestOptions>("test", "Tests something.") {
                        Options = {
                            new CmdLineOption<int>('f', "From.") { ExclusionGroups = { "EG1" } },
                            new CmdLineOption<int>('t', "To.") { ExclusionGroups = { "EG2" } },
                            new CmdLineSwitch('a', "All.") { ExclusionGroups = { "EG1", "EG2" } },
                            new CmdLineOption<int>('s', "Single.") { ExclusionGroups = { "EG1", "EG2" } }
                        },
                        CmdLineFunc = (o, _) => {
                            theTestOptions = o;
                            return 0;
                        }
                    },
                    new CmdLineSubcommand<object>("build", "Builds something.")
                }
            };

            Assert.Throws<CmdLineError>(() => theCmdLineDefinition.Try(new[] { "test" }));
            Assert.That(
                theConsoleOutput.ToString(),
                Is.EqualTo(
                    "Usage: sample test [-f <int>] [-t <int>] [-a] [-s <int>]" + DbLf +
                    "Error: Missing option: -f" + DbLf +
                    "Options" + Lf +
                    "  -f   From." + Lf +
                    "  -t   To." + Lf +
                    "  -a   All." + Lf +
                    "  -s   Single." + DbLf
                )
            );

            theConsoleOutput.GetStringBuilder().Clear();

            theCmdLineDefinition.Try(new[] { "test", "-a" });
            Assert.That(theTestOptions!.All, Is.True);
            Assert.That(theTestOptions.From, Is.EqualTo(0));
            Assert.That(theTestOptions.To, Is.EqualTo(0));
            Assert.That(theTestOptions.Single, Is.EqualTo(0));

            theConsoleOutput.GetStringBuilder().Clear();

            theCmdLineDefinition.Try(new[] { "test", "-s", "123" });
            Assert.That(theTestOptions.All, Is.False);
            Assert.That(theTestOptions.From, Is.EqualTo(0));
            Assert.That(theTestOptions.To, Is.EqualTo(0));
            Assert.That(theTestOptions.Single, Is.EqualTo(123));

            theConsoleOutput.GetStringBuilder().Clear();

            Assert.Throws<CmdLineError>(() => theCmdLineDefinition.Try(new[] { "test", "-a", "-s", "123" }));
            Assert.That(
                theConsoleOutput.ToString(),
                Is.EqualTo(
                    "Usage: sample test [-f <int>] [-t <int>] [-a] [-s <int>]" + DbLf +
                    "Error: Only one of the following options may be set: [-a], [-s <int>]" + DbLf +
                    "Options" + Lf +
                    "  -f   From." + Lf +
                    "  -t   To." + Lf +
                    "  -a   All." + Lf +
                    "  -s   Single." + DbLf
                )
            );

            theConsoleOutput.GetStringBuilder().Clear();

            theCmdLineDefinition.Try(new[] { "test", "-f", "123", "-t", "456" });
            Assert.That(theTestOptions.All, Is.False);
            Assert.That(theTestOptions.From, Is.EqualTo(123));
            Assert.That(theTestOptions.To, Is.EqualTo(456));
            Assert.That(theTestOptions.Single, Is.EqualTo(0));

            theConsoleOutput.GetStringBuilder().Clear();

            Assert.Throws<CmdLineError>(() => theCmdLineDefinition.Try(
                new[] { "test", "-f", "123", "-t", "456", "-a" }
            ));
            Assert.That(
                theConsoleOutput.ToString(),
                Is.EqualTo(
                    "Usage: sample test [-f <int>] [-t <int>] [-a] [-s <int>]" + DbLf +
                    "Error: Only one of the following options may be set: [-f <int>], [-a]" + DbLf +
                    "Options" + Lf +
                    "  -f   From." + Lf +
                    "  -t   To." + Lf +
                    "  -a   All." + Lf +
                    "  -s   Single." + DbLf
                )
            );
        }

        [Test]
        public void OptionsAndShowHelp() {
            var theConsoleOutput = new StringWriter();

            // decimal als Parametertyp ist beispielsweise nicht erlaubt:
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<TypeInitializationException>(() => new CmdLineOption<decimal>('a', null!));

            BuildOptions? theBuildOptions = null;

            var theCmdLineDefinition = new CmdLineDefinition {
                Name = "sample",
                Configuration = new CmdLineConfiguration {
                    UsageTextWriter = theConsoleOutput,
                    ErrorAction = () => { }
                },
                Subcommands = {
                    new CmdLineSubcommand<BuildOptions>("build", "Builds something.") {
                        Options = {
                            new CmdLineOption<int>('p', "Sets the project number.") { LongName = "project-number" },
                            new CmdLineOption<string>('m', "Sets the used memory.") {
                                LongName = "memory",
                                DefaultValue = "1 GB"
                            },
                            new CmdLineSwitch('d', "Disables the command.") { ExclusionGroups = { "E1" } },
                            new CmdLineSwitch('z', "Zzzz...") { ExclusionGroups = { "E1" } }
                        },
                        Parameters = {
                            new CmdLineParameter<string>("dummy", "Sets a dummy value.")
                        },
                        CmdLineFunc = (o, _) => {
                            theBuildOptions = o;
                            return 0;
                        }
                    },
                    new CmdLineSubcommand<object>("test", "Tests something.")
                }
            };

            theCmdLineDefinition.ShowHelp();

            Assert.That(
                theConsoleOutput.ToString(),
                Is.EqualTo(
                    "Usage: sample <build | test>" + DbLf +
                    "Subcommands" + Lf +
                    "  build   Builds something." + Lf +
                    "  test    Tests something." + DbLf
                )
            );

            theConsoleOutput.GetStringBuilder().Clear();

            Assert.Throws<ArgumentNullException>(() => theCmdLineDefinition.ShowHelp(null!));
            Assert.Throws<ArgumentException>(() => theCmdLineDefinition.ShowHelp("not-found"));

            theCmdLineDefinition.ShowHelp("build");

            Assert.That(
                theConsoleOutput.ToString(),
                Is.EqualTo(
                    "Usage: sample build [-p, --project-number <int>] [-m, --memory <string=\"1 GB\">]" + Lf +
                    "                    [-d] [-z] <dummy:string>" + DbLf +
                    "Options" + Lf +
                    "  -p, --project-number   Sets the project number." + Lf +
                    "  -m, --memory           Sets the used memory. >>> defaults to \"1 GB\"" + Lf +
                    "  -d                     Disables the command." + Lf +
                    "  -z                     Zzzz..." + DbLf +
                    "Parameters" + Lf +
                    "  dummy                  Sets a dummy value." + DbLf
                )
            );

            theCmdLineDefinition.Configuration.ErrorAction = () => throw new CmdLineError();
            theConsoleOutput.GetStringBuilder().Clear();

            Assert.Throws<CmdLineError>(() => theCmdLineDefinition.Try(new[] { "build" }));

            Assert.That(
                theConsoleOutput.ToString(),
                Is.EqualTo(
                    "Usage: sample build [-p, --project-number <int>] [-m, --memory <string=\"1 GB\">]" + Lf +
                    "                    [-d] [-z] <dummy:string>"                                      + DbLf +
                    "Error: Missing option: -p, --project-number"                                       + DbLf +
                    "Options"                                                                           + Lf +
                    "  -p, --project-number   Sets the project number."                                 + Lf +
                    "  -m, --memory           Sets the used memory. >>> defaults to \"1 GB\""           + Lf +
                    "  -d                     Disables the command."                                    + Lf +
                    "  -z                     Zzzz..."                                                  + DbLf +
                    "Parameters"                                                                        + Lf +
                    "  dummy                  Sets a dummy value."                                      + DbLf
                )
            );

            theConsoleOutput.GetStringBuilder().Clear();

            theCmdLineDefinition.Try(new[] { "build", "-p", "0", "dummy" });
            Assert.That(theBuildOptions!.ProjectNumber, Is.EqualTo(0));
            Assert.That(theBuildOptions.Memory, Is.EqualTo("1 GB"));
            Assert.That(theBuildOptions.DummyParameter, Is.EqualTo("dummy"));
            Assert.That(theBuildOptions.Disable, Is.False);
            Assert.That(theConsoleOutput.ToString(), Is.Empty);

            theCmdLineDefinition.Try(new[] { "build", "-d", "-p", "0", "dummy" });
            Assert.That(theBuildOptions.ProjectNumber, Is.EqualTo(0));
            Assert.That(theBuildOptions.Memory, Is.EqualTo("1 GB"));
            Assert.That(theBuildOptions.DummyParameter, Is.EqualTo("dummy"));
            Assert.That(theBuildOptions.Disable, Is.True);
            Assert.That(theConsoleOutput.ToString(), Is.Empty);

            theCmdLineDefinition.Try(new[] { "build", "-d", "-p", "11", "dummy" });
            Assert.That(theBuildOptions.ProjectNumber, Is.EqualTo(11));
            Assert.That(theBuildOptions.Memory, Is.EqualTo("1 GB"));
            Assert.That(theBuildOptions.DummyParameter, Is.EqualTo("dummy"));
            Assert.That(theBuildOptions.Disable, Is.True);
            Assert.That(theBuildOptions.Zzzz, Is.False);
            Assert.That(theConsoleOutput.ToString(), Is.Empty);

            theCmdLineDefinition.Try(new[] { "build", "-d", "--project-number", "99", "-m", "2 GB", "dummy" });
            Assert.That(theBuildOptions.ProjectNumber, Is.EqualTo(99));
            Assert.That(theBuildOptions.Memory, Is.EqualTo("2 GB"));
            Assert.That(theBuildOptions.DummyParameter, Is.EqualTo("dummy"));
            Assert.That(theBuildOptions.Disable, Is.True);
            Assert.That(theBuildOptions.Zzzz, Is.False);
            Assert.That(theConsoleOutput.ToString(), Is.Empty);

            Assert.Throws<CmdLineError>(() => theCmdLineDefinition.Try(new[] { "build", "-dz", "-p", "99", "dummy" }));

            Assert.That(
                theConsoleOutput.ToString(),
                Is.EqualTo(
                    "Usage: sample build [-p, --project-number <int>] [-m, --memory <string=\"1 GB\">]" + Lf +
                    "                    [-d] [-z] <dummy:string>" + DbLf +
                    "Error: Only one of the following options may be set: [-d], [-z]" + DbLf +
                    "Options" + Lf +
                    "  -p, --project-number   Sets the project number." + Lf +
                    "  -m, --memory           Sets the used memory. >>> defaults to \"1 GB\"" + Lf +
                    "  -d                     Disables the command." + Lf +
                    "  -z                     Zzzz..." + DbLf +
                    "Parameters" + Lf +
                    "  dummy                  Sets a dummy value." + DbLf
                )
            );

            theConsoleOutput.GetStringBuilder().Clear();

            Assert.Throws<CmdLineError>(() => theCmdLineDefinition.Try(new[] { "build", "-p", "abc", "dummy" }));

            Assert.That(
                theConsoleOutput.ToString(),
                Is.EqualTo(
                    "Usage: sample build [-p, --project-number <int>] [-m, --memory <string=\"1 GB\">]" + Lf +
                    "                    [-d] [-z] <dummy:string>"                                           + DbLf +
                    "Error: Value \"abc\" for option [-p, --project-number <int>] is invalid."          + DbLf +
                    "Options"                                                                           + Lf +
                    "  -p, --project-number   Sets the project number."                                 + Lf +
                    "  -m, --memory           Sets the used memory. >>> defaults to \"1 GB\""           + Lf +
                    "  -d                     Disables the command."                                    + Lf +
                    "  -z                     Zzzz..."                                                  + DbLf +
                    "Parameters"                                                                        + Lf +
                    "  dummy                  Sets a dummy value."                                      + DbLf
                )
            );

            theConsoleOutput.GetStringBuilder().Clear();

            Assert.Throws<CmdLineError>(() => theCmdLineDefinition.Try(new[] { "build", "-p" }));

            Assert.That(
                theConsoleOutput.ToString(),
                Does.StartWith(
                    "Usage: sample build [-p, --project-number <int>] [-m, --memory <string=\"1 GB\">]" + Lf +
                    "                    [-d] [-z] <dummy:string>"                                      + DbLf +
                    "Error: Missing value for option [-p, --project-number <int>]."
                )
            );
        }

        private class CmdLineError : Exception { }

        internal class TestOptions
        {
            [CmdLineOption('f')]
            public int From { get; set; }

            [CmdLineOption('t')]
            public int To { get; set; }

            [CmdLineOption('s')]
            public int Single { get; set; }

            [CmdLineOption('a')]
            public bool All { get; set; }
        }

        internal class BuildOptions
        {
            [CmdLineOption('p')]
            public int ProjectNumber { get; set; }

            [CmdLineOption('m')]
            public string? Memory { get; set; }

            [CmdLineOption('d')]
            public bool Disable { get; set; }

            [CmdLineOption('z')]
            public bool Zzzz { get; set; }

            [CmdLineParameter("dummy")]
            public string? DummyParameter { get; set; }
        }
    }
}
