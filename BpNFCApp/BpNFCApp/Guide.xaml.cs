using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BpNFCApp
{
    /// <summary>
    /// Interaction logic for Guide.xaml
    /// </summary>
    public partial class Guide : Window
    {
        private DispatcherTimer _timer;
        private TimeSpan _time;
        public Guide()
        {
            InitializeComponent();

            TimerTextBlock.Visibility = Visibility.Hidden;

            GuideImage.Source = new BitmapImage(new Uri("Images/position1.jpg", UriKind.Relative));
            GuideLabel.Content = "Sæt dig i en behagelig position. \n \nPlacer begge fødder på jorden. \n \nDine ben må ikke være krydsede. ";
        }

        private void Done1Button_Click(object sender, RoutedEventArgs e)
        {
            GuideImage.Source = new BitmapImage(new Uri("Images/position2.jpg", UriKind.Relative));
            GuideLabel.Content = "Sæt mancetten på højre arm:\n \n     1. Fjern tøj fra overarmen.\n \n     2. Slangen skal pege væk fra \n         kroppen. \n \n     3. Stram ikke mancetten for meget: \n         der skal kunne være to fingre \n         under mancetten.";
            Done1Button.Visibility = Visibility.Hidden;
            Done2Button.Visibility = Visibility.Visible;
        }

        private void Done2Button_Click(object sender, RoutedEventArgs e)
        {
            GuideImage.Source = new BitmapImage(new Uri("Images/position3.jpg", UriKind.Relative));
            GuideLabel.Content = "Tjek at mancetten sidder som på \nbilledet. \nDu er nu klar til at påbegynde\nundersøgelsen.\n \nTryk på Klar";
            Done2Button.Visibility = Visibility.Hidden;
            StartButton.Visibility = Visibility.Visible;
        }

        private int count = 0;

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            TimerTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            GuideLabel.Foreground = new SolidColorBrush(Colors.Black);

            TimerTextBlock.Visibility = Visibility.Visible;

            _time = TimeSpan.FromMinutes(0.1);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                TimerTextBlock.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero)
                {
                    _timer.Stop();
                    GuideImage.Source = new BitmapImage(new Uri("Images/position4.jpg", UriKind.Relative));
                    TimerTextBlock.Foreground = new SolidColorBrush(Colors.Green);
                    GuideLabel.Foreground = new SolidColorBrush(Colors.Green);
                    NewMeasurementButton.Visibility = Visibility.Visible;
                    NewMeasurementButton.IsEnabled = true;

                    GuideLabel.Content = "Tryk på Start knappen på \nblodtryksmåleren.\n \nNår målingen er færdig,\ntryk på Ny måling knappen.";
                    if (count == 3)
                    {
                        TransferButton.Visibility = Visibility.Visible;
                        GuideLabel.Content = "Du har nu udført \n3 blodtryksmålinger.\n \nTryk på Resultat knappen.";
                        NewMeasurementButton.IsEnabled = false;
                    }

                    
                }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();

            GuideImage.Source = new BitmapImage(new Uri("Images/position3.jpg", UriKind.Relative));
            GuideLabel.Content = "Vent i 5 minutter.\n \n\u2022 Sid afslappet.\n \n\u2022 Begge fødder skal være på jorden.\n \n\u2022 Benene må ikke krydses. \n \n\u2022 Du må ikke tale. \n \n";
            NewMeasurementButton.IsEnabled = false;
            StartButton.Visibility = Visibility.Hidden;
            count++;
            
        }

        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            TimerTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            GuideLabel.Foreground = new SolidColorBrush(Colors.Black);
            GuideImage.Source = new BitmapImage(new Uri("Images/position6.jpg", UriKind.Relative));
            GuideLabel.Content = "Gør som på billedet.";
            TransferButton.IsEnabled = false;

            // Algoritme:

        }
    }
}
