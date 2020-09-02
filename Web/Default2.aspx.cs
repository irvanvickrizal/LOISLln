using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<ChartData> data = new List<ChartData>();
        data.Add(new ChartData("Jan", 35));
        data.Add(new ChartData("Feb", 28));
        data.Add(new ChartData("Mar", 34));
        data.Add(new ChartData("Apr", 32));
        data.Add(new ChartData("May", 40));
        data.Add(new ChartData("Jun", 32));
        data.Add(new ChartData("Jul", 35));
        data.Add(new ChartData("Aug", 55));
        data.Add(new ChartData("Sep", 38));
        data.Add(new ChartData("Oct", 30));
        data.Add(new ChartData("Nov", 25));
        data.Add(new ChartData("Dec", 32));
        this.Chart1.DataSource = data;
        this.DataBind();
    }

    public class ChartData
    {
        public string Month;
        public double Sales;

        public ChartData(string month, double sales)
        {
            this.Month = month;
            this.Sales = sales;
        }
    }
}