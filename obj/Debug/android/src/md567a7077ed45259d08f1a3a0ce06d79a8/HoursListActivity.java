package md567a7077ed45259d08f1a3a0ce06d79a8;


public class HoursListActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("EzTimeAndroid.HoursListActivity, EzTimeAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", HoursListActivity.class, __md_methods);
	}


	public HoursListActivity ()
	{
		super ();
		if (getClass () == HoursListActivity.class)
			mono.android.TypeManager.Activate ("EzTimeAndroid.HoursListActivity, EzTimeAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
