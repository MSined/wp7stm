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
using HtmlAgilityPack;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;

namespace TransitMTL
{
    public partial class MainPage : PhoneApplicationPage
    {

        static DataSaver<string> MyDataSaver = new DataSaver<string>();
        string saveFileName = "Favorites";
        string favorites;

        List<Bus> localNetwork = new List<Bus>();
        List<Bus> allNightNetwork = new List<Bus>();
        List<Bus> expressNetwork = new List<Bus>();
        List<Bus> shuttleNetwork = new List<Bus>();

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            Visibility darkBackgroundVisibility = (Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"];

            if (darkBackgroundVisibility != Visibility.Visible)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("PanoramaBackground2.png", UriKind.Relative));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;


                PanoramaLayout.Background = imageBrush;
            }
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
                
                LoadBusLines();
                LoadFavorites();
            }
        }

        private void StopCodeTextBox_OnFocus(object sender, RoutedEventArgs e)
        {
            if (StopCodeTextBox.Text == "Enter Bus Code Here")
                StopCodeTextBox.Text = "";
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string template = "http://www2.stm.info/horaires/frmResult.aspx?Langue=En&Arret=";
            template += StopCodeTextBox.Text;
            HtmlWeb.LoadAsync(template, SearchLoaded);
        }

        private void SearchLoaded(object sender, HtmlDocumentLoadCompleted args)
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

            var spanNodes = args.Document.DocumentNode.SelectNodes("//span");

            HtmlNodeCollection busStopNumlbl = new HtmlNodeCollection(null);
            foreach (HtmlNode n in spanNodes)
            {
                if (n.Id == "lblArret")
                {
                    busStopNumlbl = n.ChildNodes;
                }
            }
            string[] stopNumArray = busStopNumlbl[0].InnerText.Split('-');

            List<BusStopData> busStopData = new List<BusStopData>();

            for (int i = 1; i < temp.Count - 1; i++)
            {
                String busNum = temp[i].ChildNodes[2].ChildNodes[0].InnerHtml;
                String direction = temp[i].ChildNodes[3].ChildNodes[0].InnerHtml;
                List<String> times = new List<String>();

                for (int j = 4; j < temp[i].ChildNodes.Count - 1; j++)
                {
                    if (temp[i].ChildNodes[j].InnerHtml != "&nbsp;")
                        if (!temp[i].ChildNodes[j].InnerHtml.Contains("href"))
                            times.Add(temp[i].ChildNodes[j].InnerHtml);
                        else
                            times.Add(temp[i].ChildNodes[j].ChildNodes[0].InnerHtml);
                }

                BusStopData busStop = new BusStopData(busNum, stopNumArray[0], direction, times);
                busStopData.Add(busStop);
            }

            ResultsList.Items.Clear();

            for (int i = 0; i < busStopData.Count; i++)
            {
                
                Grid stopContainer = new Grid();

                TextBlock busNum = new TextBlock();
                busNum.Text = busStopData[i].getBusNumber();
                busNum.Height = 70;
                busNum.HorizontalAlignment = HorizontalAlignment.Left;
                busNum.VerticalAlignment = VerticalAlignment.Top;
                busNum.FontSize = 48;
                busNum.Margin = new Thickness(13, 0, 0, 0);
                busNum.Style = (Style)Application.Current.Resources["PhoneTextSubtleStyle"];

                TextBlock busDirection = new TextBlock();
                busDirection.Text = busStopData[i].getDirection();
                busDirection.Height = 30;
                busDirection.HorizontalAlignment = HorizontalAlignment.Left;
                busDirection.VerticalAlignment = VerticalAlignment.Top;
                busDirection.Margin = new Thickness(100,20,0,0);

                TextBlock busTimeslabel = new TextBlock();
                busTimeslabel.Text = "Next passing times";
                busTimeslabel.Height = 30;
                busTimeslabel.HorizontalAlignment = HorizontalAlignment.Left;
                busTimeslabel.VerticalAlignment = VerticalAlignment.Top;
                busTimeslabel.Margin = new Thickness(12, 50, 0, 0);

                TextBlock busTimes = new TextBlock();
                for (int k = 0; k < busStopData[i].getTimes().Count; k++)
                {
                    string convertedTime;
                    string[] time = busStopData[i].getTimes()[k].Split('h');
                    time[0] = Convert.ToInt32(time[0]) + "";

                    if (Convert.ToInt32(time[0]) > 11)
                    {
                        if (Convert.ToInt32(time[0]) == 12)
                        {
                            convertedTime = String.Join(":", time.ToList()) + " PM";
                        }
                        else
                        {
                            time[0] = (Convert.ToInt32(time[0]) - 12) + "";
                            convertedTime = String.Join(":", time.ToList()) + " PM";
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(time[0]) == 0)
                            time[0] = "12";
                        convertedTime = String.Join(":", time.ToList()) + " AM";
                    }
                    if (k < busStopData[i].getTimes().Count - 1)
                    {
                        busTimes.Text += convertedTime + ", ";
                    }
                    else
                    {
                        busTimes.Text += convertedTime + "";
                    }
                }
                busTimes.HorizontalAlignment = HorizontalAlignment.Left;
                busTimes.VerticalAlignment = VerticalAlignment.Top;
                busTimes.TextWrapping = TextWrapping.Wrap;
                busTimes.Margin = new Thickness(12, 80, 0, 0);

                stopContainer.Children.Add(busNum);
                stopContainer.Children.Add(busDirection);
                stopContainer.Children.Add(busTimeslabel);
                stopContainer.Children.Add(busTimes);

                ContextMenu cMenu = new ContextMenu();
                List<MenuItem> menuItems = new List<MenuItem>();
                MenuItem item1 = new MenuItem();
                item1.Header = "Add to Favorites";
                item1.Name = busStopData[i].getBusNumber() + "|" + busStopData[i].getStopNumber();
                item1.Click += addToFavorites_Click;

                menuItems.Add(item1);

                cMenu.ItemsSource = menuItems;
                ContextMenuService.SetContextMenu(stopContainer, cMenu);

                ResultsList.Items.Add(stopContainer);
            }
        }

        private void ResultsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResultsList.SelectedItem = null;
        }

        private void FavoritesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FavoritesList.SelectedItem = null;
        }

        private void addToFavorites_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mI = (MenuItem)sender;
            string[] menuItem = mI.Name.Split('|');
            string temp = MyDataSaver.LoadMyData(saveFileName);

            if (temp != null)
            {
                if (temp.Length > 0)
                {
                    if (!temp.Contains(mI.Name))
                    {
                        temp += ";" + mI.Name;
                        MyDataSaver.SaveMyData(temp, saveFileName);
                        TextBlock help = textBlock1;
                        FavoritesList.Items.Clear();
                        FavoritesList.Items.Add(textBlock1);
                        LoadFavorites();

                        MessageBox.Show("Bus Stop " + StopCodeTextBox.Text + ", Bus line " + menuItem[0] + " has been saved to favorites.", "Favorite Added", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show("Bus Stop " + StopCodeTextBox.Text + ", Bus line " + menuItem[0] + " is already in Favorites.", "Favorite not Added", MessageBoxButton.OK);
                    }
                }
                else
                {
                    MyDataSaver.SaveMyData(mI.Name, saveFileName);
                    TextBlock help = textBlock1;
                    FavoritesList.Items.Clear();
                    FavoritesList.Items.Add(textBlock1);
                    LoadFavorites();

                    MessageBox.Show("Bus Stop " + StopCodeTextBox.Text + ", Bus line " + menuItem[0] + " has been saved to favorites.", "Favorite Added", MessageBoxButton.OK);
                }
            }

            else
            {
                MyDataSaver.SaveMyData(mI.Name, saveFileName);
                TextBlock help = textBlock1;
                FavoritesList.Items.Clear();
                FavoritesList.Items.Add(textBlock1);
                LoadFavorites();

                MessageBox.Show("Bus Stop " + StopCodeTextBox.Text + ", Bus line " + menuItem[0] + " has been saved to favorites.", "Favorite Added", MessageBoxButton.OK);
            }
        }

        private void removeFromFavorites_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mI = (MenuItem)sender;
            string temp = MyDataSaver.LoadMyData(saveFileName);
            string[] menuItem = mI.Name.Split('|');
            if (temp != null)
            {
                List<string> favStops = temp.Split(';').ToList<string>();
                favStops.Remove("");
                favStops.Remove(" ");
                favStops.Remove(mI.Name);

                temp = String.Join<string>(";", favStops);

                MyDataSaver.SaveMyData(temp, saveFileName);
                TextBlock help = textBlock1;
                FavoritesList.Items.Clear();
                FavoritesList.Items.Add(textBlock1);
                LoadFavorites();
            }
            MessageBox.Show("Bus stop number " + menuItem[0].Trim() + ", Bus line " + menuItem[1] + " has been removed from favorites.", "Favorite Deleted", MessageBoxButton.OK);
        }

        private void LoadFavorites()
        {
            string temp = MyDataSaver.LoadMyData(saveFileName);

            //System.Diagnostics.Debug.WriteLine("Favorites: " + temp);
            if (temp != null)
            {
                favorites = temp;

                string[] favsArray = favorites.Split(';');

                if (favsArray != null && favsArray.Length > 1)
                {
                    List<string> uniqueBusStops = new List<string>();
                    foreach (string s in favsArray)
                    {
                        string[] busStopNum = s.Split('|');
                        if (!uniqueBusStops.Contains(busStopNum[1]))
                        {
                            uniqueBusStops.Add(busStopNum[1]);
                        }
                    }
                    foreach (string s in uniqueBusStops)
                    {
                        string template = "http://www2.stm.info/horaires/frmResult.aspx?Langue=En&Arret=";
                        template += s;
                        HtmlWeb.LoadAsync(template, FavoritesLoaded);
                    }
                }
                else if (temp == "")
                {

                }
                else
                {
                    string[] busStopNum = temp.Split('|');

                    string template = "http://www2.stm.info/horaires/frmResult.aspx?Langue=En&Arret=";
                    template += busStopNum[1];
                    HtmlWeb.LoadAsync(template, FavoritesLoaded);
                }
            }
        }

        private void FavoritesLoaded(object sender, HtmlDocumentLoadCompleted args)
        {   
            var nodes = args.Document.DocumentNode.SelectNodes("//table");
            string favFile = MyDataSaver.LoadMyData(saveFileName);

            HtmlNodeCollection temp = new HtmlNodeCollection(null);
            foreach (HtmlNode n in nodes)
            {
                if (n.Id == "webGrille")
                {
                    temp = n.ChildNodes;
                }
            }

            var spanNodes = args.Document.DocumentNode.SelectNodes("//span");

            HtmlNodeCollection busStopNumlbl = new HtmlNodeCollection(null);
            foreach (HtmlNode n in spanNodes)
            {
                if (n.Id == "lblArret")
                {
                    busStopNumlbl = n.ChildNodes;
                }
            }
            string[] stopNumArray = busStopNumlbl[0].InnerText.Split('-');

            List<BusStopData> busStopData = new List<BusStopData>();

            for (int i = 1; i < temp.Count - 1; i++)
            {
                //System.Diagnostics.Debug.WriteLine("Favorites: " + favFile);
                //System.Diagnostics.Debug.WriteLine("Search: " + temp[i].ChildNodes[2].ChildNodes[0].InnerHtml + "|" + stopNumArray[0]);
                //System.Diagnostics.Debug.WriteLine(favFile.Contains(temp[i].ChildNodes[2].ChildNodes[0].InnerHtml + "|" + stopNumArray[0]));

                if (favFile.Contains(temp[i].ChildNodes[2].ChildNodes[0].InnerHtml + "|" + stopNumArray[0]))
                {
                    String busNum = temp[i].ChildNodes[2].ChildNodes[0].InnerHtml;
                    String direction = temp[i].ChildNodes[3].ChildNodes[0].InnerHtml;
                    List<String> times = new List<String>();

                    for (int j = 4; j < temp[i].ChildNodes.Count - 1; j++)
                    {
                        if (temp[i].ChildNodes[j].InnerHtml != "&nbsp;")
                            if (!temp[i].ChildNodes[j].InnerHtml.Contains("href"))
                                times.Add(temp[i].ChildNodes[j].InnerHtml);
                            else
                                times.Add(temp[i].ChildNodes[j].ChildNodes[0].InnerHtml);
                    }

                    BusStopData busStop = new BusStopData(busNum, stopNumArray[0], direction, times);
                    busStopData.Add(busStop);
                }
            }

            for (int i = 0; i < busStopData.Count; i++)
            {
                Grid stopContainer = new Grid();

                TextBlock busNum = new TextBlock();
                busNum.Text = busStopData[i].getBusNumber();
                busNum.Height = 70;
                busNum.HorizontalAlignment = HorizontalAlignment.Left;
                busNum.VerticalAlignment = VerticalAlignment.Top;
                busNum.FontSize = 48;
                busNum.Margin = new Thickness(13, 0, 0, 0);
                busNum.Style = (Style)Application.Current.Resources["PhoneTextSubtleStyle"];

                TextBlock busDirection = new TextBlock();
                busDirection.Text = busStopData[i].getDirection();
                busDirection.Height = 30;
                busDirection.HorizontalAlignment = HorizontalAlignment.Left;
                busDirection.VerticalAlignment = VerticalAlignment.Top;
                busDirection.Margin = new Thickness(100, 20, 0, 0);

                TextBlock busTimeslabel = new TextBlock();
                busTimeslabel.Text = "Next passing times";
                busTimeslabel.Height = 30;
                busTimeslabel.HorizontalAlignment = HorizontalAlignment.Left;
                busTimeslabel.VerticalAlignment = VerticalAlignment.Top;
                busTimeslabel.Margin = new Thickness(12, 50, 0, 0);

                TextBlock busCodeLabel = new TextBlock();
                busCodeLabel.Text = "Bus Stop Code: ";
                busCodeLabel.Text += busStopData[i].getStopNumber();
                busCodeLabel.Height = 30;
                busCodeLabel.HorizontalAlignment = HorizontalAlignment.Left;
                busCodeLabel.VerticalAlignment = VerticalAlignment.Top;
                busCodeLabel.Margin = new Thickness(200, 20, 0, 0);

                TextBlock busTimes = new TextBlock();
                for (int k = 0; k < busStopData[i].getTimes().Count; k++)
                {
                    string convertedTime;
                    string[] time = busStopData[i].getTimes()[k].Split('h');
                    time[0] = Convert.ToInt32(time[0]) + "";

                    if (Convert.ToInt32(time[0]) > 11)
                    {
                        if (Convert.ToInt32(time[0]) == 12)
                        {
                            convertedTime = String.Join(":", time.ToList()) + " PM";
                        }
                        else
                        {
                            time[0] = (Convert.ToInt32(time[0]) - 12) + "";
                            convertedTime = String.Join(":", time.ToList()) + " PM";
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(time[0]) == 0)
                            time[0] = "12";
                        convertedTime = String.Join(":", time.ToList()) + " AM";
                    }
                    if (k < busStopData[i].getTimes().Count - 1)
                    {
                        busTimes.Text += convertedTime + ", ";
                    }
                    else
                    {
                        busTimes.Text += convertedTime + "";
                    }
                }
                busTimes.HorizontalAlignment = HorizontalAlignment.Left;
                busTimes.VerticalAlignment = VerticalAlignment.Top;
                busTimes.TextWrapping = TextWrapping.Wrap;
                busTimes.Margin = new Thickness(12, 80, 0, 0);

                stopContainer.Children.Add(busNum);
                stopContainer.Children.Add(busDirection);
                stopContainer.Children.Add(busTimeslabel);
                stopContainer.Children.Add(busCodeLabel);
                stopContainer.Children.Add(busTimes);

                ContextMenu cMenu = new ContextMenu();
                List<MenuItem> menuItems = new List<MenuItem>();
                MenuItem item1 = new MenuItem();
                item1.Header = "Remove from Favorites";
                item1.Name = busStopData[i].getBusNumber() + "|" + busStopData[i].getStopNumber();
                item1.Click += removeFromFavorites_Click;

                menuItems.Add(item1);

                cMenu.ItemsSource = menuItems;
                ContextMenuService.SetContextMenu(stopContainer, cMenu);

                FavoritesList.Items.Add(stopContainer);

            }
        }

        private void LoadBusLines()
        {
            string local = "http://www.stm.info/english/bus/a-lig_local.htm";
            HtmlWeb.LoadAsync(local, LocalLinesLoaded);
        }

        private void LocalLinesLoaded(object sender, HtmlDocumentLoadCompleted args)
        {
            var nodes = args.Document.DocumentNode.SelectNodes("//table");

            HtmlNodeCollection busTable = new HtmlNodeCollection(null);
            busTable = nodes[1].ChildNodes;


            System.Diagnostics.Debug.WriteLine("Local: " + String.Join(Environment.NewLine, nodes[1].ChildNodes.Count));

            
            List<string> busNumbers = new List<string>();
            List<string> busNames = new List<string>();

            for (int i = 0; i < busTable.Count; i++)
            {
                for (int j = 0; j < busTable[i].ChildNodes.Count; j++)
                {
                    if (busTable[i].ChildNodes[j].HasAttributes)
                    {

                        if (busTable[i].ChildNodes[j].GetAttributeValue("class", "Test") == "style20" || busTable[i].ChildNodes[j].GetAttributeValue("class", "Test") == "style23")
                        {
                            if (busTable[i].ChildNodes[j].ChildNodes.Count == 1)
                            {
                                busNumbers.Add(busTable[i].ChildNodes[j].InnerHtml.Replace("\r", "").Replace("\n", "").Trim());
                                //System.Diagnostics.Debug.WriteLine(busTable[i].ChildNodes[j].InnerHtml);
                            }
                        }
                        else if (busTable[i].ChildNodes[j].GetAttributeValue("style", "Test") == "width: 296px")
                        {
                            if (busTable[i].ChildNodes[j].ChildNodes[1].InnerHtml.Replace("&nbsp;", "").Trim() != "Route Name")
                                busNames.Add(busTable[i].ChildNodes[j].ChildNodes[1].InnerHtml.Replace("&nbsp;", "").Replace("&eacute;", "é")
                                                                                                                    .Replace("&Eacute;", "É")
                                                                                                                    .Replace("&egrave;", "è")
                                                                                                                    .Replace("&Egrave;", "È")
                                                                                                                    .Replace("&ocirc;", "ô")
                                                                                                                    .Replace("&Ocirc;", "Ô")
                                                                                                                    .Replace("&aacute;", "á")
                                                                                                                    .Replace("&Aacute;", "Á")
                                                                                                                    .Replace("&agrave;", "à")
                                                                                                                    .Replace("&Agrave;", "À")
                                                                                                                    .Replace("&acirc;", "â")
                                                                                                                    .Replace("&Acirc;", "Â")
                                                                                                                    .Replace("&ccedil;", "ç")
                                                                                                                    .Replace("&ccedil;", "Ç").Trim());
                            //System.Diagnostics.Debug.WriteLine((busTable[i].ChildNodes[j].ChildNodes[1].InnerHtml).Replace("&nbsp;", "").Trim());
                        }
                    }
                }
            }

            if (busNumbers.Count == busNames.Count)
            {
                for (int i = 0; i < busNumbers.Count; i++)
                {
                    localNetwork.Add(new Bus(busNumbers[i], busNames[i]));
                }
            }
            //foreach (Bus b in localNetwork)
            //{
            //    System.Diagnostics.Debug.WriteLine("Bus Number: " + b.getBusNumber() + ", Bus Name: " + b.getBusName());
            //}

            string allNight = "http://www.stm.info/english/bus/a-lig_nuit.htm";
            HtmlWeb.LoadAsync(allNight, AllNightLinesLoaded);

            //DisplayBusData();

                    //System.Diagnostics.Debug.WriteLine("i: " + i + " j: " + j + " Has Attribute: " + busTable[i].ChildNodes[j].HasAttributes + " InnerHTML: " + busTable[i].InnerHtml);
                    //.GetAttributeValue("style", false)
            //System.Diagnostics.Debug.WriteLine(String.Join(Environment.NewLine, nodes[1].ChildNodes[16].ChildNodes
            //                                                .Select(x => x.InnerHtml)
            //                                                .ToList()));
        }

        private void AllNightLinesLoaded(object sender, HtmlDocumentLoadCompleted args)
        {
            var nodes = args.Document.DocumentNode.SelectNodes("//table");

            HtmlNodeCollection busTable = new HtmlNodeCollection(null);
            busTable = nodes[1].ChildNodes;


            System.Diagnostics.Debug.WriteLine("All Night: " + String.Join(Environment.NewLine, nodes[1].ChildNodes.Count));


            List<string> busNumbers = new List<string>();
            List<string> busNames = new List<string>();

            for (int i = 0; i < busTable.Count; i++)
            {
                for (int j = 0; j < busTable[i].ChildNodes.Count; j++)
                {
                    if (busTable[i].ChildNodes[j].HasAttributes)
                    {

                        if (busTable[i].ChildNodes[j].GetAttributeValue("class", "Test") == "style7")
                        {
                            if (busTable[i].ChildNodes[j].ChildNodes.Count == 1)
                            {
                                busNumbers.Add(busTable[i].ChildNodes[j].InnerHtml.Replace("\r", "").Replace("\n", "").Trim());
                                //System.Diagnostics.Debug.WriteLine(busTable[i].ChildNodes[j].InnerHtml);
                            }
                        }
                        else if (busTable[i].ChildNodes[j].GetAttributeValue("style", "Test") == "width: 396px")
                        {
                            if (busTable[i].ChildNodes[j].ChildNodes[1].InnerHtml.Replace("&nbsp;", "").Trim() != "Route Name")
                                busNames.Add(busTable[i].ChildNodes[j].ChildNodes[1].InnerHtml.Replace("&nbsp;", "").Replace("&eacute;", "é")
                                                                                                                    .Replace("&Eacute;", "É")
                                                                                                                    .Replace("&egrave;", "è")
                                                                                                                    .Replace("&Egrave;", "È")
                                                                                                                    .Replace("&ocirc;", "ô")
                                                                                                                    .Replace("&Ocirc;", "Ô")
                                                                                                                    .Replace("&aacute;", "á")
                                                                                                                    .Replace("&Aacute;", "Á")
                                                                                                                    .Replace("&agrave;", "à")
                                                                                                                    .Replace("&Agrave;", "À")
                                                                                                                    .Replace("&acirc;", "â")
                                                                                                                    .Replace("&Acirc;", "Â")
                                                                                                                    .Replace("&ccedil;", "ç")
                                                                                                                    .Replace("&ccedil;", "Ç").Replace("<strong></strong>", "").Trim());
                            //System.Diagnostics.Debug.WriteLine((busTable[i].ChildNodes[j].ChildNodes[1].InnerHtml).Replace("&nbsp;", "").Trim());
                        }
                    }
                }
            }

            if (busNumbers.Count == busNames.Count)
            {
                for (int i = 0; i < busNumbers.Count; i++)
                {
                    allNightNetwork.Add(new Bus(busNumbers[i], busNames[i]));
                }
            }
            //foreach (Bus b in allNightNetwork)
            //{
            //    System.Diagnostics.Debug.WriteLine("Bus Number: " + b.getBusNumber() + ", Bus Name: " + b.getBusName());
            //}

            string express = "http://www.stm.info/english/bus/a-lig_express.htm";
            HtmlWeb.LoadAsync(express, ExpressLinesLoaded);

            //DisplayBusData();

            //System.Diagnostics.Debug.WriteLine("i: " + i + " j: " + j + " Has Attribute: " + busTable[i].ChildNodes[j].HasAttributes + " InnerHTML: " + busTable[i].InnerHtml);
            //.GetAttributeValue("style", false)
            //System.Diagnostics.Debug.WriteLine(String.Join(Environment.NewLine, nodes[1].ChildNodes[16].ChildNodes
            //                                                .Select(x => x.InnerHtml)
            //                                                .ToList()));
        }

        private void ExpressLinesLoaded(object sender, HtmlDocumentLoadCompleted args)
        {
            var nodes = args.Document.DocumentNode.SelectNodes("//table");

            HtmlNodeCollection busTable = new HtmlNodeCollection(null);
            busTable = nodes[1].ChildNodes;


            System.Diagnostics.Debug.WriteLine("Express: " + String.Join(Environment.NewLine, nodes[1].ChildNodes.Count));


            List<string> busNumbers = new List<string>();
            List<string> busNames = new List<string>();

            for (int i = 0; i < busTable.Count; i++)
            {
                for (int j = 0; j < busTable[i].ChildNodes.Count; j++)
                {
                    if (busTable[i].ChildNodes[j].HasAttributes)
                    {

                        if (busTable[i].ChildNodes[j].GetAttributeValue("class", "Test") == "style46")
                        {
                            if ((busTable[i].ChildNodes[j].ChildNodes.Count == 3 || busTable[i].ChildNodes[j].ChildNodes.Count == 2) && !busTable[i].ChildNodes[j].InnerHtml.Contains("href="))
                            {
                                busNumbers.Add(busTable[i].ChildNodes[j].InnerHtml.Replace("\r", "").Replace("\n", "").Replace("<strong>", "").Replace("</strong>", "").Trim());
                                //System.Diagnostics.Debug.WriteLine(busTable[i].ChildNodes[j].InnerHtml);
                            }
                        }
                        else if (busTable[i].ChildNodes[j].GetAttributeValue("style", "Test") == "width: 338px")
                        {
                            if (busTable[i].ChildNodes[j].ChildNodes[0].InnerHtml.Replace("&nbsp;", "").Trim() != "Route Name")
                            {
                                busNames.Add(busTable[i].ChildNodes[j].ChildNodes[0].InnerHtml.Replace("&nbsp;", "").Replace("&eacute;", "é")
                                                                                                                    .Replace("&Eacute;", "É")
                                                                                                                    .Replace("&egrave;", "è")
                                                                                                                    .Replace("&Egrave;", "È")
                                                                                                                    .Replace("&ocirc;", "ô")
                                                                                                                    .Replace("&Ocirc;", "Ô")
                                                                                                                    .Replace("&aacute;", "á")
                                                                                                                    .Replace("&Aacute;", "Á")
                                                                                                                    .Replace("&agrave;", "à")
                                                                                                                    .Replace("&Agrave;", "À")
                                                                                                                    .Replace("&acirc;", "â")
                                                                                                                    .Replace("&Acirc;", "Â")
                                                                                                                    .Replace("&ccedil;", "ç")
                                                                                                                    .Replace("&ccedil;", "Ç").Trim());
                            }
                            //System.Diagnostics.Debug.WriteLine((busTable[i].ChildNodes[j].ChildNodes[1].InnerHtml).Replace("&nbsp;", "").Trim());
                        }
                    }


                    foreach (HtmlNode HN in busTable[i].ChildNodes[j].ChildNodes)
                    {
                        if (HN.GetAttributeValue("class", "Test") == "style4")
                        {
                            busNumbers.Add(busTable[i].ChildNodes[j].InnerHtml.Replace("\r", "").Replace("\n", "").Replace("<!--DEUXIEME COLONNE-->", "").Replace("<span class=\"style4\">", "").Replace("</span>", "").Trim());
                            //System.Diagnostics.Debug.WriteLine(HN.InnerHtml);
                        }
                    }
                }
            }


            if (busNumbers.Count == busNames.Count)
            {
                for (int i = 0; i < busNumbers.Count; i++)
                {
                    expressNetwork.Add(new Bus(busNumbers[i], busNames[i]));
                }
            }
            //foreach (Bus b in expressNetwork)
            //{
            //    System.Diagnostics.Debug.WriteLine("Bus Number: " + b.getBusNumber() + ", Bus Name: " + b.getBusName());
            //}

            string shuttle = "http://www.stm.info/english/bus/a-lig_navettes.htm";
            HtmlWeb.LoadAsync(shuttle, ShuttleLinesLoaded);

            //DisplayBusData();

            //System.Diagnostics.Debug.WriteLine("i: " + i + " j: " + j + " Has Attribute: " + busTable[i].ChildNodes[j].HasAttributes + " InnerHTML: " + busTable[i].InnerHtml);
            //.GetAttributeValue("style", false)
            //System.Diagnostics.Debug.WriteLine(String.Join(Environment.NewLine, nodes[1].ChildNodes[16].ChildNodes
            //                                                .Select(x => x.InnerHtml)
            //                                                .ToList()));
        }

        private void ShuttleLinesLoaded(object sender, HtmlDocumentLoadCompleted args)
        {
            var nodes = args.Document.DocumentNode.SelectNodes("//table");

            HtmlNodeCollection busTable = new HtmlNodeCollection(null);
            busTable = nodes[1].ChildNodes;


            System.Diagnostics.Debug.WriteLine("Shuttle: " + String.Join(Environment.NewLine, nodes[1].ChildNodes.Count));


            List<string> busNumbers = new List<string>();
            List<string> busNames = new List<string>();

            for (int i = 0; i < busTable.Count; i++)
            {
                for (int j = 0; j < busTable[i].ChildNodes.Count; j++)
                {
                    if (busTable[i].ChildNodes[j].HasAttributes)
                    {

                        if (busTable[i].ChildNodes[j].GetAttributeValue("class", "Test") == "style20" || busTable[i].ChildNodes[j].GetAttributeValue("class", "Test") == "style23")
                        {
                            if (busTable[i].ChildNodes[j].ChildNodes.Count == 1)
                            {
                                busNumbers.Add(busTable[i].ChildNodes[j].InnerHtml.Replace("\r", "").Replace("\n", "").Trim());
                                //System.Diagnostics.Debug.WriteLine(busTable[i].ChildNodes[j].InnerHtml);
                            }
                        }
                        else if (busTable[i].ChildNodes[j].GetAttributeValue("style", "Test") == "width: 296px")
                        {
                            if (busTable[i].ChildNodes[j].ChildNodes[1].InnerHtml.Replace("&nbsp;", "").Trim() != "Route Name")
                                busNames.Add(busTable[i].ChildNodes[j].ChildNodes[1].InnerHtml.Replace("&nbsp;", "").Replace("&eacute;", "é")
                                                                                                                    .Replace("&Eacute;", "É")
                                                                                                                    .Replace("&egrave;", "è")
                                                                                                                    .Replace("&Egrave;", "È")
                                                                                                                    .Replace("&ocirc;", "ô")
                                                                                                                    .Replace("&Ocirc;", "Ô")
                                                                                                                    .Replace("&aacute;", "á")
                                                                                                                    .Replace("&Aacute;", "Á")
                                                                                                                    .Replace("&agrave;", "à")
                                                                                                                    .Replace("&Agrave;", "À")
                                                                                                                    .Replace("&acirc;", "â")
                                                                                                                    .Replace("&Acirc;", "Â")
                                                                                                                    .Replace("&ccedil;", "ç")
                                                                                                                    .Replace("&ccedil;", "Ç").Trim());
                            //System.Diagnostics.Debug.WriteLine((busTable[i].ChildNodes[j].ChildNodes[1].InnerHtml).Replace("&nbsp;", "").Trim());
                        }
                    }
                }
            }

            if (busNumbers.Count == busNames.Count)
            {
                for (int i = 0; i < busNumbers.Count; i++)
                {
                    shuttleNetwork.Add(new Bus(busNumbers[i], busNames[i]));
                }
            }
            //foreach (Bus b in expressNetwork)
            //{
            //    System.Diagnostics.Debug.WriteLine("Bus Number: " + b.getBusNumber() + ", Bus Name: " + b.getBusName());
            //}



            DisplayBusData();


            //System.Diagnostics.Debug.WriteLine("i: " + i + " j: " + j + " Has Attribute: " + busTable[i].ChildNodes[j].HasAttributes + " InnerHTML: " + busTable[i].InnerHtml);
            //.GetAttributeValue("style", false)
            //System.Diagnostics.Debug.WriteLine(String.Join(Environment.NewLine, nodes[1].ChildNodes[16].ChildNodes
            //                                                .Select(x => x.InnerHtml)
            //                                                .ToList()));
        }

        private void DisplayBusData()
        {
            BusListBox.Items.Clear();
            foreach (Bus b in localNetwork)
            {
                Grid BusContainer = new Grid();
                BusContainer.Width = 460;

                TextBlock BusLine = new TextBlock();
                BusLine.Text = b.getBusNumber();
                BusLine.Height = 70;
                BusLine.HorizontalAlignment = HorizontalAlignment.Left;
                BusLine.VerticalAlignment = VerticalAlignment.Top;
                BusLine.FontSize = 48;
                BusLine.Margin = new Thickness(13, 0, 0, 0);
                BusLine.Style = (Style)Application.Current.Resources["PhoneTextSubtleStyle"];

                TextBlock BusName = new TextBlock();
                BusName.Text = b.getBusName();
                BusName.HorizontalAlignment = HorizontalAlignment.Left;
                BusName.VerticalAlignment = VerticalAlignment.Top;
                BusName.Margin = new Thickness(100, 10, 0, 0);
                BusName.TextWrapping = TextWrapping.Wrap;
                BusName.Style = (Style)Application.Current.Resources["PhoneTextLargeStyle"];

                BusContainer.Children.Add(BusLine);
                BusContainer.Children.Add(BusName);

                BusListBox.Items.Add(BusContainer);
            }

            foreach (Bus b in allNightNetwork)
            {
                Grid BusContainer = new Grid();

                TextBlock BusLine = new TextBlock();
                BusLine.Text = b.getBusNumber();
                BusLine.Height = 70;
                BusLine.HorizontalAlignment = HorizontalAlignment.Left;
                BusLine.VerticalAlignment = VerticalAlignment.Top;
                BusLine.FontSize = 48;
                BusLine.Margin = new Thickness(13, 0, 0, 0);
                BusLine.Style = (Style)Application.Current.Resources["PhoneTextSubtleStyle"];

                TextBlock BusName = new TextBlock();
                BusName.Text = b.getBusName();
                BusName.HorizontalAlignment = HorizontalAlignment.Left;
                BusName.VerticalAlignment = VerticalAlignment.Top;
                BusName.Margin = new Thickness(100, 10, 0, 0);
                BusName.TextWrapping = TextWrapping.Wrap;
                BusName.Style = (Style)Application.Current.Resources["PhoneTextLargeStyle"];

                BusContainer.Children.Add(BusLine);
                BusContainer.Children.Add(BusName);

                BusListBox.Items.Add(BusContainer);
            }

            foreach (Bus b in expressNetwork)
            {
                Grid BusContainer = new Grid();

                TextBlock BusLine = new TextBlock();
                BusLine.Text = b.getBusNumber();
                BusLine.Height = 70;
                BusLine.HorizontalAlignment = HorizontalAlignment.Left;
                BusLine.VerticalAlignment = VerticalAlignment.Top;
                BusLine.FontSize = 48;
                BusLine.Margin = new Thickness(13, 0, 0, 0);
                BusLine.Style = (Style)Application.Current.Resources["PhoneTextSubtleStyle"];

                TextBlock BusName = new TextBlock();
                BusName.Text = b.getBusName();
                BusName.HorizontalAlignment = HorizontalAlignment.Left;
                BusName.VerticalAlignment = VerticalAlignment.Top;
                BusName.Margin = new Thickness(100, 10, 0, 0);
                BusName.TextWrapping = TextWrapping.Wrap;
                BusName.Style = (Style)Application.Current.Resources["PhoneTextLargeStyle"];

                BusContainer.Children.Add(BusLine);
                BusContainer.Children.Add(BusName);

                BusListBox.Items.Add(BusContainer);
            }

            foreach (Bus b in shuttleNetwork)
            {
                Grid BusContainer = new Grid();

                TextBlock BusLine = new TextBlock();
                BusLine.Text = b.getBusNumber();
                BusLine.Height = 70;
                BusLine.HorizontalAlignment = HorizontalAlignment.Left;
                BusLine.VerticalAlignment = VerticalAlignment.Top;
                BusLine.FontSize = 48;
                BusLine.Margin = new Thickness(13, 0, 0, 0);
                BusLine.Style = (Style)Application.Current.Resources["PhoneTextSubtleStyle"];

                TextBlock BusName = new TextBlock();
                BusName.Text = b.getBusName();
                BusName.HorizontalAlignment = HorizontalAlignment.Left;
                BusName.VerticalAlignment = VerticalAlignment.Top;
                BusName.Margin = new Thickness(100, 10, 0, 0);
                BusName.TextWrapping = TextWrapping.Wrap;
                BusName.Style = (Style)Application.Current.Resources["PhoneTextLargeStyle"];

                BusContainer.Children.Add(BusLine);
                BusContainer.Children.Add(BusName);

                BusListBox.Items.Add(BusContainer);
            }

        }
    }
}