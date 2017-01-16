using System;
using Android.Views;
using Android.Widget;
using Kiosk.Model;

namespace Kiosk.ViewHolder
{
    public class AppListViewHolder : Java.Lang.Object
    {
        public event EventHandler<EventHandler> ElementChanged;
        public AppInfoSelected AppInfo { get; set; }
        public ImageView Icon { get; set; }
        public TextView Title { get; set; }
        public CheckBox Selected { get; set; }

        public int Position { get; set; }
        public void Initialize(View view, int position)
        {
            Title = view.FindViewById<TextView>(Resource.Id.Title);
            Icon = view?.FindViewById<ImageView>(Resource.Id.Icon);
            Selected = view?.FindViewById<CheckBox>(Resource.Id.chkSelected);
            Position = position;

           Selected.CheckedChange += SelectedOnCheckedChange;
        }
        public void Bind(AppInfoSelected appInfo)
        {
            AppInfo = appInfo;
            Title.Text = AppInfo.Name;
            Selected.Checked = AppInfo.Selected;
            Icon.SetImageDrawable(AppInfo.Icon);
        }

        private void SelectedOnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs checkedChangeEventArgs)
        {
            if (AppInfo.Selected != Selected.Checked)
            {
                AppInfo.Selected = Selected.Checked;
                ElementChanged?.Invoke(AppInfo, null);
            }
        }

    }
}