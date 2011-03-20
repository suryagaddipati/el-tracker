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
using CtaTrainTracker.ViewModels;
using RestSharp;


namespace CtaTrainTracker
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new List<route>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public List<route> Items { get; private set; }


        public string Loading { get;  set; }
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
           


            var request = new RestRequest();
            request.Resource = "cta/routes";

            

            App.client().ExecuteAsync<List<route>>(request, response =>
            {
                var resource = response.Data;

                Items = resource;
                Loading = "";

                NotifyPropertyChanged("Items");
                NotifyPropertyChanged("Loading");

            });


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