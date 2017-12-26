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
using Android.Locations;
using Android.Util;

namespace EzTimeAndroid
{
    [Activity(Label = "מערכת דיווח שעות", Icon = "@drawable/icon")]
    public class HomeActivity : Activity, ILocationListener
    {
        Location _currentLocation;
        Button record_btn;
        Button myhours_btn;
        LocationManager locMgr;
        ISharedPreferences prefs;
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Global GlobalFunc = new Global();
            base.OnCreate(savedInstanceState);
            
            prefs = Application.Context.GetSharedPreferences("PREF", FileCreationMode.Private);
            SetContentView(Resource.Layout.home);
            TextView welcome = FindViewById<TextView>(Resource.Id.welcome_txt);
           
            //Initializing button from layout
            record_btn = FindViewById<Button>(Resource.Id.record_btn);
            myhours_btn = FindViewById<Button>(Resource.Id.myhours_btn);
           
            record_btn.Click += async (object sender, EventArgs e) => {
                //string url = "http://eztime-001-site1.etempurl.com/webservices/wsrecord.aspx?e=" + prefs.GetString("EmpNo","") + "&lng=0" /* + _currentLocation.Longitude.ToString()*/ + "&lat=0" /*+ _currentLocation.Latitude.ToString()*/;
                string url = "http://412.co.il/webservices/wsrecord.aspx?e=" + prefs.GetString("EmpNo", "") + "&lng=0" /* + _currentLocation.Longitude.ToString()*/ + "&lat=0" /*+ _currentLocation.Latitude.ToString()*/;
                string s_result = await GlobalFunc.FetchAsync(url);
                if (s_result == "1")
                {
                    Toast.MakeText(this, "הדיווח נקלט בהצלחה!", Android.Widget.ToastLength.Long).Show();
                }
            };
            myhours_btn.Click += (object sender, EventArgs e) => { StartActivity(typeof(HoursListActivity)); };
            welcome.Text = "ברוך הבא, " + prefs.GetString("EmpFirstName", null) + " " + prefs.GetString("EmpLastName", null);
            
        }
        protected override void OnResume()
        {
            base.OnResume();

            // initialize location manager
            locMgr = GetSystemService(Context.LocationService) as LocationManager;

           // record_btn.Click += delegate {
                

                // pass in the provider (GPS), 
                // the minimum time between updates (in seconds), 
                // the minimum distance the user needs to move to generate an update (in meters),
                // and an ILocationListener (recall that this class impletents the ILocationListener interface)
              /*   if (locMgr.AllProviders.Contains(LocationManager.NetworkProvider)
                     && locMgr.IsProviderEnabled(LocationManager.NetworkProvider))
                 {
                     locMgr.RequestLocationUpdates(LocationManager.NetworkProvider, 0, 0, this);
                 }
                 else
                 {
                     Toast.MakeText(this, "The Network Provider does not exist or is not enabled!", ToastLength.Long).Show();
                 }*/
              /*  var locationCriteria = new Criteria();

				locationCriteria.Accuracy = Accuracy.Coarse;
				locationCriteria.PowerRequirement = Power.Medium;

				string locationProvider = locMgr.GetBestProvider(locationCriteria, true);

				
				locMgr.RequestLocationUpdates (locationProvider, 0, 0, this);
                */
               // _currentLocation = locMgr.GetLastKnownLocation(LocationManager.NetworkProvider);

                /*
                 string url = "http://eztime-001-site1.etempurl.com/webservices/wsrecord.aspx?e=" + prefs.GetString("EmpNo", "") + "&lng=" + _currentLocation.Longitude.ToString() + "&lat=" + _currentLocation.Latitude.ToString();
                 Toast.MakeText(this, prefs.GetString("EmpNo", "") + "&lng=" + _currentLocation.Longitude.ToString() + "&lat=" + _currentLocation.Latitude.ToString(), Android.Widget.ToastLength.Long).Show();
                 string s_result = await GlobalFunc.FetchAsync(url);
                 if (s_result == "1")
                 {
                     Toast.MakeText(this, "הדיווח נקלט בהצלחה!", Android.Widget.ToastLength.Long).Show();
                 }
                 */
                // Comment the line above, and uncomment the following, to test 
                // the GetBestProvider option. This will determine the best provider
                // at application launch. Note that once the provide has been set
                // it will stay the same until the next time this method is called

                /*var locationCriteria = new Criteria();

				locationCriteria.Accuracy = Accuracy.Coarse;
				locationCriteria.PowerRequirement = Power.Medium;

				string locationProvider = locMgr.GetBestProvider(locationCriteria, true);

				Log.Debug(tag, "Starting location updates with " + locationProvider.ToString());
				locMgr.RequestLocationUpdates (locationProvider, 2000, 1, this);*/
           // };
        }

        public void OnLocationChanged(Android.Locations.Location location)
        {
            _currentLocation = location;
           

        }
        public void OnProviderDisabled(string provider)
        {
           
        }
        public void OnProviderEnabled(string provider)
        {
           
        }
        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
           
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add(0, 0, 0, "כניסה/יציאה");
            menu.Add(0, 1, 1, "שעות שלי");
          
            return true;
        }

        protected override void OnPause()
        {
            base.OnPause();
            locMgr.RemoveUpdates(this);
        }
        
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case 0:
                    record_btn.PerformClick();
                    return true;
                case 1:
                    StartActivity(typeof(HoursListActivity));
                    return true;
               
                default:
                    return false;
            }
        }
    }
}