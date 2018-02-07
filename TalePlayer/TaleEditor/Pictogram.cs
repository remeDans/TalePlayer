using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Media;
using System.Windows;

namespace TestingEditor
{
    #region enum
    public enum WordType
    {
        NombreComun,
        Descriptivo,
        Verbo,
        Miscelanea,
        NombrePropio,
        ContenidoSocial,
        Ninguno
    }
    #endregion

    public class Pictogram
    {

        #region Atributos
        private String image, textToRead, sound, word;
       
        public String ImageName
        {
            get { return image; }
            set
            {
                if (image !=null)
                { image = value; }
                else
                { throw new ArgumentNullException(); }
            }
        }
     
        public String TextToRead
        {
            get { return textToRead; }
            set
            {
                if (textToRead != null)
                { textToRead = value; }
                else
                { throw new ArgumentNullException(); }
            }
        }

        /// <summary>
        /// Sonido asociado al pictograma. Este sonido puede ser reproducido en lugar de el texto indicado en TextToRead.
        /// Nota: Un pictograma no tiene por que tener sonidos asociados.
        /// </summary>
        public String Sound
        {
            get { return sound; }
            set { sound = value; }
                
        }

        public String Word
        {
            get { return word; }
            set
            {
                if (word != null)
                { word = value; }
                else
                { throw new ArgumentNullException(); }
            }
        }


       private System.Windows.Media.Brush colorWordType;
        public System.Windows.Media.Brush ColorWordType
        {
            get { return colorWordType; }
        }

        /// <summary>
        /// Indice de cada pictogrma en la pagina?
        /// Nota: Son 10 pictogramas no se si poner la restricción aqui
        /// </summary>
        private int index;
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        private WordType type;
        public WordType Type
        {
            get { return type; }
            set {
                type = value;
                colorWordType = getColorByType(value); }
        }

#endregion

        #region Constructor

        public Pictogram(int index)
        {
            this.index = index;
            this.image = "";
            this.textToRead = "";
            this.sound = "";
            this.word = "";
            this.type = WordType.Ninguno;
            this.colorWordType = getColorByType(this.type);
        }

        public Pictogram(int index, String image, String whatYouRead, String sound, String word, WordType type)
        {
            this.index = index;
            this.image = image;
            this.textToRead = whatYouRead;
            this.sound = sound;
            this.word = word;
            this.type = type;
            this.colorWordType = getColorByType(type);
        }

        #endregion

        #region Métodos

        /* public void EditPictogram(int index, String image, String whatYouRead, String sound, String word, WordType type)
        {
            this.index = index;
            this.image = image;
            this.textToRead = whatYouRead;
            this.sound = sound;
            this.word = word;
            this.type = type;
            this.colorWordType = getColorByType(type);
        }*/


        public System.Windows.Media.Brush getColorByType(WordType Wt)
        {

            System.Windows.Media.Brush color = System.Windows.Media.Brushes.White;
            switch (Wt)
            {
                case WordType.NombreComun:
                    color = System.Windows.Media.Brushes.Orange;
                    break;
                case WordType.Descriptivo:
                    color = System.Windows.Media.Brushes.Blue;
                    break;
                case WordType.Verbo:
                    color = System.Windows.Media.Brushes.Green;
                    break;
                case WordType.Miscelanea:
                    color = System.Windows.Media.Brushes.Black;
                    break;
                case WordType.NombrePropio:
                    color = System.Windows.Media.Brushes.Black;
                    break;
                case WordType.ContenidoSocial:
                    color = System.Windows.Media.Brushes.Black;
                    break;
                case WordType.Ninguno:
                    color = System.Windows.Media.Brushes.Transparent;
                    break;
                default:
                    color = System.Windows.Media.Brushes.Transparent;
                    break;
            }

            return color;
        }
        #endregion

        #region ToString
        public override String ToString()
        {
            System.Windows.Media.Brush c = colorWordType;
            return "Pictogram: " + "\r\n\tIndice: " + index + "\r\n\tImagen: " + image + "\r\n\tTexto a leer: " + textToRead + "\r\n\tSonido: " + sound + "\r\n\tPalabra: " + word + "\r\n\tTipo de palabra: " + colorWordType.ToString();
        }
#endregion



}
}
