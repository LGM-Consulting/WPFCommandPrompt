// Author: Adrian Hum
// Project: WPFCommandPrompt/ConsoleWindow.cs
// 
// Created : 2016-01-06  18:29 
// Modified: 2016-01-06 18:36)

using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WPFCommandPrompt.StyleThemes;
using SystemFonts = System.Drawing.SystemFonts;

namespace WPFCommandPrompt {
    /// <summary>
    ///     Interaction logic for ConsoleWindow.cs
    /// </summary>
    internal class ConsoleWindow : Window {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConsoleWindow" /> class.
        /// </summary>
        /// <param name="consoleSettings">The console settings.</param>
        public ConsoleWindow(ConsoleSettings.ConsoleSettings consoleSettings)
        {
            _styleThemeManager = new StyleThemeManager(consoleSettings.StyleThemeIndex,
                consoleSettings.EnableLoadStyleThemes);
            InitializeWindow(consoleSettings);
        }

        #endregion

        #region Console Window Events

        private void consoleWindow_SizeChanged(object sender, EventArgs e)
        {
            if (_maxConsoleHeight != 0 && ActualHeight > _maxConsoleHeight)
            {
                Height = _maxConsoleHeight;
            }

            if (_maxConsoleWidth != 0 && ActualWidth > _maxConsoleWidth)
            {
                Width = _maxConsoleWidth;
            }
        }

        #endregion

        #region Fields

        private readonly StyleThemeManager _styleThemeManager;
        private readonly CommandHistory.CommandHistory _commandHistory = new CommandHistory.CommandHistory();
        private RichTextBox _rtbMessageArea;
        private TextBox _txtCommandPrompt;

        // Set basic defaults
        private bool _allowEmptyCommand;
        private FontFamily _defaultFont = new FontFamily(SystemFonts.DefaultFont.ToString());
        private double _defaultFontSize = 12.00;
        private string _defaultPrompt = ">";
        private string _delimeters = @"((""((?<token>.*?)"")|(?<token>[\w]+))(\s)*)";
        private bool _enableCommandHistory = true;
        private bool _manualCommandHistory;
        private double _maxFontSize = 36.00;
        private double _minFontSize = 8.00;
        private string _prompt = ">";
        private bool _useInternalCommandParsing = true;
        private string _welcomeMessage;

        // Window height size defaults.
        private double _defaultConsoleHeight = 400.00;
        private double _minConsoleHeight = 400.00;
        private double _maxConsoleHeight = 768.00;

        // Window width size defaults.
        private double _defaultConsoleWidth = 600.00;
        private double _minConsoleWidth = 600.00;
        private double _maxConsoleWidth = 1024.00;

        #endregion

        #region Basic Properties

