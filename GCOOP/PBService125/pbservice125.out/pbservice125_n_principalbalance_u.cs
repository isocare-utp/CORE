using Sybase.PowerBuilder.WCFNVO;

//**************************************************************************
//
//                        Sybase Inc. 
//
//    THIS IS A TEMPORARY FILE GENERATED BY POWERBUILDER. IF YOU MODIFY 
//    THIS FILE, YOU DO SO AT YOUR OWN RISK. SYBASE DOES NOT PROVIDE 
//    SUPPORT FOR .NET ASSEMBLIES BUILT WITH USER-MODIFIED CS FILES. 
//
//**************************************************************************

#line 1 "c:\\gcoop_all\\core\\gcoop\\pbservice125\\pbservice125.pbl\\pbservice125.pblx\\n_principalbalance.sru"
#line hidden
namespace pbservice125
{
	#line 1 "n_principalbalance"
	#line hidden
	[Sybase.PowerBuilder.PBGroupAttribute("n_principalbalance",Sybase.PowerBuilder.PBGroupType.UserObject,"","c:\\gcoop_all\\core\\gcoop\\pbservice125\\pbservice125.pbl\\pbservice125.pblx",null,"pbservice125")]
	internal class c__n_principalbalance : Sybase.PowerBuilder.PBNonVisualObject
	{
		#line hidden

		#line 1 "{pbservice125}n_principalbalance.of_test(SS)"
		#line hidden
		[Sybase.PowerBuilder.PBFunctionAttribute("of_test", new string[]{"string"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
		public virtual Sybase.PowerBuilder.PBString of_test(Sybase.PowerBuilder.PBString as_test)
		{
			#line hidden
			Sybase.PowerBuilder.PBString ls_result = Sybase.PowerBuilder.PBString.DefaultValue;
			#line 3
			ls_result = new Sybase.PowerBuilder.PBString("test ")+ as_test;
			#line hidden
			#line 5
			return ls_result;
			#line hidden
		}

		#line hidden
		[Sybase.PowerBuilder.PBEventAttribute("create")]
		public override void create()
		{
			#line hidden
			#line hidden
			base.create();
			#line hidden
			#line hidden
			;
			#line hidden
		}

		#line hidden
		[Sybase.PowerBuilder.PBEventAttribute("destroy")]
		public override void destroy()
		{
			#line hidden
			#line hidden
			this.TriggerEvent(new Sybase.PowerBuilder.PBString("destructor"));
			#line hidden
			#line hidden
			base.destroy();
			#line hidden
		}

		void _init()
		{
			this.CreateEvent += new Sybase.PowerBuilder.PBEventHandler(this.create);
			this.DestroyEvent += new Sybase.PowerBuilder.PBEventHandler(this.destroy);

			if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
			{
			}
		}

		public c__n_principalbalance()
		{
			_init();
		}


		public static explicit operator c__n_principalbalance(Sybase.PowerBuilder.PBAny v)
		{
			if (v.Value is Sybase.PowerBuilder.PBUnboundedAnyArray)
			{
				Sybase.PowerBuilder.PBUnboundedAnyArray a = (Sybase.PowerBuilder.PBUnboundedAnyArray)v.Value;
				c__n_principalbalance vv = new c__n_principalbalance();
				if (vv.FromUnboundedAnyArray(a) > 0)
				{
					return vv;
				}
				else
				{
					return null;
				}
			}
			else
			{
				return (c__n_principalbalance) v.Value;
			}
		}
	}
}
 