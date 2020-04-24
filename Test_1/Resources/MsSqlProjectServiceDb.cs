using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Test_1.DTO.Request;
using Test_1.Exceptions;

namespace Test_1.Resources
{
    public class MsSqlProjectServiceDb : IProjectServiceDb
    {
        private static string connectionString = "Data Source=db-mssql;Initial Catalog=s19022;Integrated Security=True";

        public void DeleteProject(projectDeleteRequest request)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "select idTask from task where idProject = @id";
                command.Parameters.AddWithValue("id", request.id);

                var reader = command.ExecuteReader();
                bool canRead = false;
                var taskId = new List<int>();
                while(reader.Read())
                {
                    canRead = true;
                    taskId.Add((int)reader["idTask"]);
                }
                if (!canRead) throw new NoSuchProjectException();
            }
        }
}
