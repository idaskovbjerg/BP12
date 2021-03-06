﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenTeleNet.API;

namespace BpNFCApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string str1 = "";
        private string str2 = "";
        private string str3 = "";
        void Test()
        {
            string[] tokens;
            char[] separators = { ',' };
            string str = "";

            FileStream fs = new FileStream(@"C:/Users/ida_s/Documents/BP12/BpNFCApp/BpNFCApp/bin/Debug/NFCCommunicator/Measurements/bpmonitor.csv", FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);

            str1 = sr.ReadLine();
            str2 = sr.ReadLine();
            str3 = sr.ReadLine();
            string[] words1 = str1.Split(separators,System.StringSplitOptions.RemoveEmptyEntries);
            string[] words2 = str2.Split(separators,System.StringSplitOptions.RemoveEmptyEntries);
            string[] words3 = str3.Split(separators,System.StringSplitOptions.RemoveEmptyEntries);
            string sys1 = words1[6];
            string sys2 = words2[6];
            string sys3 = words3[6];
            if (sys1 == sys2 || sys1 + 1 == sys2 || sys1 + 2 == sys2)
            {
                string godkent = "";
            }
            else
            {
                string ikkeGodkendt = "der skal foretages endnu en måling";
                Guide guide = new Guide();
                this.Close();
            }
        }
        public MainWindow()
        {

            InitializeComponent();

            ResultLabel.Content = "Læg NFC-pinden ovenpå logoet, som er vist herunder, \npå blodtryksmåleren i et par sekunder.\n\nDen kan IKKE vende forkert.\n\n\n \n \nSe billedet";
            ResultImage.Source = new BitmapImage(new Uri("Images/position6.jpg", UriKind.Relative));

            ProcessStartInfo start = new ProcessStartInfo();

            start.FileName = "NFC-communication.exe";
            start.WorkingDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "NFCCommunicator";

            start.WindowStyle = ProcessWindowStyle.Hidden;

            start.CreateNoWindow = true;

            Process.Start(start);


            FileSystemWatcher myWatcher = new System.IO.FileSystemWatcher();
            myWatcher.Path = System.AppDomain.CurrentDomain.BaseDirectory + "NFCCommunicator\\Measurements";
            myWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.LastAccess | NotifyFilters.FileName;
            myWatcher.Created += new FileSystemEventHandler(FileCreated);

            myWatcher.Filter = "*.csv";
            myWatcher.EnableRaisingEvents = true;
            
        }
        private double diaAverage = 0;
        private double sysAverage = 0;
        private double puls = 0;
        private string severity = "";
        private void FileCreated(object sender, FileSystemEventArgs e)
        {

            Dispatcher.BeginInvoke(new Action(() => { NFClogoImage.Visibility = Visibility.Hidden; }));

            Thread.Sleep(500);
            FileInfo file = new FileInfo(e.FullPath);
            var counter = 0;
            if (file.Name.Contains("bpmonitor"))
            {
                try
                {

                    foreach (var VARIABLE in MeasurementService.ReadFile(e.FullPath))
                    {
                        counter++;
                        if (DateTime.Today.Year == VARIABLE.Year)
                        {
                            if (DateTime.Today.Month == VARIABLE.Month)
                            {
                                if (DateTime.Today.Day == VARIABLE.Day)
                                {
                                    break;
                                }
                            }
                        }
                    }

                    for (int i = counter - 1; i < counter; i++)
                    {
                        var dia = MeasurementService.ReadFile(e.FullPath)[i].Diastolic;
                        var sys = MeasurementService.ReadFile(e.FullPath)[i].Systolic;
                        puls = Math.Round((MeasurementService.ReadFile(e.FullPath)[i].Pulse +
                                           MeasurementService.ReadFile(e.FullPath)[i + 1].Pulse +
                                           MeasurementService.ReadFile(e.FullPath)[i + 2].Pulse) / 3.0);

                        for (int j = 0; j <= 6; j++)
                        {
                            var dia1 = MeasurementService.ReadFile(e.FullPath)[i + 1].Diastolic;
                            var dia2 = MeasurementService.ReadFile(e.FullPath)[i + 2].Diastolic;
                            if (dia + j == dia1 || dia - j == dia1)
                            {
                                for (int k = 0; k <= 6; k++)
                                {
                                    if (dia + k == dia2 || dia - k == dia2)
                                    {
                                        diaAverage = (dia + dia1 + dia2) / 3.0;
                                        break;
                                    }
                                }
                            }
                        }

                        for (int k = 0; k <= 10; k++)
                        {
                            var sys1 = MeasurementService.ReadFile(e.FullPath)[i + 1].Systolic;
                            var sys2 = MeasurementService.ReadFile(e.FullPath)[i + 2].Systolic;
                            if (sys + k == sys1 || sys - k == sys1)
                            {
                                for (int j = 0; j <= 10; j++)
                                {
                                    if (sys + j == sys2 || sys - j == sys2)
                                    {
                                        sysAverage = (sys + sys1 + sys2) / 3.0;
                                        break;
                                    }
                                }
                            }
                            
                        }

                        if (sysAverage == 0 || diaAverage == 0)
                        {
                            Dispatcher.BeginInvoke(new Action(() => { this.Close(); }));
                        }
                    }

                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        ResultLabel.Content = "Dit målte blodtryk:";
                        BloodPressureLabel.Content =
                            Math.Round(sysAverage) + "/" + Math.Round(diaAverage) + " mmHg";
                    }));
                    if (sysAverage >= 140)
                    {
                        severity = "RED";
                        Dispatcher.BeginInvoke(new Action(() =>
                        {
                            OkCheck.Visibility = Visibility.Visible;
                            MessageLabel.Content =
                                "Blodtryksmålingen er nu færdig.\n\nDu kan nu tage manchetten af og lægge denne \npå bordet\n\nDu skal nu gå til informationsskranken og hente en blodtrykmåler du tager med dig hjem.\n \nDu får en indkaldelse til nyt check af dit blodtryk direkte i din E-boks. ";
                            if (OkCheckBox.IsChecked == true)
                            {
                                LogoutButton.IsEnabled = true;
                            }
                        }));

                    }

                    if (sysAverage < 140 && sysAverage > 120)
                    {
                        severity = "YELLOW";

                        Dispatcher.BeginInvoke(new Action(() =>
                        {
                            BloodPressureLabel.Foreground = new SolidColorBrush(Colors.Green);

                            MessageLabel.Visibility = Visibility.Visible;
                            MessageLabel.Content =
                                "Blodtrykmålingen er nu færdig og blodtrykket er normalt.\n\nDu kan nu tage manchetten af, lægge denne \npå bordet og logge af systemet. \n\nTak for idag og hav en god dag. ";
                            //OkCheck.Visibility = Visibility.Visible;
                            LogoutButton.IsEnabled = true;
                        }));

                    }
                    else
                    {
                        if (diaAverage > 90 && diaAverage <= 100)
                        {
                            severity = "YELLOW";

                            Dispatcher.BeginInvoke(new Action(() =>
                            {
                                BloodPressureLabel.Foreground = new SolidColorBrush(Colors.Green);

                                MessageLabel.Visibility = Visibility.Visible;
                                MessageLabel.Content =
                                    "Blodtrykmålingen er nu færdig og blodtrykket er normalt. \n\nDu kan nu tage manchetten af, lægge denne \npå bordet og logge af systemet. \n\nTak for idag og hav en god dag.";
                                LogoutButton.IsEnabled = true;
                                //OkCheck.Visibility = Visibility.Visible;
                            }));

                        }

                        if (diaAverage > 100)
                        {
                            severity = "RED";

                            Dispatcher.BeginInvoke(new Action(() =>
                            {
                                OkCheck.Visibility = Visibility.Visible;
                                MessageLabel.Content =
                                    "Blodtryksmålingen er nu færdig. \n\nDu kan nu tage manchetten af og lægge denne på bordet.\n\nDu skal nu gå til informationsskranken og hente en blodtrykmåler \ndu tager med hjem.\n \nDu får en indkaldelse til nyt check af dit blodtryk direkte i din E-boks. ";
                            }));

                        }
                        else
                        {
                            severity = "GREEN";
                            Dispatcher.BeginInvoke(new Action(() =>
                            {
                                BloodPressureLabel.Foreground = new SolidColorBrush(Colors.Green);
                                MessageLabel.Visibility = Visibility.Visible;
                                MessageLabel.Content =
                                    "Blodtrykmålingen er nu færdig og blodtrykket er normalt. \n\nDu kan nu tage manchetten af, lægge denne \npå bordet og logge af systemet. \n\nTak for idag og hav en god dag.";
                                LogoutButton.IsEnabled = true;
                            }));
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
            
        }
        public string UserName
        {
            get;
            set;
        }
        public string PassWord { get; set; }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {

            

            var openTeleFacade = new OpenTeleNetWrapperFacade("http://opentele.aliviate.dk:4288/opentele-citizen-server/");
            openTeleFacade.postQuestionnaireBloodPressureMeasurement(Math.Round(sysAverage).ToString(),
                Math.Round(diaAverage).ToString(), Math.Round(puls).ToString(), severity,
                UserName, PassWord);
            Login login = new Login();
            login.Show();
            for (int intCounter = App.Current.Windows.Count - 2; intCounter >= 0; intCounter--)
            {
                App.Current.Windows[intCounter].Close();

            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Test();
        }

        private void OkCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            LogoutButton.IsEnabled = true;
            MessageLabel.Content = "Du kan nu logge af systemet. \n\nTak for i dag.";
        }

        private void SupportButton_Click(object sender, RoutedEventArgs e)
        {
            PopupWindow pop = new PopupWindow();
            pop.Show();

        }
    }
}
