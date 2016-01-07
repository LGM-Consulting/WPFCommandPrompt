// Author: Adrian Hum
// Project: WPFCommandPrompt/WPFPrompt.cs
// 
// Created : 2016-01-06  18:29 
// Modified: 2016-01-06 18:36)

using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using WPFCommandPrompt.ConsoleSettings;
using WPFCommandPrompt.StyleThemes;

namespace WPFCommandPrompt {
    /// <summary>
    ///     Public interface for the console window.
    /// </summary>
    public class WpfPrompt {
        private const string SettingsFileName = "ConsoleSettings.xml";
        private ConsoleSettings.ConsoleSettings _consoleSettings;
        private ConsoleWindow _consoleWindow;

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="WpfPrompt" /> class with defaults.
        /// </summary>
        public WpfPrompt()
        {
            _consoleSettings = ConsoleSettingsManager.LoadDefaults();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="WpfPrompt" /> class.
        /// </summary>
        /// <param name="styleThemeIndex">The index number of the style theme to use.</param>
        public WpfPrompt(int styleThemeIndex)
        {
            _consoleSettings = ConsoleSettingsManager.LoadDefaults();
            _consoleSettings.StyleThemeIndex = styleThemeIndex;
        }

        #endregion

        #region Basic Properties

        /// <summary>
        ///     Allow an empty command to be written to the message area.
        ///     Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        ///     <c>true</c> if an empty command is processed; otherwise, <c>false</c>.
        /// </value>
        public bool AllowEmptyCommand
        {
            get { return _consoleSettings.AllowEmptyCommand; }
            set { _consoleSettings.AllowEmptyCommand = value; }
        }

        /// <summary>
        ///     Gets or sets the window title.
        ///     Default: WPF Command Prompt VX.x.x
        ///     Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        ///     The window title.
        /// </value>
        public string ConsoleTitle
        {
            get { return _consoleSettings.ConsoleTitle; }
            set
            {
                _consoleSettings.ConsoleTitle = value;
                _consoleWindow.ConsoleTitle = value;
            }
        }

        /// <summary>
        ///     Gets the console assembly version.
        ///     Can also use Utility.AssemblyVersion()
        /// </summary>
        public string ConsoleVersion
        {
            get { return Utility.AssemblyVersion(true, true, true); }
        }

        /// <summary>
        ///     Gets or sets the default height of the console.
        ///     Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        ///     The default height of the console.
        /// </value>
        public double DefaultConsoleHeight
        {
            get { return _consoleSettings.DefaultConsoleHeight; }
            set { _consoleSettings.DefaultConsoleHeight = value; }
        }

        /// <summary>
        ///     Gets or sets the default width of the console.
        ///     Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        ///     The default width of the console.
        /// </value>
        public double DefaultConsoleWidth
        {
            get { return _consoleSettings.DefaultConsoleWidth; }
            set { _consoleSettings.DefaultConsoleWidth = value; }
        }

        /// <summary>
        ///     Gets or sets the default prompt (Default: '>').
        ///     Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        ///     The default prompt.
        /// </value>
        public string DefaultPrompt
        {
            get { return _consoleSettings.DefaultPrompt; }
            set { _consoleSettings.DefaultPrompt = value; }
        }

        /// <summary>
        ///     Gets or sets the regular expression string delimiter value.
        ///     Default: ((""((?&lt;token&gt;.*?)"")|(?&lt;token&gt;[\w]+))(\s)*)
        ///     Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        ///     Regular expression string.
        /// </value>
        /// <exception cref="ArgumentException">Thrown when a supplied value is null or empty.</exception>
        public string Delimiters
        {
            get { return _consoleSettings.Delimiters; }
            set { _consoleSettings.Delimiters = value; }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether to allow the command history function (Default: true).
        ///     Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        ///     Allow command history if <c>true</c> ; otherwise, <c>false</c>.
        /// </value>
        public bool EnableCommandHistory
        {
            get { return _consoleSettings.EnableCommandHistory; }
            set { _consoleSettings.EnableCommandHistory = value; }
        }

        /// <summary>
        ///     Should the StyleThemeManager() attempt to load saved style themes upon console creation or reset.
        ///     Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        ///     <c>true</c> if [load style themes]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableLoadStyleThemes
        {
            get { return _consoleSettings.EnableLoadStyleThemes; }
            set { _consoleSettings.EnableLoadStyleThemes = value; }
        }

        /// <summary>
        ///     Is the command history managed by the console or by the user.
        ///     Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        ///     <c>true</c> if [manual command history]; otherwise, <c>false</c>.
        /// </value>
        public bool ManualCommandHistory
        {
            get { return _consoleSettings.ManualCommandHistory; }
            set { _consoleSettings.ManualCommandHistory = value; }
        }

        /// <summary>
        ///     Gets or sets the maximum allowed console height (Default: 0.00). if '0', no maximum.
        ///     Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        ///     Maximum console height.
        /// </value>
        public double MaxConsoleHeight
        {
            get { return _consoleSettings.MaxConsoleHeight; }
            set { _consoleSettings.MaxConsoleHeight = value; }
        }

        /// <summary>
        ///     Gets or sets the maximum allowed console width (Default: 0.00). if '0', no maximum.
        ///     Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        ///     Maximum console width.
        /// </value>
        public double MaxConsoleWidth
        {
            get { return _consoleSettings.MaxConsoleWidth; }
            set { _consoleSettings.MaxConsoleWidth = value; }
        }

        /// <summary>
        ///     Gets or Sets the maximum allowed font size of the console.
        ///     Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when font size is over max size.</exception>
        public double MaxFontSize
        {
            get { return _consoleSettings.MaxFontSize; }
            set { _consoleSettings.MaxFontSize = value; }
        }

        /// <summary>
        ///     Gets or sets the minimum allowed console height (Default: 400.00).
        ///     Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        ///     Minimum console height.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when height value is below zero.</exception>
        public double MinConsoleHeight
        {
            get { return _consoleSettings.MinConsoleHeight; }
            set { _consoleSettings.MinConsoleHeight = value; }
        }

        /// <summary>
        ///     Gets or sets the minimum allowed console width (Default: 600.00).
        ///     Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        ///     Minimum console width.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when width value is below zero.</exception>
        public double MinConsoleWidth
        {
            get { return _consoleSettings.MinConsoleWidth; }
            set { _consoleSettings.MinConsoleWidth = value; }
        }

        /// <summary>
        ///     Gets or sets the minimum allowed font size of the console.
        ///     Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when font size is below min size.</exception>
        public double MinFontSize
        {
            get { return _consoleSettings.MinFontSize; }
            set { _consoleSettings.MinFontSize = value; }
        }

        /// <summary>
        ///     Gets or sets the prompt string (Default: '>').
        ///     Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        ///     The command prompt string.
        /// </value>
        public string Prompt
        {
            get
            {
                if (_consoleWindow != null)
                {
                    return _consoleWindow.Prompt;
                }

                return _consoleSettings.Prompt;
            }
            set
            {
                _consoleSettings.Prompt = value;

                if (_consoleWindow != null)
                {
                    _consoleWindow.Prompt = value;
                }
            }
        }

        /// <summary>
        ///     Gets or sets the default style theme index number.
        ///     Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        ///     The style theme index number.
        /// </value>
        public int StyleThemeIndex
        {
            get { return _consoleSettings.StyleThemeIndex; }
            set { _consoleSettings.StyleThemeIndex = value; }
        }

        /// <summary>
        ///     Gets or sets the welcome message.
        ///     Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        ///     The welcome message.
        /// </value>
        public string WelcomeMessage
        {
            get { return _consoleSettings.WelcomeMessage; }
            set { _consoleSettings.WelcomeMessage = value; }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the console parses
        ///     commands to an array or returns a single string[0] of unparsed commands.
        ///     Default: true. If false, access the string with an index of 0.
        ///     Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        ///     <c>true</c> if [use internal command parsing]; otherwise, <c>false</c>.
        /// </value>
        public bool UseInternalCommandParsing
        {
            get { return _consoleSettings.UseInternalCommandParsing; }
            set { _consoleSettings.UseInternalCommandParsing = value; }
        }

        #endregion

        #region Font Properties

        /// <summary>
        ///     Gets or sets the command prommpt font. Style theme property.
        /// </summary>
        /// <value>
        ///     The command text font.
        /// </value>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public FontFamily CommandPromptFont
        {
            get
            {
                if (_consoleWindow != null)
                {
                    return _consoleWindow.CommandPromptFont;
                }
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
            set
            {
                if (_consoleWindow != null)
                {
                    _consoleWindow.CommandPromptFont = value;
                }
                else
                {
                    throw new NullReferenceException(
                        "ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the color of the command prompt text. Style theme property.
        /// </summary>
        /// <value>
        ///     The color of the command area text.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Brush CommandPromptTextColor
        {
            get
            {
                if (_consoleWindow != null)
                {
                    return _consoleWindow.CommandPromptTextColor;
                }
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
            set
            {
                if (_consoleWindow != null)
                {
                    _consoleWindow.CommandPromptTextColor = value;
                }
                else
                {
                    throw new NullReferenceException(
                        "ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the size of the command prompt font. Style theme property.
        /// </summary>
        /// <value>
        ///     The size of the command text font.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when font size is outside of min-max range.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public double CommandPromptFontSize
        {
            get
            {
                if (_consoleWindow != null)
                {
                    return _consoleWindow.CommandPromptFontSize;
                }
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
            set
            {
                if (_consoleWindow != null)
                {
                    _consoleWindow.CommandPromptFontSize = value;
                }
                else
                {
                    throw new NullReferenceException(
                        "ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the message area font. Style theme property.
        /// </summary>
        /// <value>
        ///     The message area font.
        /// </value>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public FontFamily MessageAreaFont
        {
            get
            {
                if (_consoleWindow != null)
                {
                    return _consoleWindow.MessageAreaFont;
                }
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
            set
            {
                if (_consoleWindow != null)
                {
                    _consoleWindow.MessageAreaFont = value;
                }
                else
                {
                    throw new NullReferenceException(
                        "ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the size of the message area font. Style theme property.
        /// </summary>
        /// <value>
        ///     The size of the message area font.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when font size is outside of min-max range.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public double MessageAreaFontSize
        {
            get
            {
                if (_consoleWindow != null)
                {
                    return _consoleWindow.MessageAreaFontSize;
                }
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
            set
            {
                if (_consoleWindow != null)
                {
                    _consoleWindow.MessageAreaFontSize = value;
                }
                else
                {
                    throw new NullReferenceException(
                        "ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the color of the message area prompt. Style theme property.
        /// </summary>
        /// <value>
        ///     The color of the message area prompt.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Brush MessagePromptColor
        {
            get
            {
                if (_consoleWindow != null)
                {
                    return _consoleWindow.MessagePromptColor;
                }
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
            set
            {
                if (_consoleWindow != null)
                {
                    _consoleWindow.MessagePromptColor = value;
                }
                else
                {
                    throw new NullReferenceException(
                        "ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the color of the message text. Style theme property.
        /// </summary>
        /// <value>
        ///     The color of the message text.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Brush MessageTextColor
        {
            get
            {
                if (_consoleWindow != null)
                {
                    return _consoleWindow.MessageTextColor;
                }
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
            set
            {
                if (_consoleWindow != null)
                {
                    _consoleWindow.MessageTextColor = value;
                }
                else
                {
                    throw new NullReferenceException(
                        "ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the color of the welcome message. Style theme property.
        /// </summary>
        /// <value>
        ///     The color of the welcome message.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Brush WelcomeMessageColor
        {
            get
            {
                if (_consoleWindow != null)
                {
                    return _consoleWindow.WelcomeMessageColor;
                }
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
            set
            {
                if (_consoleWindow != null)
                {
                    _consoleWindow.WelcomeMessageColor = value;
                }
                else
                {
                    throw new NullReferenceException(
                        "ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        #endregion

        #region Background Color Properties

        /// <summary>
        ///     Gets or sets the color of the command area background. Style theme property.
        /// </summary>
        /// <value>
        ///     The color of the command area background.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Brush CommandPromptBackgroundColor
        {
            get
            {
                if (_consoleWindow != null)
                {
                    return _consoleWindow.CommandPromptBackgroundColor;
                }
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
            set
            {
                if (_consoleWindow != null)
                {
                    _consoleWindow.CommandPromptBackgroundColor = value;
                }
                else
                {
                    throw new NullReferenceException(
                        "ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the color of the message area background. Style theme property.
        /// </summary>
        /// <value>
        ///     The color of the message area background.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Brush MessageBackgroundColor
        {
            get
            {
                if (_consoleWindow != null)
                {
                    return _consoleWindow.MessageAreaBackgroundColor;
                }
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
            set
            {
                if (_consoleWindow != null)
                {
                    _consoleWindow.MessageAreaBackgroundColor = value;
                }
                else
                {
                    throw new NullReferenceException(
                        "ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        #endregion

        #region Padding / Border Properties

        /// <summary>
        ///     Gets or sets the color of the command prompt border. Style theme property.
        /// </summary>
        /// <value>
        ///     The color of the command prompt border.
        /// </value>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Brush CommandPromptBorderColor
        {
            get
            {
                if (_consoleWindow != null)
                {
                    return _consoleWindow.CommandPromptBorderColor;
                }
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
            set
            {
                if (_consoleWindow != null)
                {
                    _consoleWindow.CommandPromptBorderColor = value;
                }
                else
                {
                    throw new NullReferenceException(
                        "ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the command border thickness. Style theme property.
        /// </summary>
        /// <value>
        ///     The command border thickness.
        /// </value>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Thickness CommandPromptBorderThickness
        {
            get
            {
                if (_consoleWindow != null)
                {
                    return _consoleWindow.CommandPromptBorderThickness;
                }
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
            set
            {
                if (_consoleWindow != null)
                {
                    _consoleWindow.CommandPromptBorderThickness = value;
                }
                else
                {
                    throw new NullReferenceException(
                        "ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the command prompt padding. Style theme property.
        /// </summary>
        /// <value>
        ///     The command padding.
        /// </value>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Thickness CommandPromptPadding
        {
            get
            {
                if (_consoleWindow != null)
                {
                    return _consoleWindow.CommandPromptPadding;
                }
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
            set
            {
                if (_consoleWindow != null)
                {
                    _consoleWindow.CommandPromptPadding = value;
                }
                else
                {
                    throw new NullReferenceException(
                        "ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the color of the message area border. Style theme property.
        /// </summary>
        /// <value>
        ///     The color of the message area border.
        /// </value>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Brush MessageAreaBorderColor
        {
            get
            {
                if (_consoleWindow != null)
                {
                    return _consoleWindow.MessageAreaBorderColor;
                }
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
            set
            {
                if (_consoleWindow != null)
                {
                    _consoleWindow.MessageAreaBorderColor = value;
                }
                else
                {
                    throw new NullReferenceException(
                        "ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the message area border thickness. Style theme property.
        /// </summary>
        /// <value>
        ///     The message area border thickness.
        /// </value>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Thickness MessageAreaBorderThickness
        {
            get
            {
                if (_consoleWindow != null)
                {
                    return _consoleWindow.MessageAreaBorderThickness;
                }
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
            set
            {
                if (_consoleWindow != null)
                {
                    _consoleWindow.MessageAreaBorderThickness = value;
                }
                else
                {
                    throw new NullReferenceException(
                        "ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the message area padding. Style theme property.
        /// </summary>
        /// <value>
        ///     The message area padding.
        /// </value>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Thickness MessageAreaPadding
        {
            get
            {
                if (_consoleWindow != null)
                {
                    return _consoleWindow.MessageAreaPadding;
                }
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
            set
            {
                if (_consoleWindow != null)
                {
                    _consoleWindow.MessageAreaPadding = value;
                }
                else
                {
                    throw new NullReferenceException(
                        "ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the message text paragraph margins. Style theme property.
        /// </summary>
        /// <value>
        ///     The message text paragraph margin.
        /// </value>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Thickness MessageTextParagraphMargin
        {
            get
            {
                if (_consoleWindow != null)
                {
                    return _consoleWindow.MessageTextParagraphMargin;
                }
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
            set
            {
                if (_consoleWindow != null)
                {
                    _consoleWindow.MessageTextParagraphMargin = value;
                }
                else
                {
                    throw new NullReferenceException(
                        "ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        #endregion

        #region Style Theme Control

        /// <summary>
        ///     Adds a style theme to the theme list.
        /// </summary>
        /// <param name="styleTheme">The color scheme to add.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void AddStyleTheme(StyleTheme styleTheme)
        {
            if (_consoleWindow != null)
            {
                _consoleWindow.AddStyleTheme(styleTheme);
            }
            else
            {
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        ///     Changes the name of the current style theme.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        public void CurrentThemeName(string themeName)
        {
            _consoleWindow.CurrentThemeName(themeName);
        }

        /// <summary>
        ///     Adds the current style theme and any changes as a new theme. Not saved to disk.
        /// </summary>
        public void CurrentThemeToNew()
        {
            _consoleWindow.CurrentThemeToNew();
        }

        /// <summary>
        ///     Gets the the description of the current style theme.
        /// </summary>
        /// <returns>Description of the current color scheme as a string</returns>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public string CurrentStyleThemeDetails()
        {
            if (_consoleWindow != null)
            {
                return _consoleWindow.CurrentStyleThemeDetails();
            }
            throw new NullReferenceException(
                "ConsoleWindow() class not instantiated. Call Show() to create console window.");
        }

        /// <summary>
        ///     Deletes a style theme from the list, not from disk.
        /// </summary>
        /// <param name="styleThemeIndex">Index of the style theme.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when the value is outside of the list index range.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void DeleteStyleTheme(int styleThemeIndex)
        {
            if (_consoleWindow != null)
            {
                _consoleWindow.DeleteStyleTheme(styleThemeIndex);
            }
            else
            {
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        ///     Gets a listing of the style themes.
        /// </summary>
        /// <returns>A String list of the color schemes</returns>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public string GetStyleThemeList()
        {
            if (_consoleWindow != null)
            {
                return _consoleWindow.GetStyleThemeList();
            }
            throw new NullReferenceException(
                "ConsoleWindow() class not instantiated. Call Show() to create console window.");
        }

        /// <summary>
        ///     Loads the style themes from disk.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void LoadStyleThemes()
        {
            if (_consoleWindow != null)
            {
                _consoleWindow.LoadStyleThemes();
            }
            else
            {
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        ///     Resets the console theme. Any changes are discarded.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void ResetStyleTheme()
        {
            if (_consoleWindow != null)
            {
                _consoleWindow.ResetStyleTheme();
            }
            else
            {
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        ///     Saves the style themes to disk.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void SaveStyleThemes()
        {
            if (_consoleWindow != null)
            {
                _consoleWindow.SaveStyleThemes();
            }
            else
            {
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        ///     Sets the style theme.
        /// </summary>
        /// <param name="styleThemeIndex">The index of the color scheme.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when the value is outside of the list index range.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void SetStyleTheme(int styleThemeIndex)
        {
            if (_consoleWindow != null)
            {
                _consoleSettings.StyleThemeIndex = styleThemeIndex;
                _consoleWindow.SetStyleTheme(styleThemeIndex);
            }
            else
            {
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        ///     Updates the origional style theme with the current changes. Does not save to disk.
        /// </summary>
        public void UpdateStyleTheme()
        {
            _consoleWindow.UpdateStyleTheme();
        }

        #endregion

        #region Command History Control

        /// <summary>
        ///     Manually adds a command string to the command history.
        /// </summary>
        /// <param name="command">The command to add.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void AddToCommandHistory(string command)
        {
            if (_consoleWindow != null)
            {
                _consoleWindow.AddToCommandHistory(command);
            }
            else
            {
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        ///     Clears the command history.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void ClearCommandHistory()
        {
            if (_consoleWindow != null)
            {
                _consoleWindow.ClearCommandHistory();
            }
            else
            {
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        ///     Gets the command at the selected index number.
        /// </summary>
        /// <param name="indexNumber">The index number.</param>
        /// <returns>string</returns>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public string GetCommand(int indexNumber)
        {
            if (_consoleWindow != null)
            {
                return _consoleWindow.GetCommand(indexNumber);
            }
            throw new NullReferenceException(
                "ConsoleWindow() class not instantiated. Call Show() to create console window.");
        }

        /// <summary>
        ///     Gets the next command.
        /// </summary>
        /// <returns>The next command in the command history as a string.</returns>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public string GetNextCommand()
        {
            if (_consoleWindow != null)
            {
                return _consoleWindow.GetNextCommand();
            }
            throw new NullReferenceException(
                "ConsoleWindow() class not instantiated. Call Show() to create console window.");
        }

        /// <summary>
        ///     Gets the previous command.
        /// </summary>
        /// <returns>The previous command in the command history as a string.</returns>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public string GetPreviousCommand()
        {
            if (_consoleWindow != null)
            {
                return _consoleWindow.GetPreviousCommand();
            }
            throw new NullReferenceException(
                "ConsoleWindow() class not instantiated. Call Show() to create console window.");
        }

        /// <summary>
        ///     Loads the command history from an xml file using the default directory file name.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void LoadCommandHistory()
        {
            LoadCommandHistory(Environment.CurrentDirectory, string.Empty);
        }

        /// <summary>
        ///     Loads the command history from an xml file in the current directory with
        ///     the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void LoadCommandHistory(string fileName)
        {
            LoadCommandHistory(Environment.CurrentDirectory, fileName);
        }

        /// <summary>
        ///     Loads the command history from an xml file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void LoadCommandHistory(string path, string fileName)
        {
            if (_consoleWindow != null)
            {
                _consoleWindow.LoadCommandHistory(path, fileName);
            }
            else
            {
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        ///     Removes a command from the command history.
        /// </summary>
        /// <param name="indexNumber">The index number of the command to remove.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void RemoveFromCommandHistory(int indexNumber)
        {
            if (_consoleWindow != null)
            {
                _consoleWindow.RemoveFromCommandHistory(indexNumber);
            }
            else
            {
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        ///     Resets the command history.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void ResetCommandHistory()
        {
            if (_consoleWindow != null)
            {
                _consoleWindow.ResetCommandHistory();
            }
            else
            {
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        ///     Saves the command history to the default path and file.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void SaveCommandHistory()
        {
            SaveCommandHistory(Environment.CurrentDirectory, string.Empty);
        }

        /// <summary>
        ///     Saves the command history to an xml file in the current directory with
        ///     the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void SaveCommandHistory(string fileName)
        {
            SaveCommandHistory(Environment.CurrentDirectory, fileName);
        }

        /// <summary>
        ///     Saves the command history to an xml file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void SaveCommandHistory(string path, string fileName)
        {
            if (_consoleWindow != null)
            {
                _consoleWindow.SaveCommandHistory(path, fileName);
            }
            else
            {
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        #endregion

        #region Console Control

        /// <summary>
        ///     Clears the console of all content.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void ClearConsole()
        {
            if (_consoleWindow != null)
            {
                _consoleWindow.ClearConsole();
            }
            else
            {
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        ///     Closes the current console window instance.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void Close()
        {
            if (_consoleWindow != null)
            {
                _consoleWindow.CloseConsole();
            }
            else
            {
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        ///     Gets or sets the height of the console.
        /// </summary>
        /// <value>
        ///     The height of the console.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when window height is outside of min-max range.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public double ConsoleHeight
        {
            get
            {
                if (_consoleWindow != null)
                {
                    return _consoleWindow.ConsoleHeight;
                }
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
            set
            {
                if (_consoleWindow != null)
                {
                    _consoleWindow.ConsoleHeight = value;
                }
                else
                {
                    throw new NullReferenceException(
                        "ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the width of the console.
        /// </summary>
        /// <value>
        ///     The width of the console.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when window width is outside of min-max range.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public double ConsoleWidth
        {
            get
            {
                if (_consoleWindow != null)
                {
                    return _consoleWindow.ConsoleWidth;
                }
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
            set
            {
                if (_consoleWindow != null)
                {
                    _consoleWindow.ConsoleWidth = value;
                }
                else
                {
                    throw new NullReferenceException(
                        "ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        ///     Hides the console window.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void Hide()
        {
            if (_consoleWindow != null)
            {
                _consoleWindow.HideConsole();
            }
            else
            {
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        ///     Resets the console to default settings.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void ResetConsole()
        {
            if (_consoleWindow != null)
            {
                _consoleWindow.ResetConsole(_consoleSettings);
            }
            else
            {
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        ///     Resets the size of the console to default.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void ResetConsoleSize()
        {
            if (_consoleWindow != null)
            {
                _consoleWindow.ResetConsoleSize();
            }
            else
            {
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        ///     Shows the console window.
        /// </summary>
        public void Show()
        {
            if (_consoleWindow == null)
            {
                if (_consoleSettings == null)
                {
                    _consoleSettings = ConsoleSettingsManager.LoadDefaults();
                }
                _consoleWindow = new ConsoleWindow(_consoleSettings);
                _consoleWindow.ConsoleReadLine += OnConsoleReadEvent;
                ConsoleWriteLine += _consoleWindow.WriteLine;
            }

            _consoleWindow.ShowConsole();
        }

        #endregion

        #region Save/Load Console Settings

        /// <summary>
        ///     Loads the settings from the default path and file.
        ///     Uses defaults if configuration file does not exist.
        /// </summary>
        public void LoadSettings()
        {
            _consoleSettings = ConsoleSettingsManager.LoadSettings(Environment.CurrentDirectory, SettingsFileName);
        }

        /// <summary>
        ///     Loads the settings from the default path and specified file name.
        ///     Uses defaults if configuration file does not exist.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void LoadSettings(string fileName)
        {
            _consoleSettings = ConsoleSettingsManager.LoadSettings(Environment.CurrentDirectory, fileName);
        }

        /// <summary>
        ///     Loads the settings from a specified path and file name.
        ///     Uses defaults if configuration file does not exist.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        public void LoadSettings(string path, string fileName)
        {
            _consoleSettings = ConsoleSettingsManager.LoadSettings(path, fileName);
        }

        /// <summary>
        ///     Saves the settings to the default path and file.
        /// </summary>
        public void SaveSettings()
        {
            ConsoleSettingsManager.SaveSettings(Environment.CurrentDirectory, SettingsFileName, _consoleSettings);
        }

        /// <summary>
        ///     Saves the settings to the default path and specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void SaveSettings(string fileName)
        {
            ConsoleSettingsManager.SaveSettings(Environment.CurrentDirectory, fileName, _consoleSettings);
        }

        /// <summary>
        ///     Saves the settings to a specified path and file name.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        public void SaveSettings(string path, string fileName)
        {
            ConsoleSettingsManager.SaveSettings(path, fileName, _consoleSettings);
        }

        #endregion

        #region Console Write Line

        /// <summary>
        ///     Sends a string to the console output.
        /// </summary>
        /// <param name="output">String to output to console.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void WriteLine(string output)
        {
            if (_consoleWindow != null)
            {
                WriteLine(this, new ConsoleWriteLineEventArgs(output));
            }
            else
            {
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        ///     Sends a string to the console output with the desired font color.
        /// </summary>
        /// <param name="output">String to output to console.</param>
        /// <param name="foreground">The text color for text.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void WriteLine(string output, Brush foreground)
        {
            if (_consoleWindow != null)
            {
                WriteLine(this, new ConsoleWriteLineEventArgs(output, foreground));
            }
            else
            {
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        ///     Writes a FlowDocument paragraph to the console.
        /// </summary>
        /// <param name="paragraph">The paragraph to send to console.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void WriteLine(Paragraph paragraph)
        {
            if (_consoleWindow != null)
            {
                WriteLine(this, new ConsoleWriteLineEventArgs(paragraph));
            }
            else
            {
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        ///     Sends a ConsoleWriteLineEventArgs object to the console output.
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="e">The <see cref="WPFCommandPrompt.ConsoleWriteLineEventArgs" /> instance containing the event data.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void WriteLine(object sender, ConsoleWriteLineEventArgs e)
        {
            if (_consoleWindow != null)
            {
                ConsoleWriteLine(sender, e);
            }
            else
            {
                throw new NullReferenceException(
                    "ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents the message area text.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (_consoleWindow != null)
            {
                return _consoleWindow.ToString();
            }
            return string.Empty;
        }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when writing text to the console.
        /// </summary>
        public event WriteLineEventHandler ConsoleWriteLine;

        /// <summary>
        ///     Called when writing text to the console.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="WPFCommandPrompt.ConsoleWriteLineEventArgs" /> instance containing the event data.</param>
        public void OnConsoleWriteEvent(object sender, ConsoleWriteLineEventArgs e)
        {
            if (ConsoleWriteLine != null)
            {
                ConsoleWriteLine(sender, e);
            }
        }

        /// <summary>
        ///     Occurs on a console read line update event.
        /// </summary>
        public event ReadLineEventHandler ReadLine;

        /// <summary>
        ///     Called on a console read line upate event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="WPFCommandPrompt.ConsoleReadLineEventArgs" /> instance containing the event data.</param>
        public void OnConsoleReadEvent(object sender, ConsoleReadLineEventArgs e)
        {
            if (ReadLine != null)
            {
                ReadLine(this, e);
            }
        }

        #endregion
    }
}