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
    public partial class ArrivalsPage : PhoneApplicationPage
    {
        public ArrivalsPage()
        {
            InitializeComponent();
        }

        /// </summary>
        private void ArrivalList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // if an item is selected
            if (ArrivalList.SelectedIndex != -1)
            {
                // get the currently selected city and pass the information to the 
                // forecast details page
              //  arrival stop = (arrival)ArrivalList.SelectedItem;
               // this.NavigationService.Navigate(new Uri("/ArrivalsPage.xaml?RouteId=" + stop.StopId, UriKind.Relative));
            }

        }

        protected override void OnNavigatedFrom(NavigationEventArgs args)
        {
            // make sure no item is highlighted in the list of cities
            ArrivalList.SelectedIndex = -1;
            ArrivalList.SelectedItem = null;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string routeId = this.NavigationContext.QueryString["StopId"];
            string stopName = this.NavigationContext.QueryString["StopName"];
            var request = new RestRequest();
            request.Resource = "cta/arrivals/" + routeId;

            Loading.Text = "Loading...";
            PageTitle.Text = stopName + "- Arrivals";
            App.client().ExecuteAsync<List<arrival>>(request, response =>
            {
                var responseData = response.Data;
                if (responseData.Count == 0)
                {
                    responseData.Add(new arrival { Name = "No Arrivals", Due = "" });
                }
                ArrivalList.ItemsSource = responseData;
                
                Loading.Text = "";

            });
        }

    }
}