# Localization
Simple class for Unity to handle basic localization. Imports from a text file. Easily extendable.

# Usage
* Add Localization.cs file in your project.
* Please check the [localized file example](./localized file example.txt) to understand the basic file structure:
 * Columns are separated by a tab character.
 * The first column is the id as a string.
 * The next columns are the translated text.
  * In the original Localization.cs, the second column is the french text, the third is the english text.
 * A line ends by CRLF characters. You can change to LF if you prefer but don't forget to update Localization.cs by switching `"\r\n"` by `"\n"` in the file parser.

# Tips
* On Windows, use Notepad++ to check line endings (show special characters).
