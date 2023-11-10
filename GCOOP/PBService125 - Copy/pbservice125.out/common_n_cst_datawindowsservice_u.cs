//**************************************************************************
//
//                        Sybase Inc. 
//
//    THIS IS A TEMPORARY FILE GENERATED BY POWERBUILDER. IF YOU MODIFY 
//    THIS FILE, YOU DO SO AT YOUR OWN RISK. SYBASE DOES NOT PROVIDE 
//    SUPPORT FOR .NET ASSEMBLIES BUILT WITH USER-MODIFIED CS FILES. 
//
//**************************************************************************

#line 1 "c:\\gcoop_all\\core\\gcoop\\pbservice125\\pbsrvcom\\common.pbl\\common.pblx\\n_cst_datawindowsservice.sru"
#line hidden
#line 1 "n_cst_datawindowsservice"
#line hidden
[Sybase.PowerBuilder.PBGroupAttribute("n_cst_datawindowsservice",Sybase.PowerBuilder.PBGroupType.UserObject,"","c:\\gcoop_all\\core\\gcoop\\pbservice125\\pbsrvcom\\common.pbl\\common.pblx",null,"pbservice125")]
internal class c__n_cst_datawindowsservice : Sybase.PowerBuilder.PBNonVisualObject
{
	#line hidden
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "status_running", null, "n_cst_datawindowsservice", 8, typeof(Sybase.PowerBuilder.PBInt), Sybase.PowerBuilder.PBModifier.Public, "= 8")]
	[Sybase.PowerBuilder.PBConstantAttribute()]
	static public Sybase.PowerBuilder.PBInt status_running = new Sybase.PowerBuilder.PBInt(8);
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "status_success", null, "n_cst_datawindowsservice", 1, typeof(Sybase.PowerBuilder.PBInt), Sybase.PowerBuilder.PBModifier.Public, "= 1")]
	[Sybase.PowerBuilder.PBConstantAttribute()]
	static public Sybase.PowerBuilder.PBInt status_success = new Sybase.PowerBuilder.PBInt(1);
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "status_failure", null, "n_cst_datawindowsservice", -1, typeof(Sybase.PowerBuilder.PBInt), Sybase.PowerBuilder.PBModifier.Public, "= - 1")]
	[Sybase.PowerBuilder.PBConstantAttribute()]
	static public Sybase.PowerBuilder.PBInt status_failure = new Sybase.PowerBuilder.PBInt(-1);
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "status_stop", null, "n_cst_datawindowsservice", 0, typeof(Sybase.PowerBuilder.PBInt), Sybase.PowerBuilder.PBModifier.Public, "= 0")]
	[Sybase.PowerBuilder.PBConstantAttribute()]
	static public Sybase.PowerBuilder.PBInt status_stop = new Sybase.PowerBuilder.PBInt(0);
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "istr_progress", null, "n_cst_datawindowsservice", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public c__str_progress istr_progress = (c__str_progress) Sybase.PowerBuilder.PBSessionBase.GetCurrentSession().CreateInstance(typeof(c__str_progress));
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "itr_sqlca", null, "n_cst_datawindowsservice", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public Sybase.PowerBuilder.PBTransaction itr_sqlca = null;
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "ithw_exception", null, "n_cst_datawindowsservice", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public Sybase.PowerBuilder.PBException ithw_exception = null;
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "ib_stop", null, "n_cst_datawindowsservice", false, typeof(Sybase.PowerBuilder.PBBoolean), Sybase.PowerBuilder.PBModifier.Protected, "= false")]
	protected Sybase.PowerBuilder.PBBoolean ib_stop = new Sybase.PowerBuilder.PBBoolean(false);

	#line 1 "n_cst_datawindowsservice.of_create_dw(IRCn_ds.SS)"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_create_dw", new string[]{"ref n_ds", "string", "string"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	public virtual Sybase.PowerBuilder.PBInt of_create_dw_3_1438817508<T0>(ref T0 ads_data, Sybase.PowerBuilder.PBString as_sql, Sybase.PowerBuilder.PBString as_type) where T0 : c__n_ds
	{
		#line hidden
		Sybase.PowerBuilder.PBString ls_errors = Sybase.PowerBuilder.PBString.DefaultValue;
		Sybase.PowerBuilder.PBString ls_presentation = Sybase.PowerBuilder.PBString.DefaultValue;
		Sybase.PowerBuilder.PBString ls_dwsyntax = Sybase.PowerBuilder.PBString.DefaultValue;
		#line 25
		as_type = Sybase.PowerBuilder.WPF.PBSystemFunctions.Trim(as_type);
		#line hidden
		#line 26
		if ((Sybase.PowerBuilder.PBBoolean)(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(as_type))))| (as_type == new Sybase.PowerBuilder.PBString(""))))
		#line hidden
		{
			#line 26
			as_type = new Sybase.PowerBuilder.PBString("Tabular");
			#line hidden
		}
		#line 28
		ls_presentation = (new Sybase.PowerBuilder.PBString("style(type=")+ as_type)+ new Sybase.PowerBuilder.PBString(")");
		#line hidden
		#line 30
		ls_dwsyntax = itr_sqlca.SyntaxFromSQL(as_sql, ls_presentation, ref ls_errors);
		#line hidden
		#line 32
		if ((Sybase.PowerBuilder.PBBoolean)(Sybase.PowerBuilder.WPF.PBSystemFunctions.Len(ls_errors)> (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(0))))
		#line hidden
		{
			#line 33
			ithw_exception.Text += new Sybase.PowerBuilder.PBString("Caution :> Create syntax from sql these errors: ");
			#line hidden
			#line 34
			ithw_exception.Text += new Sybase.PowerBuilder.PBString("\r\n")+ ls_errors;
			#line hidden

			#line 35
			Sybase.PowerBuilder.DSI.PBSQL.DSISQLFunc.Rollback(Sybase.PowerBuilder.WPF.PBSession.CurrentSession, itr_sqlca);
			#line hidden
			#line 36
			throw new Sybase.PowerBuilder.PBExceptionE(ithw_exception);
			#line hidden
		}
		#line 39
		ads_data.Create(ls_dwsyntax, ref ls_errors);
		#line hidden
		#line 41
		if ((Sybase.PowerBuilder.PBBoolean)(Sybase.PowerBuilder.WPF.PBSystemFunctions.Len(ls_errors)> (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(0))))
		#line hidden
		{
			#line 43
			ithw_exception.Text += new Sybase.PowerBuilder.PBString("Caution :> Create cause these errors: ");
			#line hidden
			#line 44
			ithw_exception.Text += new Sybase.PowerBuilder.PBString("\r\n")+ ls_errors;
			#line hidden

			#line 45
			Sybase.PowerBuilder.DSI.PBSQL.DSISQLFunc.Rollback(Sybase.PowerBuilder.WPF.PBSession.CurrentSession, itr_sqlca);
			#line hidden
			#line 46
			throw new Sybase.PowerBuilder.PBExceptionE(ithw_exception);
			#line hidden
		}
		#line 50
		return new Sybase.PowerBuilder.PBInt(1);
		#line hidden
	}

	#line 1 "n_cst_datawindowsservice.of_initservice(ICn_cst_dbconnectservice.)"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_initservice", new string[]{"n_cst_dbconnectservice"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	public virtual Sybase.PowerBuilder.PBInt of_initservice(c__n_cst_dbconnectservice atr_dbtrans)
	{
		#line hidden
		#line 1
		itr_sqlca = atr_dbtrans.itr_dbconnection;
		#line hidden
		#line 3
		return new Sybase.PowerBuilder.PBInt(1);
		#line hidden
	}

	#line 1 "n_cst_datawindowsservice.of_update_dw(IRCn_ds.SS[]S[]SB)"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_update_dw", new string[]{"ref n_ds", "string", "string", "string", "string", "boolean"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	public virtual Sybase.PowerBuilder.PBInt of_update_dw_6_1332251270<T0>(ref T0 ads_data, Sybase.PowerBuilder.PBString as_table, [Sybase.PowerBuilder.PBArrayAttribute(typeof(Sybase.PowerBuilder.PBString))] Sybase.PowerBuilder.PBArray as_column, [Sybase.PowerBuilder.PBArrayAttribute(typeof(Sybase.PowerBuilder.PBString))] Sybase.PowerBuilder.PBArray as_keycolumn, Sybase.PowerBuilder.PBString as_whereoption, Sybase.PowerBuilder.PBBoolean ab_kepupdateinplace) where T0 : c__n_ds
	{
		#line hidden
		Sybase.PowerBuilder.PBString ls_mod_string = Sybase.PowerBuilder.PBString.DefaultValue;
		Sybase.PowerBuilder.PBString ls_column = Sybase.PowerBuilder.PBString.DefaultValue;
		Sybase.PowerBuilder.PBInt li_num_cols = Sybase.PowerBuilder.PBInt.DefaultValue;
		Sybase.PowerBuilder.PBInt li_num_updateable = Sybase.PowerBuilder.PBInt.DefaultValue;
		Sybase.PowerBuilder.PBInt li_num_keys = Sybase.PowerBuilder.PBInt.DefaultValue;
		Sybase.PowerBuilder.PBInt li_updcol = Sybase.PowerBuilder.PBInt.DefaultValue;
		Sybase.PowerBuilder.PBInt li_keycol = Sybase.PowerBuilder.PBInt.DefaultValue;
		Sybase.PowerBuilder.PBInt li_rc = Sybase.PowerBuilder.PBInt.DefaultValue;
		Sybase.PowerBuilder.PBBoolean lb_is_key = Sybase.PowerBuilder.PBBoolean.DefaultValue;
		Sybase.PowerBuilder.PBString ls_error = Sybase.PowerBuilder.PBString.DefaultValue;
		#line 7
		li_num_cols = Sybase.PowerBuilder.WPF.PBSystemFunctions.Integer((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(ads_data.Describe(new Sybase.PowerBuilder.PBString("DataWindow.Column.Count"))))));
		#line hidden
		#line 10
		ls_mod_string = (new Sybase.PowerBuilder.PBString("DataWindow.Table.UpdateTable='")+ as_table)+ new Sybase.PowerBuilder.PBString("' ");
		#line hidden
		#line 13
		ls_mod_string += (new Sybase.PowerBuilder.PBString("DataWindow.Table.UpdateWhere=")+ as_whereoption)+ new Sybase.PowerBuilder.PBString(" ");
		#line hidden
		#line 16
		if (ab_kepupdateinplace)
		#line hidden
		{
			#line 17
			ls_mod_string += new Sybase.PowerBuilder.PBString("DataWindow.Table.UpdateKeyInPlace=yes ");
			#line hidden
		}
		else
		{
			#line 19
			ls_mod_string += new Sybase.PowerBuilder.PBString("DataWindow.Table.UpdateKeyInPlace=no ");
			#line hidden
		}
		#line 23
		li_num_updateable = (Sybase.PowerBuilder.PBInt)(Sybase.PowerBuilder.WPF.PBSystemFunctions.UpperBound((Sybase.PowerBuilder.PBAny)(as_column)));
		#line hidden
		#line 24
		li_num_keys = (Sybase.PowerBuilder.PBInt)(Sybase.PowerBuilder.WPF.PBSystemFunctions.UpperBound((Sybase.PowerBuilder.PBAny)(as_keycolumn)));
		#line hidden
		#line 26
		for (li_updcol = new Sybase.PowerBuilder.PBInt(1);li_updcol <= li_num_cols;li_updcol = li_updcol + 1)
		#line hidden
		{
				#line 27
				ls_column = ads_data.Describe((new Sybase.PowerBuilder.PBString("#")+ Sybase.PowerBuilder.WPF.PBSystemFunctions.String((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(li_updcol)))))+ new Sybase.PowerBuilder.PBString(".Name"));
				#line hidden
				#line 29
				ls_mod_string += ls_column+ new Sybase.PowerBuilder.PBString(".Update=Yes ");
				#line hidden
				#line 31
				lb_is_key = new Sybase.PowerBuilder.PBBoolean(false);
				#line hidden
				#line 32
				for (li_keycol = new Sybase.PowerBuilder.PBInt(1);li_keycol <= li_num_keys;li_keycol = li_keycol + 1)
				#line hidden
				{
						#line 33
						if ((Sybase.PowerBuilder.PBBoolean)(Sybase.PowerBuilder.WPF.PBSystemFunctions.Lower(ls_column) == Sybase.PowerBuilder.WPF.PBSystemFunctions.Lower(((Sybase.PowerBuilder.PBString)as_keycolumn[(Sybase.PowerBuilder.PBLong)(li_num_keys)]))))
						#line hidden
						{
							#line 34
							lb_is_key = new Sybase.PowerBuilder.PBBoolean(true);
							#line hidden
							#line 35
							break;
							#line hidden
						}
				}
				#line 39
				if (lb_is_key)
				#line hidden
				{
					#line 40
					ls_mod_string += ls_column+ new Sybase.PowerBuilder.PBString(".Key=Yes ");
					#line hidden
				}
				else
				{
					#line 42
					ls_mod_string += ls_column+ new Sybase.PowerBuilder.PBString(".Key=No ");
					#line hidden
				}
		}
		#line 47
		ls_error = ads_data.Modify(ls_mod_string);
		#line hidden
		#line 49
		if ((Sybase.PowerBuilder.PBBoolean)(ads_data.Modify(ls_mod_string) != new Sybase.PowerBuilder.PBString("")))
		#line hidden
		{
			#line 50
			return new Sybase.PowerBuilder.PBInt(-2);
			#line hidden
		}
		#line 53
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

	#line 1 "n_cst_datawindowsservice.constructor"
	#line hidden
	[Sybase.PowerBuilder.PBEventAttribute("constructor")]
	[Sybase.PowerBuilder.PBEventToken(Sybase.PowerBuilder.PBEventTokens.pbm_constructor)]
	public override Sybase.PowerBuilder.PBLong constructor()
	{
		#line hidden
		Sybase.PowerBuilder.PBLong ancestorreturnvalue = Sybase.PowerBuilder.PBLong.DefaultValue;
		#line 2
		ithw_exception = (Sybase.PowerBuilder.PBException)this.CreateInstance(typeof(Sybase.PowerBuilder.PBException), 0);
		#line hidden
		return new Sybase.PowerBuilder.PBLong(0);
	}

	#line 1 "n_cst_datawindowsservice.destructor"
	#line hidden
	[Sybase.PowerBuilder.PBEventAttribute("destructor")]
	[Sybase.PowerBuilder.PBEventToken(Sybase.PowerBuilder.PBEventTokens.pbm_destructor)]
	public override Sybase.PowerBuilder.PBLong destructor()
	{
		#line hidden
		Sybase.PowerBuilder.PBLong ancestorreturnvalue = Sybase.PowerBuilder.PBLong.DefaultValue;
		#line 1
		if (Sybase.PowerBuilder.WPF.PBSystemFunctions.IsValid((Sybase.PowerBuilder.PBPowerObject)(ithw_exception)))
		#line hidden
		{
			#line 1
			Sybase.PowerBuilder.WPF.PBSession.CurrentSession.DestroyObject(ithw_exception);
			#line hidden
		}
		return new Sybase.PowerBuilder.PBLong(0);
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

	public c__n_cst_datawindowsservice()
	{
		_init();
	}


	public static explicit operator c__n_cst_datawindowsservice(Sybase.PowerBuilder.PBAny v)
	{
		if (v.Value is Sybase.PowerBuilder.PBUnboundedAnyArray)
		{
			Sybase.PowerBuilder.PBUnboundedAnyArray a = (Sybase.PowerBuilder.PBUnboundedAnyArray)v.Value;
			c__n_cst_datawindowsservice vv = new c__n_cst_datawindowsservice();
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
			return (c__n_cst_datawindowsservice) v.Value;
		}
	}
}
 