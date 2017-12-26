using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace EzTimeAndroid
{
    public class ListAdapter : BaseAdapter<Hour>
    {
        List<Hour> items;
        Activity context;
        public ListAdapter(Activity context, List<Hour> items)
            : base()
        {

            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override Hour this[int position]
        {
            get {
                return items[position];
            }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (position < items.Count)
            {
                var item = items[position];

                View view = convertView;
                if (view == null) // no view to re-use, create new
                    view = context.LayoutInflater.Inflate(Resource.Layout.HoursRow, null);

                view.FindViewById<TextView>(Resource.Id.monthDay).Text = item.Monthday;
                view.FindViewById<TextView>(Resource.Id.weekday).Text = item.weekday;
                view.FindViewById<TextView>(Resource.Id.hourLine).Text = item.HourLine;


                return view;
            } else
            {
                return null;
            }
        }
    }
}