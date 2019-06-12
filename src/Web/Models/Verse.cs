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
        public IList<Artifact> Words { get; set; }

        public override string Text
        {
            get
            {
                var text = String.Empty;
                for (int i = 0; i < Words.Count; i++)
                {
                    var word = Words[i];
                    var wasPreviousSegment = (i - 1 >= 0) && (Words[i - 1] is Segment);
                    if (wasPreviousSegment || word is Segment)
                    {
                        text += word.Text;
                    }
                    else
                    {
                        text += ' ' + word.Text;
                    }
                }
                return text.Replace("/", "").Trim();
            }
        }
    }
}
