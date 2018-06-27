using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReplaceVATTemp
{
    class Program
    {

        static void Main(string[] args)
        {
            int numberRecord = 10;
            //TO UPDATE
            //new LogWriter().LogTableName("RegisterTemp");
            //updateCss("RegisterTemp", "CssData", 0, numberRecord);
            //new LogWriter().LogTableName("InvTemplate");
            //updateCss("InvTemplate", "CssData", 0, numberRecord);

            //TO TEST
            LogWriter log = new LogWriter();
            log.LogTableName("RegisterTemp");
            Test("RegisterTemp");
            log.LogTableName("InvTemplate");
            Test("InvTemplate");
        }

        private static void updateCss(string tableName, string fieldCssName, int offset, int numberRecord)
        {
            string connectionStr = "Data Source=DESKTOP-JIUB5UK\\THANHLD;initial catalog=hddt_qldv;User Id=sa;Password=123456;";
            SqlConnection connection = new SqlConnection(connectionStr + "MultipleActiveResultSets=True");
            connection.Open();
            string sqlStr = string.Format(@"SELECT * FROM {0} ORDER BY id OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY", tableName, offset, numberRecord);
            SqlCommand command = new SqlCommand(sqlStr, connection);
            Dictionary<int, string> result = new Dictionary<int, string>();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add((int)reader["Id"], reader[fieldCssName].ToString());
                }
            }
            new LogWriter().LogTableName("Total record: "+result.Count);
            string testCss = "@charset \"utf-8\";.VATTEMP *{box-sizing:border-box}* html,body{margin:0;padding:0;height:100%;font-size:14px;line-height:1.42857143}#main{margin:0 auto}.VATTEMP{padding:13px 0;background-color:#fff;font-family:\'Time New Roman\';width:808px;font-size:14px!important}.VATTEMP{background-color:#fff;font-family:\'Time New Roman\';margin:13px 0;width:808px;font-size:14px!important}.VATTEMP{padding:13px 0;background-color:#fff;font-family:\'Time New Roman\';margin:13px 0;width:808px;font-size:14px!important}.VATTEMP #header,.VATTEMP #main-content{width:100%;clear:left;overflow:hidden}.VATTEMP .content{padding:10px}.VATTEMP hr{margin:0 0 .1em!important;padding:0!important}.VATTEMP .dotted{border:none;background:0 0;border-bottom:2px dotted #000;height:14px}.VATTEMP .content{width:790px;clear:left;overflow:hidden;margin:0 auto}.VATTEMP .header-title{float:left;width:300px;overflow:hidden;text-align:center}.VATTEMP .header-title p{margin:0}.VATTEMP .header-title h3{margin:0;text-transform:uppercase;color:#06066F}.VATTEMP .header-right{float:right;overflow:hidden}.VATTEMP .header-right p{margin:0 10px 10px;width:100%}.VATTEMP #header .date{clear:left;margin:15px auto 0;color:#06066F}.VATTEMP.text-upper{text-transform:uppercase}.VATTEMP .text-strong{font-weight:700}.VATTEMP .invtable{width:100%;border-collapse:collapse;border:1px solid #000;text-align:center}.VATTEMP .invtable td{border-bottom:1px dotted #000;border-right:1px solid #000;height:25px}.VATTEMP .noborder td{border-bottom:none}.VATTEMP .invtable .data td{border:1px solid #000}.VATTEMP .fl-l{float:left;width:164px;text-align:center}.VATTEMP .fl-r{float:right;width:300px;text-align:center}.VATTEMP .bgimg{border:1px solid red;cursor:pointer}.VATTEMP .bgimg p{color:#000;padding-left:13px;text-align:left}.VATTEMP .comp{float:left;width:80%}.VATTEMP .comp span,.VATTEMP .cus span{display:block;float:left}.VATTEMP .border-label,.VATTEMP .comp label{display:block;float:left;border-bottom:1px dashed #000;padding-bottom:1px;height:18px}.VATTEMP .comp p,.VATTEMP .cus p{position:relative;margin:0 0 4px;padding-bottom:6px;display:block;overflow:hidden}.VATTEMP .comp p strong,.VATTEMP .cus p strong{border:1px solid #000;padding:0 5px 2px;float:left;display:block;width:20px;height:22px}.VATTEMP .comp b:after,.VATTEMP .nextpage b:after{content:\"\";position:absolute;height:6px;margin-left:0}.VATTEMP .summary{width:500px;text-align:right}#author-inv,.VATTEMP .nextpage b:after{border-bottom:1px dashed #000;width:100%}.VATTEMP .comp b:after{bottom:0 width: 100%}.VATTEMP .cus b{padding-bottom:1px;word-wrap:break-word}.VATTEMP .nextpage b:after{bottom:8px}#author-inv{text-align:center;padding-bottom:5px;margin-bottom:0}";
            string testCssOnlyMargin = "@charset \"utf-8\";.VATTEMP *{box-sizing:border-box}* html,body{margin:0;padding:0;height:100%;font-size:14px;line-height:1.42857143}#main{margin:0 auto}.VATTEMP{background-color:#fff;font-family:\'Time New Roman\';margin:13px auto; padding:5px;width:808px;font-size:14px!important}.VATTEMP #header,.VATTEMP #main-content{width:100%;clear:left;overflow:hidden}.VATTEMP .content{padding:10px}.VATTEMP hr{margin:0 0 .1em!important;padding:0!important}.VATTEMP .dotted{border:none;background:0 0;border-bottom:2px dotted #000;height:14px}.VATTEMP .content{width:790px;clear:left;overflow:hidden;margin:0 auto}.VATTEMP .header-title{float:left;width:300px;overflow:hidden;text-align:center}.VATTEMP .header-title p{margin:0}.VATTEMP .header-title h3{margin:0;text-transform:uppercase;color:#06066F}.VATTEMP .header-right{float:right;overflow:hidden}.VATTEMP .header-right p{margin:0 10px 10px;width:100%}.VATTEMP #header .date{clear:left;margin:15px auto 0;color:#06066F}.VATTEMP.text-upper{text-transform:uppercase}.VATTEMP .text-strong{font-weight:700}.VATTEMP .invtable{width:100%;border-collapse:collapse;border:1px solid #000;text-align:center}.VATTEMP .invtable td{border-bottom:1px dotted #000;border-right:1px solid #000;height:25px}.VATTEMP .noborder td{border-bottom:none}.VATTEMP .invtable .data td{border:1px solid #000}.VATTEMP .fl-l{float:left;width:164px;text-align:center}.VATTEMP .fl-r{float:right;width:300px;text-align:center}.VATTEMP .bgimg{border:1px solid red;cursor:pointer}.VATTEMP .bgimg p{color:#000;padding-left:13px;text-align:left}.VATTEMP .comp{float:left;width:80%}.VATTEMP .comp span,.VATTEMP .cus span{display:block;float:left}.VATTEMP .border-label,.VATTEMP .comp label{display:block;float:left;border-bottom:1px dashed #000;padding-bottom:1px;height:18px}.VATTEMP .comp p,.VATTEMP .cus p{position:relative;margin:0 0 4px;padding-bottom:6px;display:block;overflow:hidden}.VATTEMP .comp p strong,.VATTEMP .cus p strong{border:1px solid #000;padding:0 5px 2px;float:left;display:block;width:20px;height:22px}.VATTEMP .comp b:after,.VATTEMP .nextpage b:after{content:\"\";position:absolute;height:6px;margin-left:0}.VATTEMP .summary{width:500px;text-align:right}#author-inv,.VATTEMP .nextpage b:after{border-bottom:1px dashed #000;width:100%}.VATTEMP .comp b:after{bottom:0 width: 100%}.VATTEMP .cus b{padding-bottom:1px;word-wrap:break-word}.VATTEMP .nextpage b:after{bottom:8px}#author-inv{text-align:center;padding-bottom:5px;margin-bottom:0}";
            //(.VATTEMP)(\s*{)([^{]*)(margin)(\s*:)([^:]*)(;)([^}]*)}
            //(.VATTEMP)(\s*{)([^{]*)(margin|padding)(\s*:)([^:]*)(;)([^}]*)}
            //(.VATTEMP)(\s*{)([^{]*)(((margin)(\s*:)([^:]*)(;))|((padding)(\s*:)([^:]*)(;)))([^}]*)}
            Regex regex = new Regex(@"(.VATTEMP)(\s*{)([^{]*)(margin|padding)(\s*:)([^:]*)(;)([^}]*)(})");
            Regex regexPadding = new Regex(@"(.VATTEMP)(\s*{)([^{]*)(padding)(\s*:)([^:]*)(;)([^}]*)(})");
            Regex regexMargin = new Regex(@"(.VATTEMP)(\s*{)([^{]*)(margin)(\s*:)([^:]*)(;)([^}]*)(})");
            Dictionary<int, string> updateList = new Dictionary<int, string>();
            foreach (KeyValuePair<int, string> entry in result)
            {
                //Match mat = regex.Match(testCss);
                MatchCollection matches = regex.Matches(entry.Value);
                if (matches.Count == 1)
                {
                    string vMatch = matches[0].Value;
                    Match matchP = regexPadding.Match(vMatch);
                    Match matchM = regexMargin.Match(vMatch);
                    if (matchP.Success && matchM.Success)
                    {
                        //replace case 1
                        int[] paddingValue = matchP.Groups[6].Value.Trim().Replace("px", "").Replace("auto", "0").Split(' ').Select(Int32.Parse).ToArray();
                        int[] marginValue = matchM.Groups[6].Value.Trim().Replace("px", "").Replace("auto", "0").Split(' ').Select(Int32.Parse).ToArray();
                        string newCssValue = "";
                        if (marginValue.Count() == 2 && paddingValue.Count() == 1)
                            newCssValue = string.Format("{0}px {1}px", marginValue[0] + paddingValue[0], marginValue[1] + paddingValue[0]);
                        else if (marginValue.Count() == 2 && paddingValue.Count() == 2)
                            newCssValue = string.Format("{0}px {1}px", marginValue[0] + paddingValue[0], marginValue[1] + paddingValue[1]);
                        else
                            continue;
                        string rs = Regex.Replace(vMatch, @"(.VATTEMP)(\s*{)([^{]*)(padding\s*:[^:]*;)([^}]*)(})", "$1$2$3$5$6");
                        rs = Regex.Replace(rs, @"(.VATTEMP)(\s*{)([^{]*)(margin)(\s*:)([^:]*)(;)([^}]*)(})", m => string.Format(
                                    "{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                                    m.Groups[1].Value,
                                    m.Groups[2].Value,
                                    m.Groups[3].Value,
                                    "padding",
                                    m.Groups[5].Value,
                                    newCssValue,
                                    m.Groups[7].Value,
                                    m.Groups[8].Value,
                                    m.Groups[9].Value));
                        updateList.Add(entry.Key, entry.Value.Replace(vMatch, rs));
                    }
                    else if (matchP.Success)
                    {
                        continue;
                    }
                    else if (matchM.Success)
                    {
                        //replace case 3
                        string rs = Regex.Replace(vMatch, @"(.VATTEMP)(\s*{)([^{]*)(margin)(\s*:)([^:]*)(;)([^}]*)(})",
                                    m => string.Format(
                                    "{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                                    m.Groups[1].Value,
                                    m.Groups[2].Value,
                                    m.Groups[3].Value,
                                    "padding",
                                    m.Groups[5].Value,
                                    m.Groups[6].Value.Replace("auto", "0px"),
                                    m.Groups[7].Value,
                                    m.Groups[8].Value,
                                    m.Groups[9].Value));
                        updateList.Add(entry.Key, entry.Value.Replace(vMatch, rs));
                    }
                }
                else if (matches.Count == 2)
                {
                    continue;
                }
                else
                {
                    continue;

                }
            }

            new LogWriter().LogTableName("Error record: " + updateList.Count);
            //SqlCommand commandUpdate = new SqlCommand(string.Format("UPDATE {0} SET {1}=@CssData"), connection);
            //commandUpdate.Parameters.AddWithValue("@CssData", );
            if (updateList.Count > 0)
            {
                SqlCommand commandUpdate = new SqlCommand();
                commandUpdate.Connection = connection;
                int i = 0;
                foreach (KeyValuePair<int, string> entry in updateList)
                {
                    commandUpdate.CommandText += string.Format("update {0} set {1}=@CssData{2} where id = @id{2};", tableName, fieldCssName, i);
                    commandUpdate.Parameters.AddWithValue("@CssData" + i, entry.Value);
                    commandUpdate.Parameters.AddWithValue("@id" + i, entry.Key);
                    i++;
                }
                commandUpdate.ExecuteNonQuery();
                
            }
            connection.Close();
        }

        static void Test(string tableName)
        {
            string connectionStr = "Data Source=DESKTOP-JIUB5UK\\THANHLD;initial catalog=hddt_qldv;User Id=sa;Password=123456;";
            SqlConnection connection = new SqlConnection(connectionStr + "MultipleActiveResultSets=True");
            connection.Open();
            string sqlStr = @"SELECT * FROM " + tableName;
            SqlCommand command = new SqlCommand(sqlStr, connection);
            Dictionary<int, string> result = new Dictionary<int, string>();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add((int)reader["Id"], reader["CssData"].ToString());
                }
            }
            new LogWriter().LogTableName("Total record: " + result.Count);
            string testCss = "@charset \"utf-8\";.VATTEMP *{box-sizing:border-box}* html,body{margin:0;padding:0;height:100%;font-size:14px;line-height:1.42857143}#main{margin:0 auto}.VATTEMP{padding:13px 0;background-color:#fff;font-family:\'Time New Roman\';width:808px;font-size:14px!important}.VATTEMP{background-color:#fff;font-family:\'Time New Roman\';margin:13px 0;width:808px;font-size:14px!important}.VATTEMP{padding:13px 0;background-color:#fff;font-family:\'Time New Roman\';margin:13px 0;width:808px;font-size:14px!important}.VATTEMP #header,.VATTEMP #main-content{width:100%;clear:left;overflow:hidden}.VATTEMP .content{padding:10px}.VATTEMP hr{margin:0 0 .1em!important;padding:0!important}.VATTEMP .dotted{border:none;background:0 0;border-bottom:2px dotted #000;height:14px}.VATTEMP .content{width:790px;clear:left;overflow:hidden;margin:0 auto}.VATTEMP .header-title{float:left;width:300px;overflow:hidden;text-align:center}.VATTEMP .header-title p{margin:0}.VATTEMP .header-title h3{margin:0;text-transform:uppercase;color:#06066F}.VATTEMP .header-right{float:right;overflow:hidden}.VATTEMP .header-right p{margin:0 10px 10px;width:100%}.VATTEMP #header .date{clear:left;margin:15px auto 0;color:#06066F}.VATTEMP.text-upper{text-transform:uppercase}.VATTEMP .text-strong{font-weight:700}.VATTEMP .invtable{width:100%;border-collapse:collapse;border:1px solid #000;text-align:center}.VATTEMP .invtable td{border-bottom:1px dotted #000;border-right:1px solid #000;height:25px}.VATTEMP .noborder td{border-bottom:none}.VATTEMP .invtable .data td{border:1px solid #000}.VATTEMP .fl-l{float:left;width:164px;text-align:center}.VATTEMP .fl-r{float:right;width:300px;text-align:center}.VATTEMP .bgimg{border:1px solid red;cursor:pointer}.VATTEMP .bgimg p{color:#000;padding-left:13px;text-align:left}.VATTEMP .comp{float:left;width:80%}.VATTEMP .comp span,.VATTEMP .cus span{display:block;float:left}.VATTEMP .border-label,.VATTEMP .comp label{display:block;float:left;border-bottom:1px dashed #000;padding-bottom:1px;height:18px}.VATTEMP .comp p,.VATTEMP .cus p{position:relative;margin:0 0 4px;padding-bottom:6px;display:block;overflow:hidden}.VATTEMP .comp p strong,.VATTEMP .cus p strong{border:1px solid #000;padding:0 5px 2px;float:left;display:block;width:20px;height:22px}.VATTEMP .comp b:after,.VATTEMP .nextpage b:after{content:\"\";position:absolute;height:6px;margin-left:0}.VATTEMP .summary{width:500px;text-align:right}#author-inv,.VATTEMP .nextpage b:after{border-bottom:1px dashed #000;width:100%}.VATTEMP .comp b:after{bottom:0 width: 100%}.VATTEMP .cus b{padding-bottom:1px;word-wrap:break-word}.VATTEMP .nextpage b:after{bottom:8px}#author-inv{text-align:center;padding-bottom:5px;margin-bottom:0}";
            string testCssOnlyMargin = "@charset \"utf-8\";.VATTEMP *{box-sizing:border-box}* html,body{margin:0;padding:0;height:100%;font-size:14px;line-height:1.42857143}#main{margin:0 auto}.VATTEMP{background-color:#fff;font-family:\'Time New Roman\';margin:13px auto; padding:5px;width:808px;font-size:14px!important}.VATTEMP #header,.VATTEMP #main-content{width:100%;clear:left;overflow:hidden}.VATTEMP .content{padding:10px}.VATTEMP hr{margin:0 0 .1em!important;padding:0!important}.VATTEMP .dotted{border:none;background:0 0;border-bottom:2px dotted #000;height:14px}.VATTEMP .content{width:790px;clear:left;overflow:hidden;margin:0 auto}.VATTEMP .header-title{float:left;width:300px;overflow:hidden;text-align:center}.VATTEMP .header-title p{margin:0}.VATTEMP .header-title h3{margin:0;text-transform:uppercase;color:#06066F}.VATTEMP .header-right{float:right;overflow:hidden}.VATTEMP .header-right p{margin:0 10px 10px;width:100%}.VATTEMP #header .date{clear:left;margin:15px auto 0;color:#06066F}.VATTEMP.text-upper{text-transform:uppercase}.VATTEMP .text-strong{font-weight:700}.VATTEMP .invtable{width:100%;border-collapse:collapse;border:1px solid #000;text-align:center}.VATTEMP .invtable td{border-bottom:1px dotted #000;border-right:1px solid #000;height:25px}.VATTEMP .noborder td{border-bottom:none}.VATTEMP .invtable .data td{border:1px solid #000}.VATTEMP .fl-l{float:left;width:164px;text-align:center}.VATTEMP .fl-r{float:right;width:300px;text-align:center}.VATTEMP .bgimg{border:1px solid red;cursor:pointer}.VATTEMP .bgimg p{color:#000;padding-left:13px;text-align:left}.VATTEMP .comp{float:left;width:80%}.VATTEMP .comp span,.VATTEMP .cus span{display:block;float:left}.VATTEMP .border-label,.VATTEMP .comp label{display:block;float:left;border-bottom:1px dashed #000;padding-bottom:1px;height:18px}.VATTEMP .comp p,.VATTEMP .cus p{position:relative;margin:0 0 4px;padding-bottom:6px;display:block;overflow:hidden}.VATTEMP .comp p strong,.VATTEMP .cus p strong{border:1px solid #000;padding:0 5px 2px;float:left;display:block;width:20px;height:22px}.VATTEMP .comp b:after,.VATTEMP .nextpage b:after{content:\"\";position:absolute;height:6px;margin-left:0}.VATTEMP .summary{width:500px;text-align:right}#author-inv,.VATTEMP .nextpage b:after{border-bottom:1px dashed #000;width:100%}.VATTEMP .comp b:after{bottom:0 width: 100%}.VATTEMP .cus b{padding-bottom:1px;word-wrap:break-word}.VATTEMP .nextpage b:after{bottom:8px}#author-inv{text-align:center;padding-bottom:5px;margin-bottom:0}";
            //(.VATTEMP)(\s*{)([^{]*)(margin)(\s*:)([^:]*)(;)([^}]*)}
            //(.VATTEMP)(\s*{)([^{]*)(margin|padding)(\s*:)([^:]*)(;)([^}]*)}
            //(.VATTEMP)(\s*{)([^{]*)(((margin)(\s*:)([^:]*)(;))|((padding)(\s*:)([^:]*)(;)))([^}]*)}
            Regex regex = new Regex(@"(.VATTEMP)(\s*{)([^{]*)(margin|padding)(\s*:)([^:]*)(;)([^}]*)(})");
            Regex regexPadding = new Regex(@"(.VATTEMP)(\s*{)([^{]*)(padding)(\s*:)([^:]*)(;)([^}]*)(})");
            Regex regexMargin = new Regex(@"(.VATTEMP)(\s*{)([^{]*)(margin)(\s*:)([^:]*)(;)([^}]*)(})");
            Dictionary<int, string[]> updateList = new Dictionary<int, string[]>();
            foreach (KeyValuePair<int, string> entry in result)
            {
                //Match mat = regex.Match(testCss);
                MatchCollection matches = regex.Matches(entry.Value);
                if (matches.Count == 1)
                {
                    string vMatch = matches[0].Value;
                    Match matchP = regexPadding.Match(vMatch);
                    Match matchM = regexMargin.Match(vMatch);
                    if (matchP.Success && matchM.Success)
                    {
                        //replace case 1
                        int[] paddingValue = matchP.Groups[6].Value.Trim().Replace("px", "").Replace("auto", "0").Split(' ').Select(Int32.Parse).ToArray();
                        int[] marginValue = matchM.Groups[6].Value.Trim().Replace("px", "").Replace("auto", "0").Split(' ').Select(Int32.Parse).ToArray();
                        string newCssValue = "";
                        if (marginValue.Count() == 2 && paddingValue.Count() == 1)
                            newCssValue = string.Format("{0}px {1}px", marginValue[0] + paddingValue[0], marginValue[1] + paddingValue[0]);
                        else if (marginValue.Count() == 2 && paddingValue.Count() == 2)
                            newCssValue = string.Format("{0}px {1}px", marginValue[0] + paddingValue[0], marginValue[1] + paddingValue[1]);
                        else
                            continue;
                        string rs = Regex.Replace(vMatch, @"(.VATTEMP)(\s*{)([^{]*)(padding\s*:[^:]*;)([^}]*)(})", "$1$2$3$5$6");
                        rs = Regex.Replace(rs, @"(.VATTEMP)(\s*{)([^{]*)(margin)(\s*:)([^:]*)(;)([^}]*)(})", m => string.Format(
                                    "{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                                    m.Groups[1].Value,
                                    m.Groups[2].Value,
                                    m.Groups[3].Value,
                                    "padding",
                                    m.Groups[5].Value,
                                    newCssValue,
                                    m.Groups[7].Value,
                                    m.Groups[8].Value,
                                    m.Groups[9].Value));
                        updateList.Add(entry.Key, new string[] { entry.Value, entry.Value.Replace(vMatch, rs) });
                    }
                    else if (matchP.Success)
                    {
                        continue;
                    }
                    else if (matchM.Success)
                    {
                        //replace case 3
                        string rs = Regex.Replace(vMatch, @"(.VATTEMP)(\s*{)([^{]*)(margin)(\s*:)([^:]*)(;)([^}]*)(})",
                                    m => string.Format(
                                    "{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                                    m.Groups[1].Value,
                                    m.Groups[2].Value,
                                    m.Groups[3].Value,
                                    "padding",
                                    m.Groups[5].Value,
                                    m.Groups[6].Value.Replace("auto", "0px"),
                                    m.Groups[7].Value,
                                    m.Groups[8].Value,
                                    m.Groups[9].Value));
                        updateList.Add(entry.Key, new string[] { entry.Value, entry.Value.Replace(vMatch, rs) });
                    }
                }
                else if (matches.Count == 2)
                {
                    continue;
                }
                else
                {
                    continue;

                }
            }
            new LogWriter().LogTableName("Error record: " + updateList.Count);
            foreach (KeyValuePair<int, string[]> entry in updateList)
            {
                new LogWriter().LogWrite(entry.Key, entry.Value[0], entry.Value[1]);
            }
        }
    }
}
