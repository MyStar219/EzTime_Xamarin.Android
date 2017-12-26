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
using Newtonsoft.Json;
namespace EzTimeAndroid
{
    [Activity(Label = "מערכת דיווח שעות", Icon = "@drawable/icon")]
    public class HoursListActivity : Activity
    {
        int ii_month, ii_year;
        DateTime dt_current;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Global GlobalFunc = new Global();
            base.OnCreate(savedInstanceState);
            ISharedPreferences prefs = Application.Context.GetSharedPreferences("PREF", FileCreationMode.Private);
            SetContentView(Resource.Layout.HoursList);
            ListView HoursList = FindViewById<ListView>(Resource.Id.hourslist);
            TextView repmonth = FindViewById<TextView>(Resource.Id.repmonth);
            Button search = FindViewById<Button>(Resource.Id.search);
            Button next = FindViewById<Button>(Resource.Id.nextmonth);
            Button prev = FindViewById<Button>(Resource.Id.prevmonth);
            ii_month = DateTime.Now.Month;
            ii_year = DateTime.Now.Year;
            dt_current = new DateTime(ii_year, ii_month, 1);
            prev.Text = " < "; next.Text = " > ";
            repmonth.Text = "חודש:  " + dt_current.ToString("MM/yyyy");
            HoursList.ScrollingCacheEnabled = false;
            
            search.Click += async (object sender, EventArgs e) =>
            {
                //string url = "http://eztime-001-site1.etempurl.com/webservices/wshours.aspx?e=" + prefs.GetInt("EmpID", 0) + "&m=" + dt_current.ToString("MM") +"&y=" + dt_current.ToString("yyyy");
                string url = "http://412.co.il/webservices/wshours.aspx?e=" + prefs.GetInt("EmpID", 0) + "&m=" + dt_current.ToString("MM") + "&y=" + dt_current.ToString("yyyy");
                string s_json = await GlobalFunc.FetchAsync(url);
                
                var json = JsonConvert.DeserializeObject<HoursRecord>(s_json);
                if (json.hours.Count > 0)
                {
                    HoursList.Adapter = new ListAdapter(this, json.hours);
                } else
                {
                    Toast.MakeText(this, "לא דווחו שעות עבור החודש.", Android.Widget.ToastLength.Long).Show();
                }
            };
            prev.Click += (object sender, EventArgs e) =>
            {
                dt_current = dt_current.AddMonths(-1); repmonth.Text = "חודש:  " + dt_current.ToString("MM/yyyy");
                search.PerformClick();
            };

            next.Click += (object sender, EventArgs e) =>
            {
                dt_current = dt_current.AddMonths(1); repmonth.Text = "חודש:  " + dt_current.ToString("MM/yyyy");
                search.PerformClick();
            };

            search.PerformClick();

        }

    }
   
    public class HoursRecord
    {     
        public List<Hour> hours { get; set; }
      
    }

    public class Hour
    {
        [JsonProperty("GregDate")]
        public DateTime GregDate { get; set; }
        [JsonProperty("MonthDay")]
        public string Monthday { get; set; }
        [JsonProperty("weekday")]
        public string weekday { get; set; }
        [JsonProperty("daytype")]
        public int daytype { get; set; }
        [JsonProperty("HourType")]
        public string HourType { get; set; }
        [JsonProperty("HourLine")]
        public string HourLine { get; set; }
    }

}