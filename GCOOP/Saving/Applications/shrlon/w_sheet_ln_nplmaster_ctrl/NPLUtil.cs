using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLibrary;
using System.Data;

namespace Saving.Applications.shrlon.w_sheet_ln_nplmaster_ctrl
{
    public class NPLUtil
    {
        //        public String coopId;

        //        public static Decimal GetIntestimate(String coopId, String loancontractNo)
        //        {
        //            NPLUtil lnv_loansrv = new NPLUtil();
        //            lnv_loansrv.coopId = coopId;
        //            string sql = @"
        //                    SELECT
        //        	            LOANTYPE_CODE,      STARTCONT_DATE ,	PERIOD_PAYAMT,	contractint_type,		principal_balance,		INTEREST_ARREAR,
        //        	            lastcalint_date,		contintexpire_date,		contract_interest
        //                    FROM			lncontmaster
        //                    WHERE coop_id = {0} and		LOANCONTRACT_NO = {1}
        //                    ";
        //            sql = WebUtil.SQLFormat(sql, coopId, loancontractNo);
        //            Sdt dt = WebUtil.QuerySdt(sql);
        //            if (!dt.Next())
        //            {
        //                throw new Exception("ไม่พบข้อมูลดอกเบี้ยประมาณของสัญญา " + loancontractNo);
        //            }
        //            int contIntType = dt.GetInt32("contractint_type");
        //            String loantype = dt.GetString("LOANTYPE_CODE");
        //            DateTime startCont = dt.GetDate("STARTCONT_DATE");
        //            int periodpay = dt.GetInt32("PERIOD_PAYAMT");
        //            int contintType = dt.GetInt32("contractint_type");
        //            decimal balance = dt.GetDecimal("principal_balance");
        //            decimal interestArrear = dt.GetDecimal("INTEREST_ARREAR");
        //            DateTime lastcalint = dt.GetDate("lastcalint_date");
        //            DateTime calintto = DateTime.Today;
        //            DateTime contintexp = dt.GetDate("contintexpire_date");
        //            decimal contintrate = dt.GetDecimal("contract_interest");
        //            decimal intAmt = 0;
        //            DateTime calIntTo = DateTime.Today;
        //            if (contIntType == 0)
        //            {
        //                intAmt = lnv_loansrv.of_computeinterest(loantype, balance, lastcalint, calintto);
        //            }
        //            else if (contIntType == 1)
        //            {
        //                if (calIntTo <= contintexp || contintexp.Year <= 1900)
        //                {
        //                    intAmt = lnv_loansrv.of_computeinterest(loantype, balance, lastcalint, calintto, contintrate);
        //                }
        //                else
        //                {
        //                    decimal intTemp = lnv_loansrv.of_computeinterest(loantype, balance, lastcalint, contintexp, contintrate);
        //                    intAmt = lnv_loansrv.of_computeinterest(loantype, balance, contintexp, calintto);
        //                    if (intTemp < 0) intTemp = 0;
        //                    if (intAmt < 0) intAmt = 0;
        //                    intAmt += intTemp;
        //                }
        //            }
        //            else if (contIntType == 2)
        //            {
        //                intAmt = lnv_loansrv.of_computeinterest(loantype, balance, lastcalint, calintto, contintrate);
        //            }
        //            intAmt += interestArrear;
        //            if (intAmt < 0) intAmt = 0;
        //            return intAmt;
        //        }

        //        private object of_getattribconstant(string column)
        //        {
        //            string sql = "select " + column + " from LNLOANCONSTANT";
        //            DataTable dt = WebUtil.Query(sql);
        //            return dt.Rows[0][column];
        //        }

        //        private object of_getattribloantype(string as_loantype, string as_attribloan)
        //        {
        //            string sql = "select " + as_attribloan + " from lnloantype where coop_id={0} and loantype_code={1}";
        //            sql = WebUtil.SQLFormat(sql, coopId, as_loantype);
        //            return WebUtil.Query(sql).Rows[0][0];
        //        }

