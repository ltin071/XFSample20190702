using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFSample20190702.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : DContentPage
	{
		public SettingsPage ()
		{
			InitializeComponent ();
            DisplayModePicker.SelectedIndex = (int)App.CurrentDisplayType;
        }

        private async void DisplayModePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if ((int)App.CurrentDisplayType == selectedIndex)
                return;

            bool confirm = await DisplayAlert("Confirm", "Confirm switch display mode?", "OK", "Cancel");
            if (confirm)
            {
                App.CurrentDisplayType = (DShell.DisplayType)selectedIndex;
                App.Current.MainPage = new DShell(App.pageDefinitions, App.CurrentDisplayType);
            }
            else
            {
                picker.SelectedIndex = (int)App.CurrentDisplayType;
            }
        }
    }
}