/*using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MyCoreApp.Pages
{
    public class IndexModel : PageModel
    {
        //Data
        public string sampleTableHeaders;
        //
        public string sampleData;

        public void OnGet()
        {
            //Build the connection string
            //local connection string
            //string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=prods;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //Azure connectionstring
            string connectionString = "Server=tcp:mycoreappdbserver.database.windows.net,1433;Initial Catalog=prods;Persist Security Info=False;User ID=cs190admin;Password=class190besties!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
            //string connectionString = "Server=tcp:13.86.216.196,1433;Initial Catalog=prods;Persist Security Info=False;User ID=cs190admin;Password=class190besties!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

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
} */




/* public IActionResult OnPost(int numPeople, List<string> items)
 {
     // Perform your C# code logic here using the provided inputs
     // For simplicity, I’m just returning a message with the input data
     string result = $“Number of People: { numPeople}< br >“;
     for (int i = 0; i < items.Count; i++)
     {
         string personItems = string.Join(“, “, items[i].Split(‘,’));
         result += $“Items for Person { i + 1}: { personItems}< br >“;
     }
     return Content(result);
 }
}
}*/



/*
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using Microsoft.AspNetCore.Hosting.Server;
using System;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace MyCoreApp.Pages
{
    public class IndexModel : PageModel
    {

        
        public int numPeople { get; set; }

        void OnGet()
       

        {
            string connString = "Server=127.0.0.1:5432;User Id=postgres;Password=Pixelmon2001;Database=split;";
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            Dictionary<string, decimal> menuItems = new Dictionary<string, decimal>();
            conn.Open();
            string sql = "SELECT * FROM menu";
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string item = reader.GetString(0);
                        decimal price = reader.GetDecimal(1);
                        menuItems.Add(item, price);
                    }
                }
            }
            conn.Close();
            Console.Write("Enter the number of people: ");
            int numPeople = int.Parse(Console.ReadLine());

            List<Dictionary<string, int>> groups = new List<Dictionary<string, int>>();
          
            for (int i = 0; i < numPeople; i++)
            {
               
                Dictionary<string, int> itemsDict = new Dictionary<string, int>();
                Console.Write($"Enter the items for person {i + 1}, seperate by commas: ");
                string itemsString = Console.ReadLine();
                string[] itemsArray = itemsString.Split(", ");
                foreach (string item in itemsArray)
                {
                    if (itemsDict.ContainsKey(item))
                    {
                        itemsDict[item]++;
                    }
                    else
                    {
                        itemsDict.Add(item, 1);
                    }
                }
                groups.Add(itemsDict);
            }
            List<decimal> totals = new List<decimal>();
            foreach (Dictionary<string, int> person in groups)
            {
                decimal total = 0.0M;
                Console.WriteLine("Order:");
             
                foreach (KeyValuePair<string, int> kvp in person)
                {
                    if (menuItems.ContainsKey(kvp.Key))
                    {
                        Console.WriteLine($"{kvp.Key}: ${menuItems[kvp.Key]} x {kvp.Value}");
                        total += menuItems[kvp.Key] * kvp.Value;
                     
                    }
                }
                Console.Write("Enter the tip percentage for this group (0, 10, 15, or 20): ");
                int tipPercentage = int.Parse(Console.ReadLine());
                decimal tip = total * (decimal)tipPercentage / 100.0M;
                decimal totalWithTip = total + tip;
                Console.WriteLine($"Total bill for this person: ${total}");
                Console.WriteLine($"Tip ({tipPercentage}%): ${tip}");
                Console.WriteLine($"Total with tip: ${totalWithTip}");
                totals.Add(totalWithTip);
            }
            decimal grandTotal = 0.0M;
            foreach (decimal total in totals)
            {
                grandTotal += total;
            }
            Console.WriteLine($"Grand total: ${grandTotal}");
        }
    }
}
*/

