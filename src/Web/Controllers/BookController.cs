using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scrolltek.Web.Models;

namespace Web.Controllers
{
    /// <summary>
    /// Fetches information from and about the Miqra.
    /// </summary>
    [ApiController]
    public abstract class BookController : ControllerBase
    {

        private XmlDocument _bookXml;

        /// <summary>
        /// Gets the absolute path to the data directory used to store the
        /// all data files.
        /// </summary>
        public string DataPath
        {
            get
            {
                return AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            }
        }

        /// <summary>
        /// Gets the absolute path to the directory which stores the data found
        /// in each book of the Miqra.
        /// </summary>
        public string BooksPath
        {
            get
            {
                return Path.Combine(DataPath, "Books");
            }
        }

        /// <summary>
        /// Gets the absolute path to the specific XML document queried by the
        /// controller.
        /// </summary>
        public string BookPath
        {
            get
            {
                return Path.Combine(BooksPath, $"{BookName}.xml");
            }
        }

        /// <summary>
        /// Gets the absolute path to the directory which stores the lexical
        /// data associated with each book of the Miqra.
        /// </summary>
        public string LexiconPath
        {
            get
            {
                return Path.Combine(DataPath, "Lexicon");
            }
        }

        /// <summary>
        /// Gets the name of the book of in the Miqra which this controller
        /// investigates.
        /// </summary>
        public abstract string BookName { get; }

        /// <summary>
        /// Gets the Xml of the primary book which this controller queries.
        /// </summary>
        public XmlDocument BookXml
        {
            get
            {
                if (_bookXml == null)
                {
                    _bookXml = new XmlDocument();
                    string xml = System.IO.File.ReadAllText(BookPath);
                    xml = xml.Replace(" xmlns=\"", " foo=\"");
                    _bookXml.LoadXml(xml);
                }
                return _bookXml;
            }
        }

        [HttpGet("{chapterNumber}/{verseNumber}")]
        public IActionResult GetVerse(int chapterNumber, int verseNumber)
        {
            IActionResult result = NotFound();
            var verse = GetVerseFromXml(chapterNumber, verseNumber);
            if (verse != null)
            {
                result = Ok(verse);
            }
            return result;
        }

        private Verse GetVerseFromXml(int chapterNumber, int verseNumber)
        {
            Verse verse = null;
            var xpath = $"//verse[@osisID='{BookName}.{chapterNumber}.{verseNumber}']";
            var node = BookXml.SelectSingleNode(xpath);
            if (node != null)
            {
                verse = new Verse();
                verse.OrderNumber = verseNumber;
                verse.Words = GetWordsFromVerseNode(node);
                verse.Segments = GetSegmentsFromVerseNode(node);
            }
            return verse;
        }

        private List<Word> GetWordsFromVerseNode(XmlNode node)
        {
            var words = new List<Word>();
            var nodes = node.SelectNodes("./w");
            if (nodes != null)
            {
                var order = 1;
                foreach (XmlNode element in nodes)
                {
                    var word = new Word
                    {
                        Id = element.Attributes["id"].Value,
                        OrderNumber = order,
                        Lemma = element.Attributes["lemma"].Value,
                        Morphology = element.Attributes["morph"].Value,
                    };
                    word.SetText(element.InnerText);
                    words.Add(word);
                    order += 1;
                }
            }
            return words;
        }

        private List<Segment> GetSegmentsFromVerseNode(XmlNode node)
        {
            var segments = new List<Segment>();
            var nodes = node.SelectNodes("./seg");
            if (nodes != null)
            {
                var order = 1;
                foreach (XmlNode element in nodes)
                {
                    var segment = new Segment
                    {
                        OrderNumber = order,
                        Type = element.Attributes["type"].Value
                    };
                    segment.SetText(element.InnerText);
                    segments.Add(segment);
                    order += 1;
                }
            }
            return segments;
        }

    }
}
