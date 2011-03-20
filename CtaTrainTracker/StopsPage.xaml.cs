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
using System.Windows.Navigation;
using RestSharp;
using CtaTrainTracker.ViewModels;

namespace CtaTrainTracker
{
    public partial class Routes : PhoneApplicationPage
    {
        public Routes()
        {
            InitializeComponent();
        }


        /// </summary>
        private void StopList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // if an item is selected
            if (StopList.SelectedIndex != -1)
            {
                // get the currently selected city and pass the information to the 
                // forecast details page
                stop stop = (stop)StopList.SelectedItem;
                this.NavigationService.Navigate(new Uri("/ArrivalsPage.xaml?StopId=" + stop.StopId + "&StopName=" + stop.Name, UriKind.Relative));
            }

        }
        protected override void OnNavigatedFrom(NavigationEventArgs args)
        {
            // make sure no item is highlighted in the list of cities
            StopList.SelectedIndex = -1;
            StopList.SelectedItem = null;
        }
        private static bool DataLoaded = false;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //sif (DataLoaded) return;
            string routeId = this.NavigationContext.QueryString["RouteId"];
            var request = new RestRequest();
            request.Resource = "cta/stops/" + routeId;
            Loading.Text = "Loading...";


            App.client().ExecuteAsync<List<stop>>(request, response =>
            {
                StopList.ItemsSource = response.Data;
                Loading.Text = "";
                DataLoaded = true;
            });
        }
    }
}