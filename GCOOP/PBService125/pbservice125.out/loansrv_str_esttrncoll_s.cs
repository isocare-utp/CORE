//**************************************************************************
//
//                        Sybase Inc. 
//
//    THIS IS A TEMPORARY FILE GENERATED BY POWERBUILDER. IF YOU MODIFY 
//    THIS FILE, YOU DO SO AT YOUR OWN RISK. SYBASE DOES NOT PROVIDE 
//    SUPPORT FOR .NET ASSEMBLIES BUILT WITH USER-MODIFIED CS FILES. 
//
//**************************************************************************

#line 1 "c:\\gcoop_all\\core\\gcoop\\pbservice125\\pbsrvbiz\\loansrv.pbl\\loansrv.pblx\\str_esttrncoll.srs"
#line hidden
[Sybase.PowerBuilder.PBGroupAttribute("str_esttrncoll",Sybase.PowerBuilder.PBGroupType.Structure,"","c:\\gcoop_all\\core\\gcoop\\pbservice125\\pbsrvbiz\\loansrv.pbl\\loansrv.pblx",null,"pbservice125")]
internal class c__str_esttrncoll : Sybase.PowerBuilder.PBStructure
{
	public Sybase.PowerBuilder.PBString xml_prnpayin = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString xml_trncoll = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString xml_trncollco = Sybase.PowerBuilder.PBString.DefaultValue;

	public override object Clone()
	{
		c__str_esttrncoll vv = (c__str_esttrncoll)base.Clone();
 		vv.xml_prnpayin = xml_prnpayin;
		vv.xml_trncoll = xml_trncoll;
		vv.xml_trncollco = xml_trncollco;
		return vv;
	}

	public static explicit operator c__str_esttrncoll(Sybase.PowerBuilder.PBAny v)
	{
		if (v.Value is Sybase.PowerBuilder.PBUnboundedArray)
		{
			Sybase.PowerBuilder.PBUnboundedArray a = (Sybase.PowerBuilder.PBUnboundedArray)v;
			c__str_esttrncoll vv = new c__str_esttrncoll();
			vv.xml_prnpayin = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[1]);
			vv.xml_trncoll = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[2]);
			vv.xml_trncollco = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[3]);

			return vv;
		}
		else
		{
			return (c__str_esttrncoll) v.Value;
		}
	}

	public override Sybase.PowerBuilder.PBUnboundedAnyArray ToUnboundedAnyArray()
	{
		Sybase.PowerBuilder.PBUnboundedAnyArray a = new Sybase.PowerBuilder.PBUnboundedAnyArray();
		a.Add(new Sybase.PowerBuilder.PBAny(this.xml_prnpayin));
		a.Add(new Sybase.PowerBuilder.PBAny(this.xml_trncoll));
		a.Add(new Sybase.PowerBuilder.PBAny(this.xml_trncollco));

		return a;
	}
}


[Sybase.PowerBuilder.PBStructureLayoutAttribute("str_esttrncoll")]
[ System.Runtime.InteropServices.StructLayout( System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1, CharSet = System.Runtime.InteropServices.CharSet.Ansi )]
internal struct c__str_esttrncoll_ansi
{
	public string xml_prnpayin;
	public string xml_trncoll;
	public string xml_trncollco;

	public static implicit operator c__str_esttrncoll_ansi(c__str_esttrncoll other)
	{

		c__str_esttrncoll_ansi ret = new c__str_esttrncoll_ansi();

		ret.xml_prnpayin = other.xml_prnpayin;

		ret.xml_trncoll = other.xml_trncoll;

		ret.xml_trncollco = other.xml_trncollco;

		return ret;
	}

	public static implicit operator c__str_esttrncoll(c__str_esttrncoll_ansi other)
	{

		c__str_esttrncoll ret = new c__str_esttrncoll();

		ret.xml_prnpayin = other.xml_prnpayin;

		ret.xml_trncoll = other.xml_trncoll;

		ret.xml_trncollco = other.xml_trncollco;

		return ret;
	}
}


[Sybase.PowerBuilder.PBStructureLayoutAttribute("str_esttrncoll")]
[ System.Runtime.InteropServices.StructLayout( System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1, CharSet = System.Runtime.InteropServices.CharSet.Unicode )]
internal struct c__str_esttrncoll_unicode
{
	public string xml_prnpayin;
	public string xml_trncoll;
	public string xml_trncollco;

	public static implicit operator c__str_esttrncoll_unicode(c__str_esttrncoll other)
	{

		c__str_esttrncoll_unicode ret = new c__str_esttrncoll_unicode();

		ret.xml_prnpayin = other.xml_prnpayin;

		ret.xml_trncoll = other.xml_trncoll;

		ret.xml_trncollco = other.xml_trncollco;

		return ret;
	}

	public static implicit operator c__str_esttrncoll(c__str_esttrncoll_unicode other)
	{

		c__str_esttrncoll ret = new c__str_esttrncoll();

		ret.xml_prnpayin = other.xml_prnpayin;

		ret.xml_trncoll = other.xml_trncoll;

		ret.xml_trncollco = other.xml_trncollco;

		return ret;
	}
}
 