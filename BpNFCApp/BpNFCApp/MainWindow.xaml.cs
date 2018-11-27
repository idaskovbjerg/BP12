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

        private void FileCreated(object sender, FileSystemEventArgs e)
        {
            Thread.Sleep(500);
            FileInfo file = new FileInfo(e.FullPath);
            var diaAverage = 0;
            var sysAverage = 0;
            if (file.Name.Contains("bpmonitor"))
            {
                for (int i = 0; i < 1; i++)
                {
                    var dia = MeasurementService.ReadFile(e.FullPath)[i].Diastolic;
                    var sys = MeasurementService.ReadFile(e.FullPath)[i].Systolic;

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
                                    diaAverage = dia + dia1 + dia2;

                                }

                                if (dia - k == dia2)
                                { 
                                    diaAverage = dia + dia1 + dia2;
                                }
                            } 
                        }

                        if (dia - j == dia1)
                        {
                            for (int k = 0; k <= 6; k++)
                            {
                                if (dia + k == dia2)
                                {
                                    diaAverage = dia + dia1 + dia2;
                                }

                                if (dia - k == dia2 )
                                {
                                    diaAverage = dia + dia1 + dia2;
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
                                    sysAverage = sys + sys1 + sys2;
                                }

                                if (sys - j == sys2)
                                {
                                    sysAverage = sys + sys1 + sys2;
                                }
                            }
                        }

                        if (sys - k == sys1)
                        {
                            for (int j = 0; j <= 10; j++)
                            {
                                if (sys + j == sys2)
                                {
                                    sysAverage = sys + sys1 + sys2;
                                }

                                if (sys - j == sys2)
                                {
                                    sysAverage = sys + sys1 + sys2;
                                }
                            }
                        }
                    }
                }
                Dispatcher.BeginInvoke(new Action(() =>
                    {
                        BloodPressureLabel.Content =
                            Math.Round(sysAverage / 3.0) + "/" + Math.Round(diaAverage / 3.0) + " mmHg";
                    }));
            }
            
        }
    }
}
