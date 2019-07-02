using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFSample20190702.Model;
using XFSample20190702.View;
using Xamarin.Essentials;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XFSample20190702
{
    public partial class App : Application
    {
        public static DShell.DisplayType CurrentDisplayType = Constants.DEFAULT_DISPLAY_TYPE;
        public static List<DPageDefinition> pageDefinitions;
        public static double TitleImageRightMargin = 0;
        public App()
        {
            InitializeComponent();


            pageDefinitions = new List<DPageDefinition>();
            //
            // Using Xamarin Forms 4.0 Shell
            // ------Insert your page here, DShell will handle it------
            //
            pageDefinitions.Add(new DPageDefinition("Calander", "Calendar_POS.png", typeof(CalendarPage)));

            // Sample for sub pages
            List<DPageDefinition> todoListSubPages = new List<DPageDefinition>();
            todoListSubPages.Add(new DPageDefinition("Todo", "Check_POS.png", typeof(TodoListPage)));
            todoListSubPages.Add(new DPageDefinition("Completed", "Check_POS.png", typeof(TodoListCompletePage)));
            pageDefinitions.Add(new DPageDefinition("Todo List", "Check_POS.png", null, todoListSubPages));

            pageDefinitions.Add(new DPageDefinition("Notifications", "Commenting_POS.png", typeof(NotificationsPage)));
            pageDefinitions.Add(new DPageDefinition("Settings", "Cog_POS.png", typeof(SettingsPage)));
            //
            // ------End of page definition------
            //

            FetchSaves();

            MainPage = new DShell(pageDefinitions, CurrentDisplayType);
        }

        // If the app is not the first time open, there should be cache for right margin of title image view
        private async void FetchSaves()
        {
            string titleImageRightMarginSaveString = await SecureStorage.GetAsync(Constants.TITLE_IMAGE_RIGHT_MARGIN_TAG);
            if (!string.IsNullOrEmpty(titleImageRightMarginSaveString))
            {
                TitleImageRightMargin = double.Parse(titleImageRightMarginSaveString);
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
