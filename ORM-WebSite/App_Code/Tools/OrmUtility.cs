using ExcelGeneratingClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Utility
/// </summary>
public static class OrmUtility
{
    public static string DataTableToXML(DataTable dt)
    {

        const string TABLE_HEADER_FORMAT_NAME = "greyBackground";
        const string CELL_FONT_FORMAT = "cellFont";
            string fontName = "Arial";
            DataTable table = dt;
            XmlExcelHelper helper = new XmlExcelHelper("", fontName, 12);

            helper.AddStringStyle(TABLE_HEADER_FORMAT_NAME, fontName, 14, "#FFFFFF", "#C0C0C0", true);
            helper.AddStringStyle(CELL_FONT_FORMAT, fontName, 12, "#000000", false);

            helper.CreateSheet("Answers");

            helper.AddRow();
            foreach (DataColumn column in table.Columns)
            {
                helper.AddCell(XmlExcelHelper.CellType.String, TABLE_HEADER_FORMAT_NAME, column.ColumnName);
            }

            foreach (DataRow row in table.Rows)
            {
                helper.AddRow();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    float ff;
                    if (float.TryParse(row[i].ToString(), out ff))
                        helper.AddCell(XmlExcelHelper.CellType.Number, CELL_FONT_FORMAT, row[i].ToString());
                    else
                        helper.AddCell(XmlExcelHelper.CellType.String, CELL_FONT_FORMAT, row[i].ToString());
                }

            }
            return helper.ExcelFileXml;
    }

    public static string DataTableToCSV(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();

        IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                          Select(column => column.ColumnName);
        sb.AppendLine(string.Join(",", columnNames));

        foreach (DataRow row in dt.Rows)
        {
            IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
            sb.AppendLine(string.Join(",", fields));
        }

        return sb.ToString();
    }

    public static string ConvertToShamsi(string filldate)
    {
        var farsiDate = string.Empty;
        PersianCalendar pDate = new PersianCalendar();
        if (filldate != string.Empty)
        {
            DateTime datefilldate = Convert.ToDateTime(filldate);
            string year = Convert.ToString(pDate.GetYear(datefilldate));
            string month = String.Format(Convert.ToString(pDate.GetMonth(datefilldate)), "00");
            string day = String.Format(Convert.ToString(pDate.GetDayOfMonth(datefilldate)), "00");
            farsiDate = year + "/" + month + "/" + day;
        }
        return farsiDate;
    }

    public static DateTime ConvertToMiladi(this string dateFA)
    {
        var stringArr = dateFA.Split('/');
        var year = Convert.ToInt32(stringArr[0]);
        var month = Convert.ToInt32(stringArr[1]);
        var day = Convert.ToInt32(stringArr[2]);
        return new DateTime(year, month, day, new PersianCalendar());
    }
}