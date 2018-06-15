
/*public class ConfigCsv
{
    // <summary>  
    /// 将Csv读入DataTable  
    // </summary>  
    // <param name="filePath">csv文件路径</param>  
    // <param name="n">表示第n行是字段title,第n+1行是记录开始</param>  
    // <param name="k">可选参数表示最后K行不算记录默认0</param>  
    public DataTable csv2dt(string filePath, int n, DataTable dt) //这个dt 是个空白的没有任何行列的DataTable  
    {
        String csvSplitBy = "(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
        StreamReader reader = new StreamReader(filePath, System.Text.Encoding.Default, false);
        int i = 0, m = 0;
        reader.Peek();
        while (reader.Peek() > 0)
        {
            m = m + 1;
            string str = reader.ReadLine();
            if (m >= n + 1)
            {
                if (m == n + 1) //如果是字段行，则自动加入字段。  
                {
                    MatchCollection mcs = Regex.Matches(str, csvSplitBy);
                    foreach (Match mc in mcs)
                    {
                        dt.Columns.Add(mc.Value); //增加列标题  
                    }

                }
                else
                {
                    MatchCollection mcs = Regex.Matches(str, "(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)");
                    i = 0;
                    System.Data.DataRow dr = dt.NewRow();
                    foreach (Match mc in mcs)
                    {
                        dr[i] = mc.Value;
                        i++;
                    }
                    dt.Rows.Add(dr);  //DataTable 增加一行       
                }

            }
        }
        return dt;
    }

}
*/