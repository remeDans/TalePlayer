using System;
using TestingEditor;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TalePlayer
{
    public class Word
    {
        private String id, wordImage,nameImage ;

        public String ID
        {
            get { return id; }
        }
        public String NameImage
        {
            get { return nameImage; }
        }
        public String WordImage
        {
            get { return wordImage; }
        }

        private WordType type;
        public WordType Type
        {
            get { return type; }
        }
        public Word(String ID, String word, String name, WordType type)
        {
            this.id = ID;
            this.nameImage = name;
            this.wordImage = word;
            this.type = type;
        }

        
    }
}
