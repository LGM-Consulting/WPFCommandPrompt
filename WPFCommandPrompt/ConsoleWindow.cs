using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Documents;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace WPFCommandPrompt
{
    /// <summary>
    /// Interaction logic for ConsoleWindow.cs
    /// </summary>
    internal class ConsoleWindow : Window
    {
        #region Fields

        private StyleThemeManager styleThemeManager;
        private CommandHistory commandHistory = new CommandHistory();
        private RichTextBox rtbMessageArea;
        private TextBox txtCommandPrompt;

        // Set basic defaults
        private bool allowEmptyCommand = false;
        private FontFamily defaultFont = new FontFamily(System.Drawing.SystemFonts.DefaultFont.ToString());
        private double defaultFontSize = 12.00;
        private string defaultPrompt = ">";
        private string delimeters = @"((""((?<token>.*?)"")|(?<token>[\w]+))(\s)*)";
        private bool enableCommandHistory = true;
        private bool manualCommandHistory = false;
        private double maxFontSize = 36.00;
        private double minFontSize = 8.00;
        private string prompt = ">";
        private bool useInternalCommandParsing = true;
        private string welcomeMessage;

        // Window height size defaults.
        private double defaultConsoleHeight = 400.00;
        private double minConsoleHeight = 400.00;
        private double maxConsoleHeight = 768.00;

        // Window width size defaults.
        private double defaultConsoleWidth = 600.00;
        private double minConsoleWidth = 600.00;
        private double maxConsoleWidth = 1024.00;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleWindow"/> class.
        /// </summary>
        /// <param name="consoleSettings">The console settings.</param>
        public ConsoleWindow(ConsoleSettings consoleSettings)
        {
            styleThemeManager = new StyleThemeManager(consoleSettings.StyleThemeIndex, consoleSettings.EnableLoadStyleThemes);
            InitializeWindow(consoleSettings);
        }

        #endregion

        #region Basic Properties

        /// <summary>
        /// Gets or sets the window title.
        /// </summary>
        /// <value>
        /// The window title.
        /// </value>
        public string ConsoleTitle
        {
            set  { this.Title = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to allow the command history function (Default: true).
        /// </summary>
        /// <value>
        /// Allow command history if <c>true</c> ; otherwise, <c>false</c>.
        /// </value>
        public bool EnableCommandHistory
        {
            get { return enableCommandHistory; }
            set { enableCommandHistory = value; }
        }

        /// <summary>
        /// Gets or sets the prompt string (Default: '>').
        /// </summary>
        /// <value>
        /// The command prompt string.
        /// </value>
        public string Prompt
        {
            get { return prompt; }
            set
            {
                //txtCommand.SelectionStart = 0;
                //txtCommand.SelectionLength = prompt.Length - 1;
                //txtCommand.SelectedText = value;
                prompt = value;
            }
        }

        #endregion

        #region Console Size Properties

        /// <summary>
        /// Gets or sets the height of the console (Default: 400.00).
        /// </summary>
        /// <value>
        /// The height of the console.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when window height is outside of min-max range.</exception>
        public double ConsoleHeight
        {
            get { return this.ActualHeight; }
            set
            {
                if (value >= minConsoleHeight && (value <= maxConsoleHeight || maxConsoleHeight == 0))
                {
                    this.Height = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Console Height: Height out of range. Min height: " + minConsoleHeight + " Max height: " + maxConsoleHeight);
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the console (Default: 600.00).
        /// </summary>
        /// <value>
        /// The width of the console.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when window width is outside of min-max range.</exception>
        public double ConsoleWidth
        {
            get { return this.ActualWidth; }
            set
            {
                if (value >= minConsoleWidth && (value <= maxConsoleWidth || maxConsoleWidth == 0))
                {
                    this.Width = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Console Width: Width out of range. Min width: " + minConsoleWidth + " Max width: " + maxConsoleWidth);
                }
            }
        }

        #endregion

        #region Font Properties

        /// <summary>
        /// Gets or sets the command prompt font.
        /// </summary>
        /// <value>
        /// The command text font.
        /// </value>
        public FontFamily CommandPromptFont
        {
            get { return styleThemeManager.CommandPromptFont; }
            set
            {
                styleThemeManager.CommandPromptFont = value;
                txtCommandPrompt.FontFamily = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of the command prompt font.
        /// </summary>
        /// <value>
        /// The size of the command text font.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when font size is outside of min-max range.</exception>
        public double CommandPromptFontSize
        {
            get { return styleThemeManager.CommandPromptFontSize; }
            set
            {
                if (value >= minFontSize && value <= maxFontSize)
                {
                    styleThemeManager.CommandPromptFontSize = value;
                    txtCommandPrompt.FontSize = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Font Size: [" + value.ToString() + "] not supported. Min:" + minFontSize + " Max:" + maxFontSize);
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the command prompt text.
        /// </summary>
        /// <value>
        /// The color of the command area text.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        public Brush CommandPromptTextColor
        {
            get { return styleThemeManager.CommandPromptTextColor; }
            set
            {
                if (value != null)
                {
                    txtCommandPrompt.Foreground = value;
                    styleThemeManager.CommandPromptTextColor = value;
                }
                else
                {
                    throw new FormatException("Color not recognized / invalid format or is Null.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the message area font.
        /// </summary>
        /// <value>
        /// The message area font.
        /// </value>
        public FontFamily MessageAreaFont
        {
            get { return styleThemeManager.MessageAreaFont; }
            set
            {
                styleThemeManager.MessageAreaFont = value;
                rtbMessageArea.FontFamily = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of the message area font.
        /// </summary>
        /// <value>
        /// The size of the message area font.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when font size is outside of min-max range.</exception>
        public double MessageAreaFontSize
        {
            get { return styleThemeManager.MessageAreaFontSize; }
            set
            {
                if (value >= minFontSize && value <= maxFontSize)
                {
                    styleThemeManager.MessageAreaFontSize = value;
                    rtbMessageArea.FontSize = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Font Size: [" + value.ToString() + "] not supported. Min:" + minFontSize + " Max:" + maxFontSize);
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the message area prompt.
        /// </summary>
        /// <value>
        /// The color of the message area prompt.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        public Brush MessagePromptColor
        {
            get { return styleThemeManager.MessagePromptColor; }
            set
            {
                if (value != null)
                {
                    styleThemeManager.MessagePromptColor = value;
                }
                else
                {
                    throw new FormatException("Color not recognized / invalid format or is Null.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the message text.
        /// </summary>
        /// <value>
        /// The color of the message text.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        public Brush MessageTextColor
        {
            get { return styleThemeManager.MessageTextColor; }
            set
            {
                if (value != null)
                {
                    styleThemeManager.MessageTextColor = value;
                }
                else
                {
                    throw new FormatException("Color not recognized / invalid format or is Null.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the welcome message.
        /// </summary>
        /// <value>
        /// The color of the welcome message.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        public Brush WelcomeMessageColor
        {
            get { return styleThemeManager.WelcomeMessageColor; }
            set
            {
                if (value != null)
                {
                    styleThemeManager.WelcomeMessageColor = value;
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
        /// Gets or sets the color of the command area background.
        /// </summary>
        /// <value>
        /// The color of the command area background.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        public Brush CommandPromptBackgroundColor
        {
            get { return styleThemeManager.CommandPromptBackgroundColor; }
            set
            {
                if (value != null)
                {
                    txtCommandPrompt.Background = value;
                    styleThemeManager.CommandPromptBackgroundColor = value;
                }
                else
                {
                    throw new FormatException("Color not recognized / invalid format or is Null.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the message area background.
        /// </summary>
        /// <value>
        /// The color of the message area background.
        /// </value>
        /// <exception cref="FormatException">Thrown when value supplied is null or not a supported color or format.</exception>
        public Brush MessageAreaBackgroundColor
        {
            get { return styleThemeManager.MessageAreaBackgroundColor; }
            set
            {
                if (value != null)
                {
                    rtbMessageArea.Background = value;
                    styleThemeManager.MessageAreaBackgroundColor = value;
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
        /// Gets or sets the color of the command prompt border.
        /// </summary>
        /// <value>
        /// The color of the command prompt border.
        /// </value>
        public Brush CommandPromptBorderColor
        {
            get { return txtCommandPrompt.BorderBrush; }
            set
            {
                txtCommandPrompt.BorderBrush = value;
                styleThemeManager.CommandPromptBorderColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the command border thickness.
        /// </summary>
        /// <value>
        /// The command border thickness.
        /// </value>
        public Thickness CommandPromptBorderThickness
        {
            get { return txtCommandPrompt.BorderThickness; }
            set
            {
                txtCommandPrompt.BorderThickness = value;
                styleThemeManager.CommandPromptBorderThickness = value;
            }
        }

        /// <summary>
        /// Gets or sets the command padding.
        /// </summary>
        /// <value>
        /// The command padding.
        /// </value>
        public Thickness CommandPromptPadding
        {
            get { return txtCommandPrompt.Padding; }
            set
            {
                txtCommandPrompt.Padding = value;
                styleThemeManager.CommandPromptPadding = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the message area border.
        /// </summary>
        /// <value>
        /// The color of the message area border.
        /// </value>
        /// <exception cref="NullReferenceException">Thrown when class ConsoleWindow() has not been instantiated.</exception>
        public Brush MessageAreaBorderColor
        {
            get { return rtbMessageArea.BorderBrush; }
            set
            {
                rtbMessageArea.BorderBrush = value;
                styleThemeManager.MessageAreaBorderColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the message border thickness.
        /// </summary>
        /// <value>
        /// The message border thickness.
        /// </value>
        public Thickness MessageAreaBorderThickness
        {
            get { return rtbMessageArea.BorderThickness; }
            set
            {
                rtbMessageArea.BorderThickness = value;
                styleThemeManager.MessageAreaBorderThickness = value;
            }
        }

        /// <summary>
        /// Gets or sets the message padding.
        /// </summary>
        /// <value>
        /// The message padding.
        /// </value>
        public Thickness MessageAreaPadding
        {
            get { return rtbMessageArea.Padding; }
            set
            {
                rtbMessageArea.Padding = value;
                styleThemeManager.MessageAreaPadding = value;
            }
        }

        /// <summary>
        /// Gets or sets the message text paragraph margins.
        /// </summary>
        /// <value>
        /// The message text paragraph margin.
        /// </value>
        public Thickness MessageTextParagraphMargin
        {
            get { return styleThemeManager.MessageTextParagraphMargin; }
            set { styleThemeManager.MessageTextParagraphMargin = value; }
        }

        #endregion

        #region Console Initialization

        /// <summary>
        /// Initializes the console window.
        /// </summary>
        /// <param name="consoleSettings">The console settings.</param>
        private void InitializeWindow(ConsoleSettings consoleSettings)
        {
            this.SizeChanged += new SizeChangedEventHandler(consoleWindow_SizeChanged);

            this.Title = consoleSettings.ConsoleTitle;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Height = consoleSettings.DefaultConsoleHeight;
            this.Width = consoleSettings.DefaultConsoleWidth;

            defaultConsoleHeight = consoleSettings.DefaultConsoleHeight;
            defaultConsoleWidth = consoleSettings.DefaultConsoleWidth;
            maxConsoleHeight = consoleSettings.MaxConsoleHeight;
            maxConsoleWidth = consoleSettings.MaxConsoleWidth;
            minConsoleHeight = consoleSettings.MinConsoleHeight;
            minConsoleWidth = consoleSettings.MinConsoleWidth;
            
            Grid grdMain = new Grid();
            grdMain.Name = "grdMain";
            this.Content = grdMain;

            // Setup the grid layout
            ColumnDefinition cd = new ColumnDefinition();
            cd.Width = new GridLength(100, GridUnitType.Star);
            grdMain.ColumnDefinitions.Add(cd);

            RowDefinition rd1 = new RowDefinition();
            rd1.Height = new GridLength(100, GridUnitType.Star);
            grdMain.RowDefinitions.Add(rd1);
            RowDefinition rd2 = new RowDefinition();
            rd2.Height = new GridLength(0, GridUnitType.Auto);
            grdMain.RowDefinitions.Add(rd2);

            rtbMessageArea = new RichTextBox();
            Grid.SetRow(rtbMessageArea, 0);
            grdMain.Children.Add(rtbMessageArea);

            txtCommandPrompt = new TextBox();
            Grid.SetRow(txtCommandPrompt, 1);
            grdMain.Children.Add(txtCommandPrompt);

            // *Box settings
            rtbMessageArea.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            rtbMessageArea.IsReadOnly = true;
            txtCommandPrompt.TextWrapping = TextWrapping.Wrap;
            txtCommandPrompt.AcceptsReturn = false;

            // Command prompt key events
            txtCommandPrompt.KeyDown += new KeyEventHandler(txtCommandPrompt_KeyDown);
            txtCommandPrompt.PreviewKeyDown += new KeyEventHandler(txtCommandPrompt_PreviewKeyDown);
            txtCommandPrompt.PreviewKeyUp += new KeyEventHandler(txtCommandPrompt_PreviewKeyUp);
            txtCommandPrompt.KeyUp += new KeyEventHandler(txtCommandPrompt_KeyUp);
            
            InitializeConsole(consoleSettings);
        }

        /// <summary>
        /// Initializes the console.
        /// </summary>
        /// <param name="consoleSettings">The console settings.</param>
        private void InitializeConsole(ConsoleSettings consoleSettings)
        {
            allowEmptyCommand = consoleSettings.AllowEmptyCommand;
            defaultFontSize = consoleSettings.DefaultFontSize;
            defaultPrompt = consoleSettings.Prompt;
            delimeters = consoleSettings.Delimiters;
            enableCommandHistory = consoleSettings.EnableCommandHistory;
            manualCommandHistory = consoleSettings.ManualCommandHistory;
            maxFontSize = consoleSettings.MaxFontSize;
            minFontSize = consoleSettings.MinFontSize;
            prompt = consoleSettings.Prompt;
            useInternalCommandParsing = consoleSettings.UseInternalCommandParsing;
            welcomeMessage = consoleSettings.WelcomeMessage;

            txtCommandPrompt.Text = consoleSettings.Prompt;
            txtCommandPrompt.CaretIndex = txtCommandPrompt.Text.Length;

            rtbMessageArea.Document = new FlowDocument();

            SetTheme();

            Paragraph paragraph = new Paragraph();
            paragraph.Margin = new Thickness(0);
            paragraph.TextAlignment = TextAlignment.Center;
            paragraph.Inlines.Add(new Run(welcomeMessage)
            {
                Foreground = styleThemeManager.WelcomeMessageColor
            });

            WriteLineToConsole(paragraph);

            this.txtCommandPrompt.Focus();
        }

        #endregion

        #region Style Theme Control

        /// <summary>
        /// Adds a color scheme to the list.
        /// </summary>
        /// <param name="styleTheme">The color scheme to add.</param>
        public void AddStyleTheme(StyleTheme styleTheme)
        {
            styleThemeManager.AddStyleTheme(styleTheme);
        }

        /// <summary>
        /// Changes the name of the current theme. Only updates the current copy of the theme in use.
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        public void CurrentThemeName(string themeName)
        {
            styleThemeManager.StyleThemeName = themeName;
        }

        /// <summary>
        /// Creates a new style theme with the current settings. Not saved to disk.
        /// </summary>
        public void CurrentThemeToNew()
        {
            styleThemeManager.CurrentThemeToNew();
        }

        /// <summary>
        /// Gets the Current color scheme details.
        /// </summary>
        /// <returns>Description of the current color scheme as a string</returns>
        public string CurrentStyleThemeDetails()
        {
            return styleThemeManager.CurrentStyleTheme();
        }

        /// <summary>
        /// Deletes a style theme.
        /// </summary>
        /// <param name="styleThemeIndex">Index of the style theme.</param>
        public void DeleteStyleTheme(int styleThemeIndex)
        {
            styleThemeManager.DeleteStyleTheme(styleThemeIndex);
        }

        /// <summary>
        /// Gets the color scheme list.
        /// </summary>
        /// <returns>A list of the color schemes as a string</returns>
        public string GetStyleThemeList()
        {
            return styleThemeManager.GetStyleThemeList();
        }

        /// <summary>
        /// Loads the style themes from disk.
        /// </summary>
        public void LoadStyleThemes()
        {
            styleThemeManager.LoadStyleThemes();
        }

        /// <summary>
        /// Resets the console theme. Any changes are discarded.
        /// </summary>
        public void ResetStyleTheme()
        {
            styleThemeManager.ResetStyleTheme();
            SetTheme();
        }

        /// <summary>
        /// Saves the style themes to disk.
        /// </summary>
        public void SaveStyleThemes()
        {
            styleThemeManager.SaveStyleThemes();
        }

        /// <summary>
        /// Sets the color scheme.
        /// </summary>
        /// <param name="styleThemeIndex">The list index of the style theme.</param>
        public void SetStyleTheme(int styleThemeIndex)
        {
            styleThemeManager.SetStyleTheme(styleThemeIndex);
            SetTheme();
            this.Background = txtCommandPrompt.Background;
        }

        /// <summary>
        /// Sets the color scheme for the console.
        /// </summary>
        private void SetTheme()
        {
            rtbMessageArea.Background = styleThemeManager.MessageAreaBackgroundColor;
            rtbMessageArea.BorderBrush = styleThemeManager.MessageAreaBorderColor;
            rtbMessageArea.BorderThickness = styleThemeManager.MessageAreaBorderThickness;
            rtbMessageArea.FontFamily = styleThemeManager.MessageAreaFont;
            rtbMessageArea.FontSize = styleThemeManager.MessageAreaFontSize;
            rtbMessageArea.Padding = styleThemeManager.MessageAreaPadding;
            txtCommandPrompt.Background = styleThemeManager.CommandPromptBackgroundColor;
            txtCommandPrompt.BorderBrush = styleThemeManager.CommandPromptBorderColor;
            txtCommandPrompt.BorderThickness = styleThemeManager.CommandPromptBorderThickness;
            txtCommandPrompt.FontFamily = styleThemeManager.CommandPromptFont;
            txtCommandPrompt.FontSize = styleThemeManager.CommandPromptFontSize;
            txtCommandPrompt.Foreground = styleThemeManager.CommandPromptTextColor;
            txtCommandPrompt.Padding = styleThemeManager.CommandPromptPadding;
        }

        /// <summary>
        /// Updates the original style theme with the current settings. Does not save to disk.
        /// </summary>
        public void UpdateStyleTheme()
        {
            styleThemeManager.UpdateStyleTheme();
        }

        #endregion

        #region Command History Control

        /// <summary>
        /// Adds a command string to the command history.
        /// </summary>
        /// <param name="command">The command to add.</param>
        public void AddToCommandHistory(string command)
        {
            commandHistory.AddCommandToHistory(command);
        }

        /// <summary>
        /// Clears the command history.
        /// </summary>
        public void ClearCommandHistory()
        {
            commandHistory.ClearHistory();
            commandHistory.CurrentIndex = -1;
        }

        /// <summary>
        /// Gets the command at the selected index number.
        /// </summary>
        /// <param name="indexNumber">The index number.</param>
        /// <returns>The command as a string</returns>
        public string GetCommand(int indexNumber)
        {
            return commandHistory.GetCommand(indexNumber);
        }

        /// <summary>
        /// Gets the next command.
        /// </summary>
        /// <returns>The next command as a string</returns>
        public string GetNextCommand()
        {
            return commandHistory.GetNext();
        }

        /// <summary>
        /// Gets the previous command.
        /// </summary>
        /// <returns>The previous command as a string</returns>
        public string GetPreviousCommand()
        {
            return commandHistory.GetPrevious();
        }

        /// <summary>
        /// Loads the command history from an xml file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        public void LoadCommandHistory(string path, string fileName)
        {
            commandHistory.LoadCommandHistory(path, fileName);
        }

        /// <summary>
        /// Removes a command from the command history.
        /// </summary>
        /// <param name="indexNumber">The index number of the command to remove.</param>
        public void RemoveFromCommandHistory(int indexNumber)
        {
            commandHistory.RemoveFromCommandHistory(indexNumber);
        }

        /// <summary>
        /// Resets the command history.
        /// </summary>
        public void ResetCommandHistory()
        {
            commandHistory.CurrentIndex = -1;
        }

        /// <summary>
        /// Saves the command history to an xml file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        public void SaveCommandHistory(string path, string fileName)
        {
            commandHistory.SaveCommandHistory(path, fileName);
        }

        #endregion

        #region Console Control

        /// <summary>
        /// Clears the console of all content.
        /// </summary>
        public void ClearConsole()
        {
            txtCommandPrompt.Text = prompt;
            rtbMessageArea.Document = new FlowDocument();
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void CloseConsole()
        {
            this.Close();
        }

        /// <summary>
        /// Hides the console window.
        /// </summary>
        public void HideConsole()
        {
            this.Hide();
        }

        /// <summary>
        /// Resets the console window and all elements to default settings.
        /// </summary>
        /// <param name="consoleSettings">The console settings object.</param>
        public void ResetConsole(ConsoleSettings consoleSettings)
        {
            InitializeConsole(consoleSettings);
            ResetConsoleSize();
        }

        /// <summary>
        /// Resets the size of the console to default.
        /// </summary>
        public void ResetConsoleSize()
        {
            this.Height = defaultConsoleHeight;
            this.Width = defaultConsoleWidth;
        }

        /// <summary>
        /// Shows the console.
        /// </summary>
        public void ShowConsole()
        {
            this.Show();
            this.Focus();
            this.txtCommandPrompt.Focus();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the message area text.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            TextRange text = new TextRange(rtbMessageArea.Document.ContentStart, rtbMessageArea.Document.ContentEnd);
            return text.ToString();
        }

        #endregion

        #region Console Events

        /// <summary>
        /// Parses commands from the command text area.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>String array of parsed commands</returns>
        private string[] ParseCommands(string input)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(delimeters, options);
            string[] result = (from Match m in regex.Matches(input)
                               where m.Groups["token"].Success
                               select m.Groups["token"].Value).ToArray();

            return result;
        }

        private void txtCommandPrompt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return | e.Key == Key.Enter) // Catch a carrage return
            {
                string currentLine = txtCommandPrompt.Text.Trim();
                string rawCommand = currentLine.Remove(0, prompt.Length);

                if (!string.IsNullOrEmpty(rawCommand) || (string.IsNullOrEmpty(rawCommand) && allowEmptyCommand))
                {
                    SendToMessagePrompt(currentLine);

                    if (useInternalCommandParsing)
                    {
                        OnConsoleReadEvent(this, new ConsoleReadLineEventArgs(ParseCommands(rawCommand)));
                    }
                    else
                    {
                        OnConsoleReadEvent(this, new ConsoleReadLineEventArgs(rawCommand));
                    }

                    if (enableCommandHistory && !manualCommandHistory)
                    {
                        commandHistory.AddCommandToHistory(rawCommand);
                        commandHistory.CurrentIndex = -1;
                    }

                    commandHistory.CurrentCommand = string.Empty;
                    txtCommandPrompt.Text = null;
                }

                txtCommandPrompt.Text = prompt;
                txtCommandPrompt.CaretIndex = prompt.Length;
            }
        }

        // Catch any attempt at typing in the prompt text area and move caret to end of prompt if so.
        private void txtCommandPrompt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            int index = txtCommandPrompt.SelectionStart;
            if (index < prompt.Length)
            {
                txtCommandPrompt.SelectionStart = prompt.Length;
                txtCommandPrompt.SelectionLength = 0;
                e.Handled = true;
            }
        }

        // Catch any attempt at moving to the prompt text area and move caret to end of prompt if so.
        private void txtCommandPrompt_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            int index = txtCommandPrompt.SelectionStart;

            if (e.Key == Key.Left && index < prompt.Length)
            {
                txtCommandPrompt.SelectionStart = prompt.Length;
                txtCommandPrompt.SelectionLength = 0;
                e.Handled = true;
            }

            if (e.Key == Key.Back && index < prompt.Length)
            {
                txtCommandPrompt.SelectionStart = 0;
                txtCommandPrompt.SelectionLength = prompt.Length - 1;
                txtCommandPrompt.SelectedText = prompt;

                txtCommandPrompt.SelectionStart = prompt.Length;
                txtCommandPrompt.SelectionLength = 0;
                e.Handled = true;
            }
        }

        private void txtCommandPrompt_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up: // Command history next
                    if (enableCommandHistory)
                    {
                        if (string.IsNullOrEmpty(commandHistory.CurrentCommand))
                        {
                            commandHistory.CurrentCommand = txtCommandPrompt.Text.Remove(0, prompt.Length);
                        }

                        txtCommandPrompt.Text = prompt + GetNextCommand();
                        txtCommandPrompt.SelectionStart = txtCommandPrompt.Text.Length;
                        txtCommandPrompt.SelectionLength = 0;
                    }
                    break;
                case Key.Down: // Command history previous
                    if (enableCommandHistory)
                    {
                        if (string.IsNullOrEmpty(commandHistory.CurrentCommand))
                        {
                            commandHistory.CurrentCommand = txtCommandPrompt.Text.Remove(0, prompt.Length);
                        }

                        txtCommandPrompt.Text = prompt + GetPreviousCommand();
                        txtCommandPrompt.SelectionStart = txtCommandPrompt.Text.Length;
                        txtCommandPrompt.SelectionLength = 0;
                    }
                    break;
                case Key.Escape: // Command history exit
                    if (enableCommandHistory)
                    {
                        txtCommandPrompt.Text = prompt + commandHistory.CurrentCommand;
                        txtCommandPrompt.SelectionStart = txtCommandPrompt.Text.Length;
                        txtCommandPrompt.SelectionLength = 0;
                        commandHistory.CurrentCommand = string.Empty;
                        commandHistory.CurrentIndex = -1;
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Console Window Events

        private void consoleWindow_SizeChanged(object sender, EventArgs e)
        {
            if (maxConsoleHeight != 0 && this.ActualHeight > maxConsoleHeight)
            {
                this.Height = maxConsoleHeight;
            }

            if (maxConsoleWidth != 0 && this.ActualWidth > maxConsoleWidth)
            {
                this.Width = maxConsoleWidth;
            }
        }

        #endregion

        #region Console Write Line

        /// <summary>
        /// Sends a string to the console output.
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="e">The <see cref="WPFCommandPrompt.ConsoleWriteLineEventArgs"/> instance containing the event data.</param>
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
                    WriteLineToConsole(e.Message, styleThemeManager.MessageTextColor);
                }
            }
        }

        /// <summary>
        /// Sends a string to the console output with the desired font color.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <param name="foreground">The foreground.</param>
        private void WriteLineToConsole(string output, Brush foreground)
        {
            Paragraph paragraph;
            paragraph = new Paragraph();
            paragraph.Margin = styleThemeManager.MessageTextParagraphMargin;
            paragraph.Inlines.Add(new Run(output)
            {
                Foreground = foreground
            });

            rtbMessageArea.Document.Blocks.Add(paragraph);
            rtbMessageArea.ScrollToEnd();
        }

        /// <summary>
        /// Writes a FlowDocument paragraph to the console.
        /// </summary>
        /// <param name="paragraph">The paragraph.</param>
        private void WriteLineToConsole(Paragraph paragraph)
        {
            rtbMessageArea.Document.Blocks.Add(paragraph);
            rtbMessageArea.ScrollToEnd();
        }

        /// <summary>
        /// Sends a string to the console window. Internal only.
        /// </summary>
        /// <param name="output">The string to output.</param>
        private void SendToMessagePrompt(string output)
        {
            Paragraph paragraph;
            paragraph = new Paragraph();
            paragraph.Margin = new Thickness(0);
            paragraph.Inlines.Add(new Run(output)
            {
                Foreground = styleThemeManager.MessagePromptColor
            });

            rtbMessageArea.Document.Blocks.Add(paragraph);
            rtbMessageArea.ScrollToEnd();
        }

        #endregion
        
        #region Read Line Event

        /// <summary>
        /// Occurs on a console read line update event.
        /// </summary>
        public event ReadLineEventHandler ConsoleReadLine;

        /// <summary>
        /// Called on a console read line update event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="WPFCommandPrompt.ConsoleReadLineEventArgs"/> instance containing the event data.</param>
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
