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
using System.Windows.Media.Imaging;
using HtmlAgilityPack;
using System.Threading;

namespace TransitMTL
{
    public partial class Times : PhoneApplicationPage
    {
        private static string titleText;
        private static string stopNumber;
        private static string busNumber;

        static DataSaver<string> MyDataSaver = new DataSaver<string>();
        string saveFileName = "Favorites";

        public Times()
        {
            InitializeComponent();

            Visibility darkBackgroundVisibility = (Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"];

            if (darkBackgroundVisibility != Visibility.Visible)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("RegularBackground2.png", UriKind.Relative));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;


                LayoutRoot.Background = imageBrush;
            }

            PageTitle.Text = AppResources.Times;

            ApplicationTitle.Text = titleText;

            setTimes();
        }

        private void setTimes()
        {
            string template = "http://www2.stm.info/horaires/frmResult.aspx?Langue=En&Arret=";
            template += stopNumber;
            HtmlWeb.LoadAsync(template, SetStopTimes);
        }

        private void SetStopTimes(object sender, HtmlDocumentLoadCompleted args)
        {
            if (args.Document == null)
            {
                MessageBox.Show(AppResources.ThisAppRequiresInternetAccess, AppResources.Error, MessageBoxButton.OK);
            }
            else
            {
                var nodes = args.Document.DocumentNode.SelectNodes("//table");

                HtmlNodeCollection temp = new HtmlNodeCollection(null);
                foreach (HtmlNode n in nodes)
                {
                    if (n.Id == "webGrille")
                    {
                        temp = n.ChildNodes;
                    }
                }

                ListBox content = new ListBox();

                Grid LineContainer = new Grid();

                TextBlock timeslabel = new TextBlock();
                timeslabel.Text = AppResources.NextPassingTimes;
                timeslabel.HorizontalAlignment = HorizontalAlignment.Left;
                timeslabel.VerticalAlignment = VerticalAlignment.Top;
                timeslabel.Margin = new Thickness(12, 0, 0, 0);
                timeslabel.Style = (Style)Application.Current.Resources["PhoneTextLargeStyle"];

                TextBlock lineTimes = new TextBlock();
                lineTimes.HorizontalAlignment = HorizontalAlignment.Left;
                lineTimes.VerticalAlignment = VerticalAlignment.Top;
                lineTimes.TextWrapping = TextWrapping.Wrap;
                lineTimes.Margin = new Thickness(12, 40, 0, 0);
                lineTimes.Style = (Style)Application.Current.Resources["PhoneTextLargeStyle"];

                Button AddToFavs = new Button();
                AddToFavs.HorizontalAlignment = HorizontalAlignment.Left;
                AddToFavs.VerticalAlignment = VerticalAlignment.Top;
                AddToFavs.Content = AppResources.TapForFav;
                AddToFavs.Margin = new Thickness(0, 200, 0, 0);
                //AddToFavs.Style = (Style)Application.Current.Resources["PhoneTextLargeStyle"];
                AddToFavs.Tap += ConfirmAddToFavs_Click;

                List<string> tempTimeList = new List<string>();

                for (int i = 1; i < temp.Count - 1; i++)
                {
                    for (int j = 4; j < temp[i].ChildNodes.Count - 1; j++)
                    {
                        if (temp[i].ChildNodes[2].InnerText == busNumber)
                        {
                            if (temp[i].ChildNodes[j].InnerHtml != "&nbsp;")
                                if (!temp[i].ChildNodes[j].InnerHtml.Contains("href"))
                                    tempTimeList.Add(temp[i].ChildNodes[j].InnerHtml);
                                else
                                    tempTimeList.Add(temp[i].ChildNodes[j].ChildNodes[0].InnerHtml);
                        }
                    }
                }

                for (int k = 0; k < tempTimeList.Count; k++)
                {
                    string timesetting = Thread.CurrentThread.CurrentCulture.ToString();
                    if (timesetting.Contains("en-"))
                    {
                        string convertedTime;
                        string[] time = tempTimeList[k].Split('h');
                        time[0] = Convert.ToInt32(time[0]) + "";

                        if (Convert.ToInt32(time[0]) > 11)
                        {
                            if (Convert.ToInt32(time[0]) == 12)
                            {
                                convertedTime = String.Join(":", time) + " PM";
                            }
                            else
                            {
                                time[0] = (Convert.ToInt32(time[0]) - 12) + "";
                                convertedTime = String.Join(":", time) + " PM";
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(time[0]) == 0)
                                time[0] = "12";
                            convertedTime = String.Join(":", time) + " AM";
                        }
                        if (k < tempTimeList.Count - 1)
                        {
                            lineTimes.Text += convertedTime + ", ";
                        }
                        else
                        {
                            lineTimes.Text += convertedTime + "";
                        }
                    }
                    else
                    {
                        if (k < tempTimeList.Count - 1)
                        {
                            lineTimes.Text += tempTimeList[k] + ", ";
                        }
                        else
                        {
                            lineTimes.Text += tempTimeList[k] + "";
                        }
                    }
                }

                LineContainer.Children.Add(timeslabel);
                LineContainer.Children.Add(lineTimes);
                LineContainer.Children.Add(AddToFavs);

                content.Items.Add(LineContainer);

                ContentPanel.Children.Add(content);
            }
        }

        private void ConfirmAddToFavs_Click(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (MessageBox.Show(AppResources.AreYouSure + stopNumber + AppResources.CommaBusLineSpace + busNumber + AppResources.ToFavs, AppResources.Confirm, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                string temp = MyDataSaver.LoadMyData(saveFileName);
                string addition = busNumber + "|" + stopNumber + " ";

                if (temp != null)
                {
                    if (temp.Length > 0)
                    {
                        if (!temp.Contains(addition))
                        {
                            temp += ";" + addition;
                            MyDataSaver.SaveMyData(temp, saveFileName);

                            MessageBox.Show(AppResources.BusStopSpace + stopNumber + AppResources.CommaBusLineSpace + busNumber + AppResources.HasBeenSavedToFavs, AppResources.FavAdded, MessageBoxButton.OK);
                            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                        }
                        else
                        {
                            MessageBox.Show(AppResources.BusStopSpace + stopNumber + AppResources.CommaBusLineSpace + busNumber + AppResources.IsAlreadyInFavorites, AppResources.FavoriteNotAdded, MessageBoxButton.OK);
                        }
                    }
                    else
                    {
                        MyDataSaver.SaveMyData(addition, saveFileName);

                        MessageBox.Show(AppResources.BusStopSpace + stopNumber + AppResources.CommaBusLineSpace + busNumber + AppResources.HasBeenSavedToFavs, AppResources.FavAdded, MessageBoxButton.OK);
                        NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                    }
                }
                else
                {
                    MyDataSaver.SaveMyData(addition, saveFileName);

                    MessageBox.Show(AppResources.BusStopSpace + stopNumber + AppResources.CommaBusLineSpace + busNumber + AppResources.HasBeenSavedToFavs, AppResources.FavAdded, MessageBoxButton.OK);
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }
            }
        }

        public static void setTitle(string input)
        {
            titleText = input;
        }

        public static void setStopNumber(string input)
        {
            stopNumber = input;
        }

        public static void setBusNumber(string input)
        {
            busNumber = input;
        }
    }
}