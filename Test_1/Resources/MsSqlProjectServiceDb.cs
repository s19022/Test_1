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

        public void DeleteProject(ProjectDeleteRequest request)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                if (!CheckProject(request.id)) throw new NoSuchProjectException();

                var taskId = GetTaskId(request.id);

                var tran = connection.BeginTransaction();
                command.CommandText = "delete from project where idProject = @id";
                command.Parameters.AddWithValue("id", request.id);

                foreach (int item in taskId)
                {
                    command.CommandText = "delete from task where idTask = @id";
                    command.Parameters.AddWithValue("id", item);
                }
                tran.Commit();
            }
        }

        private bool CheckProject(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "select idTask from task where idProject = @id";
                command.Parameters.AddWithValue("id", id);

                return command.ExecuteReader().Read();

            }
        }

        private List<int> GetTaskId(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "select idTask from task where idProject = @id";
                command.Parameters.AddWithValue("id", id);

                var reader = command.ExecuteReader();
                var taskId = new List<int>();
                while (reader.Read())
                {
                    taskId.Add((int)reader["idTask"]);
                }
                return taskId;
            }
        }
    }
}
