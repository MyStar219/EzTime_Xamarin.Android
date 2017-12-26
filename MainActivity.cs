using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using System.Net.Http.Headers;
using System.IO;
using System.Json;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace EzTimeAndroid
{
    [Activity(Label = "מערכת דיווח שעות", MainLauncher = true,  Icon = "@drawable/icon")]
   
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ISharedPreferences prefs = Application.Context.GetSharedPreferences("PREF", FileCreationMode.Private);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //Initializing button from layout
            Button login = FindViewById<Button>(Resource.Id.login);
            EditText username = FindViewById<EditText>(Resource.Id.userName);
            EditText psw = FindViewById<EditText>(Resource.Id.password);

            //Login button click action
            login.Click += async (object sender, EventArgs e) =>
            {
               
                Global GlobalFunc = new Global();
               // string url = "http://eztime-001-site1.etempurl.com/webservices/wsuser.aspx?u=" + username.Text + "&pw=" + psw.Text;
                string url = "http://412.co.il/webservices/wsuser.aspx?u=" + username.Text + "&pw=" + psw.Text;

                string s_json = await GlobalFunc.FetchAsync(url);


               
                
                if (s_json == "0")
                {

                    Toast.MakeText(this, "שם משתמש או סיסמא אינם נכונים!", Android.Widget.ToastLength.Long).Show();
                }
                else
                {
                    var json = JsonConvert.DeserializeObject<Record>(s_json);
                    ISharedPreferencesEditor editor = prefs.Edit();
                    editor.PutInt("UID",json.users[0].UserID);
                    editor.PutString("UserName", json.users[0].UserName);
                    editor.PutInt("UserTypeID", json.users[0].UserTypeID);
                    editor.PutInt("EmpID", json.users[0].EmpID);
                    editor.PutString("EmpNo", json.emp[0].EmpNo);
                    editor.PutInt("ClientID", json.emp[0].ClientID);
                    editor.PutInt("DeptID", json.emp[0].DeptID);
                    editor.PutString("ClientName", json.emp[0].ClientName);
                    editor.PutString("CompanyName", json.emp[0].CompanyName);
                    editor.PutString("EmpFirstName", json.emp[0].EmpFirstName);
                    editor.PutString("EmpLastName", json.emp[0].EmpLastName);
                    editor.PutString("DeptName", json.emp[0].DeptName);
                    editor.PutString("UPict", json.emp[0].pict);
                    editor.Commit();

                    StartActivity(typeof(HomeActivity));
                    /*User u = JsonConvert.DeserializeObject<User>(json.users);
                    Emp em = JsonConvert.DeserializeObject<Emp>(json.emp);
                    List<Permission> prm = JsonConvert.DeserializeObject<List<Permission>>(json.Perm);*/
                }
               
            };
        }

        

    }
    public class Record
    {
        public List<User> users { get; set; }
        public List<Emp> emp { get; set; }
        public List<Permission> Perm { get; set; }
    }
    public class User
    {
        public int UserID { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int UserTypeID { get; set; }
        public int EmpID { get; set; }
    }
    public class Emp
    {
        public int ClientID { get; set; }
        public int DeptID { get; set; }
        public string EmpNo { get; set; }
        public string ClientName { get; set; }
        public string CompanyName { get; set; }
        public string EmpFirstName { get; set; }
        public string EmpLastName { get; set; }
        public string DeptName { get; set; }
        public string pict { get; set; }
    }
    public class Permission
    {
        public string PrivCode { get; set; }
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanModify { get; set; }
        public bool CanDelete { get; set; }
        public int PrivID { get; set; }
        public string PrivDescr { get; set; }
    }

    }

