using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace osu_AdvancedDownloader
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CultureInfo ci = CultureInfo.InstalledUICulture;
            if (ci.Name == "ru-RU")
            {
                Main.Title = "osu! установщик";
                BeatmapDownloadButton.Content = "СКАЧАТЬ";
                BeatmapDownloadOpenLastBMButton.Content = "ОТКРЫТЬ ПОСЛЕДНЮЮ КАРТУ";
                BeatmapDownloadAndOpenButton.Content = "СКАЧАТЬ И ОТКРЫТЬ";
            }

            BeatmapBoxLink.TextChanged += BeatmapBoxLinkChanged;
        }

        Ping ping = new Ping();

        private string last_beatmap_filename;

        private int potential_beatmap_id;

        private List<string> beatmaps = new List<string>();

        private void BeatmapBoxLinkChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string theText = textBox.Text;
                if (theText.StartsWith("https://osu.ppy.sh/beatmapsets/")
                    ||
                    theText.StartsWith("https://osu.ppy.sh/beatmaps/")
                    ||
                    theText.StartsWith("https://osu.ppy.sh/b/")

                    ||

                    theText.StartsWith("osu.ppy.sh/beatmapsets/")
                    ||
                    theText.StartsWith("osu.ppy.sh/beatmaps/")
                    ||
                    theText.StartsWith("osu.ppy.sh/b/")

                    ||

                    theText.StartsWith("http://osu.ppy.sh/beatmapsets/")
                    ||
                    theText.StartsWith("http://osu.ppy.sh/beatmaps/")
                    ||
                    theText.StartsWith("http://osu.ppy.sh/b/")
                    )
                {
                    string[] text = theText.Split('/');
                    string beatmap_id = text[text.Length - 1];
                    bool isBeatmapFind = false;
                    string beatmapFounded = string.Empty;
                    BeatmapInformationText.Foreground = Brushes.White;
                    foreach (var item in beatmaps)
                    {
                        if (item.Split(';')[0] == beatmap_id)
                        {
                            isBeatmapFind = true;
                            beatmapFounded = item;
                        }
                    }
                    if (isBeatmapFind)
                    {
                        // OUTPUT: 3457998;m1v - you [insane] (i love ganyu) | 4,18? | 174BPM | 01:18;1685799;3457998 m1v - you.osz
                        string[] str = beatmapFounded.Split(';');
                        BeatmapInformationText.Text = str[1];
                        potential_beatmap_id = int.Parse(str[2]);
                        last_beatmap_filename = str[3];
                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.UriSource = new Uri($"https://b.ppy.sh/thumb/{potential_beatmap_id}l.jpg");
                        bitmapImage.EndInit();
                        BeatmapBackground.Source = bitmapImage;
                    }
                    else
                    {
                        JObject get_beatmap = Parsers.GetJSONFromAPI($"https://api.chimu.moe/v1/map/{beatmap_id}");
                        if (get_beatmap == null)
                        {
                            BeatmapInformationText.Text = "Error: beatmap on mirror dont found.";
                            BeatmapInformationText.Foreground = Brushes.Red;
                            BeatmapBackground.Source = null;
                        }
                        else
                        {
                            JToken beatmapset_id = get_beatmap["ParentSetId"];
                            JObject get_beatmapset = Parsers.GetJSONFromAPI($"https://api.chimu.moe/v1/set/{beatmapset_id}");

                            JToken length = get_beatmap["TotalLength"];
                            JToken difficulty = get_beatmap["DiffName"];
                            JToken starrating = get_beatmap["DifficultyRating"];
                            JToken bpm = get_beatmap["BPM"];

                            decimal normal_sr = Math.Round(decimal.Parse(starrating.ToString()), 2);
                            decimal normal_bpm = Math.Round(decimal.Parse(bpm.ToString()));

                            TimeSpan time = TimeSpan.FromSeconds(double.Parse(length.ToString()));

                            string normal_time;

                            if (time.TotalHours < 1)
                            {
                                normal_time = time.ToString(@"mm\:ss");
                            }
                            else
                            {
                                normal_time = time.ToString(@"hh\:mm\:ss");
                            }

                            JToken artist = get_beatmapset["Artist"];
                            JToken title = get_beatmapset["Title"];
                            JToken mapper = get_beatmapset["Creator"];

                            string ar = get_beatmap["AR"].ToString().Replace(",", ".");
                            string hp = get_beatmap["HP"].ToString().Replace(",", ".");
                            string cs = get_beatmap["CS"].ToString().Replace(",", ".");
                            string od = get_beatmap["OD"].ToString().Replace(",", ".");

                            string formated_text = $"{artist} - {title} [{difficulty}] ({mapper}) | {normal_sr}★ | {normal_bpm}BPM | {normal_time} | AR: {ar}, HP: {hp}, OD: {od}, CS: {cs}";

                            BeatmapInformationText.Text = formated_text;

                            potential_beatmap_id = int.Parse(beatmapset_id.ToString());

                            var bitmapImage = new BitmapImage();
                            bitmapImage.BeginInit();
                            bitmapImage.UriSource = new Uri($"https://b.ppy.sh/thumb/{potential_beatmap_id}l.jpg");
                            bitmapImage.EndInit();
                            BeatmapBackground.Source = bitmapImage;

                            last_beatmap_filename = $"{get_beatmap["BeatmapId"]} {artist} - {title}.osz";

                            beatmaps.Add($"{beatmap_id};{formated_text};{potential_beatmap_id};{last_beatmap_filename}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Not osu! beatmap link");
                }
            }
        }

        public void DownloadBeatmap(Uri download_url, bool openAfterComplete)
        {
            using (var client = new WebClient())
            {
                client.DownloadProgressChanged += Client_DownloadProgressChanged;
                if (openAfterComplete)
                {
                    client.DownloadFileCompleted += Client_DownloadFileCompletedWithOpen;
                }
                else
                {
                    client.DownloadFileCompleted += Client_DownloadFileCompleted;
                }
                client.DownloadFileAsync(download_url, last_beatmap_filename);
            }
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (new FileInfo(last_beatmap_filename).Length > 0)
            {
                BeatmapDownloadOpenLastBMButton.IsEnabled = true;
                BeatmapDownloadOpenLastBMButton.Effect = null;
            }
            if (new FileInfo(last_beatmap_filename).Length == 0)
            {
                File.Delete(last_beatmap_filename);
            }
        }

        private void Client_DownloadFileCompletedWithOpen(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (new FileInfo(last_beatmap_filename).Length > 0)
            {
                Process.Start(last_beatmap_filename);
                BeatmapBoxLink.Text = string.Empty;
                BeatmapInformationText.Text = string.Empty;
                BeatmapBackground.Source = null;
                BeatmapDownloadOpenLastBMButton.IsEnabled = true;
                BeatmapDownloadOpenLastBMButton.Effect = null;
                BeatmapProgressBar.Value = 0;
                BeatmapProgressBar.IsIndeterminate = false;
            }
            if (new FileInfo(last_beatmap_filename).Length == 0)
            {
                File.Delete(last_beatmap_filename);
            }
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            BeatmapProgressBar.Value = e.ProgressPercentage;
        }

        private void BeatmapDownloadAndOpenButton_Click(object sender, RoutedEventArgs e)
        {
            PingReply chimuPing = ping.Send("chimu.moe");
            PingReply kitsuPing = ping.Send("kitsu.moe");

            long winer_ms = 0;
            string winer = "";

            if (chimuPing.RoundtripTime > kitsuPing.RoundtripTime)
            {
                Uri uri = new Uri("https://kitsu.moe/api/d/" + potential_beatmap_id);
                DownloadBeatmap(uri, true);
                winer_ms = kitsuPing.RoundtripTime;
                winer = "Kitsu";
            }
            else if (chimuPing.RoundtripTime < kitsuPing.RoundtripTime)
            {
                Uri uri = new Uri("https://api.chimu.moe/v1/download/" + potential_beatmap_id);
                DownloadBeatmap(uri, true);
                winer_ms = chimuPing.RoundtripTime;
                winer = "Chimu";
            }
            else if (chimuPing.RoundtripTime == kitsuPing.RoundtripTime)
            {
                Random r = new Random();
                int rInt = r.Next(1, 2);
                if (rInt == 1)
                {
                    Uri uri = new Uri("https://kitsu.moe/api/d/" + potential_beatmap_id);
                    DownloadBeatmap(uri, true);
                    winer_ms = kitsuPing.RoundtripTime;
                    winer = "Kitsu";
                }
                else
                {
                    Uri uri = new Uri("https://api.chimu.moe/v1/download/" + potential_beatmap_id);
                    DownloadBeatmap(uri, true);
                    winer_ms = chimuPing.RoundtripTime;
                    winer = "Chimu";
                }
            }

            if (winer == "Chimu")
            {
                BeatmapProgressBar.IsIndeterminate = true;
            }
            else
            {
                BeatmapProgressBar.IsIndeterminate = false;
            }
            Console.WriteLine($"Server choosen: {winer} ({winer_ms}ms)");
        }

        private void BeatmapDownloadOpenLastBMButton_Click(object sender, RoutedEventArgs e)
        {
            if (last_beatmap_filename.Length != 0 && new FileInfo(last_beatmap_filename).Length > 0)
            {
                Process.Start(last_beatmap_filename);
                BeatmapBoxLink.Text = string.Empty;
                BeatmapInformationText.Text = string.Empty;
                BeatmapBackground.Source = null;
                BeatmapProgressBar.Value = 0;

                BeatmapDownloadOpenLastBMButton.IsEnabled = false;
                BeatmapDownloadOpenLastBMButton.Effect = new System.Windows.Media.Effects.BlurEffect() { };

                last_beatmap_filename = string.Empty;
            }
            else if (last_beatmap_filename.Length != 0 && new FileInfo(last_beatmap_filename).Length == 0)
            {
                File.Delete(last_beatmap_filename);
            }
            else
            {
                BeatmapDownloadOpenLastBMButton.IsEnabled = false;
                BeatmapDownloadOpenLastBMButton.Effect = new System.Windows.Media.Effects.BlurEffect() { };
            }
        }

        private void BeatmapDownloadButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("https://kitsu.moe/api/d/" + potential_beatmap_id);
            DownloadBeatmap(uri, false);
        }
    }
}
