// Author: Adrian Hum
// Project: WPFCommandPrompt/StyleTheme.cs
// 
// Created : 2016-01-06  18:29 
// Modified: 2016-01-06 18:35)

using System.Xml.Serialization;

namespace WPFCommandPrompt.StyleThemes {
    /// <summary>
    ///     Style Theme settings
    /// </summary>
    public struct StyleTheme {
        /// <summary>
        ///     Gets or sets the style theme index ID.
        /// </summary>
        /// <value>
        ///     The style theme index ID.
        /// </value>
        [XmlElement(DataType = "int", ElementName = "StyleThemeIndexID")]
        public int StyleThemeIndexId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the style theme.
        /// </summary>
        /// <value>
        ///     The name of the style theme.
        /// </value>
        [XmlElement(DataType = "string", ElementName = "StyleThemeName")]
        public string StyleThemeName { get; set; }

        /// <summary>
        ///     Gets or sets the color of the command background.
        /// </summary>
        /// <value>
        ///     The color of the command background.
        /// </value>
        [XmlElement(DataType = "string", ElementName = "CommandPromptBackgroundColor")]
        public string CommandPromptBackgroundColor { get; set; }

        /// <summary>
        ///     Gets or sets the color of the command prompt border.
        /// </summary>
        /// <value>
        ///     The color of the command prompt border.
        /// </value>
        [XmlElement(DataType = "string", ElementName = "CommandPromptBorderColor")]
        public string CommandPromptBorderColor { get; set; }

        /// <summary>
        ///     Gets or sets the command prompt border thickness.
        /// </summary>
        /// <value>
        ///     The command prompt border thickness.
        /// </value>
        [XmlElement(DataType = "string", ElementName = "CommandPromptBorderThickness")]
        public string CommandPromptBorderThickness { get; set; }

        /// <summary>
        ///     Gets or sets the color of the command prompt text.
        /// </summary>
        /// <value>
        ///     The color of the command prompt text.
        /// </value>
        [XmlElement(DataType = "string", ElementName = "CommandPromptTextColor")]
        public string CommandPromptTextColor { get; set; }

        /// <summary>
        ///     Gets or sets the command PRMMPT font.
        /// </summary>
        /// <value>
        ///     The command PRMMPT font.
        /// </value>
        [XmlElement(DataType = "string", ElementName = "CommandPromptFont")]
        public string CommandPromptFont { get; set; }

        /// <summary>
        ///     Gets or sets the size of the command text font.
        /// </summary>
        /// <value>
        ///     The size of the command text font.
        /// </value>
        [XmlElement(DataType = "double", ElementName = "CommandPromptFontSize")]
        public double CommandPromptFontSize { get; set; }

        /// <summary>
        ///     Gets or sets the command prompt padding.
        /// </summary>
        /// <value>
        ///     The command prompt padding.
        /// </value>
        [XmlElement(DataType = "string", ElementName = "CommandPromptPadding")]
        public string CommandPromptPadding { get; set; }

        /// <summary>
        ///     Gets or sets the message area font.
        /// </summary>
        /// <value>
        ///     The message area font.
        /// </value>
        [XmlElement(DataType = "string", ElementName = "MessageAreaFont")]
        public string MessageAreaFont { get; set; }

        /// <summary>
        ///     Gets or sets the size of the message area font.
        /// </summary>
        /// <value>
        ///     The size of the message area font.
        /// </value>
        [XmlElement(DataType = "double", ElementName = "MessageAreaFontSize")]
        public double MessageAreaFontSize { get; set; }

        /// <summary>
        ///     Gets or sets the color of the message area background.
        /// </summary>
        /// <value>
        ///     The color of the message area background.
        /// </value>
        [XmlElement(DataType = "string", ElementName = "MessageAreaBackgroundColor")]
        public string MessageAreaBackgroundColor { get; set; }

        /// <summary>
        ///     Gets or sets the color of the message area border.
        /// </summary>
        /// <value>
        ///     The color of the message area border.
        /// </value>
        [XmlElement(DataType = "string", ElementName = "MessageAreaBorderColor")]
        public string MessageAreaBorderColor { get; set; }

        /// <summary>
        ///     Gets or sets the message area border thickness.
        /// </summary>
        /// <value>
        ///     The message area border thickness.
        /// </value>
        [XmlElement(DataType = "string", ElementName = "MessageAreaBorderThickness")]
        public string MessageAreaBorderThickness { get; set; }

        /// <summary>
        ///     Gets or sets the message area padding.
        /// </summary>
        /// <value>
        ///     The message area padding.
        /// </value>
        [XmlElement(DataType = "string", ElementName = "MessageAreaPadding")]
        public string MessageAreaPadding { get; set; }

        /// <summary>
        ///     Gets or sets the color of the message prompt.
        /// </summary>
        /// <value>
        ///     The color of the message prompt.
        /// </value>
        [XmlElement(DataType = "string", ElementName = "MessagePromptColor")]
        public string MessagePromptColor { get; set; }

        /// <summary>
        ///     Gets or sets the color of the message text.
        /// </summary>
        /// <value>
        ///     The color of the message text.
        /// </value>
        [XmlElement(DataType = "string", ElementName = "MessageTextColor")]
        public string MessageTextColor { get; set; }

        /// <summary>
        ///     Gets or sets the message text paragraph margin.
        /// </summary>
        /// <value>
        ///     The message text paragraph margin.
        /// </value>
        [XmlElement(DataType = "string", ElementName = "MessageTextParagraphMargin")]
        public string MessageTextParagraphMargin { get; set; }

        /// <summary>
        ///     Gets or sets the color of the welcome message.
        /// </summary>
        /// <value>
        ///     The color of the welcome message.
        /// </value>
        [XmlElement(DataType = "string", ElementName = "WelcomeMessageColor")]
        public string WelcomeMessageColor { get; set; }
    }
}