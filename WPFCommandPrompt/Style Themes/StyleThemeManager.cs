using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.IO;
using System.Windows;

namespace WPFCommandPrompt
{
    /// <summary>
    /// Manages the console window style themes.
    /// </summary>
    internal class StyleThemeManager
    {
        private List<StyleTheme> styleThemes;
        private StyleTheme selectedStyleTheme;
        private const string themesFileName = "StyleThemes.xml";

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleThemeManager"/> class.
        /// </summary>
        public StyleThemeManager()
        {
            AddDefaultThemes();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleThemeManager"/> class.
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

            selectedStyleTheme = GetStyleTheme(styleThemeIndex);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the style theme index ID.
        /// </summary>
        public int StyleThemeIndexID
        {
            get { return selectedStyleTheme.StyleThemeIndexID; }
        }

        /// <summary>
        /// Gets the name of the style theme.
        /// </summary>
        /// <value>
        /// The name of the style theme.
        /// </value>
        public string StyleThemeName
        {
            get { return selectedStyleTheme.StyleThemeName; }
            set { selectedStyleTheme.StyleThemeName = value; }
        }

        /// <summary>
        /// Gets or sets the color of the command background.
        /// </summary>
        /// <value>
        /// The color of the command background.
        /// </value>
        public Brush CommandPromptBackgroundColor
        {
            get { return Utility.StringToBrush(selectedStyleTheme.CommandPromptBackgroundColor); }
            set { selectedStyleTheme.CommandPromptBackgroundColor = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the color of the command prompt border.
        /// </summary>
        /// <value>
        /// The color of the command prompt border.
        /// </value>
        public Brush CommandPromptBorderColor
        {
            get { return Utility.StringToBrush(selectedStyleTheme.CommandPromptBorderColor); }
            set { selectedStyleTheme.CommandPromptBorderColor = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the command prompt border thickness.
        /// </summary>
        /// <value>
        /// The command prompt border thickness.
        /// </value>
        public Thickness CommandPromptBorderThickness
        {
            get { return Utility.StringToThickness(selectedStyleTheme.CommandPromptBorderThickness); }
            set { selectedStyleTheme.CommandPromptBorderThickness = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the command prompt padding.
        /// </summary>
        /// <value>
        /// The command prompt padding.
        /// </value>
        public Thickness CommandPromptPadding
        {
            get { return Utility.StringToThickness(selectedStyleTheme.CommandPromptPadding); }
            set { selectedStyleTheme.CommandPromptPadding = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the color of the command text.
        /// </summary>
        /// <value>
        /// The color of the command text.
        /// </value>
        public Brush CommandPromptTextColor
        {
            get { return Utility.StringToBrush(selectedStyleTheme.CommandPromptTextColor); }
            set { selectedStyleTheme.CommandPromptTextColor = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the command prompt font.
        /// </summary>
        /// <value>
        /// The command text font.
        /// </value>
        public FontFamily CommandPromptFont
        {
            get { return new FontFamily(selectedStyleTheme.CommandPromptFont); }
            set { selectedStyleTheme.CommandPromptFont = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the size of the command text font.
        /// </summary>
        /// <value>
        /// The size of the command text font.
        /// </value>
        public double CommandPromptFontSize
        {
            get { return selectedStyleTheme.CommandPromptFontSize; }
            set { selectedStyleTheme.CommandPromptFontSize = value; }
        }

        /// <summary>
        /// Gets or sets the message area font.
        /// </summary>
        /// <value>
        /// The message area font.
        /// </value>
        public FontFamily MessageAreaFont
        {
            get { return new FontFamily(selectedStyleTheme.MessageAreaFont); }
            set { selectedStyleTheme.MessageAreaFont = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the size of the message area font.
        /// </summary>
        /// <value>
        /// The size of the message area font.
        /// </value>
        public double MessageAreaFontSize
        {
            get { return selectedStyleTheme.MessageAreaFontSize; }
            set { selectedStyleTheme.MessageAreaFontSize = value; }
        }

        /// <summary>
        /// Gets or sets the color of the message background.
        /// </summary>
        /// <value>
        /// The color of the message background.
        /// </value>
        public Brush MessageAreaBackgroundColor
        {
            get { return Utility.StringToBrush(selectedStyleTheme.MessageAreaBackgroundColor); }
            set { selectedStyleTheme.MessageAreaBackgroundColor = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the color of the message area border.
        /// </summary>
        /// <value>
        /// The color of the message area border.
        /// </value>
        public Brush MessageAreaBorderColor
        {
            get { return Utility.StringToBrush(selectedStyleTheme.MessageAreaBorderColor); }
            set { selectedStyleTheme.MessageAreaBorderColor = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the message area border thickness.
        /// </summary>
        /// <value>
        /// The message area border thickness.
        /// </value>
        public Thickness MessageAreaBorderThickness
        {
            get { return Utility.StringToThickness(selectedStyleTheme.MessageAreaBorderThickness); }
            set { selectedStyleTheme.MessageAreaBorderThickness = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the message area padding.
        /// </summary>
        /// <value>
        /// The message area padding.
        /// </value>
        public Thickness MessageAreaPadding
        {
            get { return Utility.StringToThickness(selectedStyleTheme.MessageAreaPadding); }
            set { selectedStyleTheme.MessageAreaPadding = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the color of the message prompt.
        /// </summary>
        /// <value>
        /// The color of the message prompt.
        /// </value>
        public Brush MessagePromptColor
        {
            get { return Utility.StringToBrush(selectedStyleTheme.MessagePromptColor); }
            set { selectedStyleTheme.MessagePromptColor = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the color of the message text.
        /// </summary>
        /// <value>
        /// The color of the message text.
        /// </value>
        public Brush MessageTextColor
        {
            get { return Utility.StringToBrush(selectedStyleTheme.MessageTextColor); }
            set { selectedStyleTheme.MessageTextColor = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the message text paragraph margin.
        /// </summary>
        /// <value>
        /// The message text paragraph margin.
        /// </value>
        public Thickness MessageTextParagraphMargin
        {
            get { return Utility.StringToThickness(selectedStyleTheme.MessageTextParagraphMargin); }
            set { selectedStyleTheme.MessageTextParagraphMargin = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the color of the welcome message.
        /// </summary>
        /// <value>
        /// The color of the welcome message.
        /// </value>
        public Brush WelcomeMessageColor
        {
            get { return Utility.StringToBrush(selectedStyleTheme.WelcomeMessageColor); }
            set { selectedStyleTheme.WelcomeMessageColor = value.ToString(); }
        }

        /// <summary>
        /// The number of style themes in the list.
        /// </summary>
        /// <returns>Total count of style themes as an integer</returns>
        public int Count()
        {
            return styleThemes.Count;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a color scheme to the list.
        /// </summary>
        /// <param name="styleTheme">The color scheme.</param>
        public void AddStyleTheme(StyleTheme styleTheme)
        {
            int index = Count();
            styleTheme.StyleThemeIndexID = index;
            styleThemes.Add(styleTheme);
        }

        /// <summary>
        /// Adds the current style theme and any changes as a new theme. Not saved to disk.
        /// </summary>
        public void CurrentThemeToNew()
        {
            AddStyleTheme(selectedStyleTheme);
        }

        /// <summary>
        /// Gets the Current style theme description.
        /// </summary>
        /// <returns>Description of the current style theme as a string</returns>
        public string CurrentStyleTheme()
        {
            return selectedStyleTheme.StyleThemeIndexID + " - " + selectedStyleTheme.StyleThemeName;
        }

        /// <summary>
        /// Deletes a style theme from the list.
        /// </summary>
        /// <param name="styleThemeIndex">Index of the style theme.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when the value is outside of the list index range.</exception>
        public void DeleteStyleTheme(int styleThemeIndex)
        {
            if (styleThemeIndex < Count() && styleThemeIndex >= 0)
            {
                styleThemes.RemoveAt(styleThemeIndex);
            }
            else
            {
                throw new IndexOutOfRangeException("Index value [" + styleThemeIndex + "] out of range of style theme list.");
            }
        }

        /// <summary>
        /// Gets the style theme by list index number.
        /// </summary>
        /// <param name="styleThemeIndex">The list index.</param>
        /// <returns>StyleTheme</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when the value is outside of the list index range.</exception>
        public StyleTheme GetStyleTheme(int styleThemeIndex)
        {
            if (styleThemeIndex >= 0 && styleThemeIndex < Count())
            {
                return styleThemes.SingleOrDefault(x => x.StyleThemeIndexID == styleThemeIndex);
            }
            else
            {
                throw new IndexOutOfRangeException("Index value [" + styleThemeIndex + "] out of range of style theme list.");
            }
        }

        /// <summary>
        /// Gets the style theme list.
        /// </summary>
        /// <returns>Style theme list as a string</returns>
        public string GetStyleThemeList()
        {
            string schemeNames = "# - Name\r----------\r";
            int x = 0;

            foreach (var scheme in styleThemes)
            {
                schemeNames += scheme.StyleThemeIndexID + " - " + scheme.StyleThemeName + "\r";
                x++;
            }

            return schemeNames;
        }

        /// <summary>
        /// Resets the console theme. Any changes are discarded.
        /// </summary>
        public void ResetStyleTheme()
        {
            SetStyleTheme(selectedStyleTheme.StyleThemeIndexID);
        }

        /// <summary>
        /// Sets the active style theme.
        /// </summary>
        /// <param name="styleThemeIndex">The index of the style theme.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when the value is outside of the list index range.</exception>
        public void SetStyleTheme(int styleThemeIndex)
        {
            if (styleThemeIndex >= 0 && styleThemeIndex < Count())
            {
                selectedStyleTheme = GetStyleTheme(styleThemeIndex);
            }
            else
            {
                throw new IndexOutOfRangeException("Index value [" + styleThemeIndex + "] out of range of style theme list.");
            }
        }

        /// <summary>
        /// Updates the original theme with the current settings. Does not save to disk.
        /// </summary>
        public void UpdateStyleTheme()
        {
            StyleTheme st = styleThemes.SingleOrDefault(x => x.StyleThemeIndexID == selectedStyleTheme.StyleThemeIndexID);

            st.StyleThemeName = selectedStyleTheme.StyleThemeName;
            st.CommandPromptBackgroundColor = selectedStyleTheme.CommandPromptBackgroundColor;
            st.CommandPromptBorderColor = selectedStyleTheme.CommandPromptBorderColor;
            st.CommandPromptBorderThickness = selectedStyleTheme.CommandPromptBorderThickness;
            st.CommandPromptFont = selectedStyleTheme.CommandPromptFont;
            st.CommandPromptFontSize = selectedStyleTheme.CommandPromptFontSize;
            st.CommandPromptPadding = selectedStyleTheme.CommandPromptPadding;
            st.CommandPromptTextColor = selectedStyleTheme.CommandPromptTextColor;
            st.MessageAreaBackgroundColor = selectedStyleTheme.MessageAreaBackgroundColor;
            st.MessageAreaBorderColor = selectedStyleTheme.MessageAreaBorderColor;
            st.MessageAreaBorderThickness = selectedStyleTheme.MessageAreaBorderThickness;
            st.MessageAreaFont = selectedStyleTheme.MessageAreaFont;
            st.MessageAreaFontSize = selectedStyleTheme.MessageAreaFontSize;
            st.MessageAreaPadding = selectedStyleTheme.MessageAreaPadding;
            st.MessagePromptColor = selectedStyleTheme.MessagePromptColor;
            st.MessageTextColor = selectedStyleTheme.MessageTextColor;
            st.MessageTextParagraphMargin = selectedStyleTheme.MessageTextParagraphMargin;
        }

        #endregion

        #region Save/Load Themes

        /// <summary>
        /// Loads the style themes.
        /// </summary>
        public void LoadStyleThemes()
        {
            try
            {
                styleThemes = Utility.XMLFileToObject<List<StyleTheme>>(System.Environment.CurrentDirectory + "\\" + themesFileName);
            }
            catch (FileNotFoundException)
            {
                if (styleThemes == null) { AddDefaultThemes(); }
            }
        }

        /// <summary>
        /// Saves the style themes.
        /// </summary>
        public void SaveStyleThemes()
        {
            Utility.ObjectToXMlFile<List<StyleTheme>>(System.Environment.CurrentDirectory + "\\" + themesFileName, styleThemes);
        }

        #endregion

        #region Default Themes

        private void AddDefaultThemes()
        {
            styleThemes = new List<StyleTheme>();

            AddStyleTheme(new StyleTheme
            {
                StyleThemeName = "Default",
                CommandPromptBackgroundColor = "Black",
                CommandPromptBorderColor = "Black",
                CommandPromptBorderThickness = "0,0,0,0",
                CommandPromptTextColor = "White",
                CommandPromptFont = System.Drawing.SystemFonts.DefaultFont.ToString(),
                CommandPromptFontSize = 12.00,
                CommandPromptPadding = "0,0,0,0",
                MessageAreaBackgroundColor = "Black",
                MessageAreaBorderColor = "Black",
                MessageAreaBorderThickness = "0,0,0,0",
                MessageAreaFont = System.Drawing.SystemFonts.DefaultFont.ToString(),
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
                CommandPromptFont = System.Drawing.SystemFonts.DefaultFont.ToString(),
                CommandPromptFontSize = 14.00,
                CommandPromptPadding = "0,0,0,0",
                MessageAreaBackgroundColor = "Black",
                MessageAreaBorderColor = "Black",
                MessageAreaBorderThickness = "0,0,0,0",
                MessageAreaFont = System.Drawing.SystemFonts.DefaultFont.ToString(),
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
                CommandPromptFont = System.Drawing.SystemFonts.DefaultFont.ToString(),
                CommandPromptFontSize = 12.00,
                MessageAreaBackgroundColor = "Black",
                MessageAreaBorderColor = "Black",
                MessageAreaBorderThickness = "0,0,0,0",
                MessageAreaFont = System.Drawing.SystemFonts.DefaultFont.ToString(),
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
                CommandPromptFont = System.Drawing.SystemFonts.DefaultFont.ToString(),
                CommandPromptFontSize = 12.00,
                MessageAreaBackgroundColor = "#3F3F3F",
                MessageAreaBorderColor = "Black",
                MessageAreaBorderThickness = "0,0,0,0",
                MessageAreaFont = System.Drawing.SystemFonts.DefaultFont.ToString(),
                MessageAreaFontSize = 12.00,
                MessageAreaPadding = "0,0,0,0",
                MessagePromptColor = "Yellow",
                MessageTextColor = "SkyBlue",
                MessageTextParagraphMargin = "5,0,0,5",
                WelcomeMessageColor = "AntiqueWhite",
            });

            AddStyleTheme(new StyleTheme
            {
                StyleThemeName = "Gray Scheme",
                CommandPromptBackgroundColor = "LightGray",
                CommandPromptBorderColor = "Black",
                CommandPromptBorderThickness = "0,0,0,0",
                CommandPromptTextColor = "Black",
                CommandPromptFont = System.Drawing.SystemFonts.DefaultFont.ToString(),
                CommandPromptFontSize = 14.00,
                MessageAreaBackgroundColor = "LightGray",
                MessageAreaBorderColor = "Black",
                MessageAreaBorderThickness = "0,0,0,0",
                MessageAreaFont = System.Drawing.SystemFonts.DefaultFont.ToString(),
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
                CommandPromptFont = System.Drawing.SystemFonts.DefaultFont.ToString(),
                CommandPromptFontSize = 12.00,
                MessageAreaBackgroundColor = "White",
                MessageAreaBorderColor = "Black",
                MessageAreaBorderThickness = "0,0,0,0",
                MessageAreaFont = System.Drawing.SystemFonts.DefaultFont.ToString(),
                MessageAreaFontSize = 12.00,
                MessageAreaPadding = "0,0,0,0",
                MessagePromptColor = "Black",
                MessageTextColor = "Blue",
                MessageTextParagraphMargin = "5,0,0,5",
                WelcomeMessageColor = "Blue"
            });
        }

        #endregion
    }
}
