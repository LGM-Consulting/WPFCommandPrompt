// Author: Adrian Hum
// Project: WPFCommandPrompt/StyleThemeManager.cs
// 
// Created : 2016-01-06  18:29 
// Modified: 2016-01-06 18:35)

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using SystemFonts = System.Drawing.SystemFonts;

namespace WPFCommandPrompt.StyleThemes {
    /// <summary>
    ///     Manages the console window style themes.
    /// </summary>
    internal class StyleThemeManager {
        private const string ThemesFileName = "StyleThemes.xml";
        private StyleTheme _selectedStyleTheme;
        private List<StyleTheme> _styleThemes;

        #region Default Themes

        private void AddDefaultThemes()
        {
            _styleThemes = new List<StyleTheme>();

            AddStyleTheme(new StyleTheme
            {
                StyleThemeName = "Default",
                CommandPromptBackgroundColor = "Black",
                CommandPromptBorderColor = "Black",
                CommandPromptBorderThickness = "0,0,0,0",
                CommandPromptTextColor = "White",
                CommandPromptFont = SystemFonts.DefaultFont.ToString(),
                CommandPromptFontSize = 12.00,
                CommandPromptPadding = "0,0,0,0",
                MessageAreaBackgroundColor = "Black",
                MessageAreaBorderColor = "Black",
                MessageAreaBorderThickness = "0,0,0,0",
                MessageAreaFont = SystemFonts.DefaultFont.ToString(),
                MessageAreaFontSize = 12.00,
                MessageAreaPadding = "0,0,0,0",
                MessagePromptColor = "White",
                MessageTextColor = "White",
                MessageTextParagraphMargin = "5,0,0,5",
                WelcomeMessageColor = "White"
            });

            AddStyleTheme(new StyleTheme
            {
                StyleThemeName = "Blue, Black and Green",
                CommandPromptBackgroundColor = "Black",
                CommandPromptBorderColor = "Black",
                CommandPromptBorderThickness = "0,0,0,0",
                CommandPromptTextColor = "White",
                CommandPromptFont = SystemFonts.DefaultFont.ToString(),
                CommandPromptFontSize = 14.00,
                CommandPromptPadding = "0,0,0,0",
                MessageAreaBackgroundColor = "Black",
                MessageAreaBorderColor = "Black",
                MessageAreaBorderThickness = "0,0,0,0",
                MessageAreaFont = SystemFonts.DefaultFont.ToString(),
                MessageAreaFontSize = 14.00,
                MessageAreaPadding = "0,0,0,0",
                MessagePromptColor = "LightBlue",
                MessageTextColor = "LightGreen",
                MessageTextParagraphMargin = "5,0,0,5",
                WelcomeMessageColor = "Yellow"
            });

            AddStyleTheme(new StyleTheme
            {
                StyleThemeName = "Green and Blue On Black",
                CommandPromptBackgroundColor = "Black",
                CommandPromptBorderColor = "Blue",
                CommandPromptBorderThickness = "0,0,0,0",
                CommandPromptTextColor = "LightGreen",
                CommandPromptFont = SystemFonts.DefaultFont.ToString(),
                CommandPromptFontSize = 12.00,
                MessageAreaBackgroundColor = "Black",
                MessageAreaBorderColor = "Black",
                MessageAreaBorderThickness = "0,0,0,0",
                MessageAreaFont = SystemFonts.DefaultFont.ToString(),
                MessageAreaFontSize = 12.00,
                MessageAreaPadding = "0,0,0,0",
                MessagePromptColor = "LightGreen",
                MessageTextColor = "LightBlue",
                MessageTextParagraphMargin = "5,0,0,5",
                WelcomeMessageColor = "Yellow"
            });

            AddStyleTheme(new StyleTheme
            {
                StyleThemeName = "Dark Grey",
                CommandPromptBackgroundColor = "Black",
                CommandPromptBorderColor = "Green",
                CommandPromptBorderThickness = "0,0,0,0",
                CommandPromptTextColor = "LightGreen",
                CommandPromptFont = SystemFonts.DefaultFont.ToString(),
                CommandPromptFontSize = 12.00,
                MessageAreaBackgroundColor = "#3F3F3F",
                MessageAreaBorderColor = "Black",
                MessageAreaBorderThickness = "0,0,0,0",
                MessageAreaFont = SystemFonts.DefaultFont.ToString(),
                MessageAreaFontSize = 12.00,
                MessageAreaPadding = "0,0,0,0",
                MessagePromptColor = "Yellow",
                MessageTextColor = "SkyBlue",
                MessageTextParagraphMargin = "5,0,0,5",
                WelcomeMessageColor = "AntiqueWhite"
            });

            AddStyleTheme(new StyleTheme
            {
                StyleThemeName = "Gray Scheme",
                CommandPromptBackgroundColor = "LightGray",
                CommandPromptBorderColor = "Black",
                CommandPromptBorderThickness = "0,0,0,0",
                CommandPromptTextColor = "Black",
                CommandPromptFont = SystemFonts.DefaultFont.ToString(),
                CommandPromptFontSize = 14.00,
                MessageAreaBackgroundColor = "LightGray",
                MessageAreaBorderColor = "Black",
                MessageAreaBorderThickness = "0,0,0,0",
                MessageAreaFont = SystemFonts.DefaultFont.ToString(),
                MessageAreaFontSize = 14.00,
                MessageAreaPadding = "0,0,0,0",
                MessagePromptColor = "Green",
                MessageTextColor = "Blue",
                MessageTextParagraphMargin = "5,0,0,5",
                WelcomeMessageColor = "Black"
            });

            AddStyleTheme(new StyleTheme
            {
                StyleThemeName = "Baby Blue And Black",
                CommandPromptBackgroundColor = "Black",
                CommandPromptBorderColor = "Black",
                CommandPromptBorderThickness = "0,0,0,0",
                CommandPromptTextColor = "LightGreen",
                CommandPromptFont = "Ariel",
                CommandPromptFontSize = 16.00,
                MessageAreaBackgroundColor = "#F8F8F8",
                MessageAreaBorderColor = "Black",
                MessageAreaBorderThickness = "0,0,0,0",
                MessageAreaFont = "Ariel",
                MessageAreaFontSize = 16.00,
                MessageAreaPadding = "0,0,0,0",
                MessagePromptColor = "DarkBlue",
                MessageTextColor = "#008038",
                MessageTextParagraphMargin = "5,0,0,5",
                WelcomeMessageColor = "Blue"
            });

            AddStyleTheme(new StyleTheme
            {
                StyleThemeName = "Black On White",
                CommandPromptBackgroundColor = "White",
                CommandPromptBorderColor = "Black",
                CommandPromptBorderThickness = "0,0,0,0",
                CommandPromptTextColor = "Black",
                CommandPromptFont = SystemFonts.DefaultFont.ToString(),
                CommandPromptFontSize = 12.00,
                MessageAreaBackgroundColor = "White",
                MessageAreaBorderColor = "Black",
                MessageAreaBorderThickness = "0,0,0,0",
                MessageAreaFont = SystemFonts.DefaultFont.ToString(),
                MessageAreaFontSize = 12.00,
                MessageAreaPadding = "0,0,0,0",
                MessagePromptColor = "Black",
                MessageTextColor = "Blue",
                MessageTextParagraphMargin = "5,0,0,5",
                WelcomeMessageColor = "Blue"
            });
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="StyleThemeManager" /> class.
        /// </summary>
        public StyleThemeManager()
        {
            AddDefaultThemes();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StyleThemeManager" /> class.
        /// </summary>
        /// <param name="styleThemeIndex">Index of the style theme.</param>
        /// <param name="loadStyleThemes">if set to <c>true</c> [load style themes].</param>
        public StyleThemeManager(int styleThemeIndex, bool loadStyleThemes)
        {
            if (loadStyleThemes)
            {
                LoadStyleThemes();
            }
            else
            {
                AddDefaultThemes();
            }

            _selectedStyleTheme = GetStyleTheme(styleThemeIndex);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the style theme index ID.
        /// </summary>
        public int StyleThemeIndexId
        {
            get { return _selectedStyleTheme.StyleThemeIndexId; }
        }

        /// <summary>
        ///     Gets the name of the style theme.
        /// </summary>
        /// <value>
        ///     The name of the style theme.
        /// </value>
        public string StyleThemeName
        {
            get { return _selectedStyleTheme.StyleThemeName; }
            set { _selectedStyleTheme.StyleThemeName = value; }
        }

        /// <summary>
        ///     Gets or sets the color of the command background.
        /// </summary>
        /// <value>
        ///     The color of the command background.
        /// </value>
        public Brush CommandPromptBackgroundColor
        {
            get { return Utility.StringToBrush(_selectedStyleTheme.CommandPromptBackgroundColor); }
            set { _selectedStyleTheme.CommandPromptBackgroundColor = value.ToString(); }
        }

        /// <summary>
        ///     Gets or sets the color of the command prompt border.
        /// </summary>
        /// <value>
        ///     The color of the command prompt border.
        /// </value>
        public Brush CommandPromptBorderColor
        {
            get { return Utility.StringToBrush(_selectedStyleTheme.CommandPromptBorderColor); }
            set { _selectedStyleTheme.CommandPromptBorderColor = value.ToString(); }
        }

        /// <summary>
        ///     Gets or sets the command prompt border thickness.
        /// </summary>
        /// <value>
        ///     The command prompt border thickness.
        /// </value>
        public Thickness CommandPromptBorderThickness
        {
            get { return Utility.StringToThickness(_selectedStyleTheme.CommandPromptBorderThickness); }
            set { _selectedStyleTheme.CommandPromptBorderThickness = value.ToString(); }
        }

        /// <summary>
        ///     Gets or sets the command prompt padding.
        /// </summary>
        /// <value>
        ///     The command prompt padding.
        /// </value>
        public Thickness CommandPromptPadding
        {
            get { return Utility.StringToThickness(_selectedStyleTheme.CommandPromptPadding); }
            set { _selectedStyleTheme.CommandPromptPadding = value.ToString(); }
        }

        /// <summary>
        ///     Gets or sets the color of the command text.
        /// </summary>
        /// <value>
        ///     The color of the command text.
        /// </value>
        public Brush CommandPromptTextColor
        {
            get { return Utility.StringToBrush(_selectedStyleTheme.CommandPromptTextColor); }
            set { _selectedStyleTheme.CommandPromptTextColor = value.ToString(); }
        }

        /// <summary>
        ///     Gets or sets the command prompt font.
        /// </summary>
        /// <value>
        ///     The command text font.
        /// </value>
        public FontFamily CommandPromptFont
        {
            get { return new FontFamily(_selectedStyleTheme.CommandPromptFont); }
            set { _selectedStyleTheme.CommandPromptFont = value.ToString(); }
        }

        /// <summary>
        ///     Gets or sets the size of the command text font.
        /// </summary>
        /// <value>
        ///     The size of the command text font.
        /// </value>
        public double CommandPromptFontSize
        {
            get { return _selectedStyleTheme.CommandPromptFontSize; }
            set { _selectedStyleTheme.CommandPromptFontSize = value; }
        }

        /// <summary>
        ///     Gets or sets the message area font.
        /// </summary>
        /// <value>
        ///     The message area font.
        /// </value>
        public FontFamily MessageAreaFont
        {
            get { return new FontFamily(_selectedStyleTheme.MessageAreaFont); }
            set { _selectedStyleTheme.MessageAreaFont = value.ToString(); }
        }

        /// <summary>
        ///     Gets or sets the size of the message area font.
        /// </summary>
        /// <value>
        ///     The size of the message area font.
        /// </value>
        public double MessageAreaFontSize
        {
            get { return _selectedStyleTheme.MessageAreaFontSize; }
            set { _selectedStyleTheme.MessageAreaFontSize = value; }
        }

        /// <summary>
        ///     Gets or sets the color of the message background.
        /// </summary>
        /// <value>
        ///     The color of the message background.
        /// </value>
        public Brush MessageAreaBackgroundColor
        {
            get { return Utility.StringToBrush(_selectedStyleTheme.MessageAreaBackgroundColor); }
            set { _selectedStyleTheme.MessageAreaBackgroundColor = value.ToString(); }
        }

        /// <summary>
        ///     Gets or sets the color of the message area border.
        /// </summary>
        /// <value>
        ///     The color of the message area border.
        /// </value>
        public Brush MessageAreaBorderColor
        {
            get { return Utility.StringToBrush(_selectedStyleTheme.MessageAreaBorderColor); }
            set { _selectedStyleTheme.MessageAreaBorderColor = value.ToString(); }
        }

        /// <summary>
        ///     Gets or sets the message area border thickness.
        /// </summary>
        /// <value>
        ///     The message area border thickness.
        /// </value>
        public Thickness MessageAreaBorderThickness
        {
            get { return Utility.StringToThickness(_selectedStyleTheme.MessageAreaBorderThickness); }
            set { _selectedStyleTheme.MessageAreaBorderThickness = value.ToString(); }
        }

        /// <summary>
        ///     Gets or sets the message area padding.
        /// </summary>
        /// <value>
        ///     The message area padding.
        /// </value>
        public Thickness MessageAreaPadding
        {
            get { return Utility.StringToThickness(_selectedStyleTheme.MessageAreaPadding); }
            set { _selectedStyleTheme.MessageAreaPadding = value.ToString(); }
        }

        /// <summary>
        ///     Gets or sets the color of the message prompt.
        /// </summary>
        /// <value>
        ///     The color of the message prompt.
        /// </value>
        public Brush MessagePromptColor
        {
            get { return Utility.StringToBrush(_selectedStyleTheme.MessagePromptColor); }
            set { _selectedStyleTheme.MessagePromptColor = value.ToString(); }
        }

        /// <summary>
        ///     Gets or sets the color of the message text.
        /// </summary>
        /// <value>
        ///     The color of the message text.
        /// </value>
        public Brush MessageTextColor
        {
            get { return Utility.StringToBrush(_selectedStyleTheme.MessageTextColor); }
            set { _selectedStyleTheme.MessageTextColor = value.ToString(); }
        }

        /// <summary>
        ///     Gets or sets the message text paragraph margin.
        /// </summary>
        /// <value>
        ///     The message text paragraph margin.
        /// </value>
        public Thickness MessageTextParagraphMargin
        {
            get { return Utility.StringToThickness(_selectedStyleTheme.MessageTextParagraphMargin); }
            set { _selectedStyleTheme.MessageTextParagraphMargin = value.ToString(); }
        }

        /// <summary>
        ///     Gets or sets the color of the welcome message.
        /// </summary>
        /// <value>
        ///     The color of the welcome message.
        /// </value>
        public Brush WelcomeMessageColor
        {
            get { return Utility.StringToBrush(_selectedStyleTheme.WelcomeMessageColor); }
            set { _selectedStyleTheme.WelcomeMessageColor = value.ToString(); }
        }

        /// <summary>
        ///     The number of style themes in the list.
        /// </summary>
        /// <returns>Total count of style themes as an integer</returns>
        public int Count()
        {
            return _styleThemes.Count;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Adds a color scheme to the list.
        /// </summary>
        /// <param name="styleTheme">The color scheme.</param>
        public void AddStyleTheme(StyleTheme styleTheme)
        {
            var index = Count();
            styleTheme.StyleThemeIndexId = index;
            _styleThemes.Add(styleTheme);
        }

        /// <summary>
        ///     Adds the current style theme and any changes as a new theme. Not saved to disk.
        /// </summary>
        public void CurrentThemeToNew()
        {
            AddStyleTheme(_selectedStyleTheme);
        }

        /// <summary>
        ///     Gets the Current style theme description.
        /// </summary>
        /// <returns>Description of the current style theme as a string</returns>
        public string CurrentStyleTheme()
        {
            return _selectedStyleTheme.StyleThemeIndexId + " - " + _selectedStyleTheme.StyleThemeName;
        }

        /// <summary>
        ///     Deletes a style theme from the list.
        /// </summary>
        /// <param name="styleThemeIndex">Index of the style theme.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when the value is outside of the list index range.</exception>
        public void DeleteStyleTheme(int styleThemeIndex)
        {
            if (styleThemeIndex < Count() && styleThemeIndex >= 0)
            {
                _styleThemes.RemoveAt(styleThemeIndex);
            }
            else
            {
                throw new IndexOutOfRangeException("Index value [" + styleThemeIndex +
                                                   "] out of range of style theme list.");
            }
        }

        /// <summary>
        ///     Gets the style theme by list index number.
        /// </summary>
        /// <param name="styleThemeIndex">The list index.</param>
        /// <returns>StyleTheme</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when the value is outside of the list index range.</exception>
        public StyleTheme GetStyleTheme(int styleThemeIndex)
        {
            if (styleThemeIndex >= 0 && styleThemeIndex < Count())
            {
                return _styleThemes.SingleOrDefault(x => x.StyleThemeIndexId == styleThemeIndex);
            }
            throw new IndexOutOfRangeException("Index value [" + styleThemeIndex + "] out of range of style theme list.");
        }

        /// <summary>
        ///     Gets the style theme list.
        /// </summary>
        /// <returns>Style theme list as a string</returns>
        public string GetStyleThemeList()
        {
            var schemeNames = "# - Name\r----------\r";
            var x = 0;

            foreach (var scheme in _styleThemes)
            {
                schemeNames += scheme.StyleThemeIndexId + " - " + scheme.StyleThemeName + "\r";
                x++;
            }

            return schemeNames;
        }

        /// <summary>
        ///     Resets the console theme. Any changes are discarded.
        /// </summary>
        public void ResetStyleTheme()
        {
            SetStyleTheme(_selectedStyleTheme.StyleThemeIndexId);
        }

        /// <summary>
        ///     Sets the active style theme.
        /// </summary>
        /// <param name="styleThemeIndex">The index of the style theme.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when the value is outside of the list index range.</exception>
        public void SetStyleTheme(int styleThemeIndex)
        {
            if (styleThemeIndex >= 0 && styleThemeIndex < Count())
            {
                _selectedStyleTheme = GetStyleTheme(styleThemeIndex);
            }
            else
            {
                throw new IndexOutOfRangeException("Index value [" + styleThemeIndex +
                                                   "] out of range of style theme list.");
            }
        }

        /// <summary>
        ///     Updates the original theme with the current settings. Does not save to disk.
        /// </summary>
        public void UpdateStyleTheme()
        {
            var st = _styleThemes.SingleOrDefault(x => x.StyleThemeIndexId == _selectedStyleTheme.StyleThemeIndexId);

            st.StyleThemeName = _selectedStyleTheme.StyleThemeName;
            st.CommandPromptBackgroundColor = _selectedStyleTheme.CommandPromptBackgroundColor;
            st.CommandPromptBorderColor = _selectedStyleTheme.CommandPromptBorderColor;
            st.CommandPromptBorderThickness = _selectedStyleTheme.CommandPromptBorderThickness;
            st.CommandPromptFont = _selectedStyleTheme.CommandPromptFont;
            st.CommandPromptFontSize = _selectedStyleTheme.CommandPromptFontSize;
            st.CommandPromptPadding = _selectedStyleTheme.CommandPromptPadding;
            st.CommandPromptTextColor = _selectedStyleTheme.CommandPromptTextColor;
            st.MessageAreaBackgroundColor = _selectedStyleTheme.MessageAreaBackgroundColor;
            st.MessageAreaBorderColor = _selectedStyleTheme.MessageAreaBorderColor;
            st.MessageAreaBorderThickness = _selectedStyleTheme.MessageAreaBorderThickness;
            st.MessageAreaFont = _selectedStyleTheme.MessageAreaFont;
            st.MessageAreaFontSize = _selectedStyleTheme.MessageAreaFontSize;
            st.MessageAreaPadding = _selectedStyleTheme.MessageAreaPadding;
            st.MessagePromptColor = _selectedStyleTheme.MessagePromptColor;
            st.MessageTextColor = _selectedStyleTheme.MessageTextColor;
            st.MessageTextParagraphMargin = _selectedStyleTheme.MessageTextParagraphMargin;
        }

        #endregion

        #region Save/Load Themes

        /// <summary>
        ///     Loads the style themes.
        /// </summary>
        public void LoadStyleThemes()
        {
            try
            {
                _styleThemes =
                    Utility.XmlFileToObject<List<StyleTheme>>(Environment.CurrentDirectory + "\\" + ThemesFileName);
            }
            catch (FileNotFoundException)
            {
                if (_styleThemes == null)
                {
                    AddDefaultThemes();
                }
            }
        }

        /// <summary>
        ///     Saves the style themes.
        /// </summary>
        public void SaveStyleThemes()
        {
            Utility.ObjectToXMlFile(Environment.CurrentDirectory + "\\" + ThemesFileName, _styleThemes);
        }

        #endregion
    }
}