using System.Collections.Generic;
using Android.App;
using Android.Provider;
using Android.Views;
using Android.Widget;
using Kiosk.Model;

namespace Kiosk.Adapter
{
    public class AppAdapter : BaseAdapter<AppInfo>
    {
        Activity _context;
        List<AppInfo> _list;

        public AppAdapter(Activity context, List<AppInfo> list)
        {
            _context = context;
            _list = list;
        }

        public override int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public override AppInfo this[int position]
        {
            get { return _list[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;


            // re-use an existing view, if one is available
            // otherwise create a new one
            if (view == null)
            {
                var rowHeight = _context.Resources.GetDimensionPixelSize(Resource.Dimension.AppCardHeightSize);
                var columnWidth = _context.Resources.GetDimensionPixelSize(Resource.Dimension.AppCardWithtSize);

                view = _context.LayoutInflater.Inflate(Resource.Layout.ListAppCellGridLayout, parent, false);


                view.LayoutParameters = new GridView.LayoutParams(rowHeight, columnWidth);

                
                AppInfo item = this[position];
                view.FindViewById<TextView>(Resource.Id.Titlegrd).Text = item.Name;
                var imageView = view.FindViewById<ImageView>(Resource.Id.Icongrd);


                imageView.SetImageDrawable(item.Icon);
            }

            return view;
        }

    }
}