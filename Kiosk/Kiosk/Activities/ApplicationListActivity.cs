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
using Kiosk.Adapter;
using Kiosk.Helper;
using Kiosk.Model;

namespace Kiosk.Activities
{
    [Activity(Label = "ApplicationList")]
    public class ApplicationListActivity : Activity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ApplicationListView);
            InitializeComponents();
        }

        public List<AppInfoSelected> AppInfoSelected { get; set; }




        private void InitializeComponents()
        {
            ListView listViewApps = FindViewById<ListView>(Resource.Id.listApplicationsInstalled);
            AppInfoSelected = new List<AppInfoSelected>(Helper.PackageManagerHelper.GetApplicationsInDevice(this));
            listViewApps.Adapter = new ListApplicationAdapter(this,AppInfoSelected);
        }

        public override void OnBackPressed()
        {
           var data = new List<AppInfo>(AppInfoSelected.Where(x=>x.Selected));
            Global.AppInfoList = data;
            base.OnBackPressed();
        }
    }
}