using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Scrolltek.Web.Models;

namespace Web.Controllers
{
    /// <summary>
    /// Fetches information from and about the Miqra.
    /// </summary>
    [Route("api/books/[controller]")]
    [ApiController]
    public abstract class BookController : ControllerBase
    {

        private XmlDocument _bookXml;
        private KorenBook _korenTranslation;

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
        /// Gets the absolute path to the specific data file which contains
        /// "The Koren Jerusalem Bible" English translation of the Hebrew text
        /// (license CC-BY-NC).
        /// </summary>
        public string KorenPath
        {
            get
            {
                return Path.Combine(BooksPath, "Koren", $"{BookName}.json");
            }
        }

        /// <summary>
        /// Gets a representation of the current book as translated by "The
        /// Koren Jerusalem Bible" (license CC-BY-NC).
        /// </summary>
        public KorenBook KorenTranslation
        {
            get
            {
                if (_korenTranslation == null)
                {
                    var text = System.IO.File.ReadAllText(KorenPath);
                    _korenTranslation = JsonConvert.DeserializeObject<KorenBook>(text);
                }
                return _korenTranslation;
            }
        }

        /// <summary>
        /// Gets the name of the book of in the Tanakh which this controller
        /// investigates.
        /// </summary>
        public abstract string BookName { get; }

        /// <summary>
        /// Gets the number of the book in the Tanakh which this controller
        /// investigates.
        /// </summary>
        public abstract int BookNumber { get; }

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

        [HttpGet]
        public Book GetBook()
        {
            var book = new Book();
            book.OrderNumber = BookNumber;
            book.Chapters = new List<Chapter>();
            var xpath = $"//chapter";
            var nodes = BookXml.SelectNodes(xpath);
            if (nodes != null)
            {
                for (int i = 1; i <= nodes.Count; i++)
                {
                    var chapter = GetChapterFromData(i);
                    book.Chapters.Add(chapter);
                }
            }
            return book;
        }

        [HttpGet("{chapterNumber}")]
        public IActionResult GetChapter(int chapterNumber)
        {
            IActionResult result = NotFound();
            var chapter = GetChapterFromData(chapterNumber);
            if (chapter != null)
            {
                result = Ok(chapter);
            }
            return result;
        }

        [HttpGet("{chapterNumber}/{verseNumber}")]
        public IActionResult GetVerse(int chapterNumber, int verseNumber)
        {
            IActionResult result = NotFound();
            var verse = GetVerseFromData(chapterNumber, verseNumber);
            if (verse != null)
            {
                result = Ok(verse);
            }
            return result;
        }

        internal Chapter GetChapterFromData(int chapterNumber)
        {
            Chapter chapter = null;
            var xpath = $"//chapter[@osisID='{BookName}.{chapterNumber}']";
            var node = BookXml.SelectSingleNode(xpath);
            if (node != null)
            {
                chapter = new Chapter();
                chapter.OrderNumber = chapterNumber;
                chapter.Verses = new List<Verse>();
                for (int i = 1; i <= node.ChildNodes.Count; i++)
                {
                    var verse = GetVerseFromData(chapterNumber, i);
                    chapter.Verses.Add(verse);
                }
            }
            return chapter;
        }

        internal Verse GetVerseFromData(int chapterNumber, int verseNumber)
        {
            Verse verse = null;
            var xpath = $"//verse[@osisID='{BookName}.{chapterNumber}.{verseNumber}']";
            var node = BookXml.SelectSingleNode(xpath);
            if (node != null)
            {
                verse = new Verse();
                verse.OrderNumber = verseNumber;
                verse.Words = GetWordsFromVerseNode(node);
                verse.Koren = GetKorenTranslation(chapterNumber, verseNumber);
            }
            return verse;
        }

        internal List<Artifact> GetWordsFromVerseNode(XmlNode node)
        {
            var words = new List<Artifact>();
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                var element = node.ChildNodes[i]; 
                if (element.Name == "w")
                {
                    var word = new Word
                    {
                        Id = element.Attributes["id"].Value,
                        OrderNumber = i + 1,
                        Lemma = element.Attributes["lemma"].Value,
                        Morphology = element.Attributes["morph"].Value,
                    };
                    word.SetText(element.InnerText);
                    words.Add(word);
                }
                else if (element.Name == "seg")
                {
                    var segment = new Segment
                    {
                        OrderNumber = i + 1,
                        Type = element.Attributes["type"].Value
                    };
                    segment.SetText(element.InnerText);
                    words.Add(segment);
                }
                
            }
            return words;
        }

        internal string GetKorenTranslation(int chapterNumber, int verseNumber)
        {
            return KorenTranslation.Text[chapterNumber - 1][verseNumber - 1];
        }

    }
}