        //        private decimal of_roundmoney(decimal adc_money, int ai_type)
        //        {
        //            string ls_int, ls_frac;
        //            string ls_dec;
        //            ls_dec = adc_money.ToString();
        //            ls_int = Math.Floor(adc_money).ToString("0");
        //            if (ls_dec.IndexOf(".") >= 0)
        //            {
        //                ls_frac = ls_dec.Substring(ls_dec.IndexOf(".") + 1);
        //            }
        //            else
        //            {
        //                ls_frac = "";
        //            }
        //            if (ai_type == 1)
        //            {
        //                int ii = ai_type * 100;
        //                if (ii >= 1 && ii <= 25)
        //                {
        //                    ls_frac = "0.25";
        //                }
        //                else if (ii >= 26 && ii <= 50)
        //                {
        //                    ls_frac = "0.50";
        //                }
        //                else if (ii >= 51 && ii <= 75)
        //                {
        //                    ls_frac = "0.75";
        //                }
        //                else if (ii >= 76 && ii <= 99)
        //                {
        //                    ls_frac = "1.00";
        //                }
        //                else
        //                {
        //                    ls_frac = "0";
        //                }
        //                ls_dec = (Convert.ToDecimal(ls_int) + Convert.ToDecimal(ls_frac)).ToString();
        //            }
        //            else if (ai_type == 2)
        //            {
        //                ls_dec = Math.Round(adc_money, 1).ToString();
        //            }
        //            else if (ai_type == 3)
        //            {
        //                ls_dec = Math.Round(adc_money, 0).ToString();
        //            }
        //            else if (ai_type == 4)
        //            {
        //                ls_dec = adc_money.ToString("0.00");
        //                ls_int = ls_dec.Substring(0, ls_dec.IndexOf(".") + 1);
        //                ls_frac = WebUtil.Right(ls_dec, 1);// right( ls_dec, 1 )
        //                int ii = int.Parse(ls_frac);
        //                if (ii >= 1 && ii <= 5)
        //                {
        //                    ls_frac = "0.05";
        //                }
        //                else if (ii >= 6 && ii <= 9)
        //                {
        //                    ls_frac = "0.10";
        //                }
        //                else
        //                {
        //                    ls_frac = "0";
        //                }
        //                ls_dec = (Convert.ToDecimal(ls_int) + Convert.ToDecimal(ls_frac)).ToString();
        //            }
        //            return Convert.ToDecimal(ls_dec);
        //        }

        //        private DataTable of_getinteresttable(String as_loantype, DateTime adtm_enddate)
        //        {
        //            DataTable ads_inttable;
        //            string ls_expr;
        //            int ll_count, li_chk;

        //            as_loantype = as_loantype.Trim();
        //            ls_expr = "loantype_code = '" + as_loantype+"' and effective_date <= '" + adtm_enddate.ToString("yyyy-MM-dd", WebUtil.EN) + "'";

        //            //ids_inttable.setfilter( ls_expr  )
        //            //ids_inttable.filter()

        //            //ll_count	= ids_inttable.rowcount()

        //            //ids_inttable.setsort( "effective_date A , loan_step A " )
        //            //ids_inttable.sort()

        //            //ads_inttable.reset()

        //            //li_chk		= ids_inttable.rowscopy( 1, ll_count, primary!, ads_inttable, 1, primary! )

        //            //ll_count	= ads_inttable.rowcount()

        //            //ids_inttable.setfilter("")
        //            //ids_inttable.filter()

        //            //if isvalid( gnv_app.inv_debug ) then 
        //            //    gnv_app.inv_debug.of_message( ls_expr + " จำนวนแถว :" + string(ads_inttable.rowcount() ) )
        //            //end if

        //            return ads_inttable;

        //        }

        //        private decimal of_computeinterest(String as_contno, DateTime adtm_calintto)
        //        {
        //            //string		ls_loantype
        //            //integer		li_paystatus, li_continttype
        //            //dec{2}		ldc_balance, ldc_intamt, ldc_inttemp
        //            //dec{4}		ldc_contintrate
        //            //datetime	ldtm_lastcalint, ldtm_contintexp

        //            //select	loantype_code,
        //            //        principal_balance,
        //            //        lastcalint_date,
        //            //        payment_status,
        //            //        contractint_type,
        //            //        contract_interest,
        //            //        contintexpire_date
        //            //into		:ls_loantype, :ldc_balance, :ldtm_lastcalint, :li_paystatus,
        //            //        :li_continttype, :ldc_contintrate, :ldtm_contintexp
        //            //from	lncontmaster
        //            //where	( loancontract_no	= :as_contno )  ;

        //            //// ไม่พบข้อมูล
        //            //if sqlca.sqlcode <> 0 then
        //            //    return 0
        //            //end if

