using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Localization : MonoBehaviour
{
    /// <summary>
    /// The localization file.
    /// </summary>
    public TextAsset localizationFile;

    private List<Line> lines = new List<Line>();

    static private Localization instance;
    static public Localization Instance { get { return instance; } }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;


        // Load the localization file
        RetrieveLocalizationFromFile();
    }


    /// <summary>
    /// Gets the strings from the localization file.
    /// </summary>
    void RetrieveLocalizationFromFile()
    {
        // Do not load if reference is not set
        if (localizationFile == null)
        {
            Debug.LogError("Localization:RetrieveLocalizationFromFile(): TextAsset localizationFile is not set.");
            return;
        }


        // Get the rows from the TextAsset. If you don't use CRLF line endings, replace "\r\n" by "\n"
        string[] rows = localizationFile.text.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);


        // Remove the BOM character if any
        if ((int)rows[0][0] == 65279)
        {
            rows[0] = rows[0].TrimStart((char)65279);
        }


        // Insert texts in the list
        foreach (string row in rows)
        {
            string[] parts = row.Split('\t');


            // Ignore this row if empty
            if (parts[0].Length == 0)
                continue;


            // Create the line (Line)
            lines.Add(new Line(parts[0], parts[1], parts[2]));
        }
    }


    /// <summary>
    /// Gets the localized string identified by the string parameter.
    /// </summary>
    /// <param name="_id">Text to retrieve id.</param>
    /// <returns>The translated string.</returns>
    public string GetLine(string _id)
    {
        Line line = lines.Find(item => item.Id == _id);

        if (line == null)
            return "Localization:GetLine() : line not found with id = " + _id.ToString();

        return line.GetLineText();
    }


    /// <summary>
    /// Gets the Line object identified by the string parameter.
    /// </summary>
    /// <param name="_id">Text to retrieve id.</param>
    /// <returns>The Line object.</returns>
    public Line GetLineObject(string _id)
    {
        Line line = lines.Find(item => item.Id == _id);


        if (line == null)
        {
            Debug.LogError("Localization:GetLineObject() : line not found with id = " + _id.ToString());
            return null;
        }

        return line;
    }


    /// <summary>
    /// A Line object contains an id and the translated texts.
    /// </summary>
    public class Line
    {
        /// <summary>
        /// Id de la ligne.
        /// </summary>
        public string Id
        {
            get
            {
                return id;
            }
        }
        private string id;


        /// <summary>
        /// Available languages. Add here for more languages.
        /// </summary>
        public enum Language
        {
            FR,
            EN
        }


        /// <summary>
        /// Selected language.
        /// </summary>
        static Language currentLanguage = Language.FR;


        /// <summary>
        /// Gets the current language.
        /// </summary>
        static public Language GetCurrentLanguage()
        {
            return currentLanguage;
        }


        /// <summary>
        /// Sets the current language.
        /// </summary>
        /// <param name="_language">Language as defined in Localization.Line.Language enum.</param>
        static public void SetCurrentLanguage(Language _language)
        {
            currentLanguage = _language;
        }

        /// <summary>
        /// Text.
        /// </summary>
        private string french, english;


        /// <summary>
        /// Creates a Line object. Add more parameters if you want more languages.
        /// </summary>
        /// <param name="_id">The identifier.</param>
        /// <param name="_french">French text.</param>
        /// <param name="_english">English text.</param>
        public Line(string _id, string _french, string _english)
        {
            id = _id;
            french = _french;
            english = _english;
        }


        /// <summary>
        /// Gets the localized text.
        /// </summary>
        /// <returns>The localized text.</returns>
        public string GetLineText()
        {
            switch (currentLanguage)
            {
                case Language.FR:
                    return french;

                case Language.EN:
                    return english;

                default:
                    return "Localization:Line:GetLineText() : Current language undefined.";
            }
        }


        /// <summary>
        /// Gets the localized audio file name. Use your own file naming convention.
        /// </summary>
        /// <returns>The localized audio file name.</returns>
        public string GetLineAudioName()
        {
            switch (currentLanguage)
            {
                case Language.FR:
                    return id + "_FR";

                case Language.EN:
                    return id + "_EN";

                default:
                    return "Localization:Line:GetLineAudioName() : Current language undefined.";
            }
        }
    }
}
