using System;
using System.Collections.Generic;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Kiosk.Adapter.Fragments;

namespace Kiosk.Adapter
{
    public class DesktopContentAdapter: FragmentPagerAdapter
    {
        private FragmentManager Manager { get; set; }
        private List<Android.Support.V4.App.Fragment> _fragmentList = new List<Android.Support.V4.App.Fragment>();

      
        public DesktopContentAdapter(Android.Support.V4.App.FragmentManager fm): base(fm) { }



        public override int Count
        {
            get { return _fragmentList.Count; }
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return _fragmentList[position];
        }

        public void AddFragment(AppsFragment fragment)
        {
            _fragmentList.Add(fragment);
        }

        public void AddFragmentView(Func<LayoutInflater, ViewGroup, Bundle, View> view)
        {
            _fragmentList.Add(new AppsFragment(view));
        }
    }
}