//THE ONE THAT WORKED
/*
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using Microsoft.AspNetCore.Hosting.Server;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace MyCoreApp.Pages
{
    public class IndexModel : PageModel
    {
        public int NumPeople { get; set; }
        public List<Dictionary<string, int>> Groups { get; set; }
        public Dictionary<string, decimal> MenuItems { get; set; }
        public List<decimal> Totals { get; set; }
        public decimal GrandTotal { get; set; }
        public void OnGet()
        {
            NumPeople = 0;
            Groups = new List<Dictionary<string, int>>();
            MenuItems = new Dictionary<string, decimal>();
            Totals = new List<decimal>();
            GrandTotal = 0.0M;
        }
        public IActionResult OnPost()
        {
            LoadMenuItems();
            GetNumPeople();
            GetGroups();
            CalculateTotals();
            CalculateGrandTotal();
            return Page();
        }
        private void LoadMenuItems()
        {
            string connString = "Server = 127.0.0.1:5432; User Id = postgres; Password = Pixelmon2001; Database = split;";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string sql = "SELECT* FROM menu";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string item = reader.GetString(0);
                            decimal price = reader.GetDecimal(1);
                            MenuItems.Add(item, price);
                        }
                    }
                }
            }
        }
        private void GetNumPeople()
        {
            Console.Write("Enter the number of people: ");
            NumPeople = int.Parse(Console.ReadLine());
        }
        private void GetGroups()
        {
            Groups.Clear();
            for (int i = 0; i < NumPeople; i++)
            {
                Dictionary<string, int> itemsDict = new Dictionary<string, int>();
                Console.Write($"Enter the items for person { i + 1}, separate by commas: ");
            string itemsString = Console.ReadLine();
            string[] itemsArray = itemsString.Split(", ");
            foreach (string item in itemsArray)
            {
                if (itemsDict.ContainsKey(item))
                {
                    itemsDict[item]++;
                }
                else
                {
                    itemsDict.Add(item, 1);
                }
            }
            Groups.Add(itemsDict);
        }
    }
    private void CalculateTotals()
    {
        Totals.Clear();
        foreach (Dictionary<string, int> person in Groups)
        {
            decimal total = 0.0M;
            Console.WriteLine("Order:");
            foreach (KeyValuePair<string, int> kvp in person)
            {
                if (MenuItems.ContainsKey(kvp.Key))
                {
                    Console.WriteLine($"{ kvp.Key}: ${ MenuItems[kvp.Key]} x { kvp.Value}");
                    total += MenuItems[kvp.Key] * kvp.Value;
                }
            }
            Console.Write("Enter the tip percentage for this group(0, 10, 15, or 20): ") ;
            int tipPercentage = int.Parse(Console.ReadLine());
            decimal tip = total * (decimal)tipPercentage / 100.0M;
            decimal totalWithTip = total + tip;
            Console.WriteLine($"Total bill for this person: ${ total}");
            Console.WriteLine($"Tip({ tipPercentage}%): ${ tip}");
            Console.WriteLine($"Total with tip: ${ totalWithTip}");
            Totals.Add(totalWithTip);
        }
    }
    private void CalculateGrandTotal()
    {
        GrandTotal = 0.0M;
        foreach (decimal total in Totals)
        {
            GrandTotal += total;
        }
        Console.WriteLine($"Grand total: ${ GrandTotal}");
    }
}
}
*/


//THIS IS THE LOADING ONE (after click)
/*
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
namespace MyCoreApp.Pages
{
    public class IndexModel : PageModel
    {
        public int NumPeople { get; set; }
        public List<Dictionary<string, int>> Groups { get; set; }
        public Dictionary<string, decimal> MenuItems { get; set; }
        public List<decimal> Totals { get; set; }
        public decimal GrandTotal { get; set; }
        public IndexModel()
        {
            Groups = new List<Dictionary<string, int>>();
            MenuItems = new Dictionary<string, decimal>();
            Totals = new List<decimal>();
            GrandTotal = 0.0M;
        }
        public void OnGet()
        {
            NumPeople = 0;
        }
        public IActionResult OnPost()
        {
            LoadMenuItems();
            GetNumPeople();
            GetGroups();
            CalculateTotals();
            CalculateGrandTotal();
            return Page();
        }
        private void LoadMenuItems()
        {
            string connString = "Server = 127.0.0.1:5432; User Id = postgres; Password = Pixelmon2001; Database = split;";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string sql = "SELECT* FROM menu";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string item = reader.GetString(0);
                            decimal price = reader.GetDecimal(1);
                            MenuItems.Add(item, price);
                        }
                    }
                }
            }
        }
        private void GetNumPeople()
        {
            Console.Write("Enter the number of people: ");
            NumPeople = int.Parse(Console.ReadLine());
        }
        private void GetGroups()
        {
            Groups.Clear();
            for (int i = 0; i < NumPeople; i++)
            {
                Dictionary<string, int> itemsDict = new Dictionary<string, int>();
                Console.Write($"nter the items for person {i + 1}, separate by commas: ");
                string itemsString = Console.ReadLine();
                string[] itemsArray = itemsString.Split(", ");
                foreach (string item in itemsArray)
                {
                    if (itemsDict.ContainsKey(item))
                    {
                        itemsDict[item]++;
                    }
                    else
                    {
                        itemsDict.Add(item, 1);
                    }
                }
                Groups.Add(itemsDict);
            }
        }
        private void CalculateTotals()
        {
            Totals.Clear();
            foreach (Dictionary<string, int> person in Groups)
            {
                decimal total = 0.0M;
                Console.WriteLine("Order:");
                foreach (KeyValuePair<string, int> kvp in person)
                {
                    if (MenuItems.ContainsKey(kvp.Key))
                    {
                        Console.WriteLine($"{kvp.Key}: ${MenuItems[kvp.Key]} x {kvp.Value}");
                        total += MenuItems[kvp.Key] * kvp.Value;
                    }
                }
                Console.Write("Enter the tip percentage for this group(0, 10, 15, or 20): ");
                int tipPercentage = int.Parse(Console.ReadLine());
                decimal tip = total * (decimal)tipPercentage / 100.0M;
                decimal totalWithTip = total + tip;
                Console.WriteLine($"Total bill for this person: ${total}");
                Console.WriteLine($"Tip({tipPercentage}%): ${tip}");
                Console.WriteLine($"Total with tip: ${totalWithTip}");
                Totals.Add(totalWithTip);
            }
        }
        private void CalculateGrandTotal()
        {
            GrandTotal = 0.0M;
            foreach (decimal total in Totals)
            {
                GrandTotal += total;
            }
            Console.WriteLine($"Grand total: ${GrandTotal}");
        }
    }
}
*/

