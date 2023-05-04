using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MyCoreApp.Pages
{
    public class IndexModel : PageModel
    {
        //Data
        public string sampleTableHeaders;
        public string sampleData;

        public void OnGet()
        {
            //Build the connection string
            //local connection string
            string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=prods;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

 
            // Connect to a database
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            // Define a query 
            SqlCommand command = new SqlCommand("SELECT * FROM customer", conn);


            //Execute the query
            SqlDataReader reader = command.ExecuteReader();


            DataTable dt = new DataTable();
            dt.Load(reader);

            print_results(dt);

            conn.Close();

        }

        void print_results(DataTable data)
        {
            //Console.WriteLine();
            sampleTableHeaders = "";
            sampleData = "";
            Dictionary<string, int> colWidths = new Dictionary<string, int>();

            foreach (DataColumn col in data.Columns)
            {
                //Console.Write(col.ColumnName);
                sampleTableHeaders = sampleTableHeaders + col.ColumnName + "\t";
                var maxLabelSize = data.Rows.OfType<DataRow>()
                        .Select(m => (m.Field<object>(col.ColumnName)?.ToString() ?? "").Length)
                        .OrderByDescending(m => m).FirstOrDefault();

                colWidths.Add(col.ColumnName, maxLabelSize);
                for (int i = 0; i < maxLabelSize - col.ColumnName.Length + 14; i++) Console.Write(" ");
            }

            //Console.WriteLine();
            //sampleTableData = sampleTableData + "\n";

            foreach (DataRow dataRow in data.Rows)
            {
                for (int j = 0; j < dataRow.ItemArray.Length; j++)
                {
                    //Console.Write(dataRow.ItemArray[j]);
                    sampleData = sampleData + dataRow.ItemArray[j].ToString() + "\t";
                    for (int i = 0; i < colWidths[data.Columns[j].ColumnName] - dataRow.ItemArray[j].ToString().Length + 14; i++) Console.Write(" ");
                }
                //Console.WriteLine();
                sampleData = sampleData + "\n";
            }
        }
    }
}