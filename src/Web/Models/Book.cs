using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scrolltek.Web.Models
{
    /// <summary>
    /// Models a book in the Hebrew Bible.
    /// </summary>
    public class Book : Artifact
    {
        /// <summary>
        /// Gets or sets a list of chapters (in order of reading) found in the
        /// book.
        /// </summary>
        public IList<Chapter> Chapters { get; set; }

        public override string Text
        {
            get
            {
                var text = String.Empty;
                foreach (var chapter in Chapters)
                {
                    text += chapter.Text + Environment.NewLine + Environment.NewLine;
                }
                return text;
            }
        }
    }
}
