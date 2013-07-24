using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using HtmlAgilityPack;
using System.Windows.Media.Imaging;

namespace TransitMTL
{
    public partial class BusPage : PhoneApplicationPage
    {
        private static string titleText;
        private static List<string> panoramaTitles;

        private List<Stop> firstStopList;
        private List<Stop> secondStopList;

        private int counter1;
        private int counter2;

        public BusPage()
        {
            InitializeComponent();
            //header.Header = titleText;
            //string title = BusSchedulePano.Title.ToString();
            BusSchedulePano.Title = titleText + " " + AppResources.StopListingTitle;

            Visibility darkBackgroundVisibility = (Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"];

            if (darkBackgroundVisibility != Visibility.Visible)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("PanoramaBackground2.png", UriKind.Relative));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;


                BusSchedulePano.Background = imageBrush;
            }

            foreach (string s in panoramaTitles)
            {
                createPanaramaItem(AppResources.Loading);
            }

            firstStopList = new List<Stop>();
            secondStopList = new List<Stop>();

            getStopListings();

        }

        private void createPanaramaItem(string title)
        {
            PanoramaItem pI = new PanoramaItem();
            pI.Header = title;
            BusSchedulePano.Items.Add(pI);
        }

        private void getStopListings()
        {
            string link = "http://www.stm.info/English/bus/GEOMET/a-GEO";
            link += titleText + ".htm";
            HtmlWeb.LoadAsync(link, GetLineStops);
        }

        private void GetLineStops(object sender, HtmlDocumentLoadCompleted args)
        {
            if (args.Document == null)
            {
                MessageBox.Show(AppResources.ThisAppRequiresInternetAccess, AppResources.Error, MessageBoxButton.OK);
            }
            else
            {
                var nodes = args.Document.DocumentNode.SelectNodes("//table");
                HtmlNodeCollection stopTable = new HtmlNodeCollection(null);
                stopTable = nodes[2].ChildNodes;

                for (int i = 0; i < nodes[2].ChildNodes.Count; i++)
                {
                    if (nodes[2].ChildNodes[i].ChildNodes.Count > 0)
                    {
                        int output;
                        if (int.TryParse(nodes[2].ChildNodes[i].ChildNodes[7].InnerText.Replace("\n", "").Replace("\r", ""), out output))
                        {
                            Stop temp = new Stop(nodes[2].ChildNodes[i].ChildNodes[7].InnerText.Replace("\n", "").Replace("\r", ""), (nodes[2].ChildNodes[i].ChildNodes[3].InnerText.Replace("\n", "").Replace("\r", "") + " / " + nodes[2].ChildNodes[i].ChildNodes[5].InnerText.Replace("\n", "").Replace("\r", "")).Replace("&Icirc;", "Î").Replace("&eacute;", "é").Replace("&Eacute;", "É").Replace("&egrave;", "è").Replace("&Egrave;", "È").Replace("&ocirc;", "ô").Replace("&Ocirc;", "Ô").Replace("&aacute;", "á").Replace("&Aacute;", "Á").Replace("&agrave;", "à").Replace("&Agrave;", "À").Replace("&acirc;", "â").Replace("&Acirc;", "Â").Replace("&ccedil;", "ç").Replace("&ccedil;", "Ç"), titleText);
                            firstStopList.Add(temp);
                            //System.Diagnostics.Debug.WriteLine(nodes[2].ChildNodes[i].ChildNodes[7].InnerText.Replace("\n", "").Replace("\r", ""));
                        }
                    }
                }

                for (int i = 0; i < nodes[4].ChildNodes.Count; i++)
                {
                    if (nodes[4].ChildNodes[i].ChildNodes.Count > 0)
                    {
                        int output;
                        if (int.TryParse(nodes[4].ChildNodes[i].ChildNodes[7].InnerText.Replace("\n", "").Replace("\r", ""), out output))
                        {
                            Stop temp = new Stop(nodes[4].ChildNodes[i].ChildNodes[7].InnerText.Replace("\n", "").Replace("\r", ""), (nodes[4].ChildNodes[i].ChildNodes[3].InnerText.Replace("\n", "").Replace("\r", "") + " / " + nodes[4].ChildNodes[i].ChildNodes[5].InnerText.Replace("\n", "").Replace("\r", "")).Replace("&Icirc;", "Î").Replace("&eacute;", "é").Replace("&Eacute;", "É").Replace("&egrave;", "è").Replace("&Egrave;", "È").Replace("&ocirc;", "ô").Replace("&Ocirc;", "Ô").Replace("&aacute;", "á").Replace("&Aacute;", "Á").Replace("&agrave;", "à").Replace("&Agrave;", "À").Replace("&acirc;", "â").Replace("&Acirc;", "Â").Replace("&ccedil;", "ç").Replace("&ccedil;", "Ç"), titleText);
                            secondStopList.Add(temp);
                            //System.Diagnostics.Debug.WriteLine(nodes[4].ChildNodes[i].ChildNodes[7].InnerText.Replace("\n", "").Replace("\r", ""));
                        }
                    }
                }
                counter1 = 0;
                displayLineStops();
                //setTimes1();
            }
        }

