using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace TransitMTL
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            // Sample data; replace with real data
            //this.Items.Add(new ItemViewModel() { LineOne = "runtime one", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu" });
            //this.Items.Add(new ItemViewModel() { LineOne = "runtime two", LineTwo = "Dictumst eleifend facilisi faucibus", LineThree = "Suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus" });
            //this.Items.Add(new ItemViewModel() { LineOne = "runtime three", LineTwo = "Habitant inceptos interdum lobortis", LineThree = "Habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent" });
            //this.Items.Add(new ItemViewModel() { LineOne = "runtime four", LineTwo = "Nascetur pharetra placerat pulvinar", LineThree = "Ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos" });
            //this.Items.Add(new ItemViewModel() { LineOne = "runtime five", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur" });
            //this.Items.Add(new ItemViewModel() { LineOne = "runtime six", LineTwo = "Dictumst eleifend facilisi faucibus", LineThree = "Pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent" });
            //this.Items.Add(new ItemViewModel() { LineOne = "runtime seven", LineTwo = "Habitant inceptos interdum lobortis", LineThree = "Accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat" });
            //this.Items.Add(new ItemViewModel() { LineOne = "runtime eight", LineTwo = "Nascetur pharetra placerat pulvinar", LineThree = "Pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum" });
            //this.Items.Add(new ItemViewModel() { LineOne = "runtime nine", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu" });
            //this.Items.Add(new ItemViewModel() { LineOne = "runtime ten", LineTwo = "Dictumst eleifend facilisi faucibus", LineThree = "Suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus" });
            //this.Items.Add(new ItemViewModel() { LineOne = "runtime eleven", LineTwo = "Habitant inceptos interdum lobortis", LineThree = "Habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent" });
            //this.Items.Add(new ItemViewModel() { LineOne = "runtime twelve", LineTwo = "Nascetur pharetra placerat pulvinar", LineThree = "Ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos" });
            //this.Items.Add(new ItemViewModel() { LineOne = "runtime thirteen", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur" });
            //this.Items.Add(new ItemViewModel() { LineOne = "runtime fourteen", LineTwo = "Dictumst eleifend facilisi faucibus", LineThree = "Pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent" });
            //this.Items.Add(new ItemViewModel() { LineOne = "runtime fifteen", LineTwo = "Habitant inceptos interdum lobortis", LineThree = "Accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat" });
            //this.Items.Add(new ItemViewModel() { LineOne = "runtime sixteen", LineTwo = "Nascetur pharetra placerat pulvinar", LineThree = "Pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum" });

            this.Items.Add(new ItemViewModel() { LineOne = "10", LineTwo = "De Lorimier"});
            this.Items.Add(new ItemViewModel() { LineOne = "11", LineTwo = "Montagne"});
            this.Items.Add(new ItemViewModel() { LineOne = "12", LineTwo = "Ile-des-Soeurs"});
            this.Items.Add(new ItemViewModel() { LineOne = "13", LineTwo = "Christophe-Colomb"});
            this.Items.Add(new ItemViewModel() { LineOne = "14", LineTwo = "Amherst"});
            this.Items.Add(new ItemViewModel() { LineOne = "15", LineTwo = "Sainte-Catherine"});
            this.Items.Add(new ItemViewModel() { LineOne = "16", LineTwo = "Graham"});
            this.Items.Add(new ItemViewModel() { LineOne = "17", LineTwo = "Décarie"});
            this.Items.Add(new ItemViewModel() { LineOne = "18", LineTwo = "Beaubien"});
            this.Items.Add(new ItemViewModel() { LineOne = "19", LineTwo = "Chabanel / Marché Central"});
            this.Items.Add(new ItemViewModel() { LineOne = "21", LineTwo = "Place du Commerce"});
            this.Items.Add(new ItemViewModel() { LineOne = "22", LineTwo = "Notre-Dame"});
            this.Items.Add(new ItemViewModel() { LineOne = "24", LineTwo = "Sherbrooke"});
            this.Items.Add(new ItemViewModel() { LineOne = "25", LineTwo = "Angus"});
            this.Items.Add(new ItemViewModel() { LineOne = "26", LineTwo = "Mercier-Est"});
            this.Items.Add(new ItemViewModel() { LineOne = "27", LineTwo = "Boulevard Saint-Joseph"});
            this.Items.Add(new ItemViewModel() { LineOne = "28", LineTwo = "Honoré-Beaugrand"});
            this.Items.Add(new ItemViewModel() { LineOne = "29", LineTwo = "Rachel"});
            this.Items.Add(new ItemViewModel() { LineOne = "30", LineTwo = "Saint-Denis / Saint-Hubert"});
            this.Items.Add(new ItemViewModel() { LineOne = "31", LineTwo = "Saint-Denis"});
            this.Items.Add(new ItemViewModel() { LineOne = "32", LineTwo = "Lacordaire"});
            this.Items.Add(new ItemViewModel() { LineOne = "33", LineTwo = "Langelier"});
            this.Items.Add(new ItemViewModel() { LineOne = "34", LineTwo = "Sainte-Catherine"});
            this.Items.Add(new ItemViewModel() { LineOne = "36", LineTwo = "Monk"});
            this.Items.Add(new ItemViewModel() { LineOne = "37", LineTwo = "Jolicoeur"});
            this.Items.Add(new ItemViewModel() { LineOne = "39", LineTwo = "Des Grandes-Prairies"});
            this.Items.Add(new ItemViewModel() { LineOne = "40", LineTwo = "Henri-Bourassa-Est"});
            this.Items.Add(new ItemViewModel() { LineOne = "41", LineTwo = "Quartier St-Michel / Ahuntsic"});
            this.Items.Add(new ItemViewModel() { LineOne = "43", LineTwo = "Monselet "});
            this.Items.Add(new ItemViewModel() { LineOne = "44", LineTwo = "Armand-Bombardier"});
            this.Items.Add(new ItemViewModel() { LineOne = "45", LineTwo = "Papineau"});
            this.Items.Add(new ItemViewModel() { LineOne = "46", LineTwo = "Casgrain"});
            this.Items.Add(new ItemViewModel() { LineOne = "47", LineTwo = "Masson"});
            this.Items.Add(new ItemViewModel() { LineOne = "48", LineTwo = "Perras"});
            this.Items.Add(new ItemViewModel() { LineOne = "49", LineTwo = "Maurice-Duplessis"});
            this.Items.Add(new ItemViewModel() { LineOne = "51", LineTwo = "Boulevard Édouard-Montpetit"});
            this.Items.Add(new ItemViewModel() { LineOne = "52", LineTwo = "Liège"});
            this.Items.Add(new ItemViewModel() { LineOne = "53", LineTwo = "Boulevard Saint-Laurent"});
            this.Items.Add(new ItemViewModel() { LineOne = "54", LineTwo = "Charland / Chabanel"});
            this.Items.Add(new ItemViewModel() { LineOne = "55", LineTwo = "Boulevard Saint-Laurent"});
            this.Items.Add(new ItemViewModel() { LineOne = "56", LineTwo = "Saint-Hubert"});
            this.Items.Add(new ItemViewModel() { LineOne = "57", LineTwo = "Pointe-Saint-Charles"});
            this.Items.Add(new ItemViewModel() { LineOne = "58", LineTwo = "Wellington"});
            this.Items.Add(new ItemViewModel() { LineOne = "61", LineTwo = "Wellington"});
            this.Items.Add(new ItemViewModel() { LineOne = "63", LineTwo = "Girouard"});
            this.Items.Add(new ItemViewModel() { LineOne = "64", LineTwo = "Grenet"});
            this.Items.Add(new ItemViewModel() { LineOne = "66", LineTwo = "The Boulevard"});
            this.Items.Add(new ItemViewModel() { LineOne = "67", LineTwo = "Saint-Michel"});
            this.Items.Add(new ItemViewModel() { LineOne = "68", LineTwo = "Pierrefonds"});
            this.Items.Add(new ItemViewModel() { LineOne = "69", LineTwo = "Gouin"});
            this.Items.Add(new ItemViewModel() { LineOne = "70", LineTwo = "Bois-Franc"});
            this.Items.Add(new ItemViewModel() { LineOne = "71", LineTwo = "Du Centre"});
            this.Items.Add(new ItemViewModel() { LineOne = "72", LineTwo = "Alfred-Nobel"});
            this.Items.Add(new ItemViewModel() { LineOne = "73", LineTwo = "Dalton"});
            this.Items.Add(new ItemViewModel() { LineOne = "74", LineTwo = "Bridge"});
            this.Items.Add(new ItemViewModel() { LineOne = "75", LineTwo = "De laCommune"});
            this.Items.Add(new ItemViewModel() { LineOne = "76", LineTwo = "Mc Arthur"});
            this.Items.Add(new ItemViewModel() { LineOne = "78", LineTwo = "Laurendeau"});
            this.Items.Add(new ItemViewModel() { LineOne = "80", LineTwo = "Avenue Du Parc"});
            this.Items.Add(new ItemViewModel() { LineOne = "85", LineTwo = "Hochelaga"});
            this.Items.Add(new ItemViewModel() { LineOne = "86", LineTwo = "Pointe-aux-Trembles"});
            this.Items.Add(new ItemViewModel() { LineOne = "90", LineTwo = "Saint-Jacques"});
            this.Items.Add(new ItemViewModel() { LineOne = "92", LineTwo = "Jean-Talon-Ouest"});
            this.Items.Add(new ItemViewModel() { LineOne = "93", LineTwo = "Jean-Talon"});
            this.Items.Add(new ItemViewModel() { LineOne = "94", LineTwo = "D'Iberville"});
            this.Items.Add(new ItemViewModel() { LineOne = "95", LineTwo = "Bélanger"});
            this.Items.Add(new ItemViewModel() { LineOne = "97", LineTwo = "Mont-Royal"});
            this.Items.Add(new ItemViewModel() { LineOne = "99", LineTwo = "Villeray"});
            this.Items.Add(new ItemViewModel() { LineOne = "100", LineTwo = "Crémazie"});
            this.Items.Add(new ItemViewModel() { LineOne = "101", LineTwo = "Saint-Patrick"});
            this.Items.Add(new ItemViewModel() { LineOne = "102", LineTwo = "Somerled"});
            this.Items.Add(new ItemViewModel() { LineOne = "103", LineTwo = "Monkland"});
            this.Items.Add(new ItemViewModel() { LineOne = "104", LineTwo = "Cavendish"});
            this.Items.Add(new ItemViewModel() { LineOne = "105", LineTwo = "Sherbrooke"});
            this.Items.Add(new ItemViewModel() { LineOne = "106", LineTwo = "Newman"});
            this.Items.Add(new ItemViewModel() { LineOne = "107", LineTwo = "Verdun"});
            this.Items.Add(new ItemViewModel() { LineOne = "108", LineTwo = "Bannantyne"});
            this.Items.Add(new ItemViewModel() { LineOne = "109", LineTwo = "BoulevardShevchenko"});
            this.Items.Add(new ItemViewModel() { LineOne = "110", LineTwo = "Centrale"});
            this.Items.Add(new ItemViewModel() { LineOne = "112", LineTwo = "Airlie"});
            this.Items.Add(new ItemViewModel() { LineOne = "113", LineTwo = "Lapierre"});
            this.Items.Add(new ItemViewModel() { LineOne = "115", LineTwo = "Paré"});
            this.Items.Add(new ItemViewModel() { LineOne = "116", LineTwo = "Lafleur / Norman"});
            this.Items.Add(new ItemViewModel() { LineOne = "117", LineTwo = "O'Brien"});
            this.Items.Add(new ItemViewModel() { LineOne = "119", LineTwo = "Rockland"});
            this.Items.Add(new ItemViewModel() { LineOne = "121", LineTwo = "Sauvé / Côte-Vertu"});
            this.Items.Add(new ItemViewModel() { LineOne = "123", LineTwo = "Dollard"});
            this.Items.Add(new ItemViewModel() { LineOne = "124", LineTwo = "Victoria"});
            this.Items.Add(new ItemViewModel() { LineOne = "125", LineTwo = "Ontario"});
            this.Items.Add(new ItemViewModel() { LineOne = "128", LineTwo = "Ville-Saint-Laurent"});
            this.Items.Add(new ItemViewModel() { LineOne = "129", LineTwo = "Côte-Sainte-Catherine"});
            this.Items.Add(new ItemViewModel() { LineOne = "131", LineTwo = "De l'Assomption"});
            this.Items.Add(new ItemViewModel() { LineOne = "135", LineTwo = "De l'Esplanade"});
            this.Items.Add(new ItemViewModel() { LineOne = "136", LineTwo = "Viau  "});
            this.Items.Add(new ItemViewModel() { LineOne = "138", LineTwo = "Notre-Dame-de-Grâce"});
            this.Items.Add(new ItemViewModel() { LineOne = "139", LineTwo = "Pie - IX"});
            this.Items.Add(new ItemViewModel() { LineOne = "140", LineTwo = "Fleury"});
            this.Items.Add(new ItemViewModel() { LineOne = "141", LineTwo = "Jean-Talon-Est"});
            this.Items.Add(new ItemViewModel() { LineOne = "144", LineTwo = "AvenuedesPins"});
            this.Items.Add(new ItemViewModel() { LineOne = "146", LineTwo = "Christophe-Colomb / Meilleur"});
            this.Items.Add(new ItemViewModel() { LineOne = "150", LineTwo = "René-Lévesque"});
            this.Items.Add(new ItemViewModel() { LineOne = "160", LineTwo = "Barclay"});
            this.Items.Add(new ItemViewModel() { LineOne = "161", LineTwo = "Van Horne"});
            this.Items.Add(new ItemViewModel() { LineOne = "162", LineTwo = "Westminster"});
            this.Items.Add(new ItemViewModel() { LineOne = "164", LineTwo = "Dudemaine"});
            this.Items.Add(new ItemViewModel() { LineOne = "165", LineTwo = "Côte-des-Neiges"});
            this.Items.Add(new ItemViewModel() { LineOne = "166", LineTwo = "Queen Mary"});
            this.Items.Add(new ItemViewModel() { LineOne = "167", LineTwo = "Les Îles - The 167 is now the 777 Le Casino"});
            this.Items.Add(new ItemViewModel() { LineOne = "168", LineTwo = "Cité-du-Havre"});
            this.Items.Add(new ItemViewModel() { LineOne = "169", LineTwo = "Ile Ronde - The 169 is now the 769 La Ronde"});
            this.Items.Add(new ItemViewModel() { LineOne = "170", LineTwo = "Keller"});
            this.Items.Add(new ItemViewModel() { LineOne = "171", LineTwo = "Henri-Bourassa"});
            this.Items.Add(new ItemViewModel() { LineOne = "174", LineTwo = "Côte-VertuOuest"});
            this.Items.Add(new ItemViewModel() { LineOne = "175", LineTwo = "Griffith/Saint-François"});
            this.Items.Add(new ItemViewModel() { LineOne = "177", LineTwo = "Thimens"});
            this.Items.Add(new ItemViewModel() { LineOne = "178", LineTwo = "Pointe-Nord/Île-des-Sœurs"});
            this.Items.Add(new ItemViewModel() { LineOne = "179", LineTwo = "De l'Acadie"});
            this.Items.Add(new ItemViewModel() { LineOne = "180", LineTwo = "De Salaberry"});
            this.Items.Add(new ItemViewModel() { LineOne = "183", LineTwo = "Gouin-Est"});
            this.Items.Add(new ItemViewModel() { LineOne = "185", LineTwo = "Sherbrooke"});
            this.Items.Add(new ItemViewModel() { LineOne = "186", LineTwo = "Sherbrooke-Est"});
            this.Items.Add(new ItemViewModel() { LineOne = "187", LineTwo = "René-Lévesque"});
            this.Items.Add(new ItemViewModel() { LineOne = "188", LineTwo = "Couture"});
            this.Items.Add(new ItemViewModel() { LineOne = "189", LineTwo = "Notre-Dame"});
            this.Items.Add(new ItemViewModel() { LineOne = "191", LineTwo = "Broadway/Provost"});
            this.Items.Add(new ItemViewModel() { LineOne = "192", LineTwo = "Robert"});
            this.Items.Add(new ItemViewModel() { LineOne = "193", LineTwo = "Jarry"});
            this.Items.Add(new ItemViewModel() { LineOne = "195", LineTwo = "Sherbrooke/Notre-Dame"});
            this.Items.Add(new ItemViewModel() { LineOne = "196", LineTwo = "Parc industriel Lachine"});
            this.Items.Add(new ItemViewModel() { LineOne = "197", LineTwo = "Rosemont"});
            this.Items.Add(new ItemViewModel() { LineOne = "200", LineTwo = "Sainte-Anne-de-Bellevue"});
            this.Items.Add(new ItemViewModel() { LineOne = "201", LineTwo = "Saint-Charles/Saint-Jean"});
            this.Items.Add(new ItemViewModel() { LineOne = "202", LineTwo = "Dawson"});
            this.Items.Add(new ItemViewModel() { LineOne = "203", LineTwo = "Carson"});
            this.Items.Add(new ItemViewModel() { LineOne = "204", LineTwo = "Cardinal"});
            this.Items.Add(new ItemViewModel() { LineOne = "205", LineTwo = "Gouin"});
            this.Items.Add(new ItemViewModel() { LineOne = "206", LineTwo = "Roger-Pilon"});
            this.Items.Add(new ItemViewModel() { LineOne = "207", LineTwo = "Jacques-Bizard"});
            this.Items.Add(new ItemViewModel() { LineOne = "208", LineTwo = "Brunswick"});
            this.Items.Add(new ItemViewModel() { LineOne = "209", LineTwo = "Des Sources"});
            this.Items.Add(new ItemViewModel() { LineOne = "211", LineTwo = "Bord-du-Lac"});
            this.Items.Add(new ItemViewModel() { LineOne = "212", LineTwo = "Sainte-Anne"});
            this.Items.Add(new ItemViewModel() { LineOne = "213", LineTwo = "Parc-Industriel-Saint-Laurent"});
            this.Items.Add(new ItemViewModel() { LineOne = "215", LineTwo = "Henri-Bourassa"});
            this.Items.Add(new ItemViewModel() { LineOne = "216", LineTwo = "Transcanadienne"});
            this.Items.Add(new ItemViewModel() { LineOne = "217", LineTwo = "Anse-à-l'Orme"});
            this.Items.Add(new ItemViewModel() { LineOne = "218", LineTwo = "Antoine-Faucon"});
            this.Items.Add(new ItemViewModel() { LineOne = "219", LineTwo = "Chemin Sainte-Marie"});
            this.Items.Add(new ItemViewModel() { LineOne = "220", LineTwo = "Kieran"});
            this.Items.Add(new ItemViewModel() { LineOne = "225", LineTwo = "Hymus"});
            this.Items.Add(new ItemViewModel() { LineOne = "252", LineTwo = "Navette Or - Montréal-Nord"});
            this.Items.Add(new ItemViewModel() { LineOne = "253", LineTwo = "Navette Or - Saint-Michel"});
            this.Items.Add(new ItemViewModel() { LineOne = "254", LineTwo = "Navette Or - Rosemont"});
            this.Items.Add(new ItemViewModel() { LineOne = "256", LineTwo = "Navette Or LaSalle"});
            this.Items.Add(new ItemViewModel() { LineOne = "257", LineTwo = "Navette Or Rivières-des-Prairies"});
            this.Items.Add(new ItemViewModel() { LineOne = "258", LineTwo = "Navette Or Hochelaga-Maisonneuve"});
            this.Items.Add(new ItemViewModel() { LineOne = "259", LineTwo = "Navette Or Mercier-Ouest"});
            this.Items.Add(new ItemViewModel() { LineOne = "260", LineTwo = "Navette Or Anjou"});
            this.Items.Add(new ItemViewModel() { LineOne = "262", LineTwo = "Navette Or CôteSaint-Luc"});
            this.Items.Add(new ItemViewModel() { LineOne = "263", LineTwo = "Navette Or Bordeaux-Cartierville"});
            this.Items.Add(new ItemViewModel() { LineOne = "350", LineTwo = "Verdun/LaSalle"});
            this.Items.Add(new ItemViewModel() { LineOne = "353", LineTwo = "Lacordaire/Maurice-Duplessis"});
            this.Items.Add(new ItemViewModel() { LineOne = "354", LineTwo = "Sainte-Anne-de-Bellevue/Centre-Ville"});
            this.Items.Add(new ItemViewModel() { LineOne = "355", LineTwo = "Pie-IX"});
            this.Items.Add(new ItemViewModel() { LineOne = "356", LineTwo = "Lachine/Mtl-Trudeau/Des Sources"});
            this.Items.Add(new ItemViewModel() { LineOne = "357", LineTwo = "Saint-Michel"});
            this.Items.Add(new ItemViewModel() { LineOne = "358", LineTwo = "Sainte-Catherine"});
            this.Items.Add(new ItemViewModel() { LineOne = "359", LineTwo = "Papineau"});
            this.Items.Add(new ItemViewModel() { LineOne = "360", LineTwo = "Avenue des Pins"});
            this.Items.Add(new ItemViewModel() { LineOne = "361", LineTwo = "Saint-Denis"});
            this.Items.Add(new ItemViewModel() { LineOne = "362", LineTwo = "Hochelaga/Notre-Dame"});
            this.Items.Add(new ItemViewModel() { LineOne = "363", LineTwo = "Boulevard Saint-Laurent"});
            this.Items.Add(new ItemViewModel() { LineOne = "364", LineTwo = "Sherbrooke/Joseph-Renaud"});
            this.Items.Add(new ItemViewModel() { LineOne = "365", LineTwo = "Avenue du Parc"});
            this.Items.Add(new ItemViewModel() { LineOne = "368", LineTwo = "Mont-Royal"});
            this.Items.Add(new ItemViewModel() { LineOne = "369", LineTwo = "Côte-des-Neiges"});
            this.Items.Add(new ItemViewModel() { LineOne = "370", LineTwo = "Rosemont"});
            this.Items.Add(new ItemViewModel() { LineOne = "371", LineTwo = "Décarie"});
            this.Items.Add(new ItemViewModel() { LineOne = "372", LineTwo = "Jean-Talon"});
            this.Items.Add(new ItemViewModel() { LineOne = "376", LineTwo = "Pierrefonds/Centre-Ville"});
            this.Items.Add(new ItemViewModel() { LineOne = "378", LineTwo = "Sauvé/Côte-Vertu/Mtl-Trudeau"});
            this.Items.Add(new ItemViewModel() { LineOne = "380", LineTwo = "Henri-Bourassa"});
            this.Items.Add(new ItemViewModel() { LineOne = "382", LineTwo = "Pierrefonds/St-Charles" });
            this.Items.Add(new ItemViewModel() { LineOne = "401", LineTwo = "Express Saint-Charles"});
            this.Items.Add(new ItemViewModel() { LineOne = "405", LineTwo = "Express Bord-Du-Lac - New"});
            this.Items.Add(new ItemViewModel() { LineOne = "406", LineTwo = "Express Newman  "});
            this.Items.Add(new ItemViewModel() { LineOne = "407", LineTwo = "Express Île-Bizard"});
            this.Items.Add(new ItemViewModel() { LineOne = "409", LineTwo = "Express Des Sources"});
            this.Items.Add(new ItemViewModel() { LineOne = "410", LineTwo = "Express Notre-Dame"});
            this.Items.Add(new ItemViewModel() { LineOne = "411", LineTwo = "Express Lionel-Groulx"});
            this.Items.Add(new ItemViewModel() { LineOne = "419", LineTwo = "Express John Abbott"});
            this.Items.Add(new ItemViewModel() { LineOne = "420", LineTwo = "Express Notre-Dame-de-Grâce"});
            this.Items.Add(new ItemViewModel() { LineOne = "425", LineTwo = "Express Anse-à-L'Orme - New"});
            this.Items.Add(new ItemViewModel() { LineOne = "427", LineTwo = "Express Saint-Joseph"});
            this.Items.Add(new ItemViewModel() { LineOne = "430", LineTwo = "Express Pointe-aux-Trembles"});
            this.Items.Add(new ItemViewModel() { LineOne = "432", LineTwo = "Express Lacordaire"});
            this.Items.Add(new ItemViewModel() { LineOne = "435", LineTwo = "Express Du Parc/Côte-des-Neiges"});
            this.Items.Add(new ItemViewModel() { LineOne = "439", LineTwo = "Express Pie-IX"});
            this.Items.Add(new ItemViewModel() { LineOne = "440", LineTwo = "Express Charleroi"});
            this.Items.Add(new ItemViewModel() { LineOne = "444", LineTwo = "Express Cégep Marie-Victorin"});
            this.Items.Add(new ItemViewModel() { LineOne = "448", LineTwo = "Express Maurice-Duplessis"});
            this.Items.Add(new ItemViewModel() { LineOne = "449", LineTwo = "Express Rivière-des-Prairies"});
            this.Items.Add(new ItemViewModel() { LineOne = "460", LineTwo = "Express Métropolitaine"});
            this.Items.Add(new ItemViewModel() { LineOne = "467", LineTwo = "Express Saint-Michel"});
            this.Items.Add(new ItemViewModel() { LineOne = "468", LineTwo = "Express Pierrefonds/Gouin"});
            this.Items.Add(new ItemViewModel() { LineOne = "469", LineTwo = "Express Henri-Bourassa"});
            this.Items.Add(new ItemViewModel() { LineOne = "470", LineTwo = "Express Pierrefonds"});
            this.Items.Add(new ItemViewModel() { LineOne = "475", LineTwo = "Express Dollard-des-Ormeaux - New"});
            this.Items.Add(new ItemViewModel() { LineOne = "485", LineTwo = "Express Antoine-Faucon - New  "});
            this.Items.Add(new ItemViewModel() { LineOne = "486", LineTwo = "Express Sherbrooke"});
            this.Items.Add(new ItemViewModel() { LineOne = "487", LineTwo = "Express Bout-de-l'Île"});
            this.Items.Add(new ItemViewModel() { LineOne = "491", LineTwo = "Express Lachine"});
            this.Items.Add(new ItemViewModel() { LineOne = "495", LineTwo = "Express Lachine/LaSalle"});
            this.Items.Add(new ItemViewModel() { LineOne = "496", LineTwo = "Express Victoria"});
            this.Items.Add(new ItemViewModel() { LineOne = "715", LineTwo = "Vieux-Montréal / Vieux-Port"});
            this.Items.Add(new ItemViewModel() { LineOne = "747", LineTwo = "Aéroport P-E-Trudeau / Centre-Ville"});
            this.Items.Add(new ItemViewModel() { LineOne = "767", LineTwo = "La Ronde/La Plage (Seasonal bus service )"});
            this.Items.Add(new ItemViewModel() { LineOne = "769", LineTwo = "La Ronde (Seasonal bus service)"});
            this.Items.Add(new ItemViewModel() { LineOne = "777", LineTwo = "Le Casino" });

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}