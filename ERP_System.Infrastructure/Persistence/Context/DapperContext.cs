using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ERP_System.Infrastructure.Persistence.Context
{
    public class DapperContext 
    {
        private readonly string _conString;
        private readonly IConfiguration _config;
        public DapperContext(IConfiguration config)
        { 
            _config = config;
            _conString = _config.GetConnectionString("DefaultConnection");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_conString);
    }
}
