using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Speech.Synthesis;
using System.Windows.Media;
//using Microsoft.Speech.Synthesis;

namespace TalePlayer
{
   public class Speak
    {
        public event EventHandler<BookmarkReachedEventArgs> BookmarkReached;
        public event EventHandler<SpeakCompletedEventArgs> SpeakCompleted;

        public PromptBuilder builder;
        public SpeechSynthesizer synth;


        public Speak(PromptBuilder builder)
        {
            this.builder = builder;

            synth = new SpeechSynthesizer();

            synth.SetOutputToDefaultAudioDevice();

            synth.BookmarkReached += Synth_BookmarkReached;
            synth.SpeakCompleted += Synth_SpeakCompleted;
            synth.Volume = 100;
            
        }

        private void Synth_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            LaunchSpeakCompleted(e);
        }

        private void Synth_BookmarkReached(object sender, BookmarkReachedEventArgs e)
        {
            //if (e.Bookmark != " ")
            //{
                LaunchBookmarkReached(e);
                //synth.Volume = 100;
            /*}
            else
            {
                synth.Volume = 20;
            }*/
        }

        protected virtual void LaunchBookmarkReached(BookmarkReachedEventArgs e)
        {
            EventHandler<BookmarkReachedEventArgs> Handler = BookmarkReached;

            // Evento será nulo si no hay suscriptores
            if (Handler != null)
            {
                // Lanzar el evento.
                Handler(this, e);
            }
        }

        protected virtual void LaunchSpeakCompleted(SpeakCompletedEventArgs e)
        {
            EventHandler<SpeakCompletedEventArgs> Handler = SpeakCompleted;

            // Evento será nulo si no hay suscriptores
            if (Handler != null)
            {
                // Lanzar el evento.
                Handler(this, e);
            }
        }


        // This method will be called when the thread is started.
        public void Speaker()
        {
            synth.SpeakAsync(builder);
        }

        public void SpeakerStop()
        {
            if (synth!=null && synth.State == SynthesizerState.Speaking)
            {
                synth.SpeakAsyncCancelAll();
                synth.Dispose();
                synth = null;
            }
        }
  
    }
}
