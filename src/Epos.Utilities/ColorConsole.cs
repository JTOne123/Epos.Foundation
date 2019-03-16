using System;

namespace Epos.Utilities
{
    /// <summary> Provides helper methods to write values to the standard output stream in colors. </summary>
    public static class ColorConsole
    {
        /// <summary> Writes the specified string value to the standard output stream
        /// in hint colors. </summary>
        /// <param name="hint">The hint to write, or null.</param>
        public static void WriteHint(string hint) {
            ConsoleColor theOldBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            WriteWhite(hint);
            Console.BackgroundColor = theOldBackgroundColor;
        }

        /// <summary> Writes the specified string value to the standard output stream
        /// in warning colors. </summary>
        /// <param name="warning">The warning to write, or null.</param>
        public static void WriteWarning(string warning) {
            ConsoleColor theOldBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Yellow;
            WriteBlack(warning);
            Console.BackgroundColor = theOldBackgroundColor;
        }

        /// <summary> Writes the specified string value to the standard output stream
        /// in error colors. </summary>
        /// <param name="error">The error to write, or null.</param>
        public static void WriteError(string error) {
            ConsoleColor theOldBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Red;
            WriteWhite(error);
            Console.BackgroundColor = theOldBackgroundColor;
        }

        /// <summary> Writes the text representation of the specified object to the standard output stream
        /// in the color black. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteBlack(object value) => WriteBlack(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value to the standard output stream
        /// in the color black. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteBlack(string value) => WriteBlack(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects
        /// to the standard output stream in the color black using the
        /// specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteBlack(string format, params object[] args) => Write(ConsoleColor.Black, format, args);

