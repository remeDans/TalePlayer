using System;
using System.Xml;
using System.IO;
using TestingEditor;

namespace TalePlayer
{
    public class LogStore
    {
        string path;
        string name;
        XmlDocument xmlDocLog;

        public LogStore(String directory, String name)
        {
            this.name = name;
            this.path = directory;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            xmlDocLog = new XmlDocument();
            xmlDocLog.LoadXml("<?xml version='1.0' encoding='utf-8'?><tale></tale>");
        }

        public LogStore()
        {
            xmlDocLog = new XmlDocument();
            xmlDocLog.LoadXml("<?xml version='1.0' encoding='utf-8'?><tale></tale>");
        }

        public LogStore(String location)
        {
            xmlDocLog = new XmlDocument();
            xmlDocLog.Load(location);
            GetTale();
            
        }

       public XmlDocument SaveTaleManagerXML(TaleManager taleManager, String location, String name)
        {
            xmlDocLog.DocumentElement.SetAttribute("dateOfCreation", taleManager.DateOfCreation.ToString());
            xmlDocLog.DocumentElement.SetAttribute("title", taleManager.Title);
            xmlDocLog.DocumentElement.SetAttribute("language", taleManager.Language);
            xmlDocLog.DocumentElement.SetAttribute("author", taleManager.Author);
            xmlDocLog.DocumentElement.SetAttribute("url", taleManager.Url);
            xmlDocLog.DocumentElement.SetAttribute("license", taleManager.License);
            xmlDocLog.DocumentElement.SetAttribute("background", taleManager.Background);
            xmlDocLog.DocumentElement.SetAttribute("music", taleManager.Music);

            foreach (Page page in taleManager.GetPages)
            {
                xmlDocLog.DocumentElement.AppendChild(CreatePage(page));
            }


            xmlDocLog.PreserveWhitespace = true;
            xmlDocLog.Save(location + "\\" + name);

            return xmlDocLog;
        }

        public XmlDocument SaveTaleManagerXML(TaleManager taleManager)
        {
            xmlDocLog.DocumentElement.SetAttribute("dateOfCreation", taleManager.DateOfCreation.ToString());
            xmlDocLog.DocumentElement.SetAttribute("title", taleManager.Title);
            xmlDocLog.DocumentElement.SetAttribute("language", taleManager.Language);
            xmlDocLog.DocumentElement.SetAttribute("author", taleManager.Author);
            xmlDocLog.DocumentElement.SetAttribute("url", taleManager.Url);
            xmlDocLog.DocumentElement.SetAttribute("license", taleManager.License);
            xmlDocLog.DocumentElement.SetAttribute("background", taleManager.Background);
            xmlDocLog.DocumentElement.SetAttribute("music", taleManager.Music);

            foreach (Page page in taleManager.GetPages)
            {
                xmlDocLog.DocumentElement.AppendChild(CreatePage(page));
            }

            return xmlDocLog;
            
        }


        public XmlElement CreatePage(Page page)
        {
            XmlElement pageElement = xmlDocLog.CreateElement("page");

            pageElement.SetAttribute("index", page.Index.ToString());
            pageElement.SetAttribute("music", page.Music);
            pageElement.SetAttribute("background", page.Background);

            foreach (Pictogram pictogram in page.Pictograms)
            {
                pageElement.AppendChild(CreatePictogram(pictogram));
            }

            return pageElement;
        }

        private XmlElement CreatePictogram(Pictogram pictogram)
        {
            XmlElement pictogramElement = xmlDocLog.CreateElement("pictogram");

            pictogramElement.SetAttribute("index", pictogram.Index.ToString());
            pictogramElement.SetAttribute("sound", pictogram.Sound);
            pictogramElement.SetAttribute("textToRead", pictogram.TextToRead);
            pictogramElement.SetAttribute("word", pictogram.Word);
            pictogramElement.SetAttribute("image", pictogram.ImageName);
            pictogramElement.SetAttribute("wordType", pictogram.Type.ToString());

            return pictogramElement;
        }


        private String GetCData(String field)
        {
            XmlElement xe = (XmlElement)xmlDocLog.SelectSingleNode("/tale");
            return xe.GetAttribute(field);

        }

        public DateTime GetDateOfCreation()
        {
            String value = GetCData("dateOfCreation");
            return DateTime.Parse(value);
        }

        public String GetTitle()
        {
            return GetCData("title");
        }

        public String GetLanguage()
        {
            return GetCData("language");
        }

        public String GetAuthor()
        {
            return GetCData("author");
        }

        public String GetLicense()
        {
            return GetCData("license");
        }

        public String GetUrl()
        {
            return GetCData("url");
        }

        public String GetBackground()
        {
            return GetCData("background");
        }

        public String GetMusic()
        {
            return GetCData("music");
        }


        public TaleManager GetTale()
        {
            String title = GetTitle();
            DateTime date = GetDateOfCreation();
            String language = GetLanguage();
            String author = GetAuthor();
            String url = GetUrl();
            String license = GetLicense();
            String background = GetBackground();
            String music = GetMusic();

            TaleManager taleManager = new TaleManager(date, title, language, author, url, license, background, music);

            int i = 0;
            int j = 0;

            XmlNodeList pages = (XmlNodeList)xmlDocLog.SelectNodes(("/tale/page"));

            foreach (XmlElement page in pages)
            {
                if (page != null)
                {
                    String musicPage = page.GetAttribute("music");
                    String backgroundPage = page.GetAttribute("background");
                    Page p = new Page(i, musicPage, backgroundPage);
                    taleManager.InsertPage(p);

                    foreach (XmlElement pictogram in page.ChildNodes)
                    {
                        if (pictogram != null)
                        {
                            String index = pictogram.GetAttribute("index");
                            j = int.Parse(index);
                            String imageNamePictogram = pictogram.GetAttribute("image");
                            String soundPictogram = pictogram.GetAttribute("sound");
                            String textToReadPictogram = pictogram.GetAttribute("textToRead");
                            String wordPictogram = pictogram.GetAttribute("word");
                            String typePictogram = pictogram.GetAttribute("wordType");
                            WordType typePicto = WordType.ContenidoSocial;
                            foreach (var type in Enum.GetValues(typeof(WordType)))
                            {
                                if (typePictogram == type.ToString())
                                {
                                    typePicto = (WordType)type;
                                }
                            }
                            taleManager.InsertPictogram(new Pictogram(j, imageNamePictogram, textToReadPictogram, soundPictogram, wordPictogram, typePicto), p);
                        }
                    }
                }
                i++;
            }
           
            
            return taleManager;
        }

        public void closeXML()
        {
            xmlDocLog = null;
            path = "";
            name = "";
        }
    }
}