import java.io.*;
import java.net.*;
import java.util.*;
import java.nio.charset.Charset;
import java.nio.CharBuffer;
import java.nio.ByteBuffer;

class sendsms {
    final protected static char[] hexArray = "0123456789ABCDEF".toCharArray();

    public static String bytesToHex(byte[] bytes) {
        char[] hexChars = new char[bytes.length * 2];
        for ( int i = 0; i < bytes.length; i++ ) {
            int v = bytes[i] & 0xFF;
            hexChars[i * 2] = hexArray[v >>> 4];
            hexChars[i * 2 + 1] = hexArray[v & 0x0F];
        }
        return new String(hexChars);
    }

    public static String toHexString(String s, String charset) {
        try {
            return bytesToHex(s.getBytes(charset));
        } catch(UnsupportedEncodingException e) {
            System.out.println("Unsupported character set" + e);
            return null;
        }
    }

    public static String convertCharset(String text, String from, String to) {
      Charset fromCharset = Charset.forName(from);
      Charset toCharset = Charset.forName(to);
      ByteBuffer inputBuffer = fromCharset.encode(CharBuffer.wrap(text));
      CharBuffer data = fromCharset.decode(inputBuffer);
      ByteBuffer outputBuffer = toCharset.encode(data);
      byte[] outputData = outputBuffer.array();
      return new String(outputData, toCharset);
    }

    public static void main(String[] args) throws Exception {
        //BufferedReader fin = new BufferedReader(new InputStreamReader(new FileInputStream("utf8.txt"), "UTF8"));
        String text = args[5];//fin.readLine();

        String msg = convertCharset(text, "UTF-8", "UTF-16BE");

        URL url = new URL("http://dtacsmsapi4.dtac.co.th/servlet/com.iess.socket.SmsCorplink");
        Map<String,Object> params = new LinkedHashMap<String,Object>();
        params.put("User", args[0]);
        params.put("Password", args[1]);
        params.put("Sender", args[2]);
        params.put("Msn", args[3]);
        params.put("RefNo", args[4]);
        params.put("MsgType", "H");
        params.put("Encoding", "25");
        params.put("Msg", toHexString(msg, "UTF-16BE"));
        //params.put("Msg", args[5]);

        StringBuilder postData = new StringBuilder();
        for (Map.Entry<String,Object> param : params.entrySet()) {
            if (postData.length() != 0) postData.append('&');
            postData.append(URLEncoder.encode(param.getKey(), "UTF-8"));
            postData.append('=');
            postData.append(URLEncoder.encode(String.valueOf(param.getValue()), "UTF-8"));
        }
        byte[] postDataBytes = postData.toString().getBytes("UTF-8");

        HttpURLConnection conn = (HttpURLConnection)url.openConnection();
        conn.setRequestMethod("POST");
        conn.setRequestProperty("Content-Type", "application/x-www-form-urlencoded");
        conn.setRequestProperty("Content-Length", String.valueOf(postDataBytes.length));
        conn.setDoOutput(true);
        conn.getOutputStream().write(postDataBytes);

        Reader in = new BufferedReader(new InputStreamReader(conn.getInputStream(), "UTF-8"));

        StringBuilder sb = new StringBuilder();
        for (int c; (c = in.read()) >= 0;)
            sb.append((char)c);
        String response = sb.toString();

        System.out.println(response);
    }
}
