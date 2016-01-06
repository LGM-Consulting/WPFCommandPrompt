using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using System.Windows.Documents;

namespace WPFCommandPrompt
{
    /// <summary>
    /// Public interface for the console window.
    /// </summary>
    public class WPFPrompt
    {
        private ConsoleWindow consoleWindow;
        private ConsoleSettings consoleSettings;
        private const string settingsFileName = "ConsoleSettings.xml";

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WPFPrompt"/> class with defaults.
        /// </summary>
        public WPFPrompt()
        {
            consoleSettings = ConsoleSettingsManager.LoadDefaults();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WPFPrompt"/> class.
        /// </summary>
        /// <param name="styleThemeIndex">The index number of the style theme to use.</param>
        public WPFPrompt(int styleThemeIndex)
        {
            consoleSettings = ConsoleSettingsManager.LoadDefaults();
            consoleSettings.StyleThemeIndex = styleThemeIndex;
        }

        #endregion

        #region Basic Properties

        /// <summary>
        /// Allow an empty command to be written to the message area.
        /// Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        ///   <c>true</c> if an empty command is processed; otherwise, <c>false</c>.
        /// </value>
        public bool AllowEmptyCommand
        {
            get { return consoleSettings.AllowEmptyCommand; }
            set { consoleSettings.AllowEmptyCommand = value; }
        }

        /// <summary>
        /// Gets or sets the window title.
        /// Default: WPF Command Prompt VX.x.x
        /// Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        /// The window title.
        /// </value>
        public string ConsoleTitle
        {
            get { return consoleSettings.ConsoleTitle; }
            set 
            { 
                consoleSettings.ConsoleTitle = value;
                consoleWindow.ConsoleTitle = value;
            }
        }

        /// <summary>
        /// Gets the console assembly version.
        /// Can also use Utility.AssemblyVersion()
        /// </summary>
        public string ConsoleVersion
        {
            get { return Utility.AssemblyVersion(true, true, true); }
        }

        /// <summary>
        /// Gets or sets the default height of the console.
        /// Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        /// The default height of the console.
        /// </value>
        public double DefaultConsoleHeight
        {
            get { return consoleSettings.DefaultConsoleHeight; }
            set { consoleSettings.DefaultConsoleHeight = value; }
        }

        /// <summary>
        /// Gets or sets the default width of the console.
        /// Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        /// The default width of the console.
        /// </value>
        public double DefaultConsoleWidth
        {
            get { return consoleSettings.DefaultConsoleWidth; }
            set { consoleSettings.DefaultConsoleWidth = value; }
        }

        /// <summary>
        /// Gets or sets the default prompt (Default: '>').
        /// Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        /// The default prompt.
        /// </value>
        public string DefaultPrompt
        {
            get { return consoleSettings.DefaultPrompt; }
            set { consoleSettings.DefaultPrompt = value; }
        }

        /// <summary>
        /// Gets or sets the regular expression string delimiter value.
        /// Default: ((""((?&lt;token&gt;.*?)"")|(?&lt;token&gt;[\w]+))(\s)*)
        /// Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        /// Regular expression string.
        /// </value>
        /// <exception cref="ArgumentException">Thrown when a supplied value is null or empty.</exception>
        public string Delimiters
        {
            get { return consoleSettings.Delimiters; }
            set { consoleSettings.Delimiters = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to allow the command history function (Default: true).
        /// Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        /// Allow command history if <c>true</c> ; otherwise, <c>false</c>.
        /// </value>
        public bool EnableCommandHistory
        {
            get { return consoleSettings.EnableCommandHistory; }
            set { consoleSettings.EnableCommandHistory = value; }
        }

        /// <summary>
        /// Should the StyleThemeManager() attempt to load saved style themes upon console creation or reset.
        /// Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [load style themes]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableLoadStyleThemes
        {
            get { return consoleSettings.EnableLoadStyleThemes; }
            set { consoleSettings.EnableLoadStyleThemes = value; }
        }

        /// <summary>
        /// Is the command history managed by the console or by the user.
        /// Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [manual command history]; otherwise, <c>false</c>.
        /// </value>
        public bool ManualCommandHistory
        {
            get { return consoleSettings.ManualCommandHistory; }
            set { consoleSettings.ManualCommandHistory = value; }
        }

        /// <summary>
        /// Gets or sets the maximum allowed console height (Default: 0.00). if '0', no maximum.
        /// Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        /// Maximum console height.
        /// </value>
        public double MaxConsoleHeight
        {
            get { return consoleSettings.MaxConsoleHeight; }
            set { consoleSettings.MaxConsoleHeight = value; }
        }

        /// <summary>
        /// Gets or sets the maximum allowed console width (Default: 0.00). if '0', no maximum.
        /// Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        /// Maximum console width.
        /// </value>
        public double MaxConsoleWidth
        {
            get { return consoleSettings.MaxConsoleWidth; }
            set { consoleSettings.MaxConsoleWidth = value; }
        }

        /// <summary>
        /// Gets or Sets the maximum allowed font size of the console.
        /// Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when font size is over max size.</exception>
        public double MaxFontSize
        {
            get { return consoleSettings.MaxFontSize; }
            set { consoleSettings.MaxFontSize = value; }
        }

        /// <summary>
        /// Gets or sets the minimum allowed console height (Default: 400.00).
        /// Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        /// Minimum console height.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when height value is below zero.</exception>
        public double MinConsoleHeight
        {
            get { return consoleSettings.MinConsoleHeight; }
            set { consoleSettings.MinConsoleHeight = value; }
        }

        /// <summary>
        /// Gets or sets the minimum allowed console width (Default: 600.00).
        /// Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        /// Minimum console width.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when width value is below zero.</exception>
        public double MinConsoleWidth
        {
            get { return consoleSettings.MinConsoleWidth; }
            set { consoleSettings.MinConsoleWidth = value; }
        }

        /// <summary>
        /// Gets or sets the minimum allowed font size of the console.
        /// Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when font size is below min size.</exception>
        public double MinFontSize
        {
            get { return consoleSettings.MinFontSize; }
            set { consoleSettings.MinFontSize = value; }
        }

        /// <summary>
        /// Gets or sets the prompt string (Default: '>').
        /// Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        /// The command prompt string.
        /// </value>
        public string Prompt
        {
            get 
            {
                if (consoleWindow != null)
                {
                    return consoleWindow.Prompt; 
                }

                return consoleSettings.Prompt;
            }
            set 
            {
                consoleSettings.Prompt = value;

                if (consoleWindow != null)
                {
                    consoleWindow.Prompt = value; 
                }
            }
        }

        /// <summary>
        /// Gets or sets the default style theme index number.
        /// Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        /// The style theme index number.
        /// </value>
        public int StyleThemeIndex
        {
            get { return consoleSettings.StyleThemeIndex; }
            set { consoleSettings.StyleThemeIndex = value; }
        }

        /// <summary>
        /// Gets or sets the welcome message.
        /// Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        /// The welcome message.
        /// </value>
        public string WelcomeMessage
        {
            get { return consoleSettings.WelcomeMessage; }
            set { consoleSettings.WelcomeMessage = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the console parses 
        /// commands to an array or returns a single string[0] of unparsed commands.
        /// Default: true. If false, access the string with an index of 0. 
        /// Memeber of the ConsoleSettings() class object.
        /// </summary>
        /// <value>
        /// <c>true</c> if [use internal command parsing]; otherwise, <c>false</c>.
        /// </value>
        public bool UseInternalCommandParsing
        {
            get { return consoleSettings.UseInternalCommandParsing; }
            set { consoleSettings.UseInternalCommandParsing = value; }
        }

        #endregion

        #region Font Properties

        /// <summary>
        /// Gets or sets the command prommpt font. Style theme property.
        /// </summary>
        /// <value>
        /// The command text font.
        /// </value>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public FontFamily CommandPromptFont
        {
            get 
            {
                if (consoleWindow != null)
                {
                    return consoleWindow.CommandPromptFont; 
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
            set 
            {
                if (consoleWindow != null)
                {
                    consoleWindow.CommandPromptFont = value;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the command prompt text. Style theme property.
        /// </summary>
        /// <value>
        /// The color of the command area text.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Brush CommandPromptTextColor
        {
            get
            {
                if (consoleWindow != null)
                {
                    return consoleWindow.CommandPromptTextColor;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
            set
            {
                if (consoleWindow != null)
                {
                    consoleWindow.CommandPromptTextColor = value;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the command prompt font. Style theme property.
        /// </summary>
        /// <value>
        /// The size of the command text font.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when font size is outside of min-max range.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public double CommandPromptFontSize
        {
            get
            {
                if (consoleWindow != null)
                {
                    return consoleWindow.CommandPromptFontSize;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
            set
            {
                if (consoleWindow != null)
                {
                    consoleWindow.CommandPromptFontSize = value;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the message area font. Style theme property.
        /// </summary>
        /// <value>
        /// The message area font.
        /// </value>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public FontFamily MessageAreaFont
        {
            get 
            {
                if (consoleWindow != null)
                {
                    return consoleWindow.MessageAreaFont; 
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
            set 
            {
                if (consoleWindow != null)
                {
                    consoleWindow.MessageAreaFont = value; 
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the message area font. Style theme property.
        /// </summary>
        /// <value>
        /// The size of the message area font.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when font size is outside of min-max range.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public double MessageAreaFontSize
        {
            get
            {
                if (consoleWindow != null)
                {
                    return consoleWindow.MessageAreaFontSize;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
            set
            {
                if (consoleWindow != null)
                {
                    consoleWindow.MessageAreaFontSize = value;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the message area prompt. Style theme property.
        /// </summary>
        /// <value>
        /// The color of the message area prompt.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Brush MessagePromptColor
        {
            get
            {
                if (consoleWindow != null)
                {
                    return consoleWindow.MessagePromptColor;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
            set
            {
                if (consoleWindow != null)
                {
                    consoleWindow.MessagePromptColor = value;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the message text. Style theme property.
        /// </summary>
        /// <value>
        /// The color of the message text.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Brush MessageTextColor
        {
            get
            {
                if (consoleWindow != null)
                {
                    return consoleWindow.MessageTextColor;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
            set
            {
                if (consoleWindow != null)
                {
                    consoleWindow.MessageTextColor = value;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the welcome message. Style theme property.
        /// </summary>
        /// <value>
        /// The color of the welcome message.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Brush WelcomeMessageColor
        {
            get
            {
                if (consoleWindow != null)
                {
                    return consoleWindow.WelcomeMessageColor;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
            set
            {
                if (consoleWindow != null)
                {
                    consoleWindow.WelcomeMessageColor = value;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        #endregion

        #region Background Color Properties

        /// <summary>
        /// Gets or sets the color of the command area background. Style theme property.
        /// </summary>
        /// <value>
        /// The color of the command area background.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Brush CommandPromptBackgroundColor
        {
            get
            {
                if (consoleWindow != null)
                {
                    return consoleWindow.CommandPromptBackgroundColor;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
            set
            {
                if (consoleWindow != null)
                {
                    consoleWindow.CommandPromptBackgroundColor = value;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the message area background. Style theme property.
        /// </summary>
        /// <value>
        /// The color of the message area background.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Brush MessageBackgroundColor
        {
            get
            {
                if (consoleWindow != null)
                {
                    return consoleWindow.MessageAreaBackgroundColor; 
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
            set 
            {
                if (consoleWindow != null)
                {
                    consoleWindow.MessageAreaBackgroundColor = value; 
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        #endregion

        #region Padding / Border Properties

        /// <summary>
        /// Gets or sets the color of the command prompt border. Style theme property.
        /// </summary>
        /// <value>
        /// The color of the command prompt border.
        /// </value>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Brush CommandPromptBorderColor
        {
            get
            {
                if (consoleWindow != null)
                {
                    return consoleWindow.CommandPromptBorderColor;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
            set
            {
                if (consoleWindow != null)
                {
                    consoleWindow.CommandPromptBorderColor = value;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the command border thickness. Style theme property.
        /// </summary>
        /// <value>
        /// The command border thickness.
        /// </value>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Thickness CommandPromptBorderThickness
        {
            get 
            {
                if (consoleWindow != null)
                {
                    return consoleWindow.CommandPromptBorderThickness;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
            set
            {
                if (consoleWindow != null)
                {
                    consoleWindow.CommandPromptBorderThickness = value; 
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the command prompt padding. Style theme property.
        /// </summary>
        /// <value>
        /// The command padding.
        /// </value>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Thickness CommandPromptPadding
        {
            get
            {
                if (consoleWindow != null)
                {
                    return consoleWindow.CommandPromptPadding;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
            set 
            {
                if (consoleWindow != null)
                {
                    consoleWindow.CommandPromptPadding = value;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the message area border. Style theme property.
        /// </summary>
        /// <value>
        /// The color of the message area border.
        /// </value>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Brush MessageAreaBorderColor
        {
            get
            {
                if (consoleWindow != null)
                {
                    return consoleWindow.MessageAreaBorderColor;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
            set
            {
                if (consoleWindow != null)
                {
                    consoleWindow.MessageAreaBorderColor = value;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the message area border thickness. Style theme property.
        /// </summary>
        /// <value>
        /// The message area border thickness.
        /// </value>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Thickness MessageAreaBorderThickness
        {
            get 
            {
                if (consoleWindow != null)
                {
                    return consoleWindow.MessageAreaBorderThickness;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
            set 
            {
                if (consoleWindow != null)
                {
                    consoleWindow.MessageAreaBorderThickness = value; 
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the message area padding. Style theme property.
        /// </summary>
        /// <value>
        /// The message area padding.
        /// </value>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Thickness MessageAreaPadding
        {
            get 
            {
                if (consoleWindow != null)
                {
                    return consoleWindow.MessageAreaPadding; 
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
            set
            {
                if (consoleWindow != null)
                {
                    consoleWindow.MessageAreaPadding = value; 
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the message text paragraph margins. Style theme property.
        /// </summary>
        /// <value>
        /// The message text paragraph margin.
        /// </value>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Thickness MessageTextParagraphMargin
        {
            get
            {
                if (consoleWindow != null)
                {
                    return consoleWindow.MessageTextParagraphMargin;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
            set
            {
                if (consoleWindow != null)
                {
                    consoleWindow.MessageTextParagraphMargin = value; 
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        #endregion

        #region Style Theme Control

        /// <summary>
        /// Adds a style theme to the theme list.
        /// </summary>
        /// <param name="styleTheme">The color scheme to add.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void AddStyleTheme(StyleTheme styleTheme)
        {
            if (consoleWindow != null)
            {
                consoleWindow.AddStyleTheme(styleTheme);
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Changes the name of the current style theme.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        public void CurrentThemeName(string themeName)
        {
            consoleWindow.CurrentThemeName(themeName);
        }

        /// <summary>
        /// Adds the current style theme and any changes as a new theme. Not saved to disk.
        /// </summary>
        public void CurrentThemeToNew()
        {
            consoleWindow.CurrentThemeToNew();
        }

        /// <summary>
        /// Gets the the description of the current style theme.
        /// </summary>
        /// <returns>Description of the current color scheme as a string</returns>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public string CurrentStyleThemeDetails()
        {
            if (consoleWindow != null)
            {
                return consoleWindow.CurrentStyleThemeDetails();
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Deletes a style theme from the list, not from disk.
        /// </summary>
        /// <param name="styleThemeIndex">Index of the style theme.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when the value is outside of the list index range.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void DeleteStyleTheme(int styleThemeIndex)
        {
            if (consoleWindow != null)
            {
                consoleWindow.DeleteStyleTheme(styleThemeIndex);
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Gets a listing of the style themes.
        /// </summary>
        /// <returns>A String list of the color schemes</returns>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public string GetStyleThemeList()
        {
            if (consoleWindow != null)
            {
                return consoleWindow.GetStyleThemeList();
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Loads the style themes from disk.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void LoadStyleThemes()
        {
            if (consoleWindow != null)
            {
                consoleWindow.LoadStyleThemes();
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Resets the console theme. Any changes are discarded.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void ResetStyleTheme()
        {
            if (consoleWindow != null)
            {
                consoleWindow.ResetStyleTheme();
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Saves the style themes to disk.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void SaveStyleThemes()
        {
            if (consoleWindow != null)
            {
                consoleWindow.SaveStyleThemes();
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Sets the style theme.
        /// </summary>
        /// <param name="styleThemeIndex">The index of the color scheme.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when the value is outside of the list index range.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void SetStyleTheme(int styleThemeIndex)
        {
            if (consoleWindow != null)
            {
                consoleSettings.StyleThemeIndex = styleThemeIndex;
                consoleWindow.SetStyleTheme(styleThemeIndex);
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Updates the origional style theme with the current changes. Does not save to disk.
        /// </summary>
        public void UpdateStyleTheme()
        {
            consoleWindow.UpdateStyleTheme();
        }

        #endregion

        #region Command History Control

        /// <summary>
        /// Manually adds a command string to the command history.
        /// </summary>
        /// <param name="command">The command to add.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void AddToCommandHistory(string command)
        {
            if (consoleWindow != null)
            {
                consoleWindow.AddToCommandHistory(command);
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Clears the command history.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void ClearCommandHistory()
        {
            if (consoleWindow != null)
            {
                consoleWindow.ClearCommandHistory();
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Gets the command at the selected index number.
        /// </summary>
        /// <param name="indexNumber">The index number.</param>
        /// <returns>string</returns>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public string GetCommand(int indexNumber)
        {
            if (consoleWindow != null)
            {
                return consoleWindow.GetCommand(indexNumber);
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Gets the next command.
        /// </summary>
        /// <returns>The next command in the command history as a string.</returns>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public string GetNextCommand()
        {
            if (consoleWindow != null)
            {
                return consoleWindow.GetNextCommand();
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Gets the previous command.
        /// </summary>
        /// <returns>The previous command in the command history as a string.</returns>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public string GetPreviousCommand()
        {
            if (consoleWindow != null)
            {
                return consoleWindow.GetPreviousCommand();
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Loads the command history from an xml file using the default directory file name.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void LoadCommandHistory()
        {
            LoadCommandHistory(System.Environment.CurrentDirectory, string.Empty);
        }

        /// <summary>
        /// Loads the command history from an xml file in the current directory with
        /// the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void LoadCommandHistory(string fileName)
        {
            LoadCommandHistory(System.Environment.CurrentDirectory, fileName);
        }

        /// <summary>
        /// Loads the command history from an xml file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void LoadCommandHistory(string path, string fileName)
        {
            if (consoleWindow != null)
            {
                consoleWindow.LoadCommandHistory(path, fileName);
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Removes a command from the command history.
        /// </summary>
        /// <param name="indexNumber">The index number of the command to remove.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void RemoveFromCommandHistory(int indexNumber)
        {
            if (consoleWindow != null)
            {
                consoleWindow.RemoveFromCommandHistory(indexNumber);
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Resets the command history.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void ResetCommandHistory()
        {
            if (consoleWindow != null)
            {
                consoleWindow.ResetCommandHistory();
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Saves the command history to the default path and file.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void SaveCommandHistory()
        {
            SaveCommandHistory(System.Environment.CurrentDirectory, string.Empty);
        }

        /// <summary>
        /// Saves the command history to an xml file in the current directory with
        /// the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void SaveCommandHistory(string fileName)
        {
            SaveCommandHistory(System.Environment.CurrentDirectory, fileName);
        }

        /// <summary>
        /// Saves the command history to an xml file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void SaveCommandHistory(string path, string fileName)
        {
            if (consoleWindow != null)
            {
                consoleWindow.SaveCommandHistory(path, fileName);
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        #endregion

        #region Console Control

        /// <summary>
        /// Clears the console of all content.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void ClearConsole()
        {
            if (consoleWindow != null)
            {
                consoleWindow.ClearConsole();
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Closes the current console window instance.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void Close()
        {
            if (consoleWindow != null)
            {
                consoleWindow.CloseConsole();
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Gets or sets the height of the console.
        /// </summary>
        /// <value>
        /// The height of the console.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when window height is outside of min-max range.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public double ConsoleHeight
        {
            get
            {
                if (consoleWindow != null)
                {
                    return consoleWindow.ConsoleHeight;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
            set
            {
                if (consoleWindow != null)
                {
                    consoleWindow.ConsoleHeight = value;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the console.
        /// </summary>
        /// <value>
        /// The width of the console.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when window width is outside of min-max range.</exception>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public double ConsoleWidth
        {
            get
            {
                if (consoleWindow != null)
                {
                    return consoleWindow.ConsoleWidth;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
            set
            {
                if (consoleWindow != null)
                {
                    consoleWindow.ConsoleWidth = value;
                }
                else
                {
                    throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
                }
            }
        }

        /// <summary>
        /// Hides the console window.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void Hide()
        {
            if (consoleWindow != null)
            {
                consoleWindow.HideConsole();
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Resets the console to default settings.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void ResetConsole()
        {
            if (consoleWindow != null)
            {
                consoleWindow.ResetConsole(consoleSettings);
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Resets the size of the console to default.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void ResetConsoleSize()
        {
            if (consoleWindow != null)
            {
                consoleWindow.ResetConsoleSize();
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Shows the console window.
        /// </summary>
        public void Show()
        {
            if (consoleWindow == null)
            {
                if (consoleSettings == null) { consoleSettings = ConsoleSettingsManager.LoadDefaults(); }
                consoleWindow = new ConsoleWindow(consoleSettings);
                consoleWindow.ConsoleReadLine += new ReadLineEventHandler(OnConsoleReadEvent);
                ConsoleWriteLine += new WriteLineEventHandler(consoleWindow.WriteLine);
            }

            consoleWindow.ShowConsole();
        }

        #endregion

        #region Save/Load Console Settings

        /// <summary>
        /// Loads the settings from the default path and file.
        /// Uses defaults if configuration file does not exist.
        /// </summary>
        public void LoadSettings()
        {
            consoleSettings = ConsoleSettingsManager.LoadSettings(System.Environment.CurrentDirectory, settingsFileName);
        }

        /// <summary>
        /// Loads the settings from the default path and specified file name.
        /// Uses defaults if configuration file does not exist.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void LoadSettings(string fileName)
        {
            consoleSettings = ConsoleSettingsManager.LoadSettings(System.Environment.CurrentDirectory, fileName);
        }

        /// <summary>
        /// Loads the settings from a specified path and file name.
        /// Uses defaults if configuration file does not exist.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        public void LoadSettings(string path, string fileName)
        {
            consoleSettings = ConsoleSettingsManager.LoadSettings(path, fileName);
        }

        /// <summary>
        /// Saves the settings to the default path and file.
        /// </summary>
        public void SaveSettings()
        {
            ConsoleSettingsManager.SaveSettings(System.Environment.CurrentDirectory, settingsFileName, consoleSettings);
        }

        /// <summary>
        /// Saves the settings to the default path and specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void SaveSettings(string fileName)
        {
            ConsoleSettingsManager.SaveSettings(System.Environment.CurrentDirectory, fileName, consoleSettings);
        }

        /// <summary>
        /// Saves the settings to a specified path and file name.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        public void SaveSettings(string path, string fileName)
        {
            ConsoleSettingsManager.SaveSettings(path, fileName, consoleSettings);
        }

        #endregion

        #region Console Write Line

        /// <summary>
        /// Sends a string to the console output.
        /// </summary>
        /// <param name="output">String to output to console.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void WriteLine(string output)
        {
            if (consoleWindow != null) 
            {
                WriteLine(this, new ConsoleWriteLineEventArgs(output)); 
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Sends a string to the console output with the desired font color.
        /// </summary>
        /// <param name="output">String to output to console.</param>
        /// <param name="foreground">The text color for text.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void WriteLine(string output, Brush foreground)
        {
            if (consoleWindow != null) 
            {
                WriteLine(this, new ConsoleWriteLineEventArgs(output, foreground)); 
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Writes a FlowDocument paragraph to the console.
        /// </summary>
        /// <param name="paragraph">The paragraph to send to console.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void WriteLine(Paragraph paragraph)
        {
            if (consoleWindow != null)
            {
                WriteLine(this, new ConsoleWriteLineEventArgs(paragraph)); 
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Sends a ConsoleWriteLineEventArgs object to the console output.
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="e">The <see cref="WPFCommandPrompt.ConsoleWriteLineEventArgs"/> instance containing the event data.</param>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public void WriteLine(object sender, ConsoleWriteLineEventArgs e)
        {
            if (consoleWindow != null) 
            {
                ConsoleWriteLine(sender, e);
            }
            else
            {
                throw new NullReferenceException("ConsoleWindow() class not instantiated. Call Show() to create console window.");
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the message area text.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (consoleWindow != null) { return consoleWindow.ToString(); }
            return string.Empty;
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when writing text to the console.
        /// </summary>
        public event WriteLineEventHandler ConsoleWriteLine;

        /// <summary>
        /// Called when writing text to the console.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="WPFCommandPrompt.ConsoleWriteLineEventArgs"/> instance containing the event data.</param>
        public void OnConsoleWriteEvent(object sender, ConsoleWriteLineEventArgs e)
        {
            if (ConsoleWriteLine != null)
            {
                ConsoleWriteLine(sender, e);
            }
        }

        /// <summary>
        /// Occurs on a console read line update event.
        /// </summary>
        public event ReadLineEventHandler ReadLine;

        /// <summary>
        /// Called on a console read line upate event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="WPFCommandPrompt.ConsoleReadLineEventArgs"/> instance containing the event data.</param>
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
