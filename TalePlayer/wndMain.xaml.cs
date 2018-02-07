using System;
using System.Windows;
using TestingEditor;
using TPage = TestingEditor.Page;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;
using System.Threading;
using System.Speech.Synthesis;
using System.IO.Compression;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using TaleEditor;
using TalePlayer;
//using Microsoft.Speech.Synthesis;


namespace TalePlayer
{
    /// <summary>
    /// Lógica de interacción para wndMain2.xaml
    /// </summary>
    /// 


    public partial class wndMain : Window
    {
        List<RowDefinition> rows0;
        List<RowDefinition> rows1;
        List<ColumnDefinition> cols0;
        List<ColumnDefinition> column;
        List<Grid> grid;
        List<Image> images;
        List<TextBlock> textBlocksWord;
        List<Border> borders;

        Double height;
        Double width;

        public int tamColums;
        public int tamPic;
        public int heightTextBlock;
        public int fontSize;

        public TaleManager taleManager;
        public PromptBuilder builder;
        MediaPlayer mediaPlayerTale;
        DispatcherTimer timerMusic;
        List<Word> words;

        List<String> imagesPictogramas;
        PromptStyle styleText;
        PromptStyle styleAudio;

        Speak speak;
        DispatcherTimer timerPage;

        Boolean hasFinishedOrStopped;

        String locationSaveImport;

        int tamPicHighlighted;

        public wndMain()
        {
            InitializeComponent();
            words = new List<Word>();
            imagesPictogramas = new List<String>();

            mediaPlayerTale = new MediaPlayer();

            timerMusic = new DispatcherTimer();
            timerMusic.Interval = TimeSpan.FromSeconds(1);

            builder = new PromptBuilder();
            styleText = new PromptStyle();
            styleText.Rate = PromptRate.Slow;
            //styleText.Volume = PromptVolume.ExtraLoud;
            styleText.Emphasis = PromptEmphasis.Strong;

            styleAudio = new PromptStyle();

            //styleAudio.Volume = PromptVolume.Soft;
            //styleAudio.Emphasis = PromptEmphasis.Reduced;

            timerPage = new DispatcherTimer();
            timerPage.Interval = TimeSpan.FromSeconds(1);
            timerPage.Tick += timerPage_Tick;
        
            locationSaveImport = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Tale Player" + "\\imports";

            hasFinishedOrStopped = true;


            

          #region brdPagePictoIni

            grid = new List<Grid>();
            grid.Add(grdPic1);
            grid.Add(grdPic2);
            grid.Add(grdPic3);
            grid.Add(grdPic4);
            grid.Add(grdPic5);
            grid.Add(grdPic6);
            grid.Add(grdPic7);
            grid.Add(grdPic8);
            grid.Add(grdPic9);
            grid.Add(grdPic10);

            column = new List<ColumnDefinition>();
            //columns.Add(col0);
            column.Add(col2);
            column.Add(col4);
            column.Add(col6);
            column.Add(col8);
            //columns.Add(col10);

            rows0 = new List<RowDefinition>();
            rows0.Add(Pic1Row0);
            rows0.Add(Pic2Row0);
            rows0.Add(Pic3Row0);
            rows0.Add(Pic4Row0);
            rows0.Add(Pic5Row0);
            rows0.Add(Pic6Row0);
            rows0.Add(Pic7Row0);
            rows0.Add(Pic8Row0);
            rows0.Add(Pic9Row0);
            rows0.Add(Pic10Row0);

            rows1 = new List<RowDefinition>();
            rows1.Add(Pic1Row1);
            rows1.Add(Pic2Row1);
            rows1.Add(Pic3Row1);
            rows1.Add(Pic4Row1);
            rows1.Add(Pic5Row1);
            rows1.Add(Pic6Row1);
            rows1.Add(Pic7Row1);
            rows1.Add(Pic8Row1);
            rows1.Add(Pic9Row1);
            rows1.Add(Pic10Row1);

            cols0 = new List<ColumnDefinition>();
            cols0.Add(Pic1Col0);
            cols0.Add(Pic2Col0);
            cols0.Add(Pic3Col0);
            cols0.Add(Pic4Col0);
            cols0.Add(Pic5Col0);
            cols0.Add(Pic6Col0);
            cols0.Add(Pic7Col0);
            cols0.Add(Pic8Col0);
            cols0.Add(Pic9Col0);
            cols0.Add(Pic10Col0);

            textBlocksWord = new List<TextBlock>();
            textBlocksWord.Add(txtWordPic1);
            textBlocksWord.Add(txtWordPic2);
            textBlocksWord.Add(txtWordPic3);
            textBlocksWord.Add(txtWordPic4);
            textBlocksWord.Add(txtWordPic5);
            textBlocksWord.Add(txtWordPic6);
            textBlocksWord.Add(txtWordPic7);
            textBlocksWord.Add(txtWordPic8);
            textBlocksWord.Add(txtWordPic9);
            textBlocksWord.Add(txtWordPic10);

            images = new List<Image>();
            images.Add(imgPic1);
            images.Add(imgPic2);
            images.Add(imgPic3);
            images.Add(imgPic4);
            images.Add(imgPic5);
            images.Add(imgPic6);
            images.Add(imgPic7);
            images.Add(imgPic8);
            images.Add(imgPic9);
            images.Add(imgPic10);

            borders = new List<Border>();
            borders.Add(brdPic1);
            borders.Add(brdPic2);
            borders.Add(brdPic3);
            borders.Add(brdPic4);
            borders.Add(brdPic5);
            borders.Add(brdPic6);
            borders.Add(brdPic7);
            borders.Add(brdPic8);
            borders.Add(brdPic9);
            borders.Add(brdPic10);

            #endregion brdPagePictoIni  WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ShowStartInterface();
        }


