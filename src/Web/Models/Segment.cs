using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scrolltek.Web.Models
{

    /// <summary>
    /// Models a verse ending segment in the Hebrew Bible.
    /// </summary>
    public class Segment : Artifact
    {

        private string _text;

        /// <summary>
        /// Gets the type of ending segment associated with the text.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets the actual text of the segment.
        /// </summary>
        public override string Text { get; }

        /// <summary>
        /// Gets the raw text of the segment.
        /// </summary>
        /// <param name="text">The nullable text to set.</param>
        public void SetText(string text)
        {
            _text = text;
        }

    }
}
