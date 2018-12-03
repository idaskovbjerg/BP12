using System;
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

            ResultLabel.Content = "Hold overførelses staven henover logoet på blodtryksmåleren. \n \nSe billedet";
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
            Thread.Sleep(500);
            FileInfo file = new FileInfo(e.FullPath);
            
            if (file.Name.Contains("bpmonitor"))
            {
                for (int i = 0; i < 1; i++)
                {
                    var dia = MeasurementService.ReadFile(e.FullPath)[i].Diastolic;
                    var sys = MeasurementService.ReadFile(e.FullPath)[i].Systolic;
                    puls = (MeasurementService.ReadFile(e.FullPath)[0].Pulse +
                            MeasurementService.ReadFile(e.FullPath)[1].Pulse +
                            MeasurementService.ReadFile(e.FullPath)[2].Pulse) / 3;

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

                                }
                            }
                        }
                        else
                        {
                            this.Close();
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
                                    sysAverage = (sys + sys1 + sys2)/3.0;
                                }
                            }
                        }
                        else
                        {
                            this.Close();
                        }
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
                    OkCheck.Visibility = Visibility.Visible;
                    MessageLabel.Content = "Du skal gå informationsskranken og hente en blodtrykmåler du tager med hjem.\n \nDu får en indkaldelse til nyt check af dit blodtryk i den E-boks. ";
                    if (OkCheckBox.IsChecked == true)
                    {
                        LogoutButton.IsEnabled = true;
                    }
                }

                if (sysAverage < 140 && sysAverage > 120)
                {
                    MessageLabel.Content = "Du modtager en indkaldelse til et nyt checkup i din E-boks";
                    OkCheck.Visibility = Visibility.Visible;
                    severity = "YELLOW";
                    if (OkCheckBox.IsChecked == true)
                    {
                        MessageLabel.Content = "Du kan nu logge af systemet";
                        LogoutButton.IsEnabled = true;
                    }
                }
                else
                {
                    if (diaAverage > 90 && diaAverage <= 100)
                    {
                        MessageLabel.Content = "Du modtager en indkaldelse til et nyt checkup i din E-boks";
                        OkCheck.Visibility = Visibility.Visible;
                        severity = "YELLOW";
                        if (OkCheckBox.IsChecked == true)
                        {
                            MessageLabel.Content = "Du kan nu logge af systemet";
                            LogoutButton.IsEnabled = true;
                        }
                    }

                    if (diaAverage > 100)
                    {
                        severity = "RED";
                        OkCheck.Visibility = Visibility.Visible;
                        MessageLabel.Content = "Du skal gå informationsskranken og hente en blodtrykmåler du tager med hjem.\n \nDu får en indkaldelse til nyt check af dit blodtryk i den E-boks. ";
                        if (OkCheckBox.IsChecked == true)
                        {
                            LogoutButton.IsEnabled = true;
                        }
                    }
                    else
                    {
                        severity = "GREEN";
                        MessageLabel.Content = "Du kan nu logge af systemet";
                    }

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
            this.Close();
            
            Login login = new Login();
            login.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Test();
        }
    }
}