        #region eventos

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            tamObjectLoaded();
            GetResolution();
            Resize();
        }

        private void tamObjectLoaded()
        {
            /*int i = 0;
            foreach(TextBlock t in  textBlocksWord)
            {
                t.Background = Brushes.White;
                i++;
            }*/

            lblFrontPage.FontSize = 28;
            tamPic = 80;
            heightTextBlock = 50;
            fontSize = 16;
            tamColums = 30;
            tamPicHighlighted = 95;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            int u = 0;

            if (this.WindowState == WindowState.Maximized)
            {
                GetResolution();
                Resize();
            }

            if(this.WindowState==WindowState.Normal)
            {
                tamObjectLoaded();

                Resize();

                foreach (TextBlock t in textBlocksWord)
                {
                    textBlocksWord[u].FontWeight = FontWeights.Normal;
                    u++;
                }

                //grdPics.VerticalAlignment = VerticalAlignment.Stretch;
                //grdPics.HorizontalAlignment = HorizontalAlignment.Stretch;

            }
        }

        private void btnGoToFrontPage_Click(object sender, RoutedEventArgs e)
        {
            stopTale();
            btnPlay.IsEnabled = true;
            btnStop.IsEnabled = false;

            taleManager.GoToFrontPage();
            UpdateGUI();
        }

        private void btnGoToEndPage_Click(object sender, RoutedEventArgs e)
        {
            stopTale();
            btnPlay.IsEnabled = true;
            btnStop.IsEnabled = false;

            taleManager.GoToEndPage();
            UpdateGUI();
        }

        private void menuOpenTale_Click(object sender, RoutedEventArgs e)
        {
            String location = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Tale Player\\";

            Utils.CreateDirectory(locationSaveImport);

            OpenFileDialog file = new OpenFileDialog();
            file.Title = "Abrir cuento";
            file.Filter = "Tale Tale|*.tale";
            file.InitialDirectory = location;
            file.RestoreDirectory = true;

            if (file.ShowDialog() == true)
            {
                String path = file.FileName;
                String nameArchive = file.SafeFileName;

                int tam = nameArchive.Length - 5;//obtnego el nombre del archivo sin la extension
                nameArchive = nameArchive.Substring(0, tam);
                String nameDirectory = locationSaveImport + "\\" + nameArchive;
                String archive = nameDirectory + ".zip";
                Utils.DeleteDirectory(nameDirectory);
                String archiveCopy = nameDirectory + "copy";
                File.Copy(path, archive);

                ZipFile.ExtractToDirectory(path, nameDirectory);

                File.Delete(archive);//elimino el zip

                LogStore lgImport = new LogStore(nameDirectory + "\\index.xml");
                if (lgImport != null)
                {
                    taleManager = new TaleManager();
                    taleManager = lgImport.GetTale();

                    ChangeToAbsolutePath(nameDirectory);

                    GetResolution();
                    Resize();
                    UpdateGUI();
                    
                }

                lgImport.closeXML();
            }
        }