        //            //// สถานะงด ด/บ
        //            //if li_paystatus = status_drop_interest then
        //            //    return 0
        //            //end if

        //            //// set null ให้อายุ ด/บ ถ้าเป็น 1900-01-01
        //            //if string( ldtm_contintexp, "yyyymmdd" ) = "19000101" then
        //            //    setnull( ldtm_contintexp )
        //            //end if

        //            //if( ldtm_lastcalint > ldtm_contintexp ) then
        //            //    setnull( ldtm_contintexp )
        //            //end if

        //            //messagebox( "MIT", string( ldtm_lastcalint ) )

        //            //choose case li_continttype
        //            //    case 0	// ตามประกาศ
        //            //        ldc_intamt		= this.of_computeinterest( ls_loantype, ldc_balance, ldtm_lastcalint, adtm_calintto )
        //            //    case 1	// คงที่มีระยะเวลา
        //            //        if date( adtm_calintto ) > date( ldtm_contintexp ) then

        //            //            ldc_inttemp	= this.of_computeinterest( ls_loantype, ldc_balance, ldtm_lastcalint, ldtm_contintexp, ldc_contintrate )

        //            //            ldc_intamt		= this.of_computeinterest( ls_loantype, ldc_balance, ldtm_contintexp, adtm_calintto )

        //            //            if ldc_inttemp < 0 then ldc_inttemp = 0
        //            //            if ldc_intamt < 0 then ldc_intamt = 0

        //            //            ldc_intamt	+= ldc_inttemp
        //            //        else
        //            //            ldc_intamt		= this.of_computeinterest( ls_loantype, ldc_balance, ldtm_lastcalint, adtm_calintto, ldc_contintrate )
        //            //        end if
        //            //    case 2	// คงที่ตลอดอายุสัญญา
        //            //        ldc_intamt		= this.of_computeinterest( ls_loantype, ldc_balance, ldtm_lastcalint, adtm_calintto, ldc_contintrate )
        //            //end choose

        //            //if ldc_intamt < 0 then ldc_intamt = 0

        //            //return ldc_intamt
        //            return 0;
        //        }

        //        private decimal of_computeinterest(String as_loantype, decimal adc_principal, DateTime adtm_calintfrom, DateTime adtm_calintto)
        //        {
        //            int li_caltype;
        //            try
        //            {
        //                li_caltype = Convert.ToInt32(this.of_getattribconstant("countdate_type"));
        //            }
        //            catch
        //            {
        //                li_caltype = 0;
        //            }
        //            if (li_caltype > 2) li_caltype = 0;
        //            return this.of_computeinterest(as_loantype, adc_principal, adtm_calintfrom, adtm_calintto, li_caltype);
        //        }

        //        private decimal of_computeinterest(string as_loantype, decimal adc_principal, DateTime adtm_calintfrom, DateTime adtm_calintto, decimal adc_memint)
        //        {
        //            int li_type;
        //            try
        //            {
        //                li_type = Convert.ToInt32(this.of_getattribconstant("countdate_type"));
        //            }
        //            catch
        //            {
        //                li_type = 0;
        //            }
        //            if (li_type > 3) li_type = 0;
        //            return this.of_computeinterest(as_loantype, adc_principal, adtm_calintfrom, adtm_calintto, adc_memint, li_type);
        //        }

        //        private decimal of_computeinterest(string as_loantype, decimal adc_principal, DateTime adtm_calintfrom, DateTime adtm_calintto, decimal adc_memint, int ai_caltype)
        //        {
        //            int li_dayyear, li_calintmethod;
        //            int li_introundstatus, li_introundnum, li_introundtype, li_introundfmt;
        //            decimal ldc_intamount = 0;

        //            // ตรวจสอบ Parameter ที่ส่งเข้ามา
        //            if (adtm_calintfrom > adtm_calintto) return 0;
        //            if (adc_principal == 0) return 0;
        //            if (string.IsNullOrEmpty(as_loantype)) return 0;

        //            // ทศนิยมของ ด/บ
        //            li_introundnum = Convert.ToInt32(this.of_getattribconstant("intround_num"));

        //            // รูปแบบทศนิยม
        //            li_introundfmt = Convert.ToInt32(this.of_getattribconstant("intround_format"));

        //            // การปัด ด/บ 2 หลัก
        //            li_introundtype = Convert.ToInt32(this.of_getattribconstant("introundsum_type"));

