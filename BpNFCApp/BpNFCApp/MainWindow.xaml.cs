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
        public MainWindow()
        {
            InitializeComponent();

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
                        if (dia + j == dia1)
                        {
                            for (int k = 0; k <= 6; k++)
                            {
                                if (dia + k == dia2)
                                {
                                    diaAverage = (dia + dia1 + dia2)/3.0;

                                }

                                if (dia - k == dia2)
                                { 
                                    diaAverage = (dia + dia1 + dia2)/3.0;
                                }
                            } 
                        }

                        if (dia - j == dia1)
                        {
                            for (int k = 0; k <= 6; k++)
                            {
                                if (dia + k == dia2)
                                {
                                    diaAverage = (dia + dia1 + dia2)/3.0;
                                }

                                if (dia - k == dia2 )
                                {
                                    diaAverage = (dia + dia1 + dia2)/3.0;
                                }
                            }
                        }
                    }

                    for (int k = 0; k <= 10; k++)
                    {
                        var sys1 = MeasurementService.ReadFile(e.FullPath)[i + 1].Systolic;
                        var sys2 = MeasurementService.ReadFile(e.FullPath)[i + 2].Systolic;
                        if (sys + k == sys1 )
                        {
                            for (int j = 0; j <= 10; j++)
                            {
                                if (sys + j == sys2)
                                {
                                    sysAverage = (sys + sys1 + sys2)/3.0;
                                }

                                if (sys - j == sys2)
                                {
                                    sysAverage = (sys + sys1 + sys2)/3.0;
                                }
                            }
                        }

                        if (sys - k == sys1)
                        {
                            for (int j = 0; j <= 10; j++)
                            {
                                if (sys + j == sys2)
                                {
                                    sysAverage = (sys + sys1 + sys2)/3.0;
                                }

                                if (sys - j == sys2)
                                {
                                    sysAverage = (sys + sys1 + sys2)/3.0;
                                }
                            }
                        }
                    }
                }
                Dispatcher.BeginInvoke(new Action(() =>
                    {
                        BloodPressureLabel.Content =
                            Math.Round(sysAverage) + "/" + Math.Round(diaAverage) + " mmHg";
                    }));
                if (sysAverage >= 140)
                {
                    severity = "RED";
                }

                if (sysAverage < 140 && sysAverage > 120)
                {
                    severity = "YELLOW";
                }
                else
                {
                    if (diaAverage > 90 && diaAverage <= 100)
                    {
                        severity = "YELLOW";
                    }

                    if (diaAverage > 100)
                    {
                        severity = "RED";
                    }
                    else
                    {
                        severity = "GREEN";
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
        }
    }
}
