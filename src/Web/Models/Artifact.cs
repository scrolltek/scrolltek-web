using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scrolltek.Web.Models
{
    /// <summary>
    /// Models an arbitrary item in the Hebrew Bible.
    /// </summary>
    public abstract class Artifact
    {
        /// <summary>
        /// Gets or sets the ordinal for the artifact.
        /// </summary>
        public int OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the actual text of the artifact.
        /// </summary>
        public abstract string Text { get; }
    }
}
