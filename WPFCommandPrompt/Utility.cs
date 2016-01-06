using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Xml;
using System.Windows;

namespace WPFCommandPrompt
{
    /// <summary>
    /// Common utilities
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Version of the current assembly.
        /// </summary>
        /// <param name="includeMinor">if set to <c>true</c> [include minor].</param>
        /// <param name="includeBuild">if set to <c>true</c> [include build].</param>
        /// <param name="includeRevision">if set to <c>true</c> [include revision].</param>
        /// <returns>Assembly version as a string in Major.Minor.Build.Revision format.</returns>
        public static string AssemblyVersion(bool includeMinor, bool includeBuild, bool includeRevision)
        {
            string pVersion = string.Empty;

            try
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

                pVersion = version.Major.ToString();

                if (includeMinor) { pVersion += "." + version.Minor.ToString(); }

                if (includeBuild) 
                {
                    if (!includeMinor) { pVersion += ".x"; }
                    pVersion += "." + version.Build.ToString(); 
                }

                if (includeRevision) 
                {
                    if (!includeBuild) { pVersion += ".x"; }
                    pVersion += "." + version.Revision.ToString(); 
                }
            }
            catch
            {
                pVersion = "0.0.0.0";
            }

            return pVersion;
        }

        /// <summary>
        /// Converts a string color value to a Brush.
        /// </summary>
        /// <param name="colorValue">The string value as a color name or a hex value</param>
        /// <returns>Brush</returns>
        public static Brush StringToBrush(string colorValue)
        {
            Brush brush;

            if (IsHexColor(colorValue))
            {
                try
                {
                    BrushConverter bc = new BrushConverter();
                    brush = (Brush)bc.ConvertFrom(colorValue);
                }
                catch
                {
                    throw new ArgumentException("The provided hex color value [" + colorValue + "] is not valid");
                }
            }
            else
            {
                try
                {
                    System.Windows.Media.BrushConverter bb = new System.Windows.Media.BrushConverter();
                    brush = bb.ConvertFromString(colorValue) as SolidColorBrush;
                }
                catch
                {
                    throw new ArgumentException("The provided color name is not valid");
                }
            }

            return brush;
        }

        /// <summary>
        /// Converts a string (e.g. "1,1,1,1") into a Thinkness object.
        /// </summary>
        /// <param name="thickness">The thickness as a string.</param>
        /// <returns>Thickness</returns>
        public static Thickness StringToThickness(string thickness)
        {
            if (!string.IsNullOrEmpty(thickness))
            {
                string[] thickarray = Regex.Split(thickness, ",");

                double left = 0;
                double top = 0;
                double right = 0;
                double bottom = 0;

                if (!double.TryParse(thickarray[0], out left)) left = 0;

                if (thickarray.Count() >= 2)
                {
                    if (!double.TryParse(thickarray[1], out top)) top = 0;
                }

                if (thickarray.Count() >= 3)
                {
                    if (!double.TryParse(thickarray[1], out right)) right = 0;
                }

                if (thickarray.Count() == 4)
                {
                    if (!double.TryParse(thickarray[1], out bottom)) bottom = 0;
                }

                return new Thickness(left, top, right, bottom);
            }

            return new Thickness(0, 0, 0, 0);
        }

        /// <summary>
        /// Determines if a string is a hex color value.
        /// </summary>
        /// <param name="hexValue">The hex value string.</param>
        /// <returns>
        ///   <c>true</c> if the string is a hex color; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsHexColor(string hexValue)
        {
            string pattern = @"^?\#?([a-fA-F0-9]{6}|[a-fA-F0-9]{3}|[a-fA-F0-9]{8})$";
            return Regex.IsMatch(hexValue, pattern);
        }

        /// <summary>
        /// Reads an xml file and converts it to its correct object type.
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="path">The path including the file name.</param>
        /// <returns>Object of type T</returns>
        public static T XMLFileToObject<T>(string path)
        {
            try
            {
                FileInfo fi = new FileInfo(path);
                if (fi.Exists)
                {
                    XmlSerializer xSerializer = new XmlSerializer(typeof(T));
                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        XmlReader xReader = new XmlTextReader(fs);
                        return (T)xSerializer.Deserialize(xReader);
                    }
                }
                else
                {
                    throw new FileNotFoundException("Not found: " + path);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Takes a serializable object and saves it to an xml file.
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="path">The path including the file name.</param>
        /// <param name="obj">The object to serialize.</param>
        public static void ObjectToXMlFile<T>(string path, T obj)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (TextWriter textWriter = new StreamWriter(path))
                {
                    serializer.Serialize(textWriter, obj);
                    textWriter.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Writes text to a file.
        /// </summary>
        /// <param name="path">The path including the file name.</param>
        /// <param name="text">The text to write.</param>
        public static void WriteTextToFile(string path, string text)
        {
            try
            {
                File.WriteAllText(path, text);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
