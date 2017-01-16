using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Kiosk.Adapter;
using Kiosk.Common;
using Kiosk.Model;
using Orientation = Android.Content.Res.Orientation;

namespace Kiosk.Activities
{
    [Activity(Label = "MenuActivity")]
    public class MenuActivity : FragmentActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MainView);
            InitializeComponents();
            SetContent();
        }

        public int AppsToShowForPage { get; set; }
        private void InitializeComponents()
        {
           
            if (BtnConfig != null)
            {
                BtnConfig.Click+=BtnConfigOnClick;
            }
        }
        //Navigate to configuration 
        private Button BtnConfig => this.FindViewById<Button>(Resource.Id.btnConfig);
        private void BtnConfigOnClick(object sender, EventArgs eventArgs)
        {
            Intent inApplicationListActivity = new Intent(this, typeof(ApplicationListActivity));
            StartActivity(inApplicationListActivity);
        }

        //Desktop 
        private ViewPager ContentPager => this.FindViewById<ViewPager>(Resource.Id.DeskTopViewPager);

        DesktopContentAdapter adapter;
        public int CurrentPage { get; set; }

        private void SetContent(bool orientationChanged = false)
        {
            ContentPager.Adapter = null;
         
            adapter = GetAdapter(this.Resources.Configuration.Orientation);
            if (!orientationChanged || adapter == null)
            {
                
                adapter = new DesktopContentAdapter(SupportFragmentManager);
                var rowHeight = Resources.GetDimensionPixelSize(Resource.Dimension.AppCardHeightSize);
                var columnWidth = Resources.GetDimensionPixelSize(Resource.Dimension.AppCardWithtSize);

                AppsToShowForPage = ScreenUtilities.CalculateNumberOfAppsForRows(this, BtnConfig.Height, 0, rowHeight, columnWidth);
                int index = 0;
                var lst = Helper.Global.AppInfoList.Count;
                if (Helper.Global.AppInfoList != null && AppsToShowForPage > 0)
                {
                    while (Helper.Global.AppInfoList.Count >= (index + 1) * AppsToShowForPage)
                    {
                        var index1 = index;
                        adapter.AddFragmentView((i, v, b) =>
                        {
                            var skip = index1 * AppsToShowForPage;
                            var view = i.Inflate(Resource.Layout.AppsFragmentLayout, v, false);
                            var grdview = view.FindViewById<GridView>(Resource.Id.DescktopAppsGrid);
                            grdview.ItemClick -= GrdviewOnItemClick;
                            grdview.ItemClick += GrdviewOnItemClick;
                            var apps = Helper.Global.AppInfoList.Skip(skip).Take(AppsToShowForPage).ToList();
                            grdview.Adapter = new AppAdapter(this, apps);

                            return view;
                        });
                        index++;
                    }

                    if (Helper.Global.AppInfoList.Count > index * AppsToShowForPage)
                    {
                        adapter.AddFragmentView((i, v, b) =>
                        {
                            var skip = index * AppsToShowForPage;
                            var view = i.Inflate(Resource.Layout.AppsFragmentLayout, v, false);
                            var grdview = view.FindViewById<GridView>(Resource.Id.DescktopAppsGrid);
                            grdview.ItemClick -= GrdviewOnItemClick;
                            grdview.ItemClick += GrdviewOnItemClick;
                            var apps =
                                Helper.Global.AppInfoList.Skip(skip)
                                    .Take(Helper.Global.AppInfoList.Count - index * AppsToShowForPage)
                                    .ToList();
                            grdview.Adapter = new AppAdapter(this,apps);
                            return view;
                        });
                    }
                }

                SetAdapter(this.Resources.Configuration.Orientation, adapter);
            }

            ContentPager.Adapter = adapter;
            ContentPager.SetCurrentItem(CurrentPage, false);
        }

        private void GrdviewOnItemClick(object sender, AdapterView.ItemClickEventArgs itemClickEventArgs)
        {
            GridView grd = sender as GridView;
            if (grd != null)
            {
                var aInfo = ((AppAdapter)grd.Adapter)[itemClickEventArgs.Position];
                LaunchApplication(aInfo.PackageName, this);
            }
        }
        public void LaunchApplication(string packageName, Context context)
        {
            Intent intent = context.PackageManager.GetLaunchIntentForPackage(packageName);
            if (intent != null)
            {
                context.StartActivity(intent);
            }
        }
        protected override void OnResume()
        {
            SetContent();
            base.OnResume();
        }

        private DesktopContentAdapter _landScape;
        private DesktopContentAdapter _portrait;

        private DesktopContentAdapter GetAdapter(Orientation orientation)
        {
            var adapter = _landScape;
            switch (orientation)
            {
                case Orientation.Landscape:
                    adapter = _landScape;
                    break;
                case Orientation.Portrait:
                    adapter = _portrait;
                    break;
            }
            return adapter;
        }

        private void SetAdapter(Orientation orientation, DesktopContentAdapter adapter)
        {
            switch (orientation)
            {
                case Orientation.Landscape:
                    _landScape = adapter;
                    break;
                case Orientation.Portrait:
                    _portrait = adapter;
                    break;
                default:
                    _landScape = adapter;
                    break;
            }
        }

    }
}