using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using System.Xml.Linq;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Re.Common;
using Re.Model;
using Re.ViewModels;

namespace Re
{

    public partial class Results : PhoneApplicationPage
    {

        ResultViewModel _viewModel;
        static List<Keyword> queryList = new List<Keyword>();
        const int BUFFER_SIZE = 1024;
        private string _searchTerm = "";

        public Results()
        {
            InitializeComponent();
            _viewModel = (ResultViewModel)Resources["viewModel"];
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var progressIndicator = SystemTray.ProgressIndicator;
            if (progressIndicator != null)
            {
                return;
            }

            progressIndicator = new ProgressIndicator();

            SystemTray.SetProgressIndicator(this, progressIndicator);

            Binding binding = new Binding("IsLoading") { Source = _viewModel };
            BindingOperations.SetBinding(
                progressIndicator, ProgressIndicator.IsVisibleProperty, binding);

            binding = new Binding("IsLoading") { Source = _viewModel };
            BindingOperations.SetBinding(
                progressIndicator, ProgressIndicator.IsIndeterminateProperty, binding);

            progressIndicator.Text = "Loading search results";

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string query = NavigationContext.QueryString["query"];

            // This only gets called when navigated to from the main page
            // To ensure that users aren't searching for weird things that they don't expect, we need to clear the list
            queryList.Clear();

            foreach (string word in query.Split(' '))
            {
                queryList.Add(new Keyword(word, 2));
            }

            // Reset the query because we're going to modify it
            query = "";

            foreach (Keyword k in queryList.Take(7))
            {
                query += k.Word + " ";
            }

            _searchTerm = Uri.EscapeDataString(query);

            _viewModel.FetchSearchResults(_searchTerm);
            // gvwQuery.ItemsSource = queryList.Take(7); // This is the databinding for the query text
        }
    }
}