//NUM PEOPLE NULL EXCEPTION ERROR
/*
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyCoreApp.Pages
{
    public class IndexModel : PageModel
    {
        public int NumPeople { get; set; }
        public List<Dictionary<string, int>> Groups { get; set; }
        public Dictionary<string, decimal> MenuItems { get; set; }
        public List<decimal> Totals { get; set; }
        public decimal GrandTotal { get; set; }
        public IndexModel()
        {
            Groups = new List<Dictionary<string, int>>();
            MenuItems = new Dictionary<string, decimal>();
            Totals = new List<decimal>();
            GrandTotal = 0.0M;
        }
        public void OnGet()
        {
            NumPeople = 0;
        }
        public IActionResult OnPost()
        {
            LoadMenuItems();
            GetNumPeople();
            GetGroups();
            CalculateTotals();
            CalculateGrandTotal();
            return Page();
        }
        private void LoadMenuItems()
        {
            string connString = "Server = 127.0.0.1:5432; User Id = postgres; Password = Pixelmon2001; Database = split;";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string sql = "SELECT* FROM menu";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string item = reader.GetString(0);
                            decimal price = reader.GetDecimal(1);
                            MenuItems.Add(item, price);
                        }
                    }
                }
            }
        }
        private void GetNumPeople()
        {
            NumPeople = int.Parse(Request.Form["NumPeople"]);
        }
        private void GetGroups()
        {
            Groups.Clear();
            for (int i = 0; i < NumPeople; i++)
            {
                Dictionary<string, int> itemsDict = new Dictionary<string, int>();
                string itemsString = Request.Form[$"Group{ i + 1}"];
                string[] itemsArray = itemsString.Split(", ");
                foreach (string item in itemsArray)
                {
                    if (itemsDict.ContainsKey(item))
                    {
                        itemsDict[item]++;
                    }
                    else
                    {
                        itemsDict.Add(item, 1);
                    }
                }
                Groups.Add(itemsDict);
            }
        }
        private void CalculateTotals()
        {
            Totals.Clear();
            foreach (Dictionary<string, int> person in Groups)
            {
                decimal total = 0.0M;
                foreach (KeyValuePair<string, int> kvp in person)
                {
                    if (MenuItems.ContainsKey(kvp.Key))
                    {
                        total += MenuItems[kvp.Key] * kvp.Value;
                    }
                }
                int tipPercentage = int.Parse(Request.Form["TipPercentage"]);
                decimal tip = total * (decimal)tipPercentage / 100.0M;
                decimal totalWithTip = total + tip;
                Totals.Add(totalWithTip);
            }
        }
        private void CalculateGrandTotal()
        {
            GrandTotal = 0.0M;
            foreach (decimal total in Totals)
            {
                GrandTotal += total;
            }
        }
    }
}
*/



