using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace aspnetapp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        private MySqlConnection GetConnection()
        {//与mysql连接有关
	    var CONN = Environment.GetEnvironmentVariable("CONN");
            return new MySqlConnection(CONN);
        }
        public void OnGet()
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            MySqlCommand command = new MySqlCommand("INSERT INTO new(ID) VALUES (200)", conn);
            command.Parameters.AddWithValue("@ID", 200);
            command.ExecuteNonQuery();
        }
    }
}