        //            //--------- รูปแบบการคำนวณดอกเบี้ย 01 - รายวัน, 02 - รายเดือน ----------
        //            li_calintmethod = Convert.ToInt32(of_getattribloantype(as_loantype, "interest_method"));
        //            if (li_calintmethod == 1)
        //            {
        //                li_dayyear = Convert.ToInt32(this.of_getattribconstant("dayinyear"));
        //                ldc_intamount = this.of_calculateinterest(adc_principal, adc_memint, adtm_calintfrom, adtm_calintto, li_dayyear, ai_caltype, li_introundnum, li_introundtype, li_introundfmt);
        //            }

        //            // ปัดเศษ ดอกเบี้ย
        //            li_introundstatus = Convert.ToInt32(this.of_getattribconstant("intround_type"));
        //            if (li_introundstatus > 0)
        //            {
        //                ldc_intamount = this.of_roundmoney(ldc_intamount, li_introundstatus);
        //            }

        //            return ldc_intamount;
        //        }

        //        private decimal of_computeinterest(string as_loantype, decimal adc_principal, DateTime adtm_calintfrom, DateTime adtm_calintto, int ai_caltype)
        //        {
        //            string ls_intcode;
        //            int li_dayyear, li_calintmethod;
        //            int li_introundnum, li_introundtype, li_introundfmt;
        //            decimal ldc_intamount;
        //            Sdt lds_inttable;

        //            // ตรวจสอบ Parameter ที่ส่งเข้ามา
        //            if (adtm_calintfrom > adtm_calintto) return 0;
        //            if (adc_principal == 0) return 0;
        //            if (string.IsNullOrEmpty(as_loantype)) return 0;

        //            // ทศนิยมของ ด/บ
        //            li_introundnum = Convert.ToInt32(this.of_getattribconstant("intround_num"));

        //            // รูปแบบทศนิยม
        //            li_introundfmt = Convert.ToInt32(this.of_getattribconstant("intround_format"));

        //            // การปัด ด/บ 2 หลัก
        //            li_introundtype = Convert.ToInt32(this.of_getattribconstant("introundsum_type"));

        //            // ดึงตารางดอกเบี้ย ----------
        //            ls_intcode = this.of_getattribloantype(as_loantype, "continttable_code").ToString();
        //            this.of_getinteresttable(ls_intcode, adtm_calintto, lds_inttable);

        //            // รูปแบบการคำนวณดอกเบี้ย 1 - รายวัน, 2 - รายเดือน ----------
        //            li_calintmethod = Convert.ToInt32(of_getattribloantype(as_loantype, "interest_method"));

        //            if (li_calintmethod == 1)
        //            {
        //                // รายวัน
        //                li_dayyear = Convert.ToInt32(this.of_getattribconstant("dayinyear"));
        //                ldc_intamount = this.of_calculateinterest(adc_principal, lds_inttable, adtm_calintfrom, adtm_calintto, li_dayyear, ai_caltype, li_introundnum, li_introundtype, li_introundfmt);
        //            }
        //            else if (li_calintmethod == 2)
        //            {
        //                // รายเดือน
        //                ldc_intamount = this.of_calculateintmonth(as_loantype, adc_principal, lds_inttable, adtm_calintfrom, adtm_calintto);
        //            }
        //            else
        //            {
        //                // ไม่รู้ว่าคิดแบบไหน
        //                return 0;
        //            }

        //            // ปัดเศษ ดอกเบี้ย
        //            int li_introundstatus;
        //            li_introundstatus = Convert.ToInt32(this.of_getattribconstant("intround_type"));
        //            if (li_introundstatus > 0)
        //            {
        //                ldc_intamount = this.of_roundmoney(ldc_intamount, li_introundstatus);
        //            }
        //            //destroy( lds_inttable )
        //            return ldc_intamount;
        //        }

        //        private decimal of_calculateinterest(decimal adc_principal, decimal adc_intrate, DateTime adtm_calintfrom, DateTime adtm_calintto, int ai_daysinyear, int ai_caltype)
        //        {
        //            int li_roundnum, li_roundsumtype, li_roundformat;

        //            li_roundnum = Convert.ToInt32(this.of_getattribconstant("intround_num"));
        //            li_roundsumtype = Convert.ToInt32(this.of_getattribconstant("introundsum_type"));
        //            li_roundformat = Convert.ToInt32(this.of_getattribconstant("intround_format"));
        //            return this.of_calculateinterest(adc_principal, adc_intrate, adtm_calintfrom, adtm_calintto, ai_daysinyear, ai_caltype, li_roundnum, li_roundsumtype, li_roundformat);
        //        }

