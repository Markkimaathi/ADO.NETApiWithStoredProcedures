﻿using StoredProceduresWithWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace StoredProceduresWithAPI.Controllers
{
    public class ValuesController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString);
        Employee emp = new Employee();
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public string Post(Employee employee)
        {
            string msg = "";
            if (employee != null)
            {
                SqlCommand cmd = new SqlCommand("usp_AddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Age", employee.Age);
                cmd.Parameters.AddWithValue("@Active", employee.Active);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i > 0)
                {
                    msg = "Data has been inserted";
                }
                else
                {
                    msg = "Error";
                }

            }
            return msg;

        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
