// Author: Adrian Hum
// Project: WPFCommandPrompt/ConsoleEventArgs.cs
// 
// Created : 2016-01-06  18:29 
// Modified: 2016-01-06 18:35)

using System;
using System.Windows.Documents;
using System.Windows.Media;

namespace WPFCommandPrompt {
    /// <summary>
    ///     Delegate for the write Event Handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="WPFCommandPrompt.ConsoleWriteLineEventArgs" /> instance containing the event data.</param>
    public delegate void WriteLineEventHandler(object sender, ConsoleWriteLineEventArgs e);

    /// <summary>
    ///     Event arguments for a console write event.
    /// </summary>
    public class ConsoleWriteLineEventArgs : EventArgs {
        /// <summary>
        ///     Text color.
        /// </summary>
        public readonly Brush Foreground;

        /// <summary>
        ///     Message string to/from the console.
        /// </summary>
        public readonly string Message;

        /// <summary>
        ///     The flow document paragraph to write to the console.
        /// </summary>
        public readonly Paragraph Paragraph;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConsoleWriteLineEventArgs" /> class.
        /// </summary>
        /// <param name="message">Message to send to the console.</param>
        public ConsoleWriteLineEventArgs(string message)
        {
            Message = message;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConsoleWriteLineEventArgs" /> class.
        /// </summary>
        /// <param name="message">>Message to send to the console.</param>
        /// <param name="foreground">Text color (overrides default).</param>
        public ConsoleWriteLineEventArgs(string message, Brush foreground)
        {
            Message = message;
            Foreground = foreground;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConsoleWriteLineEventArgs" /> class.
        /// </summary>
        /// <param name="paragraph">The paragraph to write to the console.</param>
        public ConsoleWriteLineEventArgs(Paragraph paragraph)
        {
            Paragraph = paragraph;
        }
    }

    /// <summary>
    ///     Delegate for the read Event Handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="WPFCommandPrompt.ConsoleReadLineEventArgs" /> instance containing the event data.</param>
    public delegate void ReadLineEventHandler(object sender, ConsoleReadLineEventArgs e);

    /// <summary>
    ///     Event arguments for a console read event as a parsed array of
    ///     commands or a single unparsed string of commands.
    /// </summary>
    public class ConsoleReadLineEventArgs : EventArgs {
        /// <summary>
        ///     Parsed string array of commands
        /// </summary>
        public readonly string[] Commands;

        /// <summary>
        ///     Un-parsed command string.
        /// </summary>
        public readonly string CommandString;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConsoleReadLineEventArgs" /> class.
        /// </summary>
        /// <param name="commands">The parsed commands as an array.</param>
        public ConsoleReadLineEventArgs(string[] commands)
        {
            Commands = commands;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConsoleReadLineEventArgs" /> class.
        /// </summary>
        /// <param name="commandString">The un-parsed command string.</param>
        public ConsoleReadLineEventArgs(string commandString)
        {
            CommandString = commandString;
        }
    }
}