        //        private decimal of_calculateinterest(decimal adc_principal, decimal adc_intrate, DateTime adtm_calintfrom, DateTime adtm_calintto, int ai_daysinyear, int ai_caltype, int ai_roundnum, int ai_roundsumtype, int ai_roundformat)
        //        {
        //            string ls_debugmsg, ls_format;
        //            int li_daysinyear, li_dayinterest, li_introundstatus;
        //            decimal ldc_inttemp, ldc_interestamt;
        //            DateTime ldtm_calintfrom, ldtm_calintto;

        //            // ตรวจสอบว่าวันที่ส่งเข้ามาถูกต้องหรือไม่
        //            if (adtm_calintto < adtm_calintfrom) return 0;

        //            // ตรวจสอบยอดเงินที่ส่งเข้ามา
        //            if (adc_principal == 0) return 0;

        //            // กำหนด format การแสดงผลขึ้นกับว่าปัดเศษหลักที่เท่าไหร่
        //            ls_format = "#,###." + WebUtil.Fill("0", ai_roundnum);

        //            // การปัดเศษ ดอกเบี้ย
        //            li_introundstatus = Convert.ToInt32(this.of_getattribconstant("intround_type"));

        //            ldtm_calintfrom = adtm_calintfrom;
        //            ldc_interestamt = 0;

        //            ldtm_calintto = new DateTime(1500, 1, 1);

        //            while (ldtm_calintto < adtm_calintto)
        //            {
        //                ldtm_calintto = adtm_calintto;

        //                // ตรวจสอบการคิด ด/บ ข้ามปี
        //                if (ai_daysinyear > 0)
        //                {
        //                    li_daysinyear = ai_daysinyear;
        //                }
        //                else
        //                {
        //                    int li_yearfrom, li_yearend;

        //                    li_yearfrom = ldtm_calintfrom.Year;
        //                    li_yearend = ldtm_calintto.Year;

        //                    // ตรวจสอบว่าวันที่คิด ด/บ คาบเกี่ยวระหว่างปีหรือเปล่า
        //                    if (li_yearend - li_yearfrom > 0)
        //                    {
        //                        ldtm_calintto = new DateTime(li_yearfrom + 1, 1, 1);//  datetime( date( li_yearfrom + 1, 1, 1 ) );
        //                    }
        //                    li_daysinyear = new DateTime(ldtm_calintfrom.Year, 12, 31).DayOfYear;//  this.of_daysinyear( year( date( ldtm_calintfrom ) ) )
        //                }

        //                // เริ่มคำนวณ ด/บ
        //                ldc_inttemp = 0;

        //                // นับจำนวนวันสำหรับคำนวณ ด/บ
        //                li_dayinterest = (ldtm_calintto - ldtm_calintfrom).Days;// daysafter(date(ldtm_calintfrom), date(ldtm_calintto));

        //                if (ai_caltype == 1)
        //                {
        //                    if (adtm_calintfrom == ldtm_calintfrom && adtm_calintto != ldtm_calintto)
        //                    {
        //                        // กรณีเป็นวันที่เริ่มคิด ต้อง ลบ 1 วัน
        //                        li_dayinterest--;
        //                    }
        //                    else if (adtm_calintfrom != ldtm_calintfrom && adtm_calintto == ldtm_calintto)
        //                    {
        //                        // กรณีเป็นวันที่คิดถึง ต้อง บวก 1 วัน
        //                        li_dayinterest++;
        //                    }
        //                }
        //                else if (ai_caltype == 2)
        //                {
        //                    if (adtm_calintto == ldtm_calintto)
        //                    {
        //                        // กรณีเป็นวันที่คิดถึงและวันที่คิดถึงไม่ได้เปลี่ยน อัตรา ด/บ ต้อง บวก 1 วัน
        //                        li_dayinterest++;
        //                    }
        //                }

        //                // คิดดอกเบี้ยพักใส่ตัวแปร
        //                ldc_inttemp = adc_principal * adc_intrate * Convert.ToDecimal(li_dayinterest) / Convert.ToDecimal(li_daysinyear);

