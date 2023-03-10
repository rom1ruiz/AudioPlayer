using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace AudioPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MediaPlayer player = new MediaPlayer();
        public string startPath = Directory.GetCurrentDirectory();
        public MainWindow()
        {
            InitializeComponent();
            prog.Opacity = 0;
            //Initializing our timer
            DispatcherTimer timer = new DispatcherTimer();

            //Setting up
            timer.Interval = TimeSpan.FromSeconds(1);

            //Run Update on each tick
            timer.Tick += Update;

            //Starting our timer
            timer.Start();
        }
        private void Update(object sender, EventArgs e)
        {
            //Execute an update only if our source is not null
            if (player.Source != null) 
            {
                //Setting min value
                prog.Minimum = 0;

                //Setting Maximum value based on the number of seconds of our audio file
                prog.Maximum = player.NaturalDuration.TimeSpan.TotalSeconds;

                //Setting the value of our progress bar to where the player is in our audio file
                prog.Value = player.Position.TotalSeconds;
            }
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog filedialog = new OpenFileDialog();
            filedialog.Filter = "MP3 Files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (filedialog.ShowDialog() == true)
            {
                player.Open(new System.Uri(filedialog.FileName));
                prog.Opacity = 1;
                filePath.Text = filedialog.FileName;
            }
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            player.Play();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
        }

        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            player.Volume = Volume.Value / 100;
            //MessageBox.Show(startPath + "\\icons\\volume_downL.png");
            if (Volume.Value < 50)
            {
                imgVol.Source = new BitmapImage(new Uri(startPath + "/icons/volume_downL.png"));
            }
            else
            {
                imgVol.Source = new BitmapImage(new Uri(startPath + "/icons/volume_upL.png"));
            }
        }
    }
}
