using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scrolltek.Web.Models
{
    /// <summary>
    /// Models a chapter of in the Hebrew Bible
    /// </summary>
    public class Chapter
    {
        /// <summary>
        /// Gets or sets a list (in order of reading) of verses found in the
        /// chapter.
        /// </summary>
        public IList<Verse> Verses { get; set; }
    }
}
