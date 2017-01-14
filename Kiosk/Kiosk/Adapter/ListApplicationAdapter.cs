using System.Collections.Generic;
using Android.App;
using Android.Provider;
using Android.Views;
using Android.Widget;
using Kiosk.Model;

namespace Kiosk.Adapter
{
    public class ListApplicationAdapter : BaseAdapter<AppInfoSelected>
    {
        Activity _context;
        List<AppInfoSelected> _list;

        public ListApplicationAdapter(Activity context, List<AppInfoSelected> list) : base()
        {
            _context = context;
            _list = list;
        }
        #region BaseAdapter
        public override long GetItemId(int position)
        {
            return position;
        }


        public override int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public override AppInfoSelected this[int position]
        {
            get { return _list[position]; }
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            // re-use an existing view, if one is available
            // otherwise create a new one
            if (view == null)
                view = _context.LayoutInflater.Inflate(Resource.Layout.ListApplicationCell, parent, false);

            AppInfoSelected item = this[position];
            view.FindViewById<TextView>(Resource.Id.Title).Text = item.Name;

            var imageView = view.FindViewById<ImageView>(Resource.Id.Icon);
            imageView.SetImageDrawable(item.Icon);
            var chk  = view.FindViewById<CheckBox>(Resource.Id.chkSelected);
            chk.Checked = item.Selected;

            return view;
        }

        #endregion
    }
}