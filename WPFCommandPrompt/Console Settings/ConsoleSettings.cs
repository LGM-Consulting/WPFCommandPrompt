using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WPFCommandPrompt
{
    /// <summary>
    /// Holds a reference to the current console settings.
    /// </summary>
    public class ConsoleSettings
    {
        /// <summary>
        /// Should the console show an empty command in the message prompt.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow empty command]; otherwise, <c>false</c>.
        /// </value>
        [XmlElement(DataType = "boolean", ElementName = "AllowEmptyCommand")]
        public bool AllowEmptyCommand { get; set; }

        /// <summary>
        /// Gets or sets the console window title.
        /// </summary>
        /// <value>
        /// The console title.
        /// </value>
        [XmlElement(DataType = "string", ElementName = "ConsoleTitle")]
        public string ConsoleTitle { get; set; }

        /// <summary>
        /// Gets or sets the default height of the console.
        /// </summary>
        /// <value>
        /// The default height of the console.
        /// </value>
        [XmlElement(DataType = "double", ElementName = "DefaultConsoleHeight")]
        public double DefaultConsoleHeight { get; set; }

        /// <summary>
        /// Gets or sets the default width of the console.
        /// </summary>
        /// <value>
        /// The default width of the console.
        /// </value>
        [XmlElement(DataType = "double", ElementName = "DefaultConsoleWidth")]
        public double DefaultConsoleWidth { get; set; }

        /// <summary>
        /// Gets or sets the default font size.
        /// </summary>
        /// <value>
        /// The default font size.
        /// </value>
        [XmlElement(DataType = "double", ElementName = "DefaultFontSize")]
        public double DefaultFontSize { get; set; }

        /// <summary>
        /// Gets or sets the default prompt.
        /// </summary>
        /// <value>
        /// The default prompt.
        /// </value>
        [XmlElement(DataType = "string", ElementName = "DefaultPrompt")]
        public string DefaultPrompt { get; set; }

        /// <summary>
        /// Gets or sets the delimiter string.
        /// </summary>
        /// <value>
        /// The delimiter string.
        /// </value>
        [XmlElement(DataType = "string", ElementName = "Delimiters")]
        public string Delimiters { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable command history.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [enable command history]; otherwise, <c>false</c>.
        /// </value>
        [XmlElement(DataType = "boolean", ElementName = "EnableCommandHistory")]
        public bool EnableCommandHistory { get; set; }

        /// <summary>
        /// Should the console try and load sytle themes from disk upon startup.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [enable load style themes]; otherwise, <c>false</c>.
        /// </value>
        [XmlElement(DataType = "boolean", ElementName = "EnableLoadStyleThemes")]
        public bool EnableLoadStyleThemes { get; set; }

        /// <summary>
        /// Is the command history managed by the console or by the user.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [manual command history]; otherwise, <c>false</c>.
        /// </value>
        [XmlElement(DataType = "boolean", ElementName = "ManualCommandHistory")]
        public bool ManualCommandHistory { get; set; }

        /// <summary>
        /// Gets or sets the maximum height of the console.
        /// </summary>
        /// <value>
        /// The maximum height of the console.
        /// </value>
        [XmlElement(DataType = "double", ElementName = "MaxConsoleHeight")]
        public double MaxConsoleHeight { get; set; }

        /// <summary>
        /// Gets or sets the maximum width of the console.
        /// </summary>
        /// <value>
        /// The maximum width of the console.
        /// </value>
        [XmlElement(DataType = "double", ElementName = "MaxConsoleWidth")]
        public double MaxConsoleWidth { get; set; }

        /// <summary>
        /// Gets or sets the maximum font size.
        /// </summary>
        /// <value>
        /// The maximum font size.
        /// </value>
        [XmlElement(DataType = "double", ElementName = "MaxFontSize")]
        public double MaxFontSize { get; set; }

        /// <summary>
        /// Gets or sets the minumum height of the console.
        /// </summary>
        /// <value>
        /// The minumum height of the console.
        /// </value>
        [XmlElement(DataType = "double", ElementName = "MinConsoleHeight")]
        public double MinConsoleHeight { get; set; }

        /// <summary>
        /// Gets or sets the minumum width of the console.
        /// </summary>
        /// <value>
        /// The minumum width of the console.
        /// </value>
        [XmlElement(DataType = "double", ElementName = "MinConsoleWidth")]
        public double MinConsoleWidth { get; set; }

        /// <summary>
        /// Gets or sets the minimum font size.
        /// </summary>
        /// <value>
        /// The minimum font size.
        /// </value>
        [XmlElement(DataType = "double", ElementName = "MinFontSize")]
        public double MinFontSize { get; set; }

        /// <summary>
        /// Gets or sets the prompt.
        /// </summary>
        /// <value>
        /// The prompt.
        /// </value>
        [XmlElement(DataType = "string", ElementName = "Prompt")]
        public string Prompt { get; set; }

        /// <summary>
        /// Gets or sets the index of the style theme.
        /// </summary>
        /// <value>
        /// The index of the style theme.
        /// </value>
        [XmlElement(DataType = "int", ElementName = "StyleThemeIndex")]
        public int StyleThemeIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use internal command parsing.
        /// Otherwise commands must be parse by the user.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [use internal command parsing]; otherwise, <c>false</c>.
        /// </value>
        [XmlElement(DataType = "boolean", ElementName = "UseInternalCommandParsing")]
        public bool UseInternalCommandParsing { get; set; }

        /// <summary>
        /// Gets or sets the welcome message.
        /// </summary>
        /// <value>
        /// The welcome message.
        /// </value>
        [XmlElement(DataType = "string", ElementName = "WelcomeMessage")]
        public string WelcomeMessage { get; set; }
    }
}
