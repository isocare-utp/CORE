//**************************************************************************
//
//                        Sybase Inc. 
//
//    THIS IS A TEMPORARY FILE GENERATED BY POWERBUILDER. IF YOU MODIFY 
//    THIS FILE, YOU DO SO AT YOUR OWN RISK. SYBASE DOES NOT PROVIDE 
//    SUPPORT FOR .NET ASSEMBLIES BUILT WITH USER-MODIFIED CS FILES. 
//
//**************************************************************************

#line 1 "c:\\gcoop_all\\core\\gcoop\\pbservice125\\pbsrvbiz\\divservice.pbl\\divservice.pblx\\str_divsrv_tran.srs"
#line hidden
[Sybase.PowerBuilder.PBGroupAttribute("str_divsrv_tran",Sybase.PowerBuilder.PBGroupType.Structure,"","c:\\gcoop_all\\core\\gcoop\\pbservice125\\pbsrvbiz\\divservice.pbl\\divservice.pblx",null,"pbservice125")]
internal class c__str_divsrv_tran : Sybase.PowerBuilder.PBStructure
{
	public Sybase.PowerBuilder.PBString as_refslipcoop = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString as_refslipno = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString as_entrycoop = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString as_entryid = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString as_chqbank = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString as_chqbranch = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBString as_chqno = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBDateTime adtm_chq = Sybase.PowerBuilder.PBDateTime.DefaultValue;

	public override object Clone()
	{
		c__str_divsrv_tran vv = (c__str_divsrv_tran)base.Clone();
 		vv.as_refslipcoop = as_refslipcoop;
		vv.as_refslipno = as_refslipno;
		vv.as_entrycoop = as_entrycoop;
		vv.as_entryid = as_entryid;
		vv.as_chqbank = as_chqbank;
		vv.as_chqbranch = as_chqbranch;
		vv.as_chqno = as_chqno;
		vv.adtm_chq = adtm_chq;
		return vv;
	}

	public static explicit operator c__str_divsrv_tran(Sybase.PowerBuilder.PBAny v)
	{
		if (v.Value is Sybase.PowerBuilder.PBUnboundedArray)
		{
			Sybase.PowerBuilder.PBUnboundedArray a = (Sybase.PowerBuilder.PBUnboundedArray)v;
			c__str_divsrv_tran vv = new c__str_divsrv_tran();
			vv.as_refslipcoop = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[1]);
			vv.as_refslipno = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[2]);
			vv.as_entrycoop = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[3]);
			vv.as_entryid = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[4]);
			vv.as_chqbank = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[5]);
			vv.as_chqbranch = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[6]);
			vv.as_chqno = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[7]);
			vv.adtm_chq = (Sybase.PowerBuilder.PBDateTime)((Sybase.PowerBuilder.PBAny)a[8]);

			return vv;
		}
		else
		{
			return (c__str_divsrv_tran) v.Value;
		}
	}

	public override Sybase.PowerBuilder.PBUnboundedAnyArray ToUnboundedAnyArray()
	{
		Sybase.PowerBuilder.PBUnboundedAnyArray a = new Sybase.PowerBuilder.PBUnboundedAnyArray();
		a.Add(new Sybase.PowerBuilder.PBAny(this.as_refslipcoop));
		a.Add(new Sybase.PowerBuilder.PBAny(this.as_refslipno));
		a.Add(new Sybase.PowerBuilder.PBAny(this.as_entrycoop));
		a.Add(new Sybase.PowerBuilder.PBAny(this.as_entryid));
		a.Add(new Sybase.PowerBuilder.PBAny(this.as_chqbank));
		a.Add(new Sybase.PowerBuilder.PBAny(this.as_chqbranch));
		a.Add(new Sybase.PowerBuilder.PBAny(this.as_chqno));
		a.Add(new Sybase.PowerBuilder.PBAny(this.adtm_chq));

		return a;
	}
}


[Sybase.PowerBuilder.PBStructureLayoutAttribute("str_divsrv_tran")]
[ System.Runtime.InteropServices.StructLayout( System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1, CharSet = System.Runtime.InteropServices.CharSet.Ansi )]
internal struct c__str_divsrv_tran_ansi
{
	public string as_refslipcoop;
	public string as_refslipno;
	public string as_entrycoop;
	public string as_entryid;
	public string as_chqbank;
	public string as_chqbranch;
	public string as_chqno;
	public System.DateTime adtm_chq;

	public static implicit operator c__str_divsrv_tran_ansi(c__str_divsrv_tran other)
	{

		c__str_divsrv_tran_ansi ret = new c__str_divsrv_tran_ansi();

		ret.as_refslipcoop = other.as_refslipcoop;

		ret.as_refslipno = other.as_refslipno;

		ret.as_entrycoop = other.as_entrycoop;

		ret.as_entryid = other.as_entryid;

		ret.as_chqbank = other.as_chqbank;

		ret.as_chqbranch = other.as_chqbranch;

		ret.as_chqno = other.as_chqno;

		ret.adtm_chq = other.adtm_chq;

		return ret;
	}

	public static implicit operator c__str_divsrv_tran(c__str_divsrv_tran_ansi other)
	{

		c__str_divsrv_tran ret = new c__str_divsrv_tran();

		ret.as_refslipcoop = other.as_refslipcoop;

		ret.as_refslipno = other.as_refslipno;

		ret.as_entrycoop = other.as_entrycoop;

		ret.as_entryid = other.as_entryid;

		ret.as_chqbank = other.as_chqbank;

		ret.as_chqbranch = other.as_chqbranch;

		ret.as_chqno = other.as_chqno;

		ret.adtm_chq = other.adtm_chq;

		return ret;
	}
}


[Sybase.PowerBuilder.PBStructureLayoutAttribute("str_divsrv_tran")]
[ System.Runtime.InteropServices.StructLayout( System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1, CharSet = System.Runtime.InteropServices.CharSet.Unicode )]
internal struct c__str_divsrv_tran_unicode
{
	public string as_refslipcoop;
	public string as_refslipno;
	public string as_entrycoop;
	public string as_entryid;
	public string as_chqbank;
	public string as_chqbranch;
	public string as_chqno;
	public System.DateTime adtm_chq;

	public static implicit operator c__str_divsrv_tran_unicode(c__str_divsrv_tran other)
	{

		c__str_divsrv_tran_unicode ret = new c__str_divsrv_tran_unicode();

		ret.as_refslipcoop = other.as_refslipcoop;

		ret.as_refslipno = other.as_refslipno;

		ret.as_entrycoop = other.as_entrycoop;

		ret.as_entryid = other.as_entryid;

		ret.as_chqbank = other.as_chqbank;

		ret.as_chqbranch = other.as_chqbranch;

		ret.as_chqno = other.as_chqno;

		ret.adtm_chq = other.adtm_chq;

		return ret;
	}

	public static implicit operator c__str_divsrv_tran(c__str_divsrv_tran_unicode other)
	{

		c__str_divsrv_tran ret = new c__str_divsrv_tran();

		ret.as_refslipcoop = other.as_refslipcoop;

		ret.as_refslipno = other.as_refslipno;

		ret.as_entrycoop = other.as_entrycoop;

		ret.as_entryid = other.as_entryid;

		ret.as_chqbank = other.as_chqbank;

		ret.as_chqbranch = other.as_chqbranch;

		ret.as_chqno = other.as_chqno;

		ret.adtm_chq = other.adtm_chq;

		return ret;
	}
}
 