using System;
using System.Data;
using System.Data.SQLite;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareWebApp
{
    public class DatabaseHelper
    {
        private string _connectionString;

        public DatabaseHelper()
        {
            string dbPath = @"D:\GTI\English for software developers\AlbuixechHealthcaseCenter\Albuixech-Health-Centre\AlbuixechHealthCentre\AlbuixechHealthCenter.db";
            _connectionString = $"Data Source={dbPath};Version=3;";
            Console.WriteLine($"Using database file: {_connectionString}");
        }

        public bool TestConnection()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class TestDatabaseController : ControllerBase
    {
        private readonly DatabaseHelper _dbHelper;

        public TestDatabaseController()
        {
            _dbHelper = new DatabaseHelper();
        }

        [HttpGet("connection-status")]
        public IActionResult CheckConnection()
        {
            try
            {
                bool isConnected = _dbHelper.TestConnection();
                if (isConnected)
                {
                    return Ok(new { Message = "Connection successful" });
                }
                else
                {
                    return BadRequest(new { Message = "Connection failed" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error checking connection", Error = ex.Message });
            }
        }
    }
}