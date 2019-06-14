using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scrolltek.Web.Models
{
    /// <summary>
    /// Models a book in "The Koren Jerusalem Hebrew Bible" (license CC-BY-NC).
    /// </summary>
    public class KorenBook
    {
        public string Status { get; set; }

        public string VersionTitle { get; set; }

        public IList<string> SectionNames { get; set; }

        public string License { get; set; }

        public string Language { get; set; }

        public string Title { get; set; }

        public IList<IList<string>> Text { get; set; }
    }
}