        /// <summary> Writes the text representation of the specified object to the standard output stream
        /// in the color dark Blue. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteDarkBlue(object value) =>
            WriteDarkBlue(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value to the standard output stream
        /// in the color dark Blue. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteDarkBlue(string value) => WriteDarkBlue(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects
        /// to the standard output stream in the color dark Blue using the
        /// specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteDarkBlue(string format, params object[] args) =>
            Write(ConsoleColor.DarkBlue, format, args);

        /// <summary> Writes the text representation of the specified object to the standard output stream
        /// in the color dark Green. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteDarkGreen(object value) =>
            WriteDarkGreen(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value to the standard output stream
        /// in the color dark Green. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteDarkGreen(string value) => WriteDarkGreen(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects
        /// to the standard output stream in the color dark Green using the
        /// specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteDarkGreen(string format, params object[] args) => Write(ConsoleColor.DarkGreen, format, args);

        /// <summary> Writes the text representation of the specified object to the standard output stream
        /// in the color dark Cyan. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteDarkCyan(object value) => WriteDarkCyan(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value to the standard output stream
        /// in the color dark Cyan. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteDarkCyan(string value) => WriteDarkCyan(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects
        /// to the standard output stream in the color dark Cyan using the
        /// specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteDarkCyan(string format, params object[] args) => Write(ConsoleColor.DarkCyan, format, args);

        /// <summary> Writes the text representation of the specified object to the standard output stream
        /// in the color dark Red. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteDarkRed(object value) => WriteDarkRed(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value to the standard output stream
        /// in the color dark Red. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteDarkRed(string value) => WriteDarkRed(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects
        /// to the standard output stream in the color dark Red using the
        /// specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteDarkRed(string format, params object[] args) => Write(ConsoleColor.DarkRed, format, args);

        /// <summary> Writes the text representation of the specified object to the standard output stream
        /// in the color dark Magenta. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteDarkMagenta(object value) => WriteDarkMagenta(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value to the standard output stream
        /// in the color dark Magenta. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteDarkMagenta(string value) => WriteDarkMagenta(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects
        /// to the standard output stream in the color dark Magenta using the
        /// specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteDarkMagenta(string format, params object[] args) => Write(ConsoleColor.DarkMagenta, format, args);

        /// <summary> Writes the text representation of the specified object to the standard output stream
        /// in the color dark Yellow. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteDarkYellow(object value) => WriteDarkYellow(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value to the standard output stream
        /// in the color dark Yellow. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteDarkYellow(string value) => WriteDarkYellow(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects
        /// to the standard output stream in the color dark Yellow using the
        /// specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteDarkYellow(string format, params object[] args) => Write(ConsoleColor.DarkYellow, format, args);

        /// <summary> Writes the text representation of the specified object to the standard output stream
        /// in the color gray. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteGray(object value) => WriteGray(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value to the standard output stream
        /// in the color gray. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteGray(string value) => WriteGray(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects
        /// to the standard output stream in the color gray using the
        /// specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteGray(string format, params object[] args) => Write(ConsoleColor.Gray, format, args);

        /// <summary> Writes the text representation of the specified object to the standard output stream
        /// in the color dark Gray. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteDarkGray(object value) => WriteDarkGray(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value to the standard output stream
        /// in the color dark Gray. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteDarkGray(string value) => WriteDarkGray(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects
        /// to the standard output stream in the color dark Gray using the
        /// specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteDarkGray(string format, params object[] args) => Write(ConsoleColor.DarkGray, format, args);

        /// <summary> Writes the text representation of the specified object to the standard output stream
        /// in the color blue. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteBlue(object value) => WriteBlue(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value to the standard output stream
        /// in the color blue. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteBlue(string value) => WriteBlue(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects
        /// to the standard output stream in the color blue using the
        /// specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteBlue(string format, params object[] args) => Write(ConsoleColor.Blue, format, args);

        /// <summary> Writes the text representation of the specified object to the standard output stream
        /// in the color green. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteGreen(object value) => WriteGreen(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value to the standard output stream
        /// in the color green. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteGreen(string value) => WriteGreen(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects
        /// to the standard output stream in the color green using the
        /// specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteGreen(string format, params object[] args) => Write(ConsoleColor.Green, format, args);

        /// <summary> Writes the text representation of the specified object to the standard output stream
        /// in the color cyan. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteCyan(object value) => WriteCyan(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value to the standard output stream
        /// in the color cyan. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteCyan(string value) => WriteCyan(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects
        /// to the standard output stream in the color cyan using the
        /// specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteCyan(string format, params object[] args) => Write(ConsoleColor.Cyan, format, args);

        /// <summary> Writes the text representation of the specified object to the standard output stream
        /// in the color red. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteRed(object value) => WriteRed(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value to the standard output stream
        /// in the color red. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteRed(string value) => WriteRed(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects
        /// to the standard output stream in the color red using the
        /// specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteRed(string format, params object[] args) => Write(ConsoleColor.Red, format, args);

        /// <summary> Writes the text representation of the specified object to the standard output stream
        /// in the color magenta. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteMagenta(object value) => WriteMagenta(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value to the standard output stream
        /// in the color magenta. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteMagenta(string value) => WriteMagenta(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects
        /// to the standard output stream in the color magenta using the
        /// specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteMagenta(string format, params object[] args) => Write(ConsoleColor.Magenta, format, args);

        /// <summary> Writes the text representation of the specified object to the standard output stream
        /// in the color yellow. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteYellow(object value) => WriteYellow(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value to the standard output stream
        /// in the color yellow. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteYellow(string value) => WriteYellow(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects
        /// to the standard output stream in the color yellow using the
        /// specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteYellow(string format, params object[] args) => Write(ConsoleColor.Yellow, format, args);

        /// <summary> Writes the text representation of the specified object to the standard output stream
        /// in the color white. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteWhite(object value) => WriteWhite(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value to the standard output stream
        /// in the color white. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteWhite(string value) => WriteWhite(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects
        /// to the standard output stream in the color white using the
        /// specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteWhite(string format, params object[] args) => Write(ConsoleColor.White, format, args);

        /// <summary> Writes the text representation of the specified object, followed by
        /// the current line terminator, to the standard output stream in the color black. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineBlack(object value) => WriteLineBlack(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value, followed by the current line terminator,
        /// to the standard output stream in the color black. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineBlack(string value) => WriteLineBlack(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects,
        /// followed by the current line terminator to the standard output stream in the
        /// color black using the specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteLineBlack(string format, params object[] args) => WriteLine(ConsoleColor.Black, format, args);

        /// <summary> Writes the text representation of the specified object, followed by
        /// the current line terminator, to the standard output stream in the color dark Blue. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineDarkBlue(object value) => WriteLineDarkBlue(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value, followed by the current line terminator,
        /// to the standard output stream in the color dark Blue. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineDarkBlue(string value) => WriteLineDarkBlue(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects,
        /// followed by the current line terminator to the standard output stream in the
        /// color dark Blue using the specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteLineDarkBlue(string format, params object[] args) => WriteLine(ConsoleColor.DarkBlue, format, args);

        /// <summary> Writes the text representation of the specified object, followed by
        /// the current line terminator, to the standard output stream in the color dark Green. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineDarkGreen(object value) => WriteLineDarkGreen(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value, followed by the current line terminator,
        /// to the standard output stream in the color dark Green. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineDarkGreen(string value) => WriteLineDarkGreen(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects,
        /// followed by the current line terminator to the standard output stream in the
        /// color dark Green using the specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteLineDarkGreen(string format, params object[] args) => WriteLine(ConsoleColor.DarkGreen, format, args);

        /// <summary> Writes the text representation of the specified object, followed by
        /// the current line terminator, to the standard output stream in the color dark Cyan. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineDarkCyan(object value) => WriteLineDarkCyan(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value, followed by the current line terminator,
        /// to the standard output stream in the color dark Cyan. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineDarkCyan(string value) => WriteLineDarkCyan(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects,
        /// followed by the current line terminator to the standard output stream in the
        /// color dark Cyan using the specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteLineDarkCyan(string format, params object[] args) => WriteLine(ConsoleColor.DarkCyan, format, args);

        /// <summary> Writes the text representation of the specified object, followed by
        /// the current line terminator, to the standard output stream in the color dark Red. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineDarkRed(object value) => WriteLineDarkRed(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value, followed by the current line terminator,
        /// to the standard output stream in the color dark Red. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineDarkRed(string value) => WriteLineDarkRed(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects,
        /// followed by the current line terminator to the standard output stream in the
        /// color dark Red using the specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteLineDarkRed(string format, params object[] args) => WriteLine(ConsoleColor.DarkRed, format, args);

        /// <summary> Writes the text representation of the specified object, followed by
        /// the current line terminator, to the standard output stream in the color dark Magenta. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineDarkMagenta(object value) => WriteLineDarkMagenta(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value, followed by the current line terminator,
        /// to the standard output stream in the color dark Magenta. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineDarkMagenta(string value) => WriteLineDarkMagenta(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects,
        /// followed by the current line terminator to the standard output stream in the
        /// color dark Magenta using the specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteLineDarkMagenta(string format, params object[] args) => WriteLine(ConsoleColor.DarkMagenta, format, args);

        /// <summary> Writes the text representation of the specified object, followed by
        /// the current line terminator, to the standard output stream in the color dark Yellow. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineDarkYellow(object value) => WriteLineDarkYellow(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value, followed by the current line terminator,
        /// to the standard output stream in the color dark Yellow. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineDarkYellow(string value) => WriteLineDarkYellow(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects,
        /// followed by the current line terminator to the standard output stream in the
        /// color dark Yellow using the specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteLineDarkYellow(string format, params object[] args) => WriteLine(ConsoleColor.DarkYellow, format, args);

        /// <summary> Writes the text representation of the specified object, followed by
        /// the current line terminator, to the standard output stream in the color gray. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineGray(object value) => WriteLineGray(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value, followed by the current line terminator,
        /// to the standard output stream in the color gray. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineGray(string value) => WriteLineGray(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects,
        /// followed by the current line terminator to the standard output stream in the
        /// color gray using the specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteLineGray(string format, params object[] args) =>
            WriteLine(ConsoleColor.Gray, format, args);

        /// <summary> Writes the text representation of the specified object, followed by
        /// the current line terminator, to the standard output stream in the color dark Gray. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineDarkGray(object value) =>
            WriteLineDarkGray(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value, followed by the current line terminator,
        /// to the standard output stream in the color dark Gray. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineDarkGray(string value) => WriteLineDarkGray(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects,
        /// followed by the current line terminator to the standard output stream in the
        /// color dark Gray using the specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteLineDarkGray(string format, params object[] args) =>
            WriteLine(ConsoleColor.DarkGray, format, args);

        /// <summary> Writes the text representation of the specified object, followed by
        /// the current line terminator, to the standard output stream in the color blue. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineBlue(object value) =>
            WriteLineBlue(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value, followed by the current line terminator,
        /// to the standard output stream in the color blue. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineBlue(string value) => WriteLineBlue(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects,
        /// followed by the current line terminator to the standard output stream in the
        /// color blue using the specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteLineBlue(string format, params object[] args) =>
            WriteLine(ConsoleColor.Blue, format, args);

        /// <summary> Writes the text representation of the specified object, followed by
        /// the current line terminator, to the standard output stream in the color green. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineGreen(object value) =>
            WriteLineGreen(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value, followed by the current line terminator,
        /// to the standard output stream in the color green. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineGreen(string value) => WriteLineGreen(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects,
        /// followed by the current line terminator to the standard output stream in the
        /// color green using the specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteLineGreen(string format, params object[] args) =>
            WriteLine(ConsoleColor.Green, format, args);

        /// <summary> Writes the text representation of the specified object, followed by
        /// the current line terminator, to the standard output stream in the color cyan. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineCyan(object value) =>
            WriteLineCyan(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value, followed by the current line terminator,
        /// to the standard output stream in the color cyan. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineCyan(string value) => WriteLineCyan(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects,
        /// followed by the current line terminator to the standard output stream in the
        /// color cyan using the specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteLineCyan(string format, params object[] args) =>
            WriteLine(ConsoleColor.Cyan, format, args);

        /// <summary> Writes the text representation of the specified object, followed by
        /// the current line terminator, to the standard output stream in the color red. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineRed(object value) =>
            WriteLineRed(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value, followed by the current line terminator,
        /// to the standard output stream in the color red. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineRed(string value) => WriteLineRed(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects,
        /// followed by the current line terminator to the standard output stream in the
        /// color red using the specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteLineRed(string format, params object[] args) =>
            WriteLine(ConsoleColor.Red, format, args);

        /// <summary> Writes the text representation of the specified object, followed by
        /// the current line terminator, to the standard output stream in the color magenta. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineMagenta(object value) =>
            WriteLineMagenta(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value, followed by the current line terminator,
        /// to the standard output stream in the color magenta. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineMagenta(string value) => WriteLineMagenta(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects,
        /// followed by the current line terminator to the standard output stream in the
        /// color magenta using the specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteLineMagenta(string format, params object[] args) =>
            WriteLine(ConsoleColor.Magenta, format, args);

        /// <summary> Writes the text representation of the specified object, followed by
        /// the current line terminator, to the standard output stream in the color yellow. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineYellow(object value) =>
            WriteLineYellow(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value, followed by the current line terminator,
        /// to the standard output stream in the color yellow. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineYellow(string value) => WriteLineYellow(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects,
        /// followed by the current line terminator to the standard output stream in the
        /// color yellow using the specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteLineYellow(string format, params object[] args) =>
            WriteLine(ConsoleColor.Yellow, format, args);

        /// <summary> Writes the text representation of the specified object, followed by
        /// the current line terminator, to the standard output stream in the color white. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineWhite(object value) =>
            WriteLineWhite(value != null ? value.ToString() : string.Empty);

        /// <summary> Writes the specified string value, followed by the current line terminator,
        /// to the standard output stream in the color white. </summary>
        /// <param name="value">The value to write, or null.</param>
        public static void WriteLineWhite(string value) => WriteLineWhite(value ?? string.Empty, null);

        /// <summary> Writes the text representation of the specified array of objects,
        /// followed by the current line terminator to the standard output stream in the
        /// color white using the specified format information. </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to write using format.</param>
        public static void WriteLineWhite(string format, params object[] args) =>
            WriteLine(ConsoleColor.White, format, args);

        // --- Hilfsmethoden ---

        private static void WriteCore(
            ConsoleColor color, string format, object[] args, Action<string, object[]> writeAction
        ) {
            ConsoleColor theOldConsoleForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            writeAction(format, args);
            Console.ForegroundColor = theOldConsoleForegroundColor;

        }

        private static void Write(ConsoleColor color, string format, params object[] args) =>
            WriteCore(color, format, args, Console.Write);

        private static void WriteLine(ConsoleColor color, string format, params object[] args) =>
            WriteCore(color, format, args, Console.WriteLine);
    }
}

