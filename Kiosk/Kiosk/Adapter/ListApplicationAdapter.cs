using System;
using System.Collections.Generic;
using Android.App;
using Android.Provider;
using Android.Views;
using Android.Widget;
using Kiosk.Model;
using Kiosk.ViewHolder;

namespace Kiosk.Adapter
{
    public class ListApplicationAdapter : BaseAdapter<AppInfoSelected>
    {
        public event EventHandler<EventArgs> ElementChanged;
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

            var viewHolder = view.Tag as AppListViewHolder;

            if (viewHolder == null)
            {
                viewHolder = new AppListViewHolder();
                viewHolder.Initialize(view, position);
                viewHolder.ElementChanged += ViewHolderOnElementChanged;
                view.Tag = viewHolder;
            }

            var app = this[position];
            viewHolder.Bind(app);
            return view;
        }

        private void ViewHolderOnElementChanged(object sender, EventHandler eventHandler)
        {
            ElementChanged?.Invoke(sender, null);
        }

        #endregion
    }
}