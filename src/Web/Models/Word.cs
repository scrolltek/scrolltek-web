using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scrolltek.Web.Models
{
    /// <summary>
    /// Models a word found in the Hebrew Bible.
    /// </summary>
    public class Word
    {

        /// <summary>
        /// Gets or sets The unique ID for the word.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets A reference to the root or definition of the word.
        /// </summary>
        public string Lemma { get; set; }

        /// <summary>
        /// Gets or sets a code which represents the form of the word.
        /// </summary>
        public string Morphology { get; set; }

        /// <summary>
        /// Gets or sets the actual text of the word.
        /// </summary>
        public string Text { get; set; }
    }
}
