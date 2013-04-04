using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
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
        // Privates
        ResultViewModel _viewModel;
        static List<Keyword> queryList = new List<Keyword>();
        const int BUFFER_SIZE = 1024;
        private string _searchTerm = "";

        // ApplicationBar buttons
        ApplicationBarIconButton resultMultiSelectButton;
        ApplicationBarIconButton reSearchButton;
        //ApplicationBarIconButton openMultiPageButton; -- NYI

        public Results()
        {
            InitializeComponent();
            _viewModel = (ResultViewModel)Resources["viewModel"];
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            CreateSelectionResultsApplicationBar();
            OnResultsActivated();
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

        private void lbxOutput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateApplicationBar();
        }

        private void OnGridSelectorIsSelectionEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SetupSelectionApplicationBar();
        }

        // Setup the application bar
        #region AppBar
        private void CreateSelectionResultsApplicationBar()
        {
            reSearchButton = new ApplicationBarIconButton();
            reSearchButton.IconUri = new Uri("/Toolkit.Content/ApplicationBar.Check.png", UriKind.RelativeOrAbsolute);
            reSearchButton.Text = "Re:search";
            reSearchButton.Click += reSearchButton_Click;

            resultMultiSelectButton = new ApplicationBarIconButton();
            resultMultiSelectButton.IconUri = new Uri("/Toolkit.Content/ApplicationBar.Select.png", UriKind.RelativeOrAbsolute);
            resultMultiSelectButton.Text = "select";
            resultMultiSelectButton.Click += resultMultiSelectButton_Click;

            /*
            openMultiPageButton = new ApplicationBarIconButton();
            openMultiPageButton.IconUri = new Uri("/Toolkit.Content/ApplicationBar.Upload.png", UriKind.RelativeOrAbsolute);
            openMultiPageButton.Text = "open";
            openMultiPageButton.Click += openMultiPageButton_Click;
             */

        }

        private void OnResultsActivated()
        {
            if (lbxOutput.IsSelectionEnabled)
            {
                lbxOutput.IsSelectionEnabled = false; // Will also update the Application Bar
            }
            else
            {
                SetupSelectionApplicationBar();
            }
        }

        private void SetupSelectionApplicationBar()
        {
            ClearApplicationBar();
            if (lbxOutput.IsSelectionEnabled)
            {
                ApplicationBar.Buttons.Add(reSearchButton);
                //ApplicationBar.Buttons.Add(openMultiPageButton);
                UpdateApplicationBar();
            }
            else
            {
                ApplicationBar.Buttons.Add(resultMultiSelectButton);
            }
            ApplicationBar.IsVisible = true;
        }

        void UpdateApplicationBar()
        {
            reSearchButton.IsEnabled = ((lbxOutput.SelectedItems != null) && (lbxOutput.SelectedItems.Count > 0));
            //openMultiPageButton.IsEnabled = ((lbxOutput.SelectedItems != null) && (lbxOutput.SelectedItems.Count > 0));
        }

        void openMultiPageButton_Click(object sender, EventArgs e)
        {
            LaunchWebSelection();
        }

        private async Task LaunchWebSelection()
        {
            bool success = false;
            foreach (WebResult result in lbxOutput.SelectedItems)
            {
                success = await Windows.System.Launcher.LaunchUriAsync(new Uri(@result.Url));
                if (success)
                    Debug.WriteLine("Success");
                else
                    Debug.WriteLine("Failure");
            }
        }

        async void reSearchButton_Click(object sender, EventArgs e)
        {
            string searchTerms = await ResultViewModel.CallAsync(new Uri("http://www.google.com", UriKind.Absolute));
        }

        void resultMultiSelectButton_Click(object sender, EventArgs e)
        {
            lbxOutput.EnforceIsSelectionEnabled = true;
        }

        void ClearApplicationBar()
        {
            while (ApplicationBar.Buttons.Count > 0)
            {
                ApplicationBar.Buttons.RemoveAt(0);
            }

            while (ApplicationBar.MenuItems.Count > 0)
            {
                ApplicationBar.MenuItems.RemoveAt(0);
            }
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (lbxOutput.IsSelectionEnabled)
            {
                lbxOutput.EnforceIsSelectionEnabled = false;
                e.Cancel = true;
            }
        }
        #endregion

        private void ResultItemSelected(object sender, System.Windows.Input.GestureEventArgs e)
        {
            StackPanel _r = (StackPanel)sender;
            WebResult _result = (WebResult)_r.DataContext;
            Windows.System.Launcher.LaunchUriAsync(new Uri(@_result.Url));
        }
    }
}