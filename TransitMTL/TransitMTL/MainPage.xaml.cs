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
            string template = "http://www2.stm.info/horaires/frmResult.aspx?Langue=Fr&Arret=";
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

            List<String> holder = new List<String>();

            for (int i = 1; i < temp.Count - 1; i++)
            {
                string busLine = "Stop: " + temp[i].ChildNodes[2].ChildNodes[0].InnerHtml + " Direction: " + temp[i].ChildNodes[3].ChildNodes[0].InnerHtml + " Times: ";
                for (int j = 4; j < temp[i].ChildNodes.Count - 1; j++)
                {
                    busLine += temp[i].ChildNodes[j].InnerHtml + ", ";
                }
                holder.Add(busLine);
            }

            //foreach (String s in holder)
            //{
            //    System.Diagnostics.Debug.WriteLine(s);
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

            Results.Text = String.Join(Environment.NewLine, holder.ToList());
            //System.Diagnostics.Debug.WriteLine(node);
        }
    }
}