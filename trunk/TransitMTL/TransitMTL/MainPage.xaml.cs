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

namespace TransitMTL
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void StopCodeTextBox_OnFocus(object sender, RoutedEventArgs e)
        {
            StopCodeTextBox.Text = "";
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string template = "http://www2.stm.info/horaires/frmResult.aspx?Langue=En&Arret=";
            template += StopCodeTextBox.Text;
            HtmlWeb.LoadAsync(template, SiteLoaded);
        }

        public void SiteLoaded(object sender, HtmlDocumentLoadCompleted args)
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
            List<BusStopData> busStopData = new List<BusStopData>();

            for (int i = 1; i < temp.Count - 1; i++)
            {
                String stopNum = temp[i].ChildNodes[2].ChildNodes[0].InnerHtml;
                String direction = temp[i].ChildNodes[3].ChildNodes[0].InnerHtml;
                List<String> times = new List<String>();

                for (int j = 4; j < temp[i].ChildNodes.Count - 1; j++)
                {
                    times.Add(temp[i].ChildNodes[j].InnerHtml);
                }

                BusStopData busStop = new BusStopData(stopNum, direction, times);
                busStopData.Add(busStop);
            }

            ResultsList.Items.Clear();

            for (int i = 0; i < busStopData.Count; i++)
            {
                
                Grid stopContainer = new Grid();

                TextBlock busNum = new TextBlock();
                busNum.Text = busStopData[i].getStopNumber();
                busNum.Height = 70;
                busNum.HorizontalAlignment = HorizontalAlignment.Left;
                busNum.VerticalAlignment = VerticalAlignment.Top;
                busNum.FontSize = 48;
                busNum.Margin = new Thickness(13, 0, 0, 0);

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
                busTimes.Margin = new Thickness(12,80,0,0);

                stopContainer.Children.Add(busNum);
                stopContainer.Children.Add(busDirection);
                stopContainer.Children.Add(busTimeslabel);
                stopContainer.Children.Add(busTimes);
                ResultsList.Items.Add(stopContainer);
            }

            //foreach (String s in holder)
            //{
            //    TextBlock txBlock = new TextBlock();
            //    txBlock.Text = s;
            //    System.Diagnostics.Debug.WriteLine(s);
            //    StopCodeGrid.Children.Add(txBlock);
            //}
            //System.Diagnostics.Debug.WriteLine(temp[1].ChildNodes.Count);

            //foreach (HtmlNode n in temp)
            //{
            //    System.Diagnostics.Debug.WriteLine(n.InnerHtml);
            //}

            //List<String> test = nodes.ToList().Select(x => x.InnerText).ToList();
            //Results.Text = String.Join(Environment.NewLine, nodes.ToList()
            //                                                .Select(x => x.InnerText)
            //                                                .ToList());

            

            

            //Results.Text = String.Join(Environment.NewLine, holder.ToList());
            //System.Diagnostics.Debug.WriteLine(node);
        }

        private void ResultsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResultsList.SelectedItem = null;
        }
    }
}