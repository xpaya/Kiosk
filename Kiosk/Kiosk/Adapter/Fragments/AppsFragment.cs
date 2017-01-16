using System;
using Android.App;
using Android.OS;
using Android.Views;

namespace Kiosk.Adapter.Fragments
{
    public class AppsFragment : Android.Support.V4.App.Fragment
    {
        private Func<LayoutInflater, ViewGroup, Bundle, View> _view;

        public AppsFragment(Func<LayoutInflater, ViewGroup, Bundle, View> view)
        {
            _view = view;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return _view(inflater, container, savedInstanceState);
        }
    }
}