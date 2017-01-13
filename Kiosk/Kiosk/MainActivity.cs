using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Kiosk.Activities;

namespace Kiosk
{
    [Activity(Label = "Kiosk", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MyTheme.Splash")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            Intent inMenu = new Intent(this, typeof(MenuActivity));
            StartActivity(inMenu);
        }
    }
}

