using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WinterEngine.Toolset.Helpers
{
    public class TextHelper
    {
        /// <summary>
        /// Validates a string based on a regular expression. 
        /// The regular expression should be the characters you want to keep.
        /// Returns a tuple containing:
        ///     The modified string      (Item 1)
        ///     The new cursor position  (Item 2)
        ///     The new selection length (Item 3)
        /// </summary>
        /// <param name="text">Text to be modified</param>
        /// <param name="regex">The regular expression to use.</param>
        /// <param name="selectionStart">The start of the selection on the text box.</param>
        /// <param name="selectionLength">The length of the selection on the text box.</param>
        /// <param name="allowCapital">True if you want to allow capitals. False if the text should be converted to lower case.</param>
        /// <returns></returns>
        public Tuple<string, int, int> Validate(string text, Regex regex, int selectionStart, int selectionLength, bool allowCapital = true)
        {
            int numberOfCharactersRemoved = 0;
            text = text.ToLower();

            for (int index = 0; index < text.Length; index++)
            {
                // Not a match - remove it from the string
                if (!regex.IsMatch(Convert.ToString(text[index])))
                {
                    text = text.Remove(index, 1);
                    index--;
                    numberOfCharactersRemoved++;
                }
            }

            selectionStart = selectionStart - numberOfCharactersRemoved;

            return new Tuple<string, int, int>(text, selectionStart, selectionLength);
        }
    }
}