        private void menuShowHelp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"Iconos\Ayuda.pdf");
            }
            catch
            {
                MessageBox.Show("No se puede abrir el archivo.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void menuCloseTale_Click(object sender, RoutedEventArgs e)
        {
            CloseTale();
        }

        private void menuAbout_Click(object sender, RoutedEventArgs e)
        {
            wndAbout wnd = new wndAbout();
            wnd.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            wnd.ShowDialog();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {

            hasFinishedOrStopped = false;

            if (taleManager.Music != "")
            {
                mediaPlayerTale.Open(new Uri(taleManager.Music));
                mediaPlayerTale.Volume = 0.04;
                mediaPlayerTale.Play();
                timerMusic.Start();
            }

            timerPage.Start();


            btnPlay.IsEnabled = false;
            btnStop.IsEnabled = true;
            btnGoToFrontPage.IsEnabled = false;
            btnGoToEndPage.IsEnabled = false;
            btnNextPage.IsEnabled = false;
            btnPreviousPage.IsEnabled = false;

        }

        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            /*btnPlay.IsEnabled = true;
            btnGoToEndPage.IsEnabled = true;
            btnGoToFrontPage.IsEnabled = true;
            btnStop.IsEnabled = false;*/


            hasFinishedOrStopped = true;

            GetResolution();
            Resize();

            stopTale();
            UpdateGUI();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool really = exitTale();
            if (!really)
            {
                e.Cancel = true;
            }
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (taleManager.GoToPreviousPage())
            {
                //stopTale();
                btnPlay.IsEnabled = true;
                btnStop.IsEnabled = false;

                UpdateGUI();
                ShowMenuGUI();
            }
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (taleManager.GoToNextPage())
            {
                //stopTale();
                btnPlay.IsEnabled = true;
                btnStop.IsEnabled = false;

                UpdateGUI();
                ShowMenuGUI();
            }
        }

        private void Speak_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            if(taleManager!=null)
            {
                if(!hasFinishedOrStopped)
                {
                    if (taleManager.GoToNextPage())
                    {
                        reproduce();
                    }
                    else
                    {
                        hasFinishedOrStopped = true;
                    }
                }

                UpdateGUI();
            }

        }

        

        private void Speak_BookmarkReached(object sender, BookmarkReachedEventArgs e)
        {
            int index = int.Parse(e.Bookmark);

            int indexAux = index - 1;

            if (index >= 1)
            {
                borders[indexAux].BorderThickness = new Thickness(6);

                borders[indexAux].Width = tamPic;
                borders[indexAux].Height = tamPic;
                rows0[indexAux].Height = new GridLength(tamPic);
                cols0[indexAux].Width = new GridLength(tamPic);

                textBlocksWord[indexAux].FontSize = fontSize;
                textBlocksWord[indexAux].Width = tamPic;
            }

            borders[index].BorderThickness = new Thickness(6);

            borders[index].Width = tamPicHighlighted;
            borders[index].Height = tamPicHighlighted;
            rows0[index].Height = new GridLength(tamPicHighlighted);
            cols0[index].Width = new GridLength(tamPicHighlighted);

            textBlocksWord[index].FontSize = fontSize + 2;
            textBlocksWord[index].Width = tamPicHighlighted;

        }

        void timerPage_Tick(object sender, EventArgs e)
        {
            if(taleManager.IsFrontPage())
            {
                taleManager.GoToNextPage();
            }
            timerPage.Stop();

            UpdateGUI();
            reproduce();
        }

        #endregion eventos


        #region funciones

        private void ShowStartInterface()
        {
            grdStart.Visibility = Visibility.Visible;
            grdFrontPage.Visibility = Visibility.Collapsed;
            grdPage.Visibility = Visibility.Collapsed;
        }

        private void ShowTaleGUI()
        {
            grdStart.Visibility = Visibility.Collapsed;
            grdFrontPage.Visibility = Visibility.Visible;
            grdPage.Visibility = Visibility.Collapsed;
        }

        private void ShowPageGUI()
        {
            grdStart.Visibility = Visibility.Collapsed;
            grdFrontPage.Visibility = Visibility.Collapsed;
            grdPage.Visibility = Visibility.Visible;
        }

        private void UpdateGUI()
        {


            if(taleManager==null)
            {
                menuCloseTale.IsEnabled = false;

                ShowStartInterface();
            }

            if(taleManager!=null)
            {
                menuCloseTale.IsEnabled = true;

                if (taleManager.CurrentPageIndex == -1)
                {
                    UpdateTaleGUI();
                    ShowTaleGUI();
                }
                else
                {
                    UpdatePageGUI();
                    ShowPageGUI();
                }
            }

            UpdateNavigation();
        }

        private void ChangeToAbsolutePath(String location)
        {
            //cuento
            if (taleManager.Background != "" && Utils.isArchive(taleManager.Background))
            {
                taleManager.Background = location + "\\Imagenes\\0\\" + taleManager.Background;
            }
            if (taleManager.Music != "" && Utils.isArchive(taleManager.Music))
            {
                taleManager.Music = location + "\\Audios\\0\\" + taleManager.Music;
            }
            //páginas
            foreach (TPage page in taleManager.GetPages)
            {
                int numPage = page.Index + 1;
                if (page.Background != "" && Utils.isArchive(page.Background))
                {
                    page.Background = location + "\\Imagenes\\" + numPage + "\\" + page.Background;
                }
                if (page.Music != "" && Utils.isArchive(page.Music))
                {
                    page.Music = location + "\\Audios\\" + numPage + "\\" + page.Music;
                }

                foreach (Pictogram picto in page.Pictograms)
                {
                    if (picto.ImageName != "" && Utils.isArchive(picto.ImageName))
                    {
                        picto.ImageName = location + "\\Imagenes\\" + numPage + "\\" + picto.ImageName;
                    }
                    if (picto.Sound != "" && Utils.isArchive(picto.Sound))
                    {
                        picto.Sound = location + "\\Audios\\" + numPage + "\\" + picto.Sound;
                    }
                }
            }
        }

        private void UpdateTaleGUI()
        {
            grdFrontPage.Background = Brushes.WhiteSmoke;
            imgFrontPageBackground.Source = null;

            if (taleManager != null)
            {
                lblFrontPage.Content = taleManager.Title;

                //-- Background
                String background = taleManager.Background;
                int tamBackground = background.Length;
                if (tamBackground > 0)
                {
                    if (Utils.isArchive(background))
                    {
                        imgFrontPageBackground.Source = Utils.GetBitmapFromUri(background, UriKind.Absolute);

                        imgFrontPageBackground.Stretch = Stretch.UniformToFill;
                        imgFrontPageBackground.HorizontalAlignment = HorizontalAlignment.Center;
                        imgFrontPageBackground.VerticalAlignment = VerticalAlignment.Center;

                        if (imgFrontPageBackground.Source == null)
                        {
                            taleManager.Background = "";
                        }
                    }

                    if (background.Contains("#"))
                    {
                        var bc = new BrushConverter();
                        grdFrontPage.Background = (Brush)bc.ConvertFrom(background);
                    }
                }
            }

            if (this.WindowState == WindowState.Maximized)
            {
                GetResolution();
                Resize();
            }
            
        }

        private void UpdatePageGUI()
        {
            grdPage.Background = Brushes.WhiteSmoke;
            imgPageBackground.Source = null;

            for (int i = 0; i < 10; i++)
            {
                textBlocksWord[i].Text = "";
                images[i].Source = null;
                grid[i].Visibility = Visibility.Visible;
                borders[i].BorderBrush = Brushes.Transparent;
            }

            //-- Background
            TestingEditor.Page auxPage = taleManager.CurrentPage;

            if (auxPage.Background != "")
            {
                String background = auxPage.Background;
                int tamBackground = background.Length;
                if (tamBackground > 0)
                {
                    String p = background.Substring(tamBackground - 4);
                    if (p.Contains("."))
                    {
                        String pathBackgroundPage = auxPage.Background;
                        imgPageBackground.Source = Utils.GetBitmapFromUri(pathBackgroundPage, UriKind.Absolute);
                        imgPageBackground.Stretch = Stretch.UniformToFill;
                        imgPageBackground.HorizontalAlignment = HorizontalAlignment.Center;
                        imgPageBackground.VerticalAlignment = VerticalAlignment.Center;
                    }

                    if (background.Contains("#"))
                    {
                        var bc = new BrushConverter();
                        grdPage.Background = (Brush)bc.ConvertFrom(background);
                    }

                }
            }

            //-- PICTOGRAMAS ----

            foreach (Pictogram pictogram in auxPage.Pictograms)
            {
                int i = pictogram.Index;
                textBlocksWord[i].Text = pictogram.TextToRead;

                String path = pictogram.ImageName;
                images[i].Source = Utils.GetBitmapFromUri(path, UriKind.Absolute);

                EditBorder(borders[i], pictogram.getColorByType(pictogram.Type), 6);
            }


            for (int i = 0; i < 10; i++)
            {
                if (textBlocksWord[i].Text == null && images[i].Source == null)
                {
                    grid[i].Visibility = Visibility.Hidden;
                }
            }

            if (this.WindowState == WindowState.Maximized)
            {
                GetResolution();
                Resize();
            }

        }

        private void stopTale()
        {
            if (mediaPlayerTale != null)
            {
                mediaPlayerTale.Stop();
                timerMusic.Stop();
                mediaPlayerTale.Close();
            }

            timerPage.Stop();

            if (speak != null)
            {
                speak.SpeakerStop();
                builder.ClearContent();
            }
        }

        private void EditBorder(Border border, Brush color, int borderSize)
        {
            border.BorderBrush = color;
            border.BorderThickness = new Thickness(borderSize, borderSize, borderSize, borderSize);
        }

        private void ShowMenuGUI()
        {
            if(taleManager==null)
            {
                menuCloseTale.IsEnabled = false;
                menuOpenTale.IsEnabled = true;
            }

            if (taleManager != null)
            {
                menuOpenTale.IsEnabled = false;
                menuCloseTale.IsEnabled = true;
            }
        }

        private void CloseTale()
        {
            int i = 0;

            stopTale();
            taleManager = null;

            //--- esto hay que hacerlo si o si ---
            grdFrontPage.Background = Brushes.WhiteSmoke;
            grdPage.Background = Brushes.WhiteSmoke;
            imgFrontPageBackground.Source = null;
            imgPageBackground.Source = null;

            foreach (Image img in images)
            {
                img.Source = null;
                i++;
            }
            //-------------------------------

            btnPlay.IsEnabled = false;
            btnStop.IsEnabled = false;
            btnNextPage.IsEnabled = false;
            btnPreviousPage.IsEnabled = false;
            btnGoToEndPage.IsEnabled = false;
            btnGoToFrontPage.IsEnabled = false;
            btnStop.IsEnabled = false;

            ShowMenuGUI();
            UpdateGUI();

            String[] chilFolder = Directory.GetDirectories(locationSaveImport);
            foreach (string pathFolder in chilFolder)
            {
                if (Directory.Exists(pathFolder))
                {
                    Directory.Delete(pathFolder, true);
                }
            }
        }

        private bool exitTale()
        {
            bool ret = false;
            if (taleManager != null)
            {
                MessageBoxResult result = MessageBox.Show("¿Está seguro de que quiere salir?", "Tale Player", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    ret = true;
                }
                if (result == MessageBoxResult.No)
                {
                    ret = false;
                }
            }
            else
            {
                ret = true;
            }

            return ret;
        }

        private void reproduce()
        {
            builder.ClearContent();
            speakBuilder();

            speak = new Speak(builder);
            speak.BookmarkReached += Speak_BookmarkReached;
            speak.SpeakCompleted += Speak_SpeakCompleted;

            speak.Speaker();

            if(taleManager.CurrentPage.Music!="")
            {
                mediaPlayerTale.Stop();
                timerMusic.Stop();
                mediaPlayerTale.Open(new Uri(taleManager.CurrentPage.Music));
                mediaPlayerTale.Volume = 0.04;
                mediaPlayerTale.Play();
                timerMusic.Start();
            }
        }

        public void speakBuilder()
        {
            builder.StartParagraph();
            builder.StartSentence();
            foreach (Pictogram picto in taleManager.CurrentPage.Pictograms)
            {
                if (picto != null)
                {
                    if (picto.Sound != "")
                    {
                        builder.AppendBookmark(picto.Index.ToString());
                        //builder.AppendBookmark(" ");
                        //builder.StartStyle(styleAudio);
                        builder.AppendAudio(picto.Sound);
                        //builder.EndStyle();
                    }
                    else
                    {
                        builder.AppendBookmark(picto.Index.ToString());
                        builder.StartStyle(styleText);
                        builder.AppendText(picto.TextToRead);
                        builder.EndStyle();
                    }
                }
            }

            builder.EndSentence();
            builder.EndParagraph();
        }

        private void GetResolution()
        {
            height = SystemParameters.VirtualScreenHeight;
            width = SystemParameters.VirtualScreenWidth;

            if (this.WindowState == WindowState.Maximized)
            {
                if (height >= 600 && height < 720)
                {
                    tamObjectLoaded();

                }
                else if (height >= 720 && height < 1024)
                {
                    lblFrontPage.FontSize = 32;
                    tamPic = 130;
                    fontSize = 20;
                    tamColums = 100;
                    tamPicHighlighted = 145;
                    heightTextBlock = 70;
                }
                else
                {
                    lblFrontPage.FontSize = 36;
                    tamPic = 190;
                    fontSize = 25;
                    tamColums = 100;
                    tamPicHighlighted = 205;
                    heightTextBlock = 70;
                }
            }
        }

        private void Resize()
        {
            int i = 0;
            int j = 0;
            int k = 0;
            int u = 0;
            int v = 0;
            int n = 0;

            //grdPics.VerticalAlignment = VerticalAlignment.Stretch;
            //grdPics.HorizontalAlignment = HorizontalAlignment.Stretch;


            foreach (Grid g in grid)
            {
                grid[n].Width = tamPicHighlighted;
                grid[n].Height = tamPicHighlighted+textBlocksWord[n].Height;
                n++;
            }

            foreach (RowDefinition r in rows0)
            {
                rows0[i].Height = new GridLength(tamPic);
                rows1[i].Height = new GridLength(heightTextBlock);
                i++;
            }

            foreach (ColumnDefinition c in cols0)
            {
                cols0[j].Width = new GridLength(tamPic);
                j++;
            }

            foreach (ColumnDefinition c in column)
            {
                column[v].MaxWidth = tamColums;
                v++;
            }

            foreach (Border b in borders)
            {
                borders[k].Height = tamPic;
                borders[k].Width = tamPic;
                k++;
            }

            foreach (TextBlock l in textBlocksWord)
            {
                textBlocksWord[u].FontSize = fontSize;
                textBlocksWord[u].Height = heightTextBlock;
                textBlocksWord[u].FontWeight = FontWeights.Bold;
                textBlocksWord[u].Width = tamPic;
                textBlocksWord[u].HorizontalAlignment = HorizontalAlignment.Center;
                textBlocksWord[u].TextAlignment = TextAlignment.Center;
                u++;
            }
        }

        private void UpdateNavigation()
        {
            if (taleManager != null)
            {
                if (hasFinishedOrStopped)
                {
                    btnPlay.IsEnabled = true;
                    btnStop.IsEnabled = false;

                    if (taleManager.CurrentPageIndex == -1)
                    {
                        btnGoToFrontPage.IsEnabled = false;
                        btnGoToEndPage.IsEnabled = true;

                        btnPreviousPage.IsEnabled = false;
                        btnNextPage.IsEnabled = true;

                        


                        if (taleManager.NumberOfPages == 0)
                        {
                            btnGoToFrontPage.IsEnabled = false;
                            btnPreviousPage.IsEnabled = false;
                            btnGoToEndPage.IsEnabled = false;
                            btnNextPage.IsEnabled = false;
                        }
                    }
                    else
                    {
                        if (taleManager.CurrentPageIndex >= 0 && taleManager.CurrentPageIndex < taleManager.NumberOfPages - 1)
                        {
                            btnGoToFrontPage.IsEnabled = true;
                            btnPreviousPage.IsEnabled = true;
                            btnGoToEndPage.IsEnabled = true;
                            btnNextPage.IsEnabled = true;
                        }
                        else
                        {
                            btnGoToFrontPage.IsEnabled = true;
                            btnPreviousPage.IsEnabled = true;
                            btnGoToEndPage.IsEnabled = false;
                            btnNextPage.IsEnabled = false;

                            btnPlay.IsEnabled = true;
                            btnStop.IsEnabled = false;
                            //hasFinishedOrStopped = true;
                        }

                    }
                }
                else
                {
                    btnPlay.IsEnabled = false;
                    btnStop.IsEnabled = true;
                }
            }

        }

        #endregion funciones

    }
}
