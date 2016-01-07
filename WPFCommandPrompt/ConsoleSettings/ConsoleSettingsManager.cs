// Author: Adrian Hum
// Project: WPFCommandPrompt/ConsoleSettingsManager.cs
// 
// Created : 2016-01-06  18:29 
// Modified: 2016-01-06 18:35)

using System;
using System.IO;

namespace WPFCommandPrompt.ConsoleSettings {
    /// <summary>
    ///     Manages the loading/saving of console settings.
    /// </summary>
    internal static class ConsoleSettingsManager {
        /// <summary>
        ///     Loads the default settings.
        /// </summary>
        /// <returns>Console Settings object with defaults</returns>
        public static ConsoleSettings LoadDefaults()
        {
            return SetDefaults();
        }

        /// <summary>
        ///     Reads the settings from disk.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Console Settings object</returns>
        public static ConsoleSettings LoadSettings(string path, string fileName)
        {
            try
            {
                if (!path.EndsWith(@"\\") || !path.EndsWith(@"\"))
                {
                    path += "\\";
                }
                return Utility.XmlFileToObject<ConsoleSettings>(path + fileName);
            }
            catch (FileNotFoundException)
            {
                return SetDefaults();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///     Sets the default console setting.
        /// </summary>
        /// <returns>ConsoleSettings object with defaults</returns>
        private static ConsoleSettings SetDefaults()
        {
            var settings = new ConsoleSettings
            {
                AllowEmptyCommand = false,
                ConsoleTitle = "WPF Command Prompt - V" + Utility.AssemblyVersion(true, true, false),
                DefaultConsoleHeight = 400.00,
                DefaultConsoleWidth = 600.00,
                DefaultFontSize = 12.00,
                DefaultPrompt = ">",
                Delimiters = @"((""((?<token>.*?)"")|(?<token>[\w]+))(\s)*)",
                EnableCommandHistory = true,
                EnableLoadStyleThemes = false,
                ManualCommandHistory = false,
                MaxConsoleHeight = 0.00,
                MaxConsoleWidth = 0.00,
                MaxFontSize = 44.00,
                MinConsoleHeight = 400.00,
                MinConsoleWidth = 600.00,
                MinFontSize = 8.00,
                Prompt = ">",
                StyleThemeIndex = 0,
                UseInternalCommandParsing = true,
                WelcomeMessage =
                    "---Welcome to WPF Command Prompt - Verson " + Utility.AssemblyVersion(true, true, true) + "---"
            };

            return settings;
        }

        /// <summary>
        ///     Writes the settings to disk.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="consoleSettings">The console settings</param>
        public static void SaveSettings(string path, string fileName, ConsoleSettings consoleSettings)
        {
            if (!path.EndsWith(@"\\") || !path.EndsWith(@"\"))
            {
                path += "\\";
            }
            Utility.ObjectToXMlFile(path + fileName, consoleSettings);
        }
    }
}