//TOO COMPLICATED ONE
/*
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCoreApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpContext _httpContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public int NumPeople { get; set; }
        public List<Dictionary<string, int>> Groups { get; set; }
        public Dictionary<string, decimal> MenuItems { get; set; }
        public List<decimal> Totals { get; set; }
        public decimal GrandTotal { get; set; }
        public IndexModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void OnGet()
        {
            NumPeople = 0;
            Groups = new List<Dictionary<string, int>>();
            MenuItems = new Dictionary<string, decimal>();
            Totals = new List<decimal>();
            GrandTotal = 0.0M;
        }
        public IActionResult OnPost()
        {
            LoadMenuItems();
            GetNumPeople();
            GetGroups();
            CalculateTotals();
            CalculateGrandTotal();
            return Page();
        }
        private void LoadMenuItems()
        {
            string connString = "Server = 127.0.0.1:5432; User Id = postgres; Password = Pixelmon2001; Database = split;";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string sql = "SELECT* FROM menu";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string item = reader.GetString(0);
                            decimal price = reader.GetDecimal(1);
                            MenuItems.Add(item, price);
                        }
                    }
                }
            }
        }
        private void GetNumPeople()
        {
            NumPeople = int.Parse(_httpContext.Request.Form["numPeople"]);
        }
        private void GetGroups()
        {
            Groups.Clear();
            for (int i = 0; i < NumPeople; i++)
            {
                Dictionary<string, int> itemsDict = new Dictionary<string, int>();
                string itemsString = _httpContext.Request.Form[$"items -{ i}"];
                if (!string.IsNullOrEmpty(itemsString))
                {
                    string[] itemsArray = itemsString.Split(", ");
                    foreach (string item in itemsArray)
                    {
                        if (itemsDict.ContainsKey(item))
                        {
                            itemsDict[item]++;
                        }
                        else
                        {
                            itemsDict.Add(item, 1);
                        }
                    }
                }
                Groups.Add(itemsDict);
            }
        }
        private void CalculateTotals()
        {
            Totals.Clear();
            foreach (Dictionary<string, int> person in Groups)
            {
                decimal total = 0.0M;
                foreach (KeyValuePair<string, int> kvp in person)
                {
                    if (MenuItems.ContainsKey(kvp.Key))
                    {
                        total += MenuItems[kvp.Key] * kvp.Value;
                    }
                }
                int tipPercentage = int.Parse(_httpContext.Request.Form["tipPercentage"]);
                decimal tip = total * (decimal)tipPercentage / 100.0M;
                decimal totalWithTip = total + tip;
                Totals.Add(totalWithTip);
            }
        }
        private void CalculateGrandTotal()
        {
            GrandTotal = 0.0M;
            foreach (decimal total in Totals)
            {
                GrandTotal += total;
            }
        }
    }
}
*/




