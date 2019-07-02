using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using NavigationPage = Xamarin.Forms.NavigationPage;
using VisualElement = Xamarin.Forms.VisualElement;

namespace XFSample20190702.View
{
    public class DContentPage : ContentPage
    {
        private Image TitleImage;
        public DContentPage()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            
            // Add Title image to the title view
            TitleImage = new Image() { HeightRequest = 40, Opacity = 0, Source = "myicon.png", VerticalOptions = LayoutOptions.CenterAndExpand};
            StackLayout titleImageFrame = new StackLayout();
            titleImageFrame.Children.Add(TitleImage);
            Shell.SetTitleView(this, titleImageFrame);

            //Check if the app is not first open and already have cached the calculated position of title image
            if (App.TitleImageRightMargin == 0)
            {
                Device.StartTimer(new System.TimeSpan(100), () =>
                {
                    while (!AdjustTitleImage())
                    {
                        return true;
                    }
                    return false;
                });
            }
            else
            {
                //Not first open, use the cached value for title image
                TitleImage.Margin = new Thickness(0, 0, App.TitleImageRightMargin, 0);
                TitleImage.Opacity = 1; //Turn on the image
            }
        }
        
        //
        // To calculate right margin of the title image to compansate the burger menu at the left hand side
        //
        private bool AdjustTitleImage()
        {
            var titleView = Shell.GetTitleView(this);
            var titleViewBound = titleView.Bounds;
            if (titleViewBound.Width == -1)
            {
                //The layout still not yet render, exit from execution
                return false;
            }
            
            //Get display information using xamarin essential
            DisplayInfo displayInfo = DeviceDisplay.MainDisplayInfo;
            double displayWidth = displayInfo.Width / displayInfo.Density;

            double marginWidth = 0;
            if(Device.RuntimePlatform == Device.iOS)
            {
                marginWidth = 10; //FIXME: please don't hardcode 10 margin
            }

            //Calculate right margin needed by calculating the burger menu button width
            double leftButtonWidth = displayWidth - titleViewBound.Width - marginWidth * 2; 
            
            TitleImage.Margin = new Thickness(0, 0, leftButtonWidth, 0);
            TitleImage.Opacity = 1; //Turn on the Title image
            App.TitleImageRightMargin = leftButtonWidth;

            //Save the value for future use, no need to go through this calculation again
            SecureStorage.SetAsync(Constants.TITLE_IMAGE_RIGHT_MARGIN_TAG, App.TitleImageRightMargin.ToString());
            return true;
        }
    }
}
