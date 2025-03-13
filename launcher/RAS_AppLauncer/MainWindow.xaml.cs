using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Windows;

namespace RAS_AppLauncer;

enum LauncherStatus
{
    ready,
    failed,
    downloadingApp,
    downloadingUpdate
}

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private string rootPath = Directory.GetCurrentDirectory();
    private string versionFile;
    private string appZip;
    private string appExe;

    private LauncherStatus _status;
    internal LauncherStatus Status
    {
        get => _status;
        set
        {
            _status = value;
            switch (_status)
            {
                case LauncherStatus.ready:
                    StartButton.Content = "Start";
                    break;
                case LauncherStatus.failed:
                    StartButton.Content = "Update Failed - Retry";
                    break;
                case LauncherStatus.downloadingApp:
                    StartButton.Content = "Downloading App";
                    break;
                case LauncherStatus.downloadingUpdate:
                    StartButton.Content = "Downloading Update";
                    break;
                default:
                    break;
            }
        }
    }

    public MainWindow()
    {
        InitializeComponent();
        if (rootPath == Directory.GetCurrentDirectory())
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.InitialDirectory = Directory.GetCurrentDirectory();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
                rootPath = dialog.SelectedPath;
            else
                rootPath = Directory.GetCurrentDirectory();
        }
        
        versionFile = Path.Combine(rootPath, "version.txt");
        appZip = Path.Combine(rootPath, "brs.zip");
        appExe = Path.Combine(rootPath, "brs", "Robotic Arm Simulator.exe");
    }

    private void CheckForUpdates()
    {
        if (File.Exists(versionFile))
        {
            Version localVersion = new Version(File.ReadAllText(versionFile));
            VersionText.Text = localVersion.ToString();

            try
            {
                WebClient webClient = new WebClient();
                Version onlineVersion = new Version(webClient.DownloadString("https://www.dropbox.com/scl/fi/ebnh3ghmzpfodn99wgqcl/version.txt?rlkey=cwlgixkpz5pt4tf6fm1ye39vb&st=dhk1xx3z&dl=1"));

                if (onlineVersion.IsDifferentThan(localVersion))
                {
                    InstallAppFiles(true, onlineVersion);
                }
                else
                {
                    Status = LauncherStatus.ready;
                }
            }
            catch (Exception ex)
            {
                Status = LauncherStatus.failed;
                System.Windows.MessageBox.Show($"Error checking for app updates: {ex}");
            }
        }
        else
        {
            InstallAppFiles(false, Version.zero);
        }
    }

    private void InstallAppFiles(bool _isUpdate, Version _onlineVersion)
    {
        try
        {
            WebClient webClient = new WebClient();
            if(_isUpdate)
            {
                Status = LauncherStatus.downloadingUpdate;
            }
            else
            {
                Status = LauncherStatus.downloadingApp;
                _onlineVersion = new Version(webClient.DownloadString("https://www.dropbox.com/scl/fi/ebnh3ghmzpfodn99wgqcl/version.txt?rlkey=cwlgixkpz5pt4tf6fm1ye39vb&st=dhk1xx3z&dl=1"));
            }

            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadAppCompletedCallback);
            webClient.DownloadFileAsync(new Uri("https://www.dropbox.com/scl/fi/6nbjatt0qot9nwizbmglt/brs.zip?rlkey=bzam1b0qhg51hxfi0bvnuwk3w&st=vc6cxkt0&dl=1"), appZip, _onlineVersion);
        }
        catch (Exception ex)
        {
            Status = LauncherStatus.failed;
            System.Windows.MessageBox.Show($"Error installing app files: {ex}");
        }
    }

    private void DownloadAppCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        try
        {
            string onlineVersion = ((Version)e.UserState).ToString();
            ZipFile.ExtractToDirectory(appZip, rootPath, true);
            File.Delete(appZip);

            File.WriteAllText(versionFile, onlineVersion);

            VersionText.Text = onlineVersion;
            Status = LauncherStatus.ready;
        }
        catch (Exception ex)
        {
            Status = LauncherStatus.failed;
            System.Windows.MessageBox.Show($"Error finishing download: {ex}");
        }
    }

    private void Window_ContentRendered(object sender, EventArgs e)
    {
        CheckForUpdates();
    }

    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        if(File.Exists(appExe) && Status == LauncherStatus.ready)
        {
            ProcessStartInfo startinfo = new ProcessStartInfo(appExe);
            startinfo.WorkingDirectory = Path.Combine(rootPath, "brs");
            Process.Start(startinfo);

            Close();
        }
        else if(Status == LauncherStatus.failed)
        {
            CheckForUpdates();
        }
    }

    struct Version
    {
        internal static Version zero = new Version(0, 0, 0, 0);

        private short major;
        private short minor;
        private short subMinor;
        private short bugFix;

        internal Version(short _major, short _minor, short _subMinor, short _bugFix)
        {
            major = _major;
            minor = _minor;
            subMinor = _subMinor;
            bugFix = _bugFix;
        }

        internal Version(string _version)
        {
            string[] _versionStrings = _version.Split('.');
            if(_versionStrings.Length != 4)
            {
                major = 0;
                minor = 0;
                subMinor = 0;
                bugFix = 0;
                return;
            }

            major = short.Parse(_versionStrings[0]);
            minor = short.Parse(_versionStrings[1]);
            subMinor = short.Parse(_versionStrings[2]);
            bugFix = short.Parse(_versionStrings[3]);
        }

        internal bool IsDifferentThan(Version _otherVersion)
        {
            if(major != _otherVersion.major)
            {
                return true;
            }
            else
            {
                if(minor != _otherVersion.minor)
                {
                    return true;
                }
                else
                {
                    if(subMinor != _otherVersion.subMinor)
                    {
                        return true;
                    }
                    else
                    {
                        if(bugFix != _otherVersion.bugFix)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public override string ToString()
        {
            return $"{major}.{minor}.{subMinor}.{bugFix}";
        }
    }
}