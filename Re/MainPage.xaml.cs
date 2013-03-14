using System;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Re.ViewModels;


namespace Re
{
    public partial class MainPage : PhoneApplicationPage
    {
        string _searchTerms = "";

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void onSearchTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            // Go to search results page

            _searchTerms = searchInputText.Text.Trim();

            if (String.IsNullOrEmpty(_searchTerms))
            {
                VibrateController.Default.Start(TimeSpan.FromMilliseconds(200));
                return;
            }

            NavigationService.Navigate(new Uri("/Results.xaml?query=" + _searchTerms, UriKind.Relative));
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _searchTerms = searchInputText.Text.Trim();

                if (String.IsNullOrEmpty(_searchTerms))
                {
                    VibrateController.Default.Start(TimeSpan.FromMilliseconds(200));
                    return;
                }

                this.Focus();

                NavigationService.Navigate(new Uri("/Results.xaml?query=" + searchInputText.Text.Trim(), UriKind.Relative));
            }
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}