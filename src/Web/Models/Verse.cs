using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scrolltek.Web.Models
{

    /// <summary>
    /// Models a verse in the Hebrew Bible.
    /// </summary>
    public class Verse : Artifact
    {
        /// <summary>
        /// Gets or sets a list all words (in order of reading) found in the
        /// verse.
        /// </summary>
        public IList<Word> Words { get; set; }

        /// <summary>
        /// Gets or sets all ending segments (in order of reading) found in the
        /// verse.
        /// </summary>
        public IList<Segment> Segments { get; set; }

        public override string Text
        {
            get
            {
                var text = String.Empty;
                foreach (var word in Words)
                {
                    text += ' ' + word.Text;
                }
                foreach (var segment in Segments)
                {
                    text += segment.Text;
                }
                return text.Replace("/", "").Trim();
            }
        }
    }
}
