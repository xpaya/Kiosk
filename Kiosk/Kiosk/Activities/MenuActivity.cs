using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Kiosk.Activities
{
    [Activity(Label = "MenuActivity")]
    public class MenuActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MainView);
       //     InitializeComponents();
        }

        private void InitializeComponents()
        {
            Button btnConfig = this.FindViewById<Button>(Resource.Id.btnConfig);
            if (btnConfig != null)
            {
                btnConfig.Click+=BtnConfigOnClick;
            }
        }

        private void BtnConfigOnClick(object sender, EventArgs eventArgs)
        {
            Intent inApplicationListActivity = new Intent(this, typeof(ApplicationListActivity));
            StartActivity(inApplicationListActivity);
        }
    }
}