//4:37
/*
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using Microsoft.AspNetCore.Hosting.Server;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace MyCoreApp.Pages
{
    public class IndexModel : PageModel
    {
        public int NumPeople { get; set; }
        public List<Dictionary<string, int>> Groups { get; set; }
        public Dictionary<string, decimal> MenuItems { get; set; }
        public List<decimal> Totals { get; set; }
        public decimal GrandTotal { get; set; }
        public IndexModel()
        {
            Groups = new List<Dictionary<string, int>>();
            MenuItems = new Dictionary<string, decimal>();
            Totals = new List<decimal>();
            GrandTotal = 0.0M;
        }
        public void OnGet()
        {
            NumPeople = 0;
        }
        public IActionResult OnPost()
        {
            LoadMenuItems();
            GetNumPeople();
            GetGroups();
            CalculateTotals();
            CalculateGrandTotal();
            return Page();
        }
        private void LoadMenuItems()
        {
            string connString = "Server = 127.0.0.1:5432; User Id = postgres; Password = Pixelmon2001; Database = split;";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string sql = "SELECT* FROM menu";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string item = reader.GetString(0);
                            decimal price = reader.GetDecimal(1);
                            MenuItems.Add(item, price);
                        }
                    }
                }
            }
        }
        private void GetNumPeople()
        {
            Console.Write("Enter the number of people: ");
            NumPeople = int.Parse(Console.ReadLine());
        }
        private void GetGroups()
        {
            Groups.Clear();
            for (int i = 0; i < NumPeople; i++)
            {
                Dictionary<string, int> itemsDict = new Dictionary<string, int>();
                Console.Write($"Enter the items for person {i + 1}, separate by commas: ");
                string itemsString = Console.ReadLine();
                string[] itemsArray = itemsString.Split(", ");
                foreach (string item in itemsArray)
                {
                    if (itemsDict.ContainsKey(item))
                    {
                        itemsDict[item]++;
                    }
                    else
                    {
                        itemsDict.Add(item, 1);
                    }
                }
                Groups.Add(itemsDict);
            }
        }
        private void CalculateTotals()
        {
            Totals.Clear();
            foreach (Dictionary<string, int> person in Groups)
            {
                decimal total = 0.0M;
                Console.WriteLine("Order:");
                foreach (KeyValuePair<string, int> kvp in person)
                {
                    if (MenuItems.ContainsKey(kvp.Key))
                    {
                        Console.WriteLine($"{kvp.Key}: ${MenuItems[kvp.Key]} x {kvp.Value}");
                        total += MenuItems[kvp.Key] * kvp.Value;
                    }
                }
                Console.Write("Enter the tip percentage for this group(0, 10, 15, or 20): ");
                int tipPercentage = int.Parse(Console.ReadLine());
                decimal tip = total * (decimal)tipPercentage / 100.0M;
                decimal totalWithTip = total + tip;
                Console.WriteLine($"Total bill for this person: ${total}");
                Console.WriteLine($"Tip({tipPercentage}%): ${tip}");
                Console.WriteLine($"Total with tip: ${totalWithTip}");
                Totals.Add(totalWithTip);
            }
        }
        private void CalculateGrandTotal()
        {
            GrandTotal = 0.0M;
            foreach (decimal total in Totals)
            {
                GrandTotal += total;
            }
            Console.WriteLine($"Grand total: ${GrandTotal}");
        }
    }
}
*/


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
namespace MyCoreApp.Pages
{
    public class IndexModel : PageModel
    {
        public int NumPeople { get; set; }
        public List<Dictionary<string, int>> Groups { get; set; }
        public Dictionary<string, decimal> MenuItems { get; set; }
        public List<decimal> Totals { get; set; }
        public decimal GrandTotal { get; set; }
        public IndexModel()
        {
            Groups = new List<Dictionary<string, int>>();
            MenuItems = new Dictionary<string, decimal>();
            Totals = new List<decimal>();
            GrandTotal = 0.0M;
        }
        public void OnGet()
        {
            NumPeople = 0;
        }
        public IActionResult OnPost()
        {
            LoadMenuItems();
            GetNumPeople();
            GetGroups();
            CalculateTotals();
            CalculateGrandTotal();
            return Page();
        }
        private void LoadMenuItems()
        {
            string connString = "Server = 127.0.0.1:5432; User Id = postgres; Password = Pixelmon2001; Database = split;";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string sql = "SELECT* FROM menu";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string item = reader.GetString(0);
                            decimal price = reader.GetDecimal(1);
                            MenuItems.Add(item, price);
                        }
                    }
                }
            }
        }
        private void GetNumPeople()
        {
            Console.Write("Enter the number of people: ");
            NumPeople = int.Parse(Console.ReadLine());
        }
        private void GetGroups()
        {
            Groups.Clear();
            for (int i = 0; i < NumPeople; i++)
            {
                Dictionary<string, int> itemsDict = new Dictionary<string, int>();
                Console.Write($"Enter the items for person { i + 1}, separate by commas: ");
            string itemsString = Console.ReadLine();
            string[] itemsArray = itemsString.Split(", ");
            foreach (string item in itemsArray)
            {
                if (itemsDict.ContainsKey(item))
                {
                    itemsDict[item]++;
                }
                else
                {
                    itemsDict.Add(item, 1);
                }
            }
            Groups.Add(itemsDict);
        }
    }
    private void CalculateTotals()
    {
        Totals.Clear();
        foreach (Dictionary<string, int> person in Groups)
        {
            decimal total = 0.0M;
            Console.WriteLine("Order:");
            foreach (KeyValuePair<string, int> kvp in person)
            {
                if (MenuItems.ContainsKey(kvp.Key))
                {
                    Console.WriteLine($"{ kvp.Key}: ${ MenuItems[kvp.Key]} x { kvp.Value}");
                    total += MenuItems[kvp.Key] * kvp.Value;
                }
            }
            Console.Write("Enter the tip percentage for this group(0, 10, 15, or 20): ") ;
            int tipPercentage = int.Parse(Console.ReadLine());
            decimal tip = total * (decimal)tipPercentage / 100.0M;
            decimal totalWithTip = total + tip;
            Console.WriteLine($"Total bill for this person: ${ total}");
            Console.WriteLine($"Tip({ tipPercentage}%): ${ tip}");
            Console.WriteLine($"Total with tip: ${ totalWithTip}");
            Totals.Add(totalWithTip);
        }
    }
    private void CalculateGrandTotal()
    {
        GrandTotal = 0.0M;
        foreach (decimal total in Totals)
        {
            GrandTotal += total;
        }
        Console.WriteLine($"Grand total: ${ GrandTotal}");
    }
}
}