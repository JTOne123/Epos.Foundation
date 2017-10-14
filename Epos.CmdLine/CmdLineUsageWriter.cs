﻿using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Epos.CmdLine
{
    internal sealed class CmdLineUsageWriter
    {
        private const char Space = ' ';
        private const int SeparatorCharCount = 3;

        private readonly CmdLineDefinition myDefinition;
        private readonly TextWriter myTextWriter;
        private readonly Action myErrorAction;

        public CmdLineUsageWriter(CmdLineDefinition definition) {
            myDefinition = definition;

            myTextWriter = definition.Configuration.UsageTextWriter;
            myErrorAction = definition.Configuration.ErrorAction;
        }

        public void WriteAndExit(string errorMessage = null) {
            var theResult = new StringBuilder("Usage: ");

            theResult
                .Append(myDefinition.Name).Append(Space)
                .Append('<')
                .Append(string.Join(" | ", myDefinition.Subcommands.Select(sc => sc.Name)))
                .Append('>')
                .Append(Environment.NewLine);

            if (errorMessage != null) {
                theResult
                    .Append(Environment.NewLine)
                    .Append("Error: ")
                    .Append(errorMessage)
                    .Append(Environment.NewLine);
            }
            
            theResult
                .Append(Environment.NewLine)
                .Append("Subcommands")
                .Append(Environment.NewLine);

            int theMaxSubcommandNameLength =
                myDefinition.Subcommands.Any() ?
                myDefinition.Subcommands.Max(sc => sc.Name.Length) :
                0;

            string theFormatString = string.Format(
                "  {{0,-{0}}}{{1}}",
                theMaxSubcommandNameLength + SeparatorCharCount
            );

            foreach (CmdLineSubcommand theSubcommand in myDefinition.Subcommands) {
                theResult.AppendFormat(
                    theFormatString,
                    theSubcommand.Name,
                    GetLineBreakedText(
                        theSubcommand.Description,
                        theMaxSubcommandNameLength + SeparatorCharCount + 2 /* Spaces am Anfang */
                    )
                );

                theResult.Append(Environment.NewLine);
            }

            myTextWriter.WriteLine(theResult.ToString());

            myErrorAction();
        }

        public void WriteAndExit(CmdLineSubcommand subcommand, string errorMessage = null) {
            var theResult = new StringBuilder()
                .Append("Usage: ")
                .Append(myDefinition.Name);

            int theLineInsertionCount = theResult.Length;

            if (subcommand.Name != CmdLineSubcommand.DefaultName) {
                theResult.Append(Space).Append(subcommand.Name);
                theLineInsertionCount += 2 + subcommand.Name.Length;
            }

            foreach (CmdLineOption theOption in subcommand.Options) {
                theResult
                    .Append(Space)
                    .Append(theOption.ToCmdLineString());

                MakeLineBreakIfNeccessary(theResult, theLineInsertionCount);
            }

            foreach (CmdLineParameter theParameter in subcommand.Parameters) {
                theResult
                    .Append(Space)
                    .Append(theParameter.ToCmdLineString());

                MakeLineBreakIfNeccessary(theResult, theLineInsertionCount);
            }

            theResult.Append(Environment.NewLine);

            if (errorMessage != null) {
                theResult
                    .Append(Environment.NewLine)
                    .Append("Error: ")
                    .Append(errorMessage)
                    .Append(Environment.NewLine);
            }

            int theMaxOptionLength =
                subcommand.Options.Any() ?
                    subcommand.Options.Max(o => o.ToString().Length) :
                    0;
            int theMaxParameterLength =
                subcommand.Parameters.Any() ?
                    subcommand.Parameters.Max(p => p.Name.Length) :
                    0;
            int theMaxOptionParameterLength = Math.Max(theMaxOptionLength, theMaxParameterLength);

            string theFormatString = string.Format(
                "  {{0,-{0}}}{{1}}",
                theMaxOptionParameterLength + SeparatorCharCount
            );

            if (subcommand.Options.Any()) {
                theResult
                    .Append(Environment.NewLine)
                    .Append("Options")
                    .Append(Environment.NewLine);

                foreach (CmdLineOption theOption in subcommand.Options) {
                    string theDescription = theOption.Description;

                    object theDefaultValue = theOption.GetDefaultValue();
                    if (theDefaultValue != null) {
                        theDescription += " " + GetDefaultsToText(theDefaultValue);
                    }

                    theResult.AppendFormat(
                        theFormatString,
                        theOption,
                        GetLineBreakedText(
                            theDescription,
                            theMaxOptionParameterLength + SeparatorCharCount + 2 /* Spaces am Anfang */
                        )
                    );

                    theResult.Append(Environment.NewLine);
                }
            }

            if (subcommand.Parameters.Any()) {
                theResult
                    .Append(Environment.NewLine)
                    .Append("Parameters")
                    .Append(Environment.NewLine);

                foreach (CmdLineParameter theParameter in subcommand.Parameters) {
                    string theDescription = theParameter.Description;
                    if (theParameter.IsOptional) {
                        theDescription += " " + GetDefaultsToText(theParameter.GetDefaultValue());
                    }

                    theResult.AppendFormat(
                        theFormatString,
                        theParameter.Name,
                        GetLineBreakedText(
                            theDescription,
                            theMaxOptionParameterLength + SeparatorCharCount + 2 /* Spaces am Anfang */
                        )
                    );

                    theResult.Append(Environment.NewLine);
                }
            }

            myTextWriter.WriteLine(theResult.ToString());
            myErrorAction();
        }

        private static string GetDefaultsToText(object defaultValue) {
            bool isString = defaultValue is string;

            var theResult = new StringBuilder(">>> defaults to ");

            if (isString) {
                theResult.Append('"');
            }

            theResult.Append(defaultValue);

            if (isString) {
                theResult.Append('"');
            }

            return theResult.ToString();
        }

        private int ConsoleWindowWidth => myTextWriter == Console.Out ? Console.WindowWidth : 80;

        private string GetLineBreakedText(string text, int lineInsertionCount) {
            var theResult = new StringBuilder();

            int theLength = lineInsertionCount;
            foreach (string theTextToken in text.Split()) {
                if (theLength + theTextToken.Length + 1 > ConsoleWindowWidth) {
                    theResult
                        .Append(Environment.NewLine)
                        .Append(Space, lineInsertionCount)
                        .Append(theTextToken)
                        .Append(Space);
                    theLength = lineInsertionCount + theTextToken.Length + 1;
                }
                else {
                    theResult
                        .Append(theTextToken)
                        .Append(Space);

                    theLength += theTextToken.Length + 1;
                }
            }

            if (theResult[theResult.Length - 1] == Space) {
                theResult.Length = theResult.Length - 1;
            }

            return theResult.ToString();
        }

        private void MakeLineBreakIfNeccessary(StringBuilder currentResult, int lineInsertionCount) {
            string theCurrentResult = currentResult.ToString();
            int theLastLineBreakIndex = theCurrentResult.LastIndexOf(Environment.NewLine, StringComparison.Ordinal);

            // Aktuelle Zeile ermitteln
            string theCurrentLine =
                theLastLineBreakIndex == -1 ?
                    theCurrentResult :
                    theCurrentResult.Substring(theLastLineBreakIndex + Environment.NewLine.Length);

            int theStartIndex = theCurrentLine.Length - 1;
            if (theCurrentLine.Length > ConsoleWindowWidth) {
                // Letztes Space suchen
                int theLastSpaceIndex;
                do {
                    theLastSpaceIndex = theCurrentLine.LastIndexOf(Space, theStartIndex);
                    theStartIndex = theLastSpaceIndex - 1;
                } while (theLastSpaceIndex > ConsoleWindowWidth);

                // Am letzten Space umbrechen und nächste Zeile mit lineInsertionCount vielen Leerzeichen beginnen
                if (theLastSpaceIndex != -1) {
                    currentResult.Replace(
                        oldValue: Space.ToString(),
                        newValue: Environment.NewLine + new String(Space, lineInsertionCount),
                        startIndex:
                        (theLastLineBreakIndex == -1 ? 0 : theLastLineBreakIndex + Environment.NewLine.Length) +
                        theLastSpaceIndex,
                        count: 1
                    );
                }
            }
        }
    }
}
