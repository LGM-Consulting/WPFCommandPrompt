// Author: Adrian Hum
// Project: WPFCommandPromptDemo/MainWindow.xaml.cs
// 
// Created : 2016-01-06  18:33 
// Modified: 2016-01-06 18:34)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using WPFCommandPrompt;

namespace WPFCommandPromptDemo {
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow  {
        private readonly List<CommandHelp> _commandDictionary = new List<CommandHelp>();
        private WpfPrompt _commandPrompt;

        public MainWindow()
        {
            InitializeComponent();
            CreateHelpDictionary();

            _commandPrompt = new WpfPrompt();
            _commandPrompt.Prompt = "console demo>";
            // Registers CommandProcess() with the ReadLine() event from the console.
            _commandPrompt.ReadLine += CommandProcess;
            //commandPrompt.LoadSettings(); // Loads the settings if they exist otherwise uses defaults.
            _commandPrompt.Show();

            WindowState = WindowState.Minimized;
        }

        private void CommandProcess(object sender, ConsoleReadLineEventArgs eventArgs)
        {
            var command = new string[eventArgs.Commands.Length];

            for (var i = 0; i < eventArgs.Commands.Length; i++)
            {
                command[i] = eventArgs.Commands[i].ToLower();
            }

            if (command.Length > 0)
            {
                try
                {
                    switch (command[0])
                    {
                        case "clear":
                            ProcessClear(command);
                            break;
                        case "demo":
                            ProcessParagraph(command);
                            break;
                        case "exit":
                            _commandPrompt.Close();
                            _commandPrompt = null;
                            Close();
                            break;
                        case "get":
                            ProcessGet(command);
                            break;
                        case "help":
                            ProcessHelp(command);
                            break;
                        case "list":
                            ProcessList(command);
                            break;
                        case "load":
                            ProcessLoad(command);
                            break;
                        case "reset":
                            ProcessReset(command);
                            break;
                        case "save":
                            ProcessSave(command);
                            break;
                        case "set":
                            ProcessSet(command);
                            break;
                        default:
                            WriteToConsole(
                                new ConsoleWriteLineEventArgs("Command not recognized: " + command[0] +
                                                              " (Type 'help' for a list of commands)"));
                            break;
                    }
                }
                catch (Exception ex)
                {
                    WriteToConsole(new ConsoleWriteLineEventArgs("Console Error: \r" + ex.Message));
                }
            }
        }

        // 
        /// <summary>
        ///     Write text to the console, or could alternatly call:
        ///     commandPrompt.WriteLine(string)
        ///     commandPrompt.WriteLine(string, Brush)
        ///     commandPrompt.WriteLine(Paragraph)
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="WPFCommandPrompt.ConsoleWriteLineEventArgs" /> instance containing the event data.</param>
        private void WriteToConsole(ConsoleWriteLineEventArgs e)
        {
            _commandPrompt.OnConsoleWriteEvent(this, e);
        }

        #region Process commands

        private void ProcessClear(string[] command)
        {
            if (command.Length > 1)
            {
                if (command[1] == "history")
                {
                    _commandPrompt.ClearCommandHistory();
                }
                else
                {
                    WriteToConsole(
                        new ConsoleWriteLineEventArgs("Command argument [" + command[1] + "] not recognized: \r" +
                                                      GetCommandEntry(command[0])));
                }
            }
            else
            {
                _commandPrompt.ClearConsole();
            }
        }

        private void ProcessGet(string[] command)
        {
            if (command.Length > 1)
            {
                if (command[1] == "console" && command.Length > 2)
                {
                    switch (command[2])
                    {
                        case "height":
                            WriteToConsole(
                                new ConsoleWriteLineEventArgs("Console height: " + _commandPrompt.ConsoleHeight));
                            break;
                        case "width":
                            WriteToConsole(new ConsoleWriteLineEventArgs("Console width: " + _commandPrompt.ConsoleWidth));
                            break;
                        case "theme":
                            WriteToConsole(new ConsoleWriteLineEventArgs(_commandPrompt.CurrentStyleThemeDetails()));
                            break;
                        default:
                            WriteToConsole(
                                new ConsoleWriteLineEventArgs("Command argument [" + command[2] + "] not recognized: \r" +
                                                              GetCommandEntry(command[0])));
                            break;
                    }
                }
                else
                {
                    WriteToConsole(new ConsoleWriteLineEventArgs("Command argument [" + command[1] +
                                                                 "] not recognized :\r" + GetCommandEntry(command[0])));
                }
            }
            else
            {
                WriteToConsole(
                    new ConsoleWriteLineEventArgs("Missing arguments for [" + command[0] + "]:\r" +
                                                  GetCommandEntry(command[0])));
            }
        }

        private void ProcessHelp(string[] command)
        {
            var helpCommands = new StringBuilder();

            if (command.Count() > 1)
            {
                var ch = GetCommandEntry(command[1]);
                if (ch != null)
                {
                    helpCommands.Append(ch);
                }
                else
                {
                    helpCommands.Append("Command [" + command[1] + "] not found");
                }
            }
            else
            {
                foreach (var commands in _commandDictionary)
                {
                    helpCommands.Append(commands.ShortDescription() + "\r");
                }
            }

            WriteToConsole(new ConsoleWriteLineEventArgs(helpCommands.ToString()));
        }

        private void ProcessList(string[] command)
        {
            if (command.Length > 1)
            {
                switch (command[1])
                {
                    case "themes":
                        WriteToConsole(new ConsoleWriteLineEventArgs(_commandPrompt.GetStyleThemeList()));
                        break;
                    default:
                        WriteToConsole(
                            new ConsoleWriteLineEventArgs("Command argument [" + command[1] + "] not recognized:\r" +
                                                          GetCommandEntry(command[0])));
                        break;
                }
            }
            else
            {
                WriteToConsole(
                    new ConsoleWriteLineEventArgs("Missing arguments for [" + command[0] + "]:\r" +
                                                  GetCommandEntry(command[0])));
            }
        }

        private void ProcessLoad(string[] command)
        {
            if (command.Length > 1)
            {
                switch (command[1])
                {
                    case "commandhistory":
                        if (command.Length == 2)
                        {
                            try
                            {
                                _commandPrompt.LoadCommandHistory(); // Load from defaults
                                WriteToConsole(new ConsoleWriteLineEventArgs("Command history loaded"));
                            }
                            catch (Exception ex)
                            {
                                WriteToConsole(
                                    new ConsoleWriteLineEventArgs("Error loading command history: " + ex.Message));
                            }
                        }
                        else if (command.Length > 2)
                        {
                            try
                            {
                                _commandPrompt.LoadCommandHistory(command[2]);
                                    // File name must be in quotes: "history.xml"
                                WriteToConsole(new ConsoleWriteLineEventArgs("Command history loaded"));
                            }
                            catch (Exception ex)
                            {
                                WriteToConsole(
                                    new ConsoleWriteLineEventArgs("Error loading command history: " + ex.Message));
                            }
                        }
                        else
                        {
                            WriteToConsole(
                                new ConsoleWriteLineEventArgs("Missing arguments for [" + command[1] +
                                                              "] (Type 'help' for a list of commands)"));
                        }
                        break;
                    case "themes":
                        try
                        {
                            _commandPrompt.LoadStyleThemes();
                            WriteToConsole(new ConsoleWriteLineEventArgs("Style themes loaded"));
                        }
                        catch (Exception ex)
                        {
                            WriteToConsole(new ConsoleWriteLineEventArgs("Error loading style themes: " + ex.Message));
                        }
                        break;
                    default:
                        WriteToConsole(
                            new ConsoleWriteLineEventArgs("Command not recognized: " + command[1] +
                                                          " (Type 'help' for a list of commands)"));
                        break;
                }
            }
            else
            {
                WriteToConsole(
                    new ConsoleWriteLineEventArgs("Missing arguments for [" + command[0] + "]:\r" +
                                                  GetCommandEntry(command[0])));
            }
        }

        private void ProcessParagraph(string[] command)
        {
            // Demoes the use the FlowDocument Paragraph object for formatting the console output.
            var paragraph = new Paragraph();
            paragraph.Inlines.Add(
                "Using a FlowDocument Paragraph can allow many different text style and formatting options");
            paragraph.Inlines.Add("\rThis text is ");
            paragraph.Inlines.Add(new Run("Red and Normal")
            {
                Foreground = Brushes.Red
            });
            paragraph.Inlines.Add(new Run("\rThis text is "));
            paragraph.Inlines.Add(new Bold(new Run("Blue and BOLD"))
            {
                Foreground = Brushes.Blue
            });
            paragraph.Inlines.Add(new Run("\rThis text is "));
            paragraph.Inlines.Add(new Italic(new Run("Yellow and Italic"))
            {
                Foreground = Brushes.Yellow
            });

            _commandPrompt.WriteLine(paragraph);
        }

        private void ProcessReset(string[] command)
        {
            if (command.Length > 1 && !string.IsNullOrEmpty(command[1]))
            {
                switch (command[1])
                {
                    case "console":
                        _commandPrompt.ResetConsole();
                        break;
                    case "size":
                        _commandPrompt.ResetConsoleSize();
                        break;
                    case "theme":
                        _commandPrompt.ResetStyleTheme();
                        break;
                    case "clear":
                        _commandPrompt.ClearConsole();
                        break;
                    default:
                        WriteToConsole(
                            new ConsoleWriteLineEventArgs("Command argument [" + command[1] + "] not recognized: \r" +
                                                          GetCommandEntry(command[0])));
                        break;
                }
            }
            else
            {
                WriteToConsole(
                    new ConsoleWriteLineEventArgs("Missing argument for [" + command[0] + "]:\r" +
                                                  GetCommandEntry(command[0])));
            }
        }

        private void ProcessSave(string[] command)
        {
            if (command.Length > 1)
            {
                switch (command[1])
                {
                    case "settings":
                        try
                        {
                            _commandPrompt.SaveSettings();
                            WriteToConsole(new ConsoleWriteLineEventArgs("Settings saved"));
                        }
                        catch (Exception ex)
                        {
                            WriteToConsole(new ConsoleWriteLineEventArgs("Error saving settings: " + ex.Message));
                        }
                        break;
                    case "commandhistory":
                        if (command.Length == 2)
                        {
                            try
                            {
                                _commandPrompt.SaveCommandHistory(); // Saves to default
                                WriteToConsole(new ConsoleWriteLineEventArgs("Command history saved"));
                            }
                            catch (Exception ex)
                            {
                                WriteToConsole(
                                    new ConsoleWriteLineEventArgs("Error saving command history: " + ex.Message));
                            }
                        }
                        else if (command.Length > 2)
                        {
                            try
                            {
                                _commandPrompt.SaveCommandHistory(command[2]);
                                    // File name must be in quotes: "history.xml"
                                WriteToConsole(new ConsoleWriteLineEventArgs("Command history saved"));
                            }
                            catch (Exception ex)
                            {
                                WriteToConsole(
                                    new ConsoleWriteLineEventArgs("Error saving command history: " + ex.Message));
                            }
                        }
                        else
                        {
                            WriteToConsole(
                                new ConsoleWriteLineEventArgs("Missing arguments for [" + command[1] +
                                                              "] (Type 'help' for a list of commands)"));
                        }
                        break;
                    case "themes":
                        try
                        {
                            _commandPrompt.SaveStyleThemes();
                            WriteToConsole(new ConsoleWriteLineEventArgs("Style themes saved"));
                        }
                        catch (Exception ex)
                        {
                            WriteToConsole(new ConsoleWriteLineEventArgs("Error saving style themes: " + ex.Message));
                        }
                        break;
                    default:
                        WriteToConsole(
                            new ConsoleWriteLineEventArgs("Command not recognized: " + command[1] +
                                                          " (Type 'help' for a list of commands)"));
                        break;
                }
            }
            else
            {
                WriteToConsole(
                    new ConsoleWriteLineEventArgs("Missing arguments for [" + command[0] + "]:\r" +
                                                  GetCommandEntry(command[0])));
            }
        }

        private void ProcessSet(string[] command)
        {
            var isNum = false;
            var num = 0;

            if (command.Count() > 1)
            {
                if (command.Count() > 2)
                {
                    switch (command[1])
                    {
                        case "console":
                            switch (command[2])
                            {
                                case "theme":
                                    if (command.Count() >= 4)
                                    {
                                        isNum = Int32.TryParse(command[3], out num);
                                        if (isNum)
                                        {
                                            _commandPrompt.SetStyleTheme(num);
                                        }
                                        else
                                        {
                                            WriteToConsole(
                                                new ConsoleWriteLineEventArgs("Command argument [" + command[3] +
                                                                              "] not a number"));
                                        }
                                    }
                                    else
                                    {
                                        WriteToConsole(
                                            new ConsoleWriteLineEventArgs("Missing command arguments for [" + command[2] +
                                                                          "]:\r" + GetCommandEntry(command[0])));
                                    }
                                    break;
                                case "font":
                                    if (command.Length >= 4)
                                    {
                                        var fontFamily = new FontFamily(command[3]);

                                        if (fontFamily != null)
                                        {
                                            _commandPrompt.MessageAreaFont = fontFamily;
                                            _commandPrompt.CommandPromptFont = fontFamily;
                                        }
                                        else
                                        {
                                            WriteToConsole(
                                                new ConsoleWriteLineEventArgs("Invalid font name [" + command[3] + "]"));
                                        }
                                    }
                                    else
                                    {
                                        WriteToConsole(
                                            new ConsoleWriteLineEventArgs("Missing command arguments for [" + command[2] +
                                                                          "]:\r" + GetCommandEntry(command[0])));
                                    }
                                    break;
                                case "fontsize":
                                    if (command.Length >= 4)
                                    {
                                        isNum = Int32.TryParse(command[3], out num);

                                        if (isNum)
                                        {
                                            _commandPrompt.MessageAreaFontSize = num;
                                            _commandPrompt.CommandPromptFontSize = num;
                                        }
                                        else
                                        {
                                            WriteToConsole(
                                                new ConsoleWriteLineEventArgs("Command argument [" + command[3] +
                                                                              "] not a number"));
                                        }
                                    }
                                    else
                                    {
                                        WriteToConsole(
                                            new ConsoleWriteLineEventArgs("Missing command arguments for [" + command[2] +
                                                                          "]:\r" + GetCommandEntry(command[0])));
                                    }
                                    break;
                                case "height":
                                    if (command.Count() >= 4)
                                    {
                                        isNum = Int32.TryParse(command[3], out num);
                                        if (isNum)
                                        {
                                            _commandPrompt.ConsoleHeight = num;
                                        }
                                        else
                                        {
                                            WriteToConsole(
                                                new ConsoleWriteLineEventArgs("Command argument [" + command[3] +
                                                                              "] not a number"));
                                        }
                                    }
                                    else
                                    {
                                        WriteToConsole(
                                            new ConsoleWriteLineEventArgs("Missing command arguments for [" + command[2] +
                                                                          "]:\r" + GetCommandEntry(command[0])));
                                    }
                                    break;
                                case "width":
                                    if (command.Count() >= 4)
                                    {
                                        isNum = Int32.TryParse(command[3], out num);
                                        if (isNum)
                                        {
                                            _commandPrompt.ConsoleWidth = num;
                                        }
                                        else
                                        {
                                            WriteToConsole(
                                                new ConsoleWriteLineEventArgs("Command argument [" + command[3] +
                                                                              "] not a number"));
                                        }
                                    }
                                    else
                                    {
                                        WriteToConsole(
                                            new ConsoleWriteLineEventArgs("Missing command arguments for [" + command[2] +
                                                                          "]:\r" + GetCommandEntry(command[0])));
                                    }
                                    break;
                                default:
                                    WriteToConsole(
                                        new ConsoleWriteLineEventArgs("Command argument [" + command[2] +
                                                                      "] not recognized: \r" +
                                                                      GetCommandEntry(command[0])));
                                    break;
                            }
                            break;
                        case "messagearea":
                        case "commandarea":
                            if (command.Length > 3)
                            {
                                switch (command[2])
                                {
                                    case "font":
                                        if (command.Length >= 4)
                                        {
                                            var fontFamily = new FontFamily(command[3]);

                                            if (fontFamily != null)
                                            {
                                                if (command[1] == "messagearea")
                                                {
                                                    _commandPrompt.MessageAreaFont = fontFamily;
                                                }
                                                else
                                                {
                                                    _commandPrompt.CommandPromptFont = fontFamily;
                                                }
                                            }
                                            else
                                            {
                                                WriteToConsole(
                                                    new ConsoleWriteLineEventArgs("Invalid font name [" + command[2] +
                                                                                  "]"));
                                            }
                                        }
                                        else
                                        {
                                            WriteToConsole(
                                                new ConsoleWriteLineEventArgs("Missing command arguments for [" +
                                                                              command[2] +
                                                                              "]:\r" + GetCommandEntry(command[0])));
                                        }
                                        break;
                                    case "fontsize":
                                        if (command.Length >= 4)
                                        {
                                            isNum = Int32.TryParse(command[3], out num);

                                            if (isNum)
                                            {
                                                if (command[1] == "messagearea")
                                                {
                                                    _commandPrompt.MessageAreaFontSize = num;
                                                }
                                                else
                                                {
                                                    _commandPrompt.CommandPromptFontSize = num;
                                                }
                                            }
                                            else
                                            {
                                                WriteToConsole(
                                                    new ConsoleWriteLineEventArgs("Command argument [" + command[3] +
                                                                                  "] not a number:"));
                                            }
                                        }
                                        else
                                        {
                                            WriteToConsole(
                                                new ConsoleWriteLineEventArgs("Missing command arguments for [" +
                                                                              command[2] +
                                                                              "]:\r" + GetCommandEntry(command[0])));
                                        }
                                        break;
                                    case "color":
                                        if (command.Length >= 4)
                                        {
                                            Brush b;

                                            b = Utility.StringToBrush(command[3]);

                                            if (b != null)
                                            {
                                                if (command[1] == "messagearea")
                                                {
                                                    _commandPrompt.MessageBackgroundColor = b;
                                                }
                                                else
                                                {
                                                    _commandPrompt.CommandPromptBackgroundColor = b;
                                                }
                                            }
                                            else
                                            {
                                                WriteToConsole(
                                                    new ConsoleWriteLineEventArgs("Command argument [" + command[3] +
                                                                                  "] not a valid color"));
                                            }
                                        }
                                        break;
                                    default:
                                        WriteToConsole(
                                            new ConsoleWriteLineEventArgs("Command argument [" + command[2] +
                                                                          "] not recognized: \r" +
                                                                          GetCommandEntry(command[0])));
                                        break;
                                }
                            }
                            else
                            {
                                WriteToConsole(
                                    new ConsoleWriteLineEventArgs("Missing command arguments for [" + command[1] +
                                                                  "]:\r" + GetCommandEntry(command[0])));
                            }
                            break;
                        default:
                            WriteToConsole(new ConsoleWriteLineEventArgs("Command argument [" + command[1] +
                                                                         "] not recognized :\r" +
                                                                         GetCommandEntry(command[0])));
                            break;
                    }
                }
                else
                {
                    WriteToConsole(new ConsoleWriteLineEventArgs("Missing command arguments for [" + command[1] +
                                                                 "]:\r" + GetCommandEntry(command[0])));
                }
            }
            else
            {
                WriteToConsole(new ConsoleWriteLineEventArgs("Missing command arguments for [" + command[0] +
                                                             "]:\r" + GetCommandEntry(command[0])));
            }
        }

        #endregion

        #region Help Dictionary

        private CommandHelp GetCommandEntry(string command)
        {
            return _commandDictionary.SingleOrDefault(x => x.Command == command);
        }

        private void CreateHelpDictionary()
        {
            List<string> temp;

            temp = new List<string>();
            temp.Add("clear <none> : Clears the console text");
            temp.Add("clear history : clears the command history");
            _commandDictionary.Add(new CommandHelp
            {
                Command = "clear",
                CommandDescription = "Clears the console text",
                CommandArguments = "clear <optional: history>",
                CommandDetails = temp
            });

            temp = new List<string>();
            temp.Add("-No options available");
            _commandDictionary.Add(new CommandHelp
            {
                Command = "demo",
                CommandDescription = "Demo of multi colored text using a FlowDocument Paragraph",
                CommandArguments = "demo <none>",
                CommandDetails = temp
            });

            temp = new List<string>();
            temp.Add("-No options available");
            _commandDictionary.Add(new CommandHelp
            {
                Command = "exit",
                CommandDescription = "Exits the console",
                CommandArguments = "exit <none>",
                CommandDetails = temp
            });

            temp = new List<string>();
            temp.Add("get console height : Gets the current height of the console");
            temp.Add("get console width : Gets the current width of the console");
            temp.Add("get console theme : Gets the current style theme of the console");
            _commandDictionary.Add(new CommandHelp
            {
                Command = "get",
                CommandDescription = "Returns selected console settings",
                CommandArguments = "get <target> <argument>",
                CommandDetails = temp
            });

            temp = new List<string>();
            temp.Add("help <demo, exit, get, list, set, reset>");
            _commandDictionary.Add(new CommandHelp
            {
                Command = "help",
                CommandDescription =
                    "Returns a list of commands and their descriptions, \r\tor details for an individual command",
                CommandArguments = "help <optional: command>",
                CommandDetails = temp
            });

            temp = new List<string>();
            temp.Add("list themes : Lists the available style themes for the console");
            _commandDictionary.Add(new CommandHelp
            {
                Command = "list",
                CommandDescription = "Retuns a list for the selected argument",
                CommandArguments = "list <argument>",
                CommandDetails = temp
            });

            temp = new List<string>();
            temp.Add("load commandhistory <optional: filename> : Loads a command history from an xml file");
            temp.Add("load themes : Loads the style themes from disk");
            _commandDictionary.Add(new CommandHelp
            {
                Command = "load",
                CommandDescription = "Loads data from a file to the console",
                CommandArguments = "load <argument> <value>",
                CommandDetails = temp
            });

            temp = new List<string>();
            temp.Add(
                "save settings <optional: path> : Saves console settings to an xml file with an optional path including file name");
            temp.Add("save commandhistory <optional: filename> : Saves the command history to an xml file");
            temp.Add("save themes : Saves the style themes to disk");
            _commandDictionary.Add(new CommandHelp
            {
                Command = "save",
                CommandDescription = "Saves console data to a file",
                CommandArguments = "save <argument> <value>",
                CommandDetails = temp
            });

            temp = new List<string>();
            temp.Add("set console theme <value> : Sets the style theme for the console");
            temp.Add("set console font <value> : Sets the font for the console");
            temp.Add("set console fontsize <value> : Sets the font size for the console");
            temp.Add("set console height <value> : Sets the console height");
            temp.Add("set console width <value> : Sets the console width");
            temp.Add("set commandarea color <value> : Sets the background color for the command area");
            temp.Add("set commandarea font <value> : Sets the font for the command area");
            temp.Add("set commandarea fontsize <value> : Sets the font size for the command area");
            temp.Add("set messagearea color <value> : Sets the background color for the message area");
            temp.Add("set messagearea font <value> : Sets the font for the message area");
            temp.Add("set messagearea fontsize <value> : Sets the font size for the message area");
            _commandDictionary.Add(new CommandHelp
            {
                Command = "set",
                CommandDescription = "Sets various console settings",
                CommandArguments = "set <target> <argument> <value>",
                CommandDetails = temp
            });

            temp = new List<string>();
            temp.Add("reset console : Resets the console to default settings.");
            temp.Add("reset size : Resets the console size to default");
            temp.Add("reset theme : Resets the style theme to default [0]");
            _commandDictionary.Add(new CommandHelp
            {
                Command = "reset",
                CommandDescription = "Resets the console or various console settings",
                CommandArguments = "reset <argument>",
                CommandDetails = temp
            });
        }

        #endregion
    }
}