//**************************************************************************
//
//                        Sybase Inc. 
//
//    THIS IS A TEMPORARY FILE GENERATED BY POWERBUILDER. IF YOU MODIFY 
//    THIS FILE, YOU DO SO AT YOUR OWN RISK. SYBASE DOES NOT PROVIDE 
//    SUPPORT FOR .NET ASSEMBLIES BUILT WITH USER-MODIFIED CS FILES. 
//
//**************************************************************************

#line 1 "c:\\gcoop_all\\core\\gcoop\\pbservice125\\pbsrvbiz\\divservice.pbl\\divservice.pblx\\n_cst_divsrv_proc_service.sru"
#line hidden
#line 1 "n_cst_divsrv_proc_service"
#line hidden
[Sybase.PowerBuilder.PBGroupAttribute("n_cst_divsrv_proc_service",Sybase.PowerBuilder.PBGroupType.UserObject,"","c:\\gcoop_all\\core\\gcoop\\pbservice125\\pbsrvbiz\\divservice.pbl\\divservice.pblx",null,"pbservice125")]
internal class c__n_cst_divsrv_proc_service : Sybase.PowerBuilder.PBNonVisualObject
{
	#line hidden
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "itr_sqlca", null, "n_cst_divsrv_proc_service", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public Sybase.PowerBuilder.PBTransaction itr_sqlca = null;
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "ithw_exception", null, "n_cst_divsrv_proc_service", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public Sybase.PowerBuilder.PBException ithw_exception = null;
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "inv_transection", null, "n_cst_divsrv_proc_service", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public c__n_cst_dbconnectservice inv_transection = null;
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "inv_dwxmliesrv", null, "n_cst_divsrv_proc_service", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public c__n_cst_dwxmlieservice inv_dwxmliesrv = null;
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "inv_stringsrv", null, "n_cst_divsrv_proc_service", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public c__n_cst_stringservice inv_stringsrv = null;
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "is_recvperiod", null, "n_cst_divsrv_proc_service", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public Sybase.PowerBuilder.PBString is_recvperiod = Sybase.PowerBuilder.PBString.DefaultValue;
	[Sybase.PowerBuilder.PBArrayAttribute(typeof(Sybase.PowerBuilder.PBString))] 
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "is_arg", null, "n_cst_divsrv_proc_service", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public Sybase.PowerBuilder.PBArray is_arg = new Sybase.PowerBuilder.PBUnboundedStringArray();
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "is_sqlext", null, "n_cst_divsrv_proc_service", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public Sybase.PowerBuilder.PBString is_sqlext = Sybase.PowerBuilder.PBString.DefaultValue;
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "is_proctxt", null, "n_cst_divsrv_proc_service", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public Sybase.PowerBuilder.PBString is_proctxt = Sybase.PowerBuilder.PBString.DefaultValue;
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "ii_proctype", null, "n_cst_divsrv_proc_service", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public Sybase.PowerBuilder.PBInt ii_proctype = Sybase.PowerBuilder.PBInt.DefaultValue;

