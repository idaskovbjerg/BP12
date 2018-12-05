using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        private TimeSpan timeSpan;
        private DispatcherTimer dispatcherTimer;
        private double minut = 1;
        private int sek = 40;
        public Guide()
        {
            InitializeComponent();

            TimerTextBlock.Visibility = Visibility.Hidden;
            GuideImage.Source = new BitmapImage(new Uri("Images/position1.jpg", UriKind.Relative));
            GuideLabel.Content = "Velkommen.\n\nDu skal nu udføre 3 blodtryksmålinger \npå dig selv.\n\nNår alle blodtryksmålinger er udført, \nbliver du guided til at overføre data \ntil systemet.\n\nHele undersøgelsen tager omtrendt \n20 minutter.";

        }
        private void Back0Button_Click(object sender, RoutedEventArgs e)
        {
            GuideImage.Source = new BitmapImage(new Uri("Images/position1.jpg", UriKind.Relative));
            GuideLabel.Content = "Velkommen.\n\nDu skal nu udføre 3 blodtryksmålinger \npå dig selv.\n\nNår alle blodtryksmålinger er udført, \nbliver du guided til at overføre data \ntil systemet.\n\nHele undersøgelsen tager omtrendt \n20 minutter";
            Back0Button.Visibility = Visibility.Hidden;
            Done0Button.Visibility = Visibility.Visible;
            Done1Button.Visibility = Visibility.Hidden;
        }
        private void Done0Button_Click(object sender, RoutedEventArgs e)
        {
            Back0Button.Visibility = Visibility.Visible;
            Done0Button.Visibility = Visibility.Hidden;
            Done1Button.Visibility = Visibility.Visible;
            GuideImage.Source = new BitmapImage(new Uri("Images/position1.jpg", UriKind.Relative));
            GuideLabel.Content = "Sæt dig i en behagelig position. \n \nPlacer begge fødder på jorden. \n \nDine ben må ikke være krydsede. ";
        }

        private void Back1Button_Click(object sender, RoutedEventArgs e)
        {
            GuideImage.Source = new BitmapImage(new Uri("Images/position1.jpg", UriKind.Relative));
            GuideLabel.Content = "Sæt dig i en behagelig position. \n \nPlacer begge fødder på jorden. \n \nDine ben må ikke være krydsede. ";

            Done1Button.Visibility = Visibility.Visible;
            Done2Button.Visibility = Visibility.Hidden;
            Back0Button.Visibility = Visibility.Visible;
            Back1Button.Visibility = Visibility.Hidden;
        }

        private void Done1Button_Click(object sender, RoutedEventArgs e)
        {
            GuideImage.Source = new BitmapImage(new Uri("Images/position2.jpg", UriKind.Relative));
            GuideLabel.Content = "Sæt mancetten på højre arm:\n \n    1. Fjern tøj fra overarmen.\n \n    2. Sæt mancetten på overarmen.\n \n    3. Slangen skal være langs \n        armen.\n \n    4. Stram ikke mancetten for meget: \n        der skal kunne være to fingre \n        under mancetten.";
            Done1Button.Visibility = Visibility.Hidden;
            Done2Button.Visibility = Visibility.Visible;
            Back0Button.Visibility = Visibility.Hidden;
            Back1Button.Visibility = Visibility.Visible;
        }

        private void Back2Button_Click(object sender, RoutedEventArgs e)
        {
            GuideImage.Source = new BitmapImage(new Uri("Images/position2.jpg", UriKind.Relative));
            GuideLabel.Content = "Sæt mancetten på højre arm:\n \n    1. Fjern tøj fra overarmen.\n \n    2. Sæt mancetten på overarmen.\n \n    3. Slangen skal være langs \n        armen.\n \n    4. Stram ikke mancetten for meget: \n        der skal kunne være to fingre \n        under mancetten.";
            Done2Button.Visibility = Visibility.Visible;
            StartButton.Visibility = Visibility.Hidden;
            Back1Button.Visibility = Visibility.Visible;
            Back2Button.Visibility = Visibility.Hidden;
        }

        private void Done2Button_Click(object sender, RoutedEventArgs e)
        {
            GuideImage.Source = new BitmapImage(new Uri("Images/position3.jpg", UriKind.Relative));
            GuideLabel.Content = "Tjek at mancetten sidder som på \nbilledet. \nDu er nu klar til at påbegynde\nundersøgelsen.\n \nTryk på Klar";
            Done2Button.Visibility = Visibility.Hidden;
            StartButton.Visibility = Visibility.Visible;
            Back1Button.Visibility = Visibility.Hidden;
            Back2Button.Visibility = Visibility.Visible;
        }
        
        private int count = 0;
        
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Back2Button.Visibility = Visibility.Hidden;
            NewMeasurementButton.Visibility = Visibility.Visible;
            NewMeasurementButton.IsEnabled = false;

            TimerTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            GuideLabel.Foreground = new SolidColorBrush(Colors.Black);

            TimerTextBlock.Visibility = Visibility.Visible;

            _time = TimeSpan.FromMinutes(1); // 5 minutes

            timeSpan = TimeSpan.FromSeconds(sek); // ca. 40 sek

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                TimerTextBlock.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero)
                {
                    _timer.Stop();
                    GuideImage.Source = new BitmapImage(new Uri("Images/position4.jpg", UriKind.Relative));
                    TimerTextBlock.Foreground = new SolidColorBrush(Colors.Green);
                    GuideLabel.Foreground = new SolidColorBrush(Colors.Green);
                    
                    GuideLabel.Content = "Blodtryksmåling nr. " + count + " af 3.\n\n" +"Tryk på Start knappen på \nblodtryksmåleren.\n \nManchetten pustes nu op \nog måler blodtrykket.";

                    dispatcherTimer =
                        new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                        {
                            if (timeSpan == TimeSpan.Zero)
                            {
                                dispatcherTimer.Stop(); 
                                GuideLabel.Content = "Når blodtryksmålingen er foretaget, \nvil skærmen på blodtryksmåleren\nvære som på billedet.\n \nNår målingen er færdig,\ntryk på Ny måling knappen.";
                                GuideImage.Source = new BitmapImage(new Uri("Images/position5.jpg", UriKind.Relative));
                                NewMeasurementButton.IsEnabled = true;
                            }
                            timeSpan = timeSpan.Add(TimeSpan.FromSeconds(-1));
                        }, Application.Current.Dispatcher);
                    dispatcherTimer.Start();
                }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();

            GuideImage.Source = new BitmapImage(new Uri("Images/position3.jpg", UriKind.Relative));
            GuideLabel.Content = "Vent i 5 minutter.\n \n\u2022 Sid afslappet.\n \n\u2022 Begge fødder skal være på jorden.\n \n\u2022 Benene må ikke krydses. \n \n\u2022 Du må ikke tale. \n \n";
            StartButton.Visibility = Visibility.Hidden;
            count++;
            
        }

        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            TimerTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            GuideLabel.Foreground = new SolidColorBrush(Colors.Black);
            GuideImage.Source = new BitmapImage(new Uri("Images/position3.jpg", UriKind.Relative));
            GuideLabel.Content = "Der skal foretages endnu en \nblodtryksmåling. \n\nTryk på Ny måling.";
            TransferButton.IsEnabled = false;
            NewMeasurement2Button.IsEnabled = true;

            
            MainWindow resultat = new MainWindow();
            resultat.UserName = UserName;
            resultat.PassWord = PassWord;
            resultat.Show();
        }
        public string UserName
        {
            get;
            set;
        }
        public string PassWord { get; set; }

        private void NewMeasurement2Button_Click(object sender, RoutedEventArgs e)
        {
            TimerTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            GuideLabel.Foreground = new SolidColorBrush(Colors.Black);

            TimerTextBlock.Visibility = Visibility.Visible;

            _time = TimeSpan.FromMinutes(minut);

            timeSpan = TimeSpan.FromSeconds(sek);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                TimerTextBlock.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero)
                {
                    _timer.Stop();
                    GuideImage.Source = new BitmapImage(new Uri("Images/position4.jpg", UriKind.Relative));
                    TimerTextBlock.Foreground = new SolidColorBrush(Colors.Green);
                    GuideLabel.Foreground = new SolidColorBrush(Colors.Green);
                    NewMeasurement2Button.Visibility = Visibility.Visible;

                    GuideLabel.Content = "Blodtryksmåling nr. " + count + " af 3.\n\n" + "Tryk på Start knappen på \nblodtryksmåleren.\n \nManchetten pustes nu op \nog måler blodtrykket.";

                    dispatcherTimer =
                        new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                        {
                            if (timeSpan == TimeSpan.Zero)
                            {
                                dispatcherTimer.Stop();
                                GuideLabel.Content = "Når blodtryksmålingen er foretaget, \nvil skærmen på blodtryksmåleren\nvære som på billedet.\n \nNår målingen er færdig,\ntryk på Ny måling knappen.";
                                GuideImage.Source = new BitmapImage(new Uri("Images/position5.jpg", UriKind.Relative));
                                NewMeasurement2Button.IsEnabled = true;

                            }
                            if (timeSpan == TimeSpan.Zero && count >= 3)
                            {
                                dispatcherTimer.Stop();
                                TransferButton.Visibility = Visibility.Visible;
                                GuideImage.Source = new BitmapImage(new Uri("Images/position5.jpg", UriKind.Relative));
                                GuideLabel.Content = "Du har nu udført de \n"+count+" blodtryksmålinger.\n \nTryk på Resultat knappen.";
                                NewMeasurement2Button.IsEnabled = false;
                                TransferButton.IsEnabled = true;
                            }
                            timeSpan = timeSpan.Add(TimeSpan.FromSeconds(-1));
                        }, Application.Current.Dispatcher);
                    dispatcherTimer.Start();
                }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();

            GuideImage.Source = new BitmapImage(new Uri("Images/position3.jpg", UriKind.Relative));
            GuideLabel.Content = "Vent i 1 minut.\n \n\u2022 Sid afslappet.\n \n\u2022 Begge fødder skal være på jorden.\n \n\u2022 Benene må ikke krydses. \n \n\u2022 Du må ikke tale. \n \n";
            NewMeasurement2Button.IsEnabled = false;
            StartButton.Visibility = Visibility.Hidden;
            count++;
        }

        private void NewMeasurementButton_Click(object sender, RoutedEventArgs e)
        {
            TimerTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            GuideLabel.Foreground = new SolidColorBrush(Colors.Black);

            TimerTextBlock.Visibility = Visibility.Visible;

            _time = TimeSpan.FromMinutes(minut);

            timeSpan = TimeSpan.FromSeconds(sek);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                TimerTextBlock.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero)
                {
                    _timer.Stop();
                    GuideImage.Source = new BitmapImage(new Uri("Images/position4.jpg", UriKind.Relative));
                    TimerTextBlock.Foreground = new SolidColorBrush(Colors.Green);
                    GuideLabel.Foreground = new SolidColorBrush(Colors.Green);
                    NewMeasurement2Button.Visibility = Visibility.Visible;

                    GuideLabel.Content = "Blodtryksmåling nr. " + count + " af 3.\n\n"+"Tryk på Start knappen på \nblodtryksmåleren.\n \nManchetten pustes nu op \nog måler blodtrykket.";

                    dispatcherTimer =
                        new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                        {
                            if (timeSpan == TimeSpan.Zero)
                            {
                                dispatcherTimer.Stop();
                                GuideLabel.Content = "Når blodtryksmålingen er foretaget, \nvil skærmen på blodtryksmåleren\nvære som på billedet.\n \nNår målingen er færdig,\ntryk på Ny måling knappen.";
                                GuideImage.Source = new BitmapImage(new Uri("Images/position5.jpg", UriKind.Relative));
                                NewMeasurement2Button.IsEnabled = true;

                            }
                            timeSpan = timeSpan.Add(TimeSpan.FromSeconds(-1));
                        }, Application.Current.Dispatcher);
                    dispatcherTimer.Start();
                }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();

            GuideImage.Source = new BitmapImage(new Uri("Images/position3.jpg", UriKind.Relative));
            GuideLabel.Content = "Vent i 1 minut.\n \n\u2022 Sid afslappet.\n \n\u2022 Begge fødder skal være på jorden.\n \n\u2022 Benene må ikke krydses. \n \n\u2022 Du må ikke tale. \n \n";
            NewMeasurementButton.Visibility = Visibility.Hidden;
            NewMeasurement2Button.Visibility = Visibility.Visible;
            NewMeasurement2Button.IsEnabled = false;
            count++;
        }

        private void SupportButton_Click(object sender, RoutedEventArgs e)
        {
            PopupWindow popup = new PopupWindow();
            popup.Show();
        }

        
    }
}
