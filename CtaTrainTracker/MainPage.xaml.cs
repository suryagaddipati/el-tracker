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
using CtaTrainTracker.ViewModels;
using System.Windows.Navigation;
using System.ComponentModel;

namespace CtaTrainTracker
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
            App.ViewModel.PropertyChanged += new PropertyChangedEventHandler(this.Notifychanges);

        }

      
        void Notifychanges(object sender, PropertyChangedEventArgs e)
        {
            Loading.Text = "";
        }
        /// </summary>
        private void RouteList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // if an item is selected
            if (RouteList.SelectedIndex != -1)
            {
                // get the currently selected city and pass the information to the 
                // forecast details page
                route route = (route)RouteList.SelectedItem;
                this.NavigationService.Navigate(new Uri("/StopsPage.xaml?RouteId=" + route.RouteId, UriKind.Relative));
            }

        }

        protected override void OnNavigatedFrom(NavigationEventArgs args)
        {
            // make sure no item is highlighted in the list of cities
            RouteList.SelectedIndex = -1;
            RouteList.SelectedItem = null;
        }
        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }
    }
}