        //                // ปัดขึ้นหรือลงตามหลักที่กำหนด
        //                if (ai_roundformat == 1)
        //                {
        //                    ldc_inttemp = Math.Round(ldc_inttemp, ai_roundnum);
        //                }
        //                else
        //                {
        //                    ldc_inttemp = WebUtil.MathTruncate(ldc_inttemp, ai_roundnum);
        //                }

        //                // ปัด 2 ขั้นอีกรอบ
        //                ldc_inttemp = Math.Round(ldc_inttemp, 2);

        //                // ดูว่าต้องสตางค์ทุกขั้นหรือเปล่า 1 ปัดทุกขั้น 2 รวมแล้วปัดทีเดียว
        //                if (ai_roundsumtype == 1 && li_introundstatus > 0)
        //                {
        //                    ldc_inttemp = this.of_roundmoney(ldc_inttemp, li_introundstatus);
        //                }
        //                ldtm_calintfrom = ldtm_calintto;
        //                ldc_interestamt += ldc_inttemp;
        //            }
        //            return ldc_interestamt;
        //        }

        //        private decimal of_calculateinterest(decimal adc_principal, Sdt ads_inttable, DateTime adtm_calintfrom, DateTime adtm_calintto, int ai_daysinyear, int ai_caltype)
        //        {
        //            int li_roundnum, li_roundsumtype, li_roundformat;
        //            li_roundnum = Convert.ToInt32(this.of_getattribconstant("intround_num"));
        //            li_roundformat = Convert.ToInt32(this.of_getattribconstant("intround_format"));
        //            li_roundsumtype = Convert.ToInt32(this.of_getattribconstant("introundsum_type"));
        //            return this.of_calculateinterest(adc_principal, ads_inttable, adtm_calintfrom, adtm_calintto, ai_daysinyear, ai_caltype, li_roundnum, li_roundsumtype, li_roundformat);
        //        }

        //        private decimal of_calculateinterest(decimal adc_principal, DataTable ads_inttable, DateTime adtm_calintfrom, DateTime adtm_calintto, int ai_daysinyear, int ai_caltype, int ai_roundnum, int ai_roundsumtype, int ai_roundformat)
        //{
        //    string ls_expr, ls_debugmsg, ls_format;
        //    int li_found, li_index, li_count, li_days, li_daysinyear, li_lastchgint, li_introundstatus;
        //    decimal ldc_tempinterest, ldc_intrate, ldc_interestamt, ldc_tempintamt;
        //    decimal ldc_upperamt, ldc_loweramt, ldc_tempprincipal;
        //    DateTime ldtm_calintfrom, ldtm_calintto, ldtm_fromint;

        //    ldc_interestamt = 0;

        //    // ตรวจสอบว่ามี ตารางดอกเบี้ยหรือไม่
        //    try
        //    {
        //        if (ads_inttable == null || ads_inttable.Rows.Count <= 0) return 0;
        //    }
        //    catch
        //    {
        //        return 0;
        //    }

        //    // ตรวจสอบว่าวันที่ส่งเข้ามาถูกต้องหรือไม่
        //    if (adtm_calintto < adtm_calintfrom) return 0;

        //    // ตรวจสอบยอดเงินที่ส่งเข้ามา
        //    if (adc_principal == 0) return 0;

        //    ldtm_calintfrom = adtm_calintfrom;

        //    // กำหนด format การแสดงผลขึ้นกับว่าปัดเศษหลักที่เท่าไหร่
        //    ls_format = "#,###." + WebUtil.Fill("0", ai_roundnum);

        //    // การปัดเศษ ดอกเบี้ย
        //    li_introundstatus = Convert.ToInt32(this.of_getattribconstant("intround_type"));

        //    ldtm_calintto = new DateTime(1500, 1, 1);
        //    li_lastchgint = 0;
        //    while (ldtm_calintto < adtm_calintto || li_lastchgint == 1)
        //    {
        //        // กำหนดค่าเริ่มต้นให้ตาราง ด/บ
        //        DataView dv1 = ads_inttable.DefaultView;
        //        dv1.RowFilter = "";
        //        dv1.Sort = "loantype_code ASC, effective_date ASC, loan_step ASC";
        //        ads_inttable = dv1.ToTable();

        //        // ค้นหาว่าใช้อัตรา ด/บ ของช่วงไหน
        //        ls_expr = "'" + ldtm_calintfrom.ToString("yyyy-MM-dd", WebUtil.EN) + "' >= effective_date";
        //        li_found = ads_inttable.Select(ls_expr).Length; //.find( ls_expr, ads_inttable.rowcount(), 1 ) ;//  ค้นจากข้างล่างขึ้นมา จะค้นจากข้างบนลงล่างไม่ได้

