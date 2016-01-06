# WcfCommandPrompt
![screen shot](https://github.com/LGM-Consulting/WcfCommandPrompt/blob/master/logo.png)

WPF Command Prompt is a command line console that comes with a number of features including:

* Save/Load different console settings (or use internal defaults)
* Save/Load different command histories
* Save/Load style themes (or use internal defaults)
* Message area background color, fonts/font sizes/font colors, border size/color and padding size manually or using themes
* Command prompt background color, font/font size/font colors, border size/color and padding size manually or using themes
* Internal or external command parsing
* Command history function

The basic layout has a message area (RichTextBox) and a command prompt area (TextBox) in a user control. I kept the command prompt at the bottom of the window as there is a resizing issue I have yet to work out. But, after using it for a while, I find I prefer the command prompt at the bottom of the window anyway. The prompt text (e.g. "C:\MyStuff\>") in the command prompt area is protected for being changed or deleted by the end user.
