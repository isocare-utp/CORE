
// convert number to thai
import java.io.*; 
import javax.swing.*;
import java.util.*;
import java.awt.*;
class jav_cnvnumtothaiwa{
    //Variable in Class
   static BufferedReader keyboard = new BufferedReader(new InputStreamReader(System.in)); // �Ѻ��Ҩҡ Keyboard
   static public JTextArea outputTextArea = new JTextArea(10,50) ; //��˹���� textarea
   static String mtch[] = {"","˹��","�ͧ","���","���","���","ˡ","��","Ỵ","���"};
   static String mtdec[]={"�ѹ","����","�Ժ","��ҹ","�ʹ","����","�ѹ","����","�Ժ",""} ;
   static String mstnum="",mstthai="",choice1="",cnumthai="",cin_number = "",tmp_key="" ;
   static double  choice=0;
   static int ncnt = 1,nlen = 0,nlen2 = 0,nnumber=0,din_number2 = 0 ;
      //-------------------------------
     public static void main(Double in_number) throws IOException // main method
    {
         
	   if ((in_number > 9999999999.99) || (in_number <= 0)) {
	        return "" ;
	   }
	    cnumthai = "" ;
	    din_number2 = (int) (in_number*100) ;
	    cin_number = Integer.toString(din_number2) ;
	    mstnum = cin_number ;
	    nlen = cin_number.length() ;
	    nlen2= 12-nlen ;
	    //add "x"
	    for (int xj=0;xj<nlen2;xj++){
	        mstnum = "x"+mstnum ;
	    }
	    for(int xe =0;xe<10;xe++){
	        String  ccharnum = mstnum.substring(xe,xe+1) ;
	        if (ccharnum.equals("x")){
	            cnumthai = cnumthai + "" ;
	       }else{
	           if (ccharnum.equals("0")){
	               if (xe==4){
	                    cnumthai= cnumthai + "��ҹ" ;
	               }else{
	                    cnumthai = cnumthai + "" ;
	               }
	            }else{
	                   if (ccharnum.equals("1")){
	                   if ((xe==3&&nlen!=9)||(xe==9&&nlen!=3)){
	                        cnumthai = cnumthai+"���";
	                   }else {
	                        if (xe!=2||xe!=8) {
	                            cnumthai=cnumthai + mtch[Integer.parseInt(ccharnum)];
	                        }
	                   }
	               }else{
	                   if (ccharnum.equals("2")&&(xe==2||xe==8)) {
	                            cnumthai=cnumthai + "���";
	                    }else{
	                            cnumthai=cnumthai + mtch[Integer.parseInt(ccharnum)];
	                    }
	               }
	               cnumthai = cnumthai+ mtdec[xe] ;
	           }
	       }
	    }
	    cnumthai = cnumthai+"�ҷ" ;
	    //decimal
	    String ccharnum = mstnum.substring(10) ;
	    if (ccharnum.equals("00")){
	        cnumthai = cnumthai+"��ǹ" ;
	   } else{
	        ccharnum = mstnum.substring(10,11) ;
	        if (!ccharnum.equals("0")){
	            if (!ccharnum.equals("1")){
	               if (ccharnum.equals("2")){
	                  cnumthai = cnumthai+"���" ;
	               }else{
	                   cnumthai = cnumthai+mtch[Integer.parseInt(ccharnum)] ;
	               }
	            }
	            cnumthai = cnumthai+mtdec[8] ;
	        }
	         String ccharnum1 = mstnum.substring(11) ;
	            if (!ccharnum1.equals("0")){
	                if (ccharnum1.equals("1")||ccharnum.equals("0")){
	                      cnumthai = cnumthai+"���" ;
	                   }else{
	                       cnumthai = cnumthai+mtch[Integer.parseInt(ccharnum1)] ;
	                   }
	            }
	        cnumthai = cnumthai+"ʵҧ��" ;
	       }
	        return cnumthai ;
    }
    public static String  showmath(String mnext)
    {
        outputTextArea.setText(null);  //��ҧ������
        outputTextArea.append("\n")  ;
        outputTextArea.append("KEY :     "+tmp_key)  ;
        outputTextArea.append("\n")  ;
        outputTextArea.append("\n")  ;
        outputTextArea.append("����������\n")  ;
        outputTextArea.append("\n"+cnumthai)  ;
        outputTextArea.append("\n")  ;
        outputTextArea.setFont(new Font("Tohoma", Font.BOLD, 15));
        outputTextArea.setLineWrap(true);
        outputTextArea.setWrapStyleWord(true);
         return mnext ;
    }
}
 
// end code