        //        // กรณีไม่เจอวันที่เริ่มใช้ ด/บ (วันที่จะคิด ด/บ ดันมีก่อน วันที่กำหนด ด/บ วันแรก)
        //        if (li_found <= 0)
        //        {
        //            return -1;
        //        }

        //        // ลบอัตรา ด/บ ก่อนหน้านี้ทั้งหมด
        //        ldtm_fromint = Convert.ToDateTime(ads_inttable.Rows[li_found - 1]["effective_date"]);//.getitemdatetime( li_found, "effective_date" );
        //        ls_expr = "'" + ldtm_fromint.ToString("yyyy-MM-dd", WebUtil.EN) + "' > string( effective_date, 'yyyy-mm-dd' ) ";
        //        li_found = ads_inttable.Select(ls_expr).Length;
        //        if (li_found > 0)
        //        {
        //            //ads_inttable.rowsdiscard( 1, li_found, primary! );
        //        }

        //        // กำหนดวันที่คิด ด/บ ถึง
        //        ls_expr = "effective_date > '" + ldtm_calintfrom.ToString("yyyy-MM-dd", WebUtil.EN) + "'";
        //        li_found = ads_inttable.Select(ls_expr).Length;

        //        if (li_found <= 0)
        //        {
        //            ldtm_calintto = adtm_calintto; // ถ้าไม่มี วันที่เริ่มใช้ใหม่ ใช้วันที่ที่ส่งมา
        //            li_lastchgint = 0;
        //        }
        //        else
        //        {
        //            ldtm_calintto = Convert.ToDateTime(ads_inttable.Rows[li_found - 1]["effective_date"]);//( li_found, "effective_date" );
        //            dv1 = ads_inttable.DefaultView;
        //            dv1.RowFilter = "effective_date < '" + ldtm_calintto.ToString("yyyy-MM-dd", WebUtil.EN) + "'";
        //            ads_inttable = dv1.ToTable();
        //            if (adtm_calintto == ldtm_calintto) li_lastchgint = 1;
        //        }

        //        // กรณีไม่มีแถว ( ไม่น่าเกิด )
        //        if (ads_inttable.Rows.Count <= 0)
        //        {
        //            return -1;
        //        }

        //        // ตรวจสอบการคิด ด/บ ข้ามปี
        //        if (ai_daysinyear > 0)
        //        {
        //            li_daysinyear = ai_daysinyear;
        //        }
        //        else
        //        {
        //            int li_yearfrom, li_yearend;
        //            li_yearfrom = ldtm_calintfrom.Year;//  year( date( ldtm_calintfrom ) )
        //            li_yearend = ldtm_calintto.Year;// year( date( ldtm_calintto ) )

        //            // ตรวจสอบว่าวันที่คิด ด/บ คาบเกี่ยวระหว่างปีหรือเปล่า
        //            if (li_yearend - li_yearfrom > 0)
        //            {
        //                ldtm_calintto = new DateTime(li_yearfrom + 1, 1, 1);// datetime( date( li_yearfrom + 1, 1, 1 ) )
        //            }
        //            li_daysinyear = new DateTime(ldtm_calintfrom.Year, 12, 31).DayOfYear;// this.of_daysinyear( year( date( ldtm_calintfrom ) ) )
        //        }

        //        // เริ่มคำนวณ ด/บ
        //        ldc_tempprincipal = adc_principal;
        //        li_count = ads_inttable.Rows.Count;//.rowcount() // นับจำนวนขั้น

        //        ldc_tempintamt = 0;
        //        for (li_index = 1; li_index <= li_count; li_index++)
        //        {
        //            ldc_loweramt = Convert.ToDecimal(ads_inttable.Rows[li_index - 1]["lower_amount"]);//.getitemdecimal( li_index, "lower_amount" )
        //            ldc_upperamt = Convert.ToDecimal(ads_inttable.Rows[li_index - 1]["upper_amount"]);//.getitemdecimal( li_index, "upper_amount" )
        //            if (adc_principal >= ldc_loweramt)
        //            {
        //                if (adc_principal > ldc_upperamt)
        //                {
        //                    ldc_tempprincipal = ldc_upperamt;
        //                }
        //                else
        //                {
        //                    ldc_tempprincipal = adc_principal;
        //                }
        //            }
        //            else
        //            {
        //                return -1;
        //            }
        //            ldc_tempprincipal = ldc_tempprincipal - ldc_loweramt;// ยอดเงินที่จะคิด ด/บ ขั้นนั้น
        //            ldc_intrate = Convert.ToDecimal(ads_inttable.Rows[li_index - 4]["interest_rate"]);//.getitemdecimal(li_index, "interest_rate");

        //            // นับจำนวนวันสำหรับคำนวณ ด/บ
        //            li_days = (ldtm_calintto - ldtm_calintfrom).Days;//daysafter(date(ldtm_calintfrom), date(ldtm_calintto));

        //            if (ai_caltype == 0)
        //            {
        //                li_lastchgint = 0;
        //            }
        //            else if (ai_caltype == 1)
        //            {
        //                if (adtm_calintfrom == ldtm_calintfrom && adtm_calintto != ldtm_calintto)
        //                {
        //                    // กรณีเป็นวันที่เริ่มคิดแล้วมีการเปลี่ยน ด/บ ก่อนวันที่คิดถึง ต้อง ลบ 1 วัน
        //                    li_days--;
        //                }
        //                else if (adtm_calintfrom == ldtm_calintfrom && adtm_calintto == ldtm_calintto && li_lastchgint == 1)
        //                {
        //                    // กรณีเป็นวันที่เริ่มคิดและวันที่คิดถึงเปลี่ยน ด/บ  ต้อง ลบ 1 วัน
        //                    li_days--;
        //                }
        //                else if (adtm_calintfrom != ldtm_calintfrom && adtm_calintto == ldtm_calintto && li_lastchgint == 0)
        //                {
        //                    // กรณีเป็นวันที่คิดถึงและวันที่คิดถึงไม่ได้เปลี่ยน อัตรา ด/บ ต้อง บวก 1 วัน
        //                    li_days++;
        //                }
        //            }
        //            else if (ai_caltype == 2)
        //            {
        //                if (adtm_calintto == ldtm_calintto && li_lastchgint == 0)
        //                {
        //                    // กรณีเป็นวันที่คิดถึงและวันที่คิดถึงไม่ได้เปลี่ยน อัตรา ด/บ ต้อง บวก 1 วัน
        //                    li_days++;
        //                }
        //            }

        //            ldc_tempinterest = ldc_tempprincipal * ldc_intrate * li_days / li_daysinyear; // คิดดอกเบี้ยพักใส่ตัวแปร

        //            // ปัดเศษขึ้นหลักหรือลงตามที่กำหนดก่อนปัด 2 หลัก
        //            if (ai_roundformat == 1)
        //            {
        //                ldc_tempinterest = Math.Round(ldc_tempinterest, ai_roundnum);
        //            }
        //            else
        //            {
        //                ldc_tempinterest = WebUtil.MathTruncate(ldc_tempinterest, ai_roundnum);
        //            }

        //            // ปัด 2 ขั้นอีกรอบ
        //            ldc_tempinterest = Math.Round(ldc_tempinterest, 2);

        //            // ดูว่าต้องปัดสตางค์ทุกขั้นหรือเปล่า 1 ปัดทุกขั้น 2 รวมแล้วปัดทีเดียว
        //            if (ai_roundsumtype == 1 && li_introundstatus > 0)
        //            {
        //                ldc_tempinterest = this.of_roundmoney(ldc_tempinterest, li_introundstatus);
        //            }

        //            // ปัด 2 ขั้นอีกรอบ
        //            ldc_tempinterest = Math.Round(ldc_tempinterest, 2);

        //            // ดูว่าต้องปัดสตางค์ทุกขั้นหรือเปล่า 1 ปัดทุกขั้น 2 รวมแล้วปัดทีเดียว
        //            if (ai_roundsumtype == 1 && li_introundstatus > 0)
        //            {
        //                ldc_tempinterest = this.of_roundmoney(ldc_tempinterest, li_introundstatus);
        //            }

        //            // สะสมดอกเบี้ยสำหรับขั้นต่อไป
        //            ldc_tempintamt += ldc_tempinterest;
        //        }
        //        ldtm_calintfrom = ldtm_calintto;
        //        ldc_interestamt += ldc_tempintamt;
        //        // จบขั้นตอนการคำนวณ ด/บ
        //    }
        //    return ldc_interestamt;
        //}
    }
}