        private void displayLineStops()
        {
            PanoramaItem pI = (PanoramaItem)BusSchedulePano.Items[0];
            pI.Header = panoramaTitles[0];

            pI = (PanoramaItem)BusSchedulePano.Items[1];
            pI.Header = panoramaTitles[1];

            busDisplayHelper(firstStopList, 0);
            busDisplayHelper(secondStopList, 1);
        }

        private void busDisplayHelper(List<Stop> input, int panoIndex)
        {
            ListBox contentContainer = new ListBox();

            foreach (Stop s in input)
            {

                Grid LineContainer = new Grid();

                TextBlock stopNumber = new TextBlock();
                stopNumber.Height = 70;
                stopNumber.HorizontalAlignment = HorizontalAlignment.Left;
                stopNumber.VerticalAlignment = VerticalAlignment.Top;
                stopNumber.Width = 135;
                stopNumber.FontWeight = FontWeights.Normal;
                stopNumber.FontSize = 48;
                stopNumber.Margin = new Thickness(13, 0, 0, 0);
                stopNumber.Text = s.getStopNumber();
                stopNumber.Style = (Style)Application.Current.Resources["PhoneTextSubtleStyle"];

                TextBlock corner = new TextBlock();
                corner.HorizontalAlignment = HorizontalAlignment.Left;
                corner.Margin = new Thickness(145, 10, 0, 0);
                corner.VerticalAlignment = VerticalAlignment.Top;
                corner.TextWrapping = TextWrapping.Wrap;
                corner.Text = AppResources.Corner + ": " + s.getCorner();
                corner.Style = (Style)Application.Current.Resources["PhoneTextNormalStyle"];

                TextBlock timeslabel = new TextBlock();
                timeslabel.Text = AppResources.NextPassingTimes;
                timeslabel.Height = 30;
                timeslabel.HorizontalAlignment = HorizontalAlignment.Left;
                timeslabel.VerticalAlignment = VerticalAlignment.Top;
                timeslabel.Margin = new Thickness(12, 60, 0, 0);

                //TextBlock lineTimes = new TextBlock();
                //List<string> times = s.getTimes();
                //if (s.getTimes().Count == 0)
                //{
                //    lineTimes.Text = "Loading...";
                //}
                //else
                //{
                //    foreach (string time in times)
                //    {
                //        lineTimes.Text += time;
                //    }
                //}
                //lineTimes.HorizontalAlignment = HorizontalAlignment.Left;
                //lineTimes.VerticalAlignment = VerticalAlignment.Top;
                //lineTimes.TextWrapping = TextWrapping.Wrap;
                //lineTimes.Margin = new Thickness(12, 80, 0, 0);

                LineContainer.Children.Add(stopNumber);
                LineContainer.Children.Add(corner);
                //LineContainer.Children.Add(timeslabel);
                //LineContainer.Children.Add(lineTimes);
                LineContainer.Tap += LineListTap_Click;

                contentContainer.Items.Add(LineContainer);
            }

            PanoramaItem pITemp = (PanoramaItem)BusSchedulePano.Items[panoIndex];
            pITemp.Content = contentContainer;
        }

        private void LineListTap_Click(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Grid busContainer = (Grid)sender;
            TextBlock stopNumberTextbox = (TextBlock)busContainer.Children[0];
            //System.Diagnostics.Debug.WriteLine("Test: " + test2.Text);
            //BusPage.setTitle(lineNumberTextbox.Text);
            //getBusDirections(lineNumberTextbox.Text);

            Times.setTitle(AppResources.StopNumber + ": " + stopNumberTextbox.Text + ", " + AppResources.Line + ": " + titleText);
            Times.setStopNumber(stopNumberTextbox.Text);
            Times.setBusNumber(titleText);

            NavigationService.Navigate(new Uri("/Times.xaml", UriKind.Relative));
        }

        public static void setPanoramaTitles(List<string> input)    
        {
            panoramaTitles = input;
        }

        public static void setTitle(string input)
        {
            titleText = input;
        }
    }
}