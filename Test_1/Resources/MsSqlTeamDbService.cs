using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Test_1.DTO.Response;
using Test_1.Exceptions;

namespace Test_1.Resources
{
    public class MsSqlTeamDbService : ITeamsDbService
    {
        private static string connectionString = "Data Source=db-mssql;Initial Catalog=s19022;Integrated Security=True";

        public List<GetTaskResponse> GetAssignedTasks(int id)
        {
            return GetTaskResponses("idAssignedTo", id);
        }

        public List<GetTaskResponse> GetCreatedTasks(int id)
        {
            return GetTaskResponses("idCreator", id);      
        }

    private List<GetTaskResponse> GetTaskResponses(string s, int id)
    {
        using (var connection = new SqlConnection(connectionString))
        using (var command = new SqlCommand())
        {
            command.Connection = connection;
            command.CommandText = "select t.name as 'name',  t.description, t.deadline,  p.name as 'pName' from task t ,project p " +
                " where " + s +" = @id and p.idProject = t.idProject" +
                " order by deadline desc";
            command.Parameters.AddWithValue("id", id);
            connection.Open();

            var reader = command.ExecuteReader();

            var result = new List<GetTaskResponse>();
            while (reader.Read())
            {
                result.Add(new GetTaskResponse
                {
                    name = (string)reader["name"],
                    description = (string)reader["description"],
                    deadline = (DateTime)reader["deadline"],
                    projectName = (string)reader["pName"]
                });
            }
            return result;
        } 
    }

        public GetTeamMemberResponse GetTeamMember(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "select firstName, lastName, email from teamMember where idTeamMember = @id";
                command.Parameters.AddWithValue("id", id);
                connection.Open();

                var reader = command.ExecuteReader();
                if (!reader.Read()) throw new NoSuchTeamMemberException();

                return new GetTeamMemberResponse
                {
                    firstName = (string)reader["firstName"],
                    lastName = (string)reader["lastName"],
                    email = (string)reader["email"],
                    assigned = GetAssignedTasks(id),
                    created = GetCreatedTasks(id)

            };
            }
        }

      
    }
}