        /// <summary>
        ///     Gets or sets the window title.
        /// </summary>
        /// <value>
        ///     The window title.
        /// </value>
        public string ConsoleTitle
        {
            set { Title = value; }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether to allow the command history function (Default: true).
        /// </summary>
        /// <value>
        ///     Allow command history if <c>true</c> ; otherwise, <c>false</c>.
        /// </value>
        public bool EnableCommandHistory
        {
            get { return _enableCommandHistory; }
            set { _enableCommandHistory = value; }
        }

        /// <summary>
        ///     Gets or sets the prompt string (Default: '>').
        /// </summary>
        /// <value>
        ///     The command prompt string.
        /// </value>
        public string Prompt
        {
            get { return _prompt; }
            set
            {
                //txtCommand.SelectionStart = 0;
                //txtCommand.SelectionLength = prompt.Length - 1;
                //txtCommand.SelectedText = value;
                _prompt = value;
            }
        }

        #endregion

        #region Console Size Properties

        /// <summary>
        ///     Gets or sets the height of the console (Default: 400.00).
        /// </summary>
        /// <value>
        ///     The height of the console.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when window height is outside of min-max range.</exception>
        public double ConsoleHeight
        {
            get { return ActualHeight; }
            set
            {
                if (value >= _minConsoleHeight && (value <= _maxConsoleHeight || _maxConsoleHeight == 0))
                {
                    Height = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Console Height: Height out of range. Min height: " +
                                                          _minConsoleHeight + " Max height: " + _maxConsoleHeight);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the width of the console (Default: 600.00).
        /// </summary>
        /// <value>
        ///     The width of the console.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when window width is outside of min-max range.</exception>
        public double ConsoleWidth
        {
            get { return ActualWidth; }
            set
            {
                if (value >= _minConsoleWidth && (value <= _maxConsoleWidth || _maxConsoleWidth == 0))
                {
                    Width = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Console Width: Width out of range. Min width: " +
                                                          _minConsoleWidth + " Max width: " + _maxConsoleWidth);
                }
            }
        }

        #endregion

        #region Font Properties

        /// <summary>
        ///     Gets or sets the command prompt font.
        /// </summary>
        /// <value>
        ///     The command text font.
        /// </value>
        public FontFamily CommandPromptFont
        {
            get { return _styleThemeManager.CommandPromptFont; }
            set
            {
                _styleThemeManager.CommandPromptFont = value;
                _txtCommandPrompt.FontFamily = value;
            }
        }

        /// <summary>
        ///     Gets or sets the size of the command prompt font.
        /// </summary>
        /// <value>
        ///     The size of the command text font.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when font size is outside of min-max range.</exception>
        public double CommandPromptFontSize
        {
            get { return _styleThemeManager.CommandPromptFontSize; }
            set
            {
                if (value >= _minFontSize && value <= _maxFontSize)
                {
                    _styleThemeManager.CommandPromptFontSize = value;
                    _txtCommandPrompt.FontSize = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Font Size: [" + value + "] not supported. Min:" + _minFontSize +
                                                          " Max:" + _maxFontSize);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the color of the command prompt text.
        /// </summary>
        /// <value>
        ///     The color of the command area text.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        public Brush CommandPromptTextColor
        {
            get { return _styleThemeManager.CommandPromptTextColor; }
            set
            {
                if (value != null)
                {
                    _txtCommandPrompt.Foreground = value;
                    _styleThemeManager.CommandPromptTextColor = value;
                }
                else
                {
                    throw new FormatException("Color not recognized / invalid format or is Null.");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the message area font.
        /// </summary>
        /// <value>
        ///     The message area font.
        /// </value>
        public FontFamily MessageAreaFont
        {
            get { return _styleThemeManager.MessageAreaFont; }
            set
            {
                _styleThemeManager.MessageAreaFont = value;
                _rtbMessageArea.FontFamily = value;
            }
        }

        /// <summary>
        ///     Gets or sets the size of the message area font.
        /// </summary>
        /// <value>
        ///     The size of the message area font.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when font size is outside of min-max range.</exception>
        public double MessageAreaFontSize
        {
            get { return _styleThemeManager.MessageAreaFontSize; }
            set
            {
                if (value >= _minFontSize && value <= _maxFontSize)
                {
                    _styleThemeManager.MessageAreaFontSize = value;
                    _rtbMessageArea.FontSize = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Font Size: [" + value + "] not supported. Min:" + _minFontSize +
                                                          " Max:" + _maxFontSize);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the color of the message area prompt.
        /// </summary>
        /// <value>
        ///     The color of the message area prompt.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        public Brush MessagePromptColor
        {
            get { return _styleThemeManager.MessagePromptColor; }
            set
            {
                if (value != null)
                {
                    _styleThemeManager.MessagePromptColor = value;
                }
                else
                {
                    throw new FormatException("Color not recognized / invalid format or is Null.");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the color of the message text.
        /// </summary>
        /// <value>
        ///     The color of the message text.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        public Brush MessageTextColor
        {
            get { return _styleThemeManager.MessageTextColor; }
            set
            {
                if (value != null)
                {
                    _styleThemeManager.MessageTextColor = value;
                }
                else
                {
                    throw new FormatException("Color not recognized / invalid format or is Null.");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the color of the welcome message.
        /// </summary>
        /// <value>
        ///     The color of the welcome message.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        public Brush WelcomeMessageColor
        {
            get { return _styleThemeManager.WelcomeMessageColor; }
            set
            {
                if (value != null)
                {
                    _styleThemeManager.WelcomeMessageColor = value;
                }
                else
                {
                    throw new FormatException("Color not recognized / invalid format or is Null.");
                }
            }
        }

        #endregion

        #region Background Color Properties

        /// <summary>
        ///     Gets or sets the color of the command area background.
        /// </summary>
        /// <value>
        ///     The color of the command area background.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        public Brush CommandPromptBackgroundColor
        {
            get { return _styleThemeManager.CommandPromptBackgroundColor; }
            set
            {
                if (value != null)
                {
                    _txtCommandPrompt.Background = value;
                    _styleThemeManager.CommandPromptBackgroundColor = value;
                }
                else
                {
                    throw new FormatException("Color not recognized / invalid format or is Null.");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the color of the message area background.
        /// </summary>
        /// <value>
        ///     The color of the message area background.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        public Brush MessageAreaBackgroundColor
        {
            get { return _styleThemeManager.MessageAreaBackgroundColor; }
            set
            {
                if (value != null)
                {
                    _rtbMessageArea.Background = value;
                    _styleThemeManager.MessageAreaBackgroundColor = value;
                }
                else
                {
                    throw new FormatException("Color not recognized / invalid format or is Null.");
                }
            }
        }

        #endregion

        #region Padding / Border Properties

        /// <summary>
        ///     Gets or sets the color of the command prompt border.
        /// </summary>
        /// <value>
        ///     The color of the command prompt border.
        /// </value>
        public Brush CommandPromptBorderColor
        {
            get { return _txtCommandPrompt.BorderBrush; }
            set
            {
                _txtCommandPrompt.BorderBrush = value;
                _styleThemeManager.CommandPromptBorderColor = value;
            }
        }

        /// <summary>
        ///     Gets or sets the command border thickness.
        /// </summary>
        /// <value>
        ///     The command border thickness.
        /// </value>
        public Thickness CommandPromptBorderThickness
        {
            get { return _txtCommandPrompt.BorderThickness; }
            set
            {
                _txtCommandPrompt.BorderThickness = value;
                _styleThemeManager.CommandPromptBorderThickness = value;
            }
        }

        /// <summary>
        ///     Gets or sets the command padding.
        /// </summary>
        /// <value>
        ///     The command padding.
        /// </value>
        public Thickness CommandPromptPadding
        {
            get { return _txtCommandPrompt.Padding; }
            set
            {
                _txtCommandPrompt.Padding = value;
                _styleThemeManager.CommandPromptPadding = value;
            }
        }

        /// <summary>
        ///     Gets or sets the color of the message area border.
        /// </summary>
        /// <value>
        ///     The color of the message area border.
        /// </value>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Brush MessageAreaBorderColor
        {
            get { return _rtbMessageArea.BorderBrush; }
            set
            {
                _rtbMessageArea.BorderBrush = value;
                _styleThemeManager.MessageAreaBorderColor = value;
            }
        }

        /// <summary>
        ///     Gets or sets the message border thickness.
        /// </summary>
        /// <value>
        ///     The message border thickness.
        /// </value>
        public Thickness MessageAreaBorderThickness
        {
            get { return _rtbMessageArea.BorderThickness; }
            set
            {
                _rtbMessageArea.BorderThickness = value;
                _styleThemeManager.MessageAreaBorderThickness = value;
            }
        }

        /// <summary>
        ///     Gets or sets the message padding.
        /// </summary>
        /// <value>
        ///     The message padding.
        /// </value>
        public Thickness MessageAreaPadding
        {
            get { return _rtbMessageArea.Padding; }
            set
            {
                _rtbMessageArea.Padding = value;
                _styleThemeManager.MessageAreaPadding = value;
            }
        }

        /// <summary>
        ///     Gets or sets the message text paragraph margins.
        /// </summary>
        /// <value>
        ///     The message text paragraph margin.
        /// </value>
        public Thickness MessageTextParagraphMargin
        {
            get { return _styleThemeManager.MessageTextParagraphMargin; }
            set { _styleThemeManager.MessageTextParagraphMargin = value; }
        }

        #endregion

        #region Console Initialization

        /// <summary>
        ///     Initializes the console window.
        /// </summary>
        /// <param name="consoleSettings">The console settings.</param>
        private void InitializeWindow(ConsoleSettings.ConsoleSettings consoleSettings)
        {
            SizeChanged += consoleWindow_SizeChanged;

            Title = consoleSettings.ConsoleTitle;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Height = consoleSettings.DefaultConsoleHeight;
            Width = consoleSettings.DefaultConsoleWidth;

            _defaultConsoleHeight = consoleSettings.DefaultConsoleHeight;
            _defaultConsoleWidth = consoleSettings.DefaultConsoleWidth;
            _maxConsoleHeight = consoleSettings.MaxConsoleHeight;
            _maxConsoleWidth = consoleSettings.MaxConsoleWidth;
            _minConsoleHeight = consoleSettings.MinConsoleHeight;
            _minConsoleWidth = consoleSettings.MinConsoleWidth;

            var grdMain = new Grid();
            grdMain.Name = "grdMain";
            Content = grdMain;

            // Setup the grid layout
            var cd = new ColumnDefinition();
            cd.Width = new GridLength(100, GridUnitType.Star);
            grdMain.ColumnDefinitions.Add(cd);

            var rd1 = new RowDefinition();
            rd1.Height = new GridLength(100, GridUnitType.Star);
            grdMain.RowDefinitions.Add(rd1);
            var rd2 = new RowDefinition();
            rd2.Height = new GridLength(0, GridUnitType.Auto);
            grdMain.RowDefinitions.Add(rd2);

            _rtbMessageArea = new RichTextBox();
            Grid.SetRow(_rtbMessageArea, 0);
            grdMain.Children.Add(_rtbMessageArea);

            _txtCommandPrompt = new TextBox();
            Grid.SetRow(_txtCommandPrompt, 1);
            grdMain.Children.Add(_txtCommandPrompt);

            // *Box settings
            _rtbMessageArea.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            _rtbMessageArea.IsReadOnly = true;
            _txtCommandPrompt.TextWrapping = TextWrapping.Wrap;
            _txtCommandPrompt.AcceptsReturn = false;

            // Command prompt key events
            _txtCommandPrompt.KeyDown += txtCommandPrompt_KeyDown;
            _txtCommandPrompt.PreviewKeyDown += txtCommandPrompt_PreviewKeyDown;
            _txtCommandPrompt.PreviewKeyUp += txtCommandPrompt_PreviewKeyUp;
            _txtCommandPrompt.KeyUp += txtCommandPrompt_KeyUp;

            InitializeConsole(consoleSettings);
        }

        /// <summary>
        ///     Initializes the console.
        /// </summary>
        /// <param name="consoleSettings">The console settings.</param>
        private void InitializeConsole(ConsoleSettings.ConsoleSettings consoleSettings)
        {
            _allowEmptyCommand = consoleSettings.AllowEmptyCommand;
            _defaultFontSize = consoleSettings.DefaultFontSize;
            _defaultPrompt = consoleSettings.Prompt;
            _delimeters = consoleSettings.Delimiters;
            _enableCommandHistory = consoleSettings.EnableCommandHistory;
            _manualCommandHistory = consoleSettings.ManualCommandHistory;
            _maxFontSize = consoleSettings.MaxFontSize;
            _minFontSize = consoleSettings.MinFontSize;
            _prompt = consoleSettings.Prompt;
            _useInternalCommandParsing = consoleSettings.UseInternalCommandParsing;
            _welcomeMessage = consoleSettings.WelcomeMessage;

            _txtCommandPrompt.Text = consoleSettings.Prompt;
            _txtCommandPrompt.CaretIndex = _txtCommandPrompt.Text.Length;

            _rtbMessageArea.Document = new FlowDocument();

            SetTheme();

            var paragraph = new Paragraph();
            paragraph.Margin = new Thickness(0);
            paragraph.TextAlignment = TextAlignment.Center;
            paragraph.Inlines.Add(new Run(_welcomeMessage)
            {
                Foreground = _styleThemeManager.WelcomeMessageColor
            });

            WriteLineToConsole(paragraph);

            _txtCommandPrompt.Focus();
        }

        #endregion

        #region Style Theme Control

        /// <summary>
        ///     Adds a color scheme to the list.
        /// </summary>
        /// <param name="styleTheme">The color scheme to add.</param>
        public void AddStyleTheme(StyleTheme styleTheme)
        {
            _styleThemeManager.AddStyleTheme(styleTheme);
        }

        /// <summary>
        ///     Changes the name of the current theme. Only updates the current copy of the theme in use.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        public void CurrentThemeName(string themeName)
        {
            _styleThemeManager.StyleThemeName = themeName;
        }

        /// <summary>
        ///     Creates a new style theme with the current settings. Not saved to disk.
        /// </summary>
        public void CurrentThemeToNew()
        {
            _styleThemeManager.CurrentThemeToNew();
        }

        /// <summary>
        ///     Gets the Current color scheme details.
        /// </summary>
        /// <returns>Description of the current color scheme as a string</returns>
        public string CurrentStyleThemeDetails()
        {
            return _styleThemeManager.CurrentStyleTheme();
        }

        /// <summary>
        ///     Deletes a style theme.
        /// </summary>
        /// <param name="styleThemeIndex">Index of the style theme.</param>
        public void DeleteStyleTheme(int styleThemeIndex)
        {
            _styleThemeManager.DeleteStyleTheme(styleThemeIndex);
        }

        /// <summary>
        ///     Gets the color scheme list.
        /// </summary>
        /// <returns>A list of the color schemes as a string</returns>
        public string GetStyleThemeList()
        {
            return _styleThemeManager.GetStyleThemeList();
        }

        /// <summary>
        ///     Loads the style themes from disk.
        /// </summary>
        public void LoadStyleThemes()
        {
            _styleThemeManager.LoadStyleThemes();
        }

        /// <summary>
        ///     Resets the console theme. Any changes are discarded.
        /// </summary>
        public void ResetStyleTheme()
        {
            _styleThemeManager.ResetStyleTheme();
            SetTheme();
        }

        /// <summary>
        ///     Saves the style themes to disk.
        /// </summary>
        public void SaveStyleThemes()
        {
            _styleThemeManager.SaveStyleThemes();
        }

        /// <summary>
        ///     Sets the color scheme.
        /// </summary>
        /// <param name="styleThemeIndex">The list index of the style theme.</param>
        public void SetStyleTheme(int styleThemeIndex)
        {
            _styleThemeManager.SetStyleTheme(styleThemeIndex);
            SetTheme();
            Background = _txtCommandPrompt.Background;
        }

        /// <summary>
        ///     Sets the color scheme for the console.
        /// </summary>
        private void SetTheme()
        {
            _rtbMessageArea.Background = _styleThemeManager.MessageAreaBackgroundColor;
            _rtbMessageArea.BorderBrush = _styleThemeManager.MessageAreaBorderColor;
            _rtbMessageArea.BorderThickness = _styleThemeManager.MessageAreaBorderThickness;
            _rtbMessageArea.FontFamily = _styleThemeManager.MessageAreaFont;
            _rtbMessageArea.FontSize = _styleThemeManager.MessageAreaFontSize;
            _rtbMessageArea.Padding = _styleThemeManager.MessageAreaPadding;
            _txtCommandPrompt.Background = _styleThemeManager.CommandPromptBackgroundColor;
            _txtCommandPrompt.BorderBrush = _styleThemeManager.CommandPromptBorderColor;
            _txtCommandPrompt.BorderThickness = _styleThemeManager.CommandPromptBorderThickness;
            _txtCommandPrompt.FontFamily = _styleThemeManager.CommandPromptFont;
            _txtCommandPrompt.FontSize = _styleThemeManager.CommandPromptFontSize;
            _txtCommandPrompt.Foreground = _styleThemeManager.CommandPromptTextColor;
            _txtCommandPrompt.Padding = _styleThemeManager.CommandPromptPadding;
        }

        /// <summary>
        ///     Updates the original style theme with the current settings. Does not save to disk.
        /// </summary>
        public void UpdateStyleTheme()
        {
            _styleThemeManager.UpdateStyleTheme();
        }

        #endregion

        #region Command History Control

        /// <summary>
        ///     Adds a command string to the command history.
        /// </summary>
        /// <param name="command">The command to add.</param>
        public void AddToCommandHistory(string command)
        {
            _commandHistory.AddCommandToHistory(command);
        }

        /// <summary>
        ///     Clears the command history.
        /// </summary>
        public void ClearCommandHistory()
        {
            _commandHistory.ClearHistory();
            _commandHistory.CurrentIndex = -1;
        }

        /// <summary>
        ///     Gets the command at the selected index number.
        /// </summary>
        /// <param name="indexNumber">The index number.</param>
        /// <returns>The command as a string</returns>
        public string GetCommand(int indexNumber)
        {
            return _commandHistory.GetCommand(indexNumber);
        }

        /// <summary>
        ///     Gets the next command.
        /// </summary>
        /// <returns>The next command as a string</returns>
        public string GetNextCommand()
        {
            return _commandHistory.GetNext();
        }

        /// <summary>
        ///     Gets the previous command.
        /// </summary>
        /// <returns>The previous command as a string</returns>
        public string GetPreviousCommand()
        {
            return _commandHistory.GetPrevious();
        }

        /// <summary>
        ///     Loads the command history from an xml file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        public void LoadCommandHistory(string path, string fileName)
        {
            _commandHistory.LoadCommandHistory(path, fileName);
        }

        /// <summary>
        ///     Removes a command from the command history.
        /// </summary>
        /// <param name="indexNumber">The index number of the command to remove.</param>
        public void RemoveFromCommandHistory(int indexNumber)
        {
            _commandHistory.RemoveFromCommandHistory(indexNumber);
        }

        /// <summary>
        ///     Resets the command history.
        /// </summary>
        public void ResetCommandHistory()
        {
            _commandHistory.CurrentIndex = -1;
        }

        /// <summary>
        ///     Saves the command history to an xml file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        public void SaveCommandHistory(string path, string fileName)
        {
            _commandHistory.SaveCommandHistory(path, fileName);
        }

        #endregion

        #region Console Control

        /// <summary>
        ///     Clears the console of all content.
        /// </summary>
        public void ClearConsole()
        {
            _txtCommandPrompt.Text = _prompt;
            _rtbMessageArea.Document = new FlowDocument();
        }

        /// <summary>
        ///     Closes this instance.
        /// </summary>
        public void CloseConsole()
        {
            Close();
        }

        /// <summary>
        ///     Hides the console window.
        /// </summary>
        public void HideConsole()
        {
            Hide();
        }

        /// <summary>
        ///     Resets the console window and all elements to default settings.
        /// </summary>
        /// <param name="consoleSettings">The console settings object.</param>
        public void ResetConsole(ConsoleSettings.ConsoleSettings consoleSettings)
        {
            InitializeConsole(consoleSettings);
            ResetConsoleSize();
        }

        /// <summary>
        ///     Resets the size of the console to default.
        /// </summary>
        public void ResetConsoleSize()
        {
            Height = _defaultConsoleHeight;
            Width = _defaultConsoleWidth;
        }

        /// <summary>
        ///     Shows the console.
        /// </summary>
        public void ShowConsole()
        {
            Show();
            Focus();
            _txtCommandPrompt.Focus();
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents the message area text.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var text = new TextRange(_rtbMessageArea.Document.ContentStart, _rtbMessageArea.Document.ContentEnd);
            return text.ToString();
        }

        #endregion

        #region Console Events

        /// <summary>
        ///     Parses commands from the command text area.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>String array of parsed commands</returns>
        private string[] ParseCommands(string input)
        {
            var options = RegexOptions.None;
            var regex = new Regex(_delimeters, options);
            var result = (from Match m in regex.Matches(input)
                where m.Groups["token"].Success
                select m.Groups["token"].Value).ToArray();

            return result;
        }

        private void txtCommandPrompt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return | e.Key == Key.Enter) // Catch a carrage return
            {
                var currentLine = _txtCommandPrompt.Text.Trim();
                var rawCommand = currentLine.Remove(0, _prompt.Length);

                if (!string.IsNullOrEmpty(rawCommand) || (string.IsNullOrEmpty(rawCommand) && _allowEmptyCommand))
                {
                    SendToMessagePrompt(currentLine);

                    if (_useInternalCommandParsing)
                    {
                        OnConsoleReadEvent(this, new ConsoleReadLineEventArgs(ParseCommands(rawCommand)));
                    }
                    else
                    {
                        OnConsoleReadEvent(this, new ConsoleReadLineEventArgs(rawCommand));
                    }

                    if (_enableCommandHistory && !_manualCommandHistory)
                    {
                        _commandHistory.AddCommandToHistory(rawCommand);
                        _commandHistory.CurrentIndex = -1;
                    }

                    _commandHistory.CurrentCommand = string.Empty;
                    _txtCommandPrompt.Text = null;
                }

                _txtCommandPrompt.Text = _prompt;
                _txtCommandPrompt.CaretIndex = _prompt.Length;
            }
        }

        // Catch any attempt at typing in the prompt text area and move caret to end of prompt if so.
        private void txtCommandPrompt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var index = _txtCommandPrompt.SelectionStart;
            if (index < _prompt.Length)
            {
                _txtCommandPrompt.SelectionStart = _prompt.Length;
                _txtCommandPrompt.SelectionLength = 0;
                e.Handled = true;
            }
        }

        // Catch any attempt at moving to the prompt text area and move caret to end of prompt if so.
        private void txtCommandPrompt_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            var index = _txtCommandPrompt.SelectionStart;

            if (e.Key == Key.Left && index < _prompt.Length)
            {
                _txtCommandPrompt.SelectionStart = _prompt.Length;
                _txtCommandPrompt.SelectionLength = 0;
                e.Handled = true;
            }

            if (e.Key == Key.Back && index < _prompt.Length)
            {
                _txtCommandPrompt.SelectionStart = 0;
                _txtCommandPrompt.SelectionLength = _prompt.Length - 1;
                _txtCommandPrompt.SelectedText = _prompt;

                _txtCommandPrompt.SelectionStart = _prompt.Length;
                _txtCommandPrompt.SelectionLength = 0;
                e.Handled = true;
            }
        }

        private void txtCommandPrompt_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up: // Command history next
                    if (_enableCommandHistory)
                    {
                        if (string.IsNullOrEmpty(_commandHistory.CurrentCommand))
                        {
                            _commandHistory.CurrentCommand = _txtCommandPrompt.Text.Remove(0, _prompt.Length);
                        }

                        _txtCommandPrompt.Text = _prompt + GetNextCommand();
                        _txtCommandPrompt.SelectionStart = _txtCommandPrompt.Text.Length;
                        _txtCommandPrompt.SelectionLength = 0;
                    }
                    break;
                case Key.Down: // Command history previous
                    if (_enableCommandHistory)
                    {
                        if (string.IsNullOrEmpty(_commandHistory.CurrentCommand))
                        {
                            _commandHistory.CurrentCommand = _txtCommandPrompt.Text.Remove(0, _prompt.Length);
                        }

                        _txtCommandPrompt.Text = _prompt + GetPreviousCommand();
                        _txtCommandPrompt.SelectionStart = _txtCommandPrompt.Text.Length;
                        _txtCommandPrompt.SelectionLength = 0;
                    }
                    break;
                case Key.Escape: // Command history exit
                    if (_enableCommandHistory)
                    {
                        _txtCommandPrompt.Text = _prompt + _commandHistory.CurrentCommand;
                        _txtCommandPrompt.SelectionStart = _txtCommandPrompt.Text.Length;
                        _txtCommandPrompt.SelectionLength = 0;
                        _commandHistory.CurrentCommand = string.Empty;
                        _commandHistory.CurrentIndex = -1;
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Console Write Line

        /// <summary>
        ///     Sends a string to the console output.
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="e">The <see cref="WPFCommandPrompt.ConsoleWriteLineEventArgs" /> instance containing the event data.</param>
        public void WriteLine(object sender, ConsoleWriteLineEventArgs e)
        {
            if (e.Paragraph != null)
            {
                WriteLineToConsole(e.Paragraph);
            }
            else
            {
                if (e.Foreground != null)
                {
                    WriteLineToConsole(e.Message, e.Foreground);
                }
                else
                {
                    WriteLineToConsole(e.Message, _styleThemeManager.MessageTextColor);
                }
            }
        }

        /// <summary>
        ///     Sends a string to the console output with the desired font color.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <param name="foreground">The foreground.</param>
        private void WriteLineToConsole(string output, Brush foreground)
        {
            Paragraph paragraph;
            paragraph = new Paragraph();
            paragraph.Margin = _styleThemeManager.MessageTextParagraphMargin;
            paragraph.Inlines.Add(new Run(output)
            {
                Foreground = foreground
            });

            _rtbMessageArea.Document.Blocks.Add(paragraph);
            _rtbMessageArea.ScrollToEnd();
        }

        /// <summary>
        ///     Writes a FlowDocument paragraph to the console.
        /// </summary>
        /// <param name="paragraph">The paragraph.</param>
        private void WriteLineToConsole(Paragraph paragraph)
        {
            _rtbMessageArea.Document.Blocks.Add(paragraph);
            _rtbMessageArea.ScrollToEnd();
        }

        /// <summary>
        ///     Sends a string to the console window. Internal only.
        /// </summary>
        /// <param name="output">The string to output.</param>
        private void SendToMessagePrompt(string output)
        {
            Paragraph paragraph;
            paragraph = new Paragraph();
            paragraph.Margin = new Thickness(0);
            paragraph.Inlines.Add(new Run(output)
            {
                Foreground = _styleThemeManager.MessagePromptColor
            });

            _rtbMessageArea.Document.Blocks.Add(paragraph);
            _rtbMessageArea.ScrollToEnd();
        }

        #endregion

        #region Read Line Event

        /// <summary>
        ///     Occurs on a console read line update event.
        /// </summary>
        public event ReadLineEventHandler ConsoleReadLine;

        /// <summary>
        ///     Called on a console read line update event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="WPFCommandPrompt.ConsoleReadLineEventArgs" /> instance containing the event data.</param>
        public void OnConsoleReadEvent(object sender, ConsoleReadLineEventArgs e)
        {
            if (ConsoleReadLine != null)
            {
                ConsoleReadLine(this, e);
            }
        }

        #endregion
    }
}