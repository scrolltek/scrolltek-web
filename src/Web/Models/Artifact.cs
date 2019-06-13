using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public string HebrewOrdinal
        {
            get
            {
                return EllisWeb.Gematria.Calculator.ConvertToGematriaNumericString(OrderNumber, false);
            }
        }

        /// <summary>
        /// Gets or sets the actual text of the artifact.
        /// </summary>
        public abstract string Text { get; }

        public static string FormatHebrew(int num)
        {
            if (num <= 0)
                throw new ArgumentOutOfRangeException();
            StringBuilder ret = new StringBuilder(new string('ת', num / 400));
            num %= 400;
            if (num >= 100)
            {
                ret.Append("קרש"[num / 100 - 1]);
                num %= 100;
            }
            if (num >= 10)
            {
                ret.Append("כלמנסעפצי"[num / 10 - 1]);
                num %= 10;
            }
            if (num > 0)
                ret.Append("אבגדהוזחט"[num - 1]);
            return ret.ToString();
        }
    }
}