	#line 1 "n_cst_divsrv_proc_service.of_setsrvdwxmlie(IB)"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_setsrvdwxmlie", new string[]{"boolean"}, Sybase.PowerBuilder.PBModifier.Private, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	private Sybase.PowerBuilder.PBInt of_setsrvdwxmlie(Sybase.PowerBuilder.PBBoolean ab_switch)
	{
		#line hidden
		#line 2
		if (Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(ab_switch)))))
		#line hidden
		{
			#line 3
			return new Sybase.PowerBuilder.PBInt(-1);
			#line hidden
		}
		#line 6
		if (ab_switch)
		#line hidden
		{
			#line 7
			if ((Sybase.PowerBuilder.PBBoolean)(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)((new Sybase.PowerBuilder.PBAny(((c__n_cst_dwxmlieservice)(Sybase.PowerBuilder.PBSystemFunctions.PBCheckNull(inv_dwxmliesrv)))))))| !(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsValid((Sybase.PowerBuilder.PBPowerObject)(inv_dwxmliesrv)))))
			#line hidden
			{
				#line 8
				inv_dwxmliesrv = (c__n_cst_dwxmlieservice)this.CreateInstance(typeof(c__n_cst_dwxmlieservice), 0);
				#line hidden
				#line 9
				return new Sybase.PowerBuilder.PBInt(1);
				#line hidden
			}
		}
		else
		{
			#line 12
			if (Sybase.PowerBuilder.WPF.PBSystemFunctions.IsValid((Sybase.PowerBuilder.PBPowerObject)(inv_dwxmliesrv)))
			#line hidden
			{
				#line 13
				Sybase.PowerBuilder.WPF.PBSession.CurrentSession.DestroyObject(inv_dwxmliesrv);
				#line hidden
				#line 14
				return new Sybase.PowerBuilder.PBInt(1);
				#line hidden
			}
		}
		#line 18
		return new Sybase.PowerBuilder.PBInt(0);
		#line hidden
	}

	#line 1 "n_cst_divsrv_proc_service.of_set_txtproc(IS)"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_set_txtproc", new string[]{"string"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	public virtual Sybase.PowerBuilder.PBInt of_set_txtproc(Sybase.PowerBuilder.PBString as_proctxt)
	{
		#line hidden
		#line 20
		is_proctxt = as_proctxt;
		#line hidden
		#line 21
		return new Sybase.PowerBuilder.PBInt(1);
		#line hidden
	}

	#line 1 "n_cst_divsrv_proc_service.of_get_argument(IRS[])"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_get_argument", new string[]{"ref string"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	public virtual Sybase.PowerBuilder.PBInt of_get_argument_1_345252223([Sybase.PowerBuilder.PBArrayAttribute(typeof(Sybase.PowerBuilder.PBString))] ref Sybase.PowerBuilder.PBArray as_arg)
	{
		#line hidden
		#line 1
		as_arg.AssignFrom((Sybase.PowerBuilder.PBArray)is_arg);
		#line hidden
		#line 3
		return new Sybase.PowerBuilder.PBInt(1);
		#line hidden
	}

	#line 1 "n_cst_divsrv_proc_service.of_get_proctype(IRI)"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_get_proctype", new string[]{"ref integer"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	public virtual Sybase.PowerBuilder.PBInt of_get_proctype(ref Sybase.PowerBuilder.PBInt ai_proctype)
	{
		#line hidden
		#line 1
		ai_proctype = ii_proctype;
		#line hidden
		#line 3
		return new Sybase.PowerBuilder.PBInt(1);
		#line hidden
	}

	#line 1 "n_cst_divsrv_proc_service.of_get_sqlselect(IRS)"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_get_sqlselect", new string[]{"ref string"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	public virtual Sybase.PowerBuilder.PBInt of_get_sqlselect(ref Sybase.PowerBuilder.PBString as_sqlext)
	{
		#line hidden
		#line 1
		as_sqlext = is_sqlext;
		#line hidden
		#line 3
		return new Sybase.PowerBuilder.PBInt(1);
		#line hidden
	}

	#line 1 "n_cst_divsrv_proc_service.of_initservice(I)"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_initservice", new string[]{}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	public virtual Sybase.PowerBuilder.PBInt of_initservice()
	{
		#line hidden
		#line 21
		if ((Sybase.PowerBuilder.PBBoolean)(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)((new Sybase.PowerBuilder.PBAny(((c__n_cst_stringservice)(Sybase.PowerBuilder.PBSystemFunctions.PBCheckNull(inv_stringsrv)))))))| !(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsValid((Sybase.PowerBuilder.PBPowerObject)(inv_stringsrv)))))
		#line hidden
		{
			#line 22
			inv_stringsrv = (c__n_cst_stringservice)this.CreateInstance(typeof(c__n_cst_stringservice), 0);
			#line hidden
		}
		#line 25
		return new Sybase.PowerBuilder.PBInt(1);
		#line hidden
	}

	#line 1 "n_cst_divsrv_proc_service.of_set_analyze(I)"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_set_analyze", new string[]{}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	public virtual Sybase.PowerBuilder.PBInt of_set_analyze()
	{
		#line hidden
		#line 20
		if ((Sybase.PowerBuilder.PBBoolean)((Sybase.PowerBuilder.PBLong)(ii_proctype)> (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(1))))
		#line hidden
		{
			#line 20
			of_analyzestring_2_345252223_21090703144(inv_stringsrv, is_proctxt, ref is_arg);
			#line hidden
		}
		#line 22
		return new Sybase.PowerBuilder.PBInt(1);
		#line hidden
	}

	#line 1 "n_cst_divsrv_proc_service.of_set_argument(IS[])"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_set_argument", new string[]{"string"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	public virtual Sybase.PowerBuilder.PBInt of_set_argument_1_345252223([Sybase.PowerBuilder.PBArrayAttribute(typeof(Sybase.PowerBuilder.PBString))] Sybase.PowerBuilder.PBArray as_arg)
	{
		#line hidden
		#line 20
		is_arg.AssignFrom((Sybase.PowerBuilder.PBArray)as_arg);
		#line hidden
		#line 22
		return new Sybase.PowerBuilder.PBInt(1);
		#line hidden
	}

	#line 1 "n_cst_divsrv_proc_service.of_set_proctype(II)"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_set_proctype", new string[]{"integer"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	public virtual Sybase.PowerBuilder.PBInt of_set_proctype(Sybase.PowerBuilder.PBInt ai_proctype)
	{
		#line hidden
		#line 20
		ii_proctype = ai_proctype;
		#line hidden
		#line 22
		return new Sybase.PowerBuilder.PBInt(1);
		#line hidden
	}

	#line 1 "n_cst_divsrv_proc_service.of_set_sqldw(IRCn_ds.)"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_set_sqldw", new string[]{"ref n_ds"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	public virtual Sybase.PowerBuilder.PBInt of_set_sqldw_1_1438817508<T0>(ref T0 ads_info) where T0 : c__n_ds
	{
		#line hidden
		Sybase.PowerBuilder.PBString ls_temp = Sybase.PowerBuilder.PBString.DefaultValue;
		Sybase.PowerBuilder.PBInt li_pos = Sybase.PowerBuilder.PBInt.DefaultValue;
		Sybase.PowerBuilder.PBInt li_ret = Sybase.PowerBuilder.PBInt.DefaultValue;
		#line 24
		if ((Sybase.PowerBuilder.PBBoolean)(is_sqlext != new Sybase.PowerBuilder.PBString("")))
		#line hidden
		{
			#line 26
			ls_temp = ads_info.GetSQLSelect();
			#line hidden
			#line 27
			li_pos = (Sybase.PowerBuilder.PBInt)(Sybase.PowerBuilder.WPF.PBSystemFunctions.Pos(ls_temp, new Sybase.PowerBuilder.PBString("GROUP BY")));
			#line hidden
			#line 28
			if ((Sybase.PowerBuilder.PBBoolean)(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(li_pos))))| (li_pos == new Sybase.PowerBuilder.PBInt(0))))
			#line hidden
			{
				#line 28
				ls_temp += is_sqlext;
				#line hidden
			}
			#line 29
			if ((Sybase.PowerBuilder.PBBoolean)((Sybase.PowerBuilder.PBLong)(li_pos)> (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(0))))
			#line hidden
			{
				#line 29
				ls_temp = (((Sybase.PowerBuilder.WPF.PBSystemFunctions.Mid(ls_temp, (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(1)), (Sybase.PowerBuilder.PBLong)(li_pos)- (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(1)))+ new Sybase.PowerBuilder.PBString(" "))+ is_sqlext)+ new Sybase.PowerBuilder.PBString(" "))+ Sybase.PowerBuilder.WPF.PBSystemFunctions.Mid(ls_temp, (Sybase.PowerBuilder.PBLong)(li_pos), Sybase.PowerBuilder.WPF.PBSystemFunctions.Len(ls_temp));
				#line hidden
			}
			#line 31
			li_ret = ads_info.SetSQLSelect(ls_temp);
			#line hidden
			#line 33
			if ((Sybase.PowerBuilder.PBBoolean)(li_ret != new Sybase.PowerBuilder.PBInt(1)))
			#line hidden
			{
				#line 34
				ithw_exception.Text += new Sybase.PowerBuilder.PBString("\r\n")+ ads_info.of_geterrormessage();
				#line hidden
				#line 35
				throw new Sybase.PowerBuilder.PBExceptionE(ithw_exception);
				#line hidden
			}
		}
		#line 39
		return new Sybase.PowerBuilder.PBInt(1);
		#line hidden
	}

	#line 1 "n_cst_divsrv_proc_service.of_set_sqlselect(IS)"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_set_sqlselect", new string[]{"string"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	public virtual Sybase.PowerBuilder.PBInt of_set_sqlselect(Sybase.PowerBuilder.PBString as_tablename)
	{
		#line hidden
		Sybase.PowerBuilder.PBString ls_tablename = Sybase.PowerBuilder.PBString.DefaultValue;
		Sybase.PowerBuilder.PBString ls_column = Sybase.PowerBuilder.PBString.DefaultValue;
		Sybase.PowerBuilder.PBString ls_temp = Sybase.PowerBuilder.PBString.DefaultValue;
		#line 24
		is_sqlext = new Sybase.PowerBuilder.PBString("");
		#line hidden
		#line 25
		ls_tablename = as_tablename;
		#line hidden
		#line 27
		if ((Sybase.PowerBuilder.PBBoolean)(((Sybase.PowerBuilder.WPF.PBSystemFunctions.Len(ls_tablename) == (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(0)))| (ls_tablename == new Sybase.PowerBuilder.PBString("")))| Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(ls_tablename))))))
		#line hidden
		{
			#line 28
			ls_column = new Sybase.PowerBuilder.PBString("mbmembmaster");
			#line hidden
		}
		else
		{
			#line 30
			ls_column = Sybase.PowerBuilder.WPF.PBSystemFunctions.Trim(ls_tablename);
			#line hidden
		}
		#line 33
		Sybase.PowerBuilder.PBInt __PB_TEMP______switchTmpVar0 = ii_proctype;
		#line hidden
		#line 34
		if (__PB_TEMP______switchTmpVar0 == new Sybase.PowerBuilder.PBInt(1) )
		#line hidden
		{
			#line 36
			is_sqlext = new Sybase.PowerBuilder.PBString("");
			#line hidden
		}
		#line 37
		else if (__PB_TEMP______switchTmpVar0 == new Sybase.PowerBuilder.PBInt(20) )
		#line hidden
		{
			#line 39
			ls_column += new Sybase.PowerBuilder.PBString(".membtype_code");
			#line hidden
			#line 40
			of_buildsqlext_3_345252223_3_n1017240298(inv_stringsrv, (Sybase.PowerBuilder.PBUnboundedStringArray)(is_arg).ToUnbounded(typeof(Sybase.PowerBuilder.PBString)), ls_column, ref is_sqlext);
			#line hidden
			#line 41
			is_sqlext = new Sybase.PowerBuilder.PBString(" and ")+ is_sqlext;
			#line hidden
		}
		#line 42
		else if (__PB_TEMP______switchTmpVar0 == new Sybase.PowerBuilder.PBInt(40) )
		#line hidden
		{
			#line 44
			ls_column += new Sybase.PowerBuilder.PBString(".membgroup_code");
			#line hidden
			#line 45
			of_buildsqlext_3_345252223_3_n1017240298(inv_stringsrv, (Sybase.PowerBuilder.PBUnboundedStringArray)(is_arg).ToUnbounded(typeof(Sybase.PowerBuilder.PBString)), ls_column, ref is_sqlext);
			#line hidden
			#line 46
			is_sqlext = new Sybase.PowerBuilder.PBString(" and ")+ is_sqlext;
			#line hidden
		}
		#line 47
		else if (__PB_TEMP______switchTmpVar0 == new Sybase.PowerBuilder.PBInt(60) )
		#line hidden
		{
			#line 49
			ls_column += new Sybase.PowerBuilder.PBString(".member_no");
			#line hidden
			#line 50
			of_buildsqlext_3_345252223_3_n1017240298(inv_stringsrv, (Sybase.PowerBuilder.PBUnboundedStringArray)(is_arg).ToUnbounded(typeof(Sybase.PowerBuilder.PBString)), ls_column, ref is_sqlext);
			#line hidden
			#line 51
			is_sqlext = new Sybase.PowerBuilder.PBString(" and ")+ is_sqlext;
			#line hidden
		}

		#line 54
		return new Sybase.PowerBuilder.PBInt(1);
		#line hidden
	}

	#line 1 "n_cst_divsrv_proc_service.of_set_sqldw_column(IRCn_ds.SSSA)"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_set_sqldw_column", new string[]{"ref n_ds", "string", "string", "string", "any"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	public virtual Sybase.PowerBuilder.PBInt of_set_sqldw_column_5_1438817508<T0>(ref T0 ads_info, Sybase.PowerBuilder.PBString as_table_name, Sybase.PowerBuilder.PBString as_coln_name, Sybase.PowerBuilder.PBString as_operator, Sybase.PowerBuilder.PBAny aa_coln_value) where T0 : c__n_ds
	{
		#line hidden
		Sybase.PowerBuilder.PBString ls_temp = Sybase.PowerBuilder.PBString.DefaultValue;
		Sybase.PowerBuilder.PBString ls_column = Sybase.PowerBuilder.PBString.DefaultValue;
		Sybase.PowerBuilder.PBInt li_pos = Sybase.PowerBuilder.PBInt.DefaultValue;
		Sybase.PowerBuilder.PBInt li_ret = Sybase.PowerBuilder.PBInt.DefaultValue;
		Sybase.PowerBuilder.PBBoolean lb_err = Sybase.PowerBuilder.PBBoolean.DefaultValue;
		#line 27
		if ((Sybase.PowerBuilder.PBBoolean)(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(as_table_name))))| (Sybase.PowerBuilder.WPF.PBSystemFunctions.Len(Sybase.PowerBuilder.WPF.PBSystemFunctions.Trim(as_table_name)) == (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(0)))))
		#line hidden
		{
			#line 28
			ithw_exception.Text += new Sybase.PowerBuilder.PBString("\r\nไม่พบข้อมูล Table ที่ส่งมาทำรายการ");
			#line hidden
			#line 29
			throw new Sybase.PowerBuilder.PBExceptionE(ithw_exception);
			#line hidden
		}
		#line 32
		if ((Sybase.PowerBuilder.PBBoolean)(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(as_coln_name))))| (Sybase.PowerBuilder.WPF.PBSystemFunctions.Len(Sybase.PowerBuilder.WPF.PBSystemFunctions.Trim(as_coln_name)) == (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(0)))))
		#line hidden
		{
			#line 33
			ithw_exception.Text += new Sybase.PowerBuilder.PBString("\r\nไม่พบข้อมูล Column ที่ส่งมาทำรายการ");
			#line hidden
			#line 34
			throw new Sybase.PowerBuilder.PBExceptionE(ithw_exception);
			#line hidden
		}
		#line 37
		if ((Sybase.PowerBuilder.PBBoolean)(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(as_operator))))| (Sybase.PowerBuilder.WPF.PBSystemFunctions.Len(Sybase.PowerBuilder.WPF.PBSystemFunctions.Trim(as_operator)) == (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(0)))))
		#line hidden
		{
			#line 38
			ithw_exception.Text += new Sybase.PowerBuilder.PBString("\r\nไม่พบข้อมูล Operator ที่ส่งมาทำรายการ");
			#line hidden
			#line 39
			throw new Sybase.PowerBuilder.PBExceptionE(ithw_exception);
			#line hidden
		}
		#line 42
		if (Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull(((Sybase.PowerBuilder.PBAny)(aa_coln_value))))
		#line hidden
		{
			#line 43
			ithw_exception.Text += new Sybase.PowerBuilder.PBString("\r\nไม่พบข้อมูล Value ที่ส่งมาทำรายการ");
			#line hidden
			#line 44
			throw new Sybase.PowerBuilder.PBExceptionE(ithw_exception);
			#line hidden
		}
		#line 47
		as_table_name = Sybase.PowerBuilder.WPF.PBSystemFunctions.Trim(as_table_name);
		#line hidden
		#line 48
		as_coln_name = Sybase.PowerBuilder.WPF.PBSystemFunctions.Trim(as_coln_name);
		#line hidden
		#line 49
		aa_coln_value = (Sybase.PowerBuilder.PBAny)(Sybase.PowerBuilder.WPF.PBSystemFunctions.String(((Sybase.PowerBuilder.PBAny)(aa_coln_value))));
		#line hidden
		#line 51
		ls_column = (Sybase.PowerBuilder.PBString)(((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(new Sybase.PowerBuilder.PBString(" and "))+ (Sybase.PowerBuilder.PBAny)(as_table_name)))+ (Sybase.PowerBuilder.PBAny)(new Sybase.PowerBuilder.PBString("."))))+ (Sybase.PowerBuilder.PBAny)(as_coln_name)))+ (Sybase.PowerBuilder.PBAny)(new Sybase.PowerBuilder.PBString(" "))))+ (Sybase.PowerBuilder.PBAny)(as_operator)))+ (Sybase.PowerBuilder.PBAny)(new Sybase.PowerBuilder.PBString(" '"))))+ aa_coln_value)+ (Sybase.PowerBuilder.PBAny)(new Sybase.PowerBuilder.PBString("'")));
		#line hidden
		#line 53
		ls_temp = ads_info.GetSQLSelect();
		#line hidden
		#line 54
		li_pos = (Sybase.PowerBuilder.PBInt)(Sybase.PowerBuilder.WPF.PBSystemFunctions.Pos(ls_temp, new Sybase.PowerBuilder.PBString("GROUP BY")));
		#line hidden
		#line 55
		if ((Sybase.PowerBuilder.PBBoolean)(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(li_pos))))| (li_pos == new Sybase.PowerBuilder.PBInt(0))))
		#line hidden
		{
			#line 55
			ls_temp += ls_column;
			#line hidden
		}
		#line 56
		if ((Sybase.PowerBuilder.PBBoolean)((Sybase.PowerBuilder.PBLong)(li_pos)> (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(0))))
		#line hidden
		{
			#line 56
			ls_temp = (((Sybase.PowerBuilder.WPF.PBSystemFunctions.Mid(ls_temp, (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(1)), (Sybase.PowerBuilder.PBLong)(li_pos)- (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(1)))+ new Sybase.PowerBuilder.PBString(" "))+ ls_column)+ new Sybase.PowerBuilder.PBString(" "))+ Sybase.PowerBuilder.WPF.PBSystemFunctions.Mid(ls_temp, (Sybase.PowerBuilder.PBLong)(li_pos), Sybase.PowerBuilder.WPF.PBSystemFunctions.Len(ls_temp));
			#line hidden
		}
		#line 58
		li_ret = ads_info.SetSQLSelect(ls_temp);
		#line hidden
		#line 60
		if ((Sybase.PowerBuilder.PBBoolean)(li_ret != new Sybase.PowerBuilder.PBInt(1)))
		#line hidden
		{
			#line 61
			ithw_exception.Text += new Sybase.PowerBuilder.PBString("\r\n")+ ads_info.of_geterrormessage();
			#line hidden
			#line 62
			throw new Sybase.PowerBuilder.PBExceptionE(ithw_exception);
			#line hidden
		}
		#line 65
		return new Sybase.PowerBuilder.PBInt(1);
		#line hidden
	}

	#line 1 "n_cst_divsrv_proc_service.of_set_sqldw_column(IRCn_ds.S)"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_set_sqldw_column", new string[]{"ref n_ds", "string"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	public virtual Sybase.PowerBuilder.PBInt of_set_sqldw_column_2_1438817508<T0>(ref T0 ads_info, Sybase.PowerBuilder.PBString as_condition) where T0 : c__n_ds
	{
		#line hidden
		Sybase.PowerBuilder.PBString ls_temp = Sybase.PowerBuilder.PBString.DefaultValue;
		Sybase.PowerBuilder.PBString ls_condition = Sybase.PowerBuilder.PBString.DefaultValue;
		Sybase.PowerBuilder.PBInt li_pos = Sybase.PowerBuilder.PBInt.DefaultValue;
		Sybase.PowerBuilder.PBInt li_ret = Sybase.PowerBuilder.PBInt.DefaultValue;
		Sybase.PowerBuilder.PBBoolean lb_err = Sybase.PowerBuilder.PBBoolean.DefaultValue;
		#line 27
		if ((Sybase.PowerBuilder.PBBoolean)(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(as_condition))))| (Sybase.PowerBuilder.WPF.PBSystemFunctions.Len(Sybase.PowerBuilder.WPF.PBSystemFunctions.Trim(as_condition)) == (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(0)))))
		#line hidden
		{
			#line 28
			ithw_exception.Text += new Sybase.PowerBuilder.PBString("\r\nไม่พบข้อมูล เงื่อนไข ที่ส่งมาทำรายการ");
			#line hidden
			#line 29
			throw new Sybase.PowerBuilder.PBExceptionE(ithw_exception);
			#line hidden
		}
		#line 32
		as_condition = Sybase.PowerBuilder.WPF.PBSystemFunctions.Trim(as_condition);
		#line hidden
		#line 34
		ls_condition = (new Sybase.PowerBuilder.PBString(" ")+ as_condition)+ new Sybase.PowerBuilder.PBString(" ");
		#line hidden
		#line 36
		ls_temp = ads_info.GetSQLSelect();
		#line hidden
		#line 37
		li_pos = (Sybase.PowerBuilder.PBInt)(Sybase.PowerBuilder.WPF.PBSystemFunctions.Pos(ls_temp, new Sybase.PowerBuilder.PBString("GROUP BY")));
		#line hidden
		#line 38
		if ((Sybase.PowerBuilder.PBBoolean)(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(li_pos))))| (li_pos == new Sybase.PowerBuilder.PBInt(0))))
		#line hidden
		{
			#line 38
			ls_temp += ls_condition;
			#line hidden
		}
		#line 39
		if ((Sybase.PowerBuilder.PBBoolean)((Sybase.PowerBuilder.PBLong)(li_pos)> (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(0))))
		#line hidden
		{
			#line 39
			ls_temp = (((Sybase.PowerBuilder.WPF.PBSystemFunctions.Mid(ls_temp, (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(1)), (Sybase.PowerBuilder.PBLong)(li_pos)- (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(1)))+ new Sybase.PowerBuilder.PBString(" "))+ ls_condition)+ new Sybase.PowerBuilder.PBString(" "))+ Sybase.PowerBuilder.WPF.PBSystemFunctions.Mid(ls_temp, (Sybase.PowerBuilder.PBLong)(li_pos), Sybase.PowerBuilder.WPF.PBSystemFunctions.Len(ls_temp));
			#line hidden
		}
		#line 41
		li_ret = ads_info.SetSQLSelect(ls_temp);
		#line hidden
		#line 43
		if ((Sybase.PowerBuilder.PBBoolean)(li_ret != new Sybase.PowerBuilder.PBInt(1)))
		#line hidden
		{
			#line 44
			ithw_exception.Text += new Sybase.PowerBuilder.PBString("\r\n")+ ads_info.of_geterrormessage();
			#line hidden
			#line 45
			throw new Sybase.PowerBuilder.PBExceptionE(ithw_exception);
			#line hidden
		}
		#line 48
		return new Sybase.PowerBuilder.PBInt(1);
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

	#line 1 "n_cst_divsrv_proc_service.constructor"
	#line hidden
	[Sybase.PowerBuilder.PBEventAttribute("constructor")]
	[Sybase.PowerBuilder.PBEventToken(Sybase.PowerBuilder.PBEventTokens.pbm_constructor)]
	public override Sybase.PowerBuilder.PBLong constructor()
	{
		#line hidden
		Sybase.PowerBuilder.PBLong ancestorreturnvalue = Sybase.PowerBuilder.PBLong.DefaultValue;
		#line 1
		ithw_exception = (Sybase.PowerBuilder.PBException)this.CreateInstance(typeof(Sybase.PowerBuilder.PBException), 0);
		#line hidden
		return new Sybase.PowerBuilder.PBLong(0);
	}

	#line 1 "n_cst_divsrv_proc_service.destructor"
	#line hidden
	[Sybase.PowerBuilder.PBEventAttribute("destructor")]
	[Sybase.PowerBuilder.PBEventToken(Sybase.PowerBuilder.PBEventTokens.pbm_destructor)]
	public override Sybase.PowerBuilder.PBLong destructor()
	{
		#line hidden
		Sybase.PowerBuilder.PBLong ancestorreturnvalue = Sybase.PowerBuilder.PBLong.DefaultValue;
		#line 1
		this.of_setsrvdwxmlie(new Sybase.PowerBuilder.PBBoolean(false));
		#line hidden
		#line 2
		if (Sybase.PowerBuilder.WPF.PBSystemFunctions.IsValid((Sybase.PowerBuilder.PBPowerObject)(inv_stringsrv)))
		#line hidden
		{
			#line 2
			Sybase.PowerBuilder.WPF.PBSession.CurrentSession.DestroyObject(inv_stringsrv);
			#line hidden
		}
		return new Sybase.PowerBuilder.PBLong(0);
	}

	public Sybase.PowerBuilder.PBInt of_analyzestring_2_345252223_21090703144(c__n_cst_stringservice this__object, Sybase.PowerBuilder.PBString as_sourcetext, ref Sybase.PowerBuilder.PBArray as_arg)
	{
		Sybase.PowerBuilder.PBInt return_value = this__object.of_analyzestring_2_345252223(as_sourcetext, ref as_arg);
		return return_value;
	}

	public Sybase.PowerBuilder.PBInt of_buildsqlext_3_345252223_3_n1017240298(c__n_cst_stringservice this__object, Sybase.PowerBuilder.PBArray as_arg, Sybase.PowerBuilder.PBString as_column, ref Sybase.PowerBuilder.PBString as_sqlext)
	{
		 Sybase.PowerBuilder.PBArray temp_var_as_arg = new Sybase.PowerBuilder.PBUnboundedStringArray();
		temp_var_as_arg.AssignFrom(as_arg);
		Sybase.PowerBuilder.PBInt return_value = this__object.of_buildsqlext_3_345252223(temp_var_as_arg, as_column, ref as_sqlext);
		return return_value;
	}


	void _init()
	{
		this.CreateEvent += new Sybase.PowerBuilder.PBEventHandler(this.create);
		this.DestroyEvent += new Sybase.PowerBuilder.PBEventHandler(this.destroy);
		this.ConstructorEvent += new Sybase.PowerBuilder.PBM_EventHandler(this.constructor);
		this.DestructorEvent += new Sybase.PowerBuilder.PBM_EventHandler(this.destructor);

		if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
		{
		}
	}

	public c__n_cst_divsrv_proc_service()
	{
		_init();
	}


	public static explicit operator c__n_cst_divsrv_proc_service(Sybase.PowerBuilder.PBAny v)
	{
		if (v.Value is Sybase.PowerBuilder.PBUnboundedAnyArray)
		{
			Sybase.PowerBuilder.PBUnboundedAnyArray a = (Sybase.PowerBuilder.PBUnboundedAnyArray)v.Value;
			c__n_cst_divsrv_proc_service vv = new c__n_cst_divsrv_proc_service();
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
			return (c__n_cst_divsrv_proc_service) v.Value;
		}
	}
}
 