using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAccessManager
    {
        private string connectionString;

        public DataAccessManager(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int InsertItem(Item item)
        {
            string sqlString = @"INSERT INTO Item OUTPUT INSERTED.ID VALUES(@FIRSTNAME, @LASTNAME)";

            SqlParameter firstName = new SqlParameter("@FIRSTNAME", item.FirstName);
            SqlParameter lastName = new SqlParameter("@LASTNAME", item.LastName);

            int result = -1;
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlString, conn);
                sqlCommand.Parameters.Add(firstName);
                sqlCommand.Parameters.Add(lastName);

                try
                {
                    conn.Open();
                    result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                }
                catch (Exception e)
                {
                    throw;
                }

                foreach (Position position in item.PositionHistory)
                {
                    InsertPosition(position, result);
                }

                foreach (Job job in item.JobHistory)
                {
                    InsertJob(job, result);
                }


            }

            return result;
        }


        public Item GetItemById(int id)
        {
            string sqlString = @"SELECT * 
                                 FROM Item
                                 WHERE id = @ID";

            SqlParameter idParam = new SqlParameter("@ID", id);
            Item result = new Item();

            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlString, conn);
                sqlCommand.Parameters.Add(idParam);

                try
                {
                    conn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Id = Convert.ToInt32(reader[0].ToString());
                        result.FirstName = reader[1].ToString();
                        result.LastName = reader[2].ToString();

                        break;
                    }
                }
                catch (Exception e)
                {
                    throw;
                }

                result.PositionHistory = GetPositionsById(id);
                result.JobHistory = GetJobsById(id);

            }

            return result;
        }


        public List<Item> GetItems()
        {
            string sqlString = @"SELECT * 
                                 FROM Item";

            List<Item> result = new List<Item>();

            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlString, conn);
               
                try
                {
                    conn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Item item = new Item();
                        item.Id = Convert.ToInt32(reader[0].ToString());
                        item.FirstName = reader[1].ToString();
                        item.LastName = reader[2].ToString();
                        item.PositionHistory = GetPositionsById(item.Id);
                        item.JobHistory = GetJobsById(item.Id);
                        result.Add(item);
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            Items items = new Items();
            items.CurItems = result;
            Console.WriteLine(items);

            return result;
        }

        public int InsertJob(Job job, int ItemId)
        {
            string sqlString = @"INSERT INTO Job VALUES(@TIME, @DESCRIPTION, @PHONE, @USERID , @ITEMID)";

            SqlParameter time = new SqlParameter(@"TIME", job.Time);
            SqlParameter description = new SqlParameter(@"DESCRIPTION", job.Description);
            SqlParameter phone = new SqlParameter(@"PHONE", job.Phone);
            SqlParameter userId = new SqlParameter(@"USERID", job.UserId);
            SqlParameter itemId = new SqlParameter(@"ITEMID", ItemId);

            int result = -1;

            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlString, conn);
                sqlCommand.Parameters.Add(time);
                sqlCommand.Parameters.Add(description);
                sqlCommand.Parameters.Add(phone);
                sqlCommand.Parameters.Add(userId);
                sqlCommand.Parameters.Add(itemId);

                try
                {
                    conn.Open();
                    result = sqlCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                { }
            }

            return result;
        }

        public List<Job> GetJobsById(int id)
        {
            string sqlString = @"SELECT * 
                                 FROM Job
                                 WHERE ItemID = @ID";

            SqlParameter idParam = new SqlParameter("@ID", id);
            List<Job> result = new List<Job>();

            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlString, conn);
                sqlCommand.Parameters.Add(idParam);

                try
                {
                    conn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        Job job = new Job();
                        job.Time = Convert.ToDateTime(reader[1].ToString());
                        job.Description = reader[2].ToString();
                        job.Phone = reader[3].ToString();
                        job.UserId = Convert.ToInt32(reader[4].ToString());
                        
                        result.Add(job);
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            }

            return result;
        }

        public int InsertPosition(Position position, int ItemId)
        {
            string sqlString = @"INSERT INTO Position 
                                 VALUES(@LATITUDE, @LONGITUDE, @ACCURACY, @DATE, @ITEMID)";

            SqlParameter latitude = new SqlParameter(@"LATITUDE", position.Latitude);
            SqlParameter longitude = new SqlParameter(@"LONGITUDE", position.Longitude);
            SqlParameter accuracy = new SqlParameter(@"ACCURACY", position.Accuracy);
            SqlParameter date = new SqlParameter(@"DATE", position.Date);
            SqlParameter itemId = new SqlParameter(@"ITEMID", ItemId);

            int result = -1;

            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlString, conn);
                sqlCommand.Parameters.Add(latitude);
                sqlCommand.Parameters.Add(longitude);
                sqlCommand.Parameters.Add(accuracy);
                sqlCommand.Parameters.Add(date);
                sqlCommand.Parameters.Add(itemId);

                try
                {
                    conn.Open();
                    result = sqlCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                { }
            }

            return result;
        }

        public List<Position> GetPositionsById(int id)
        {
            string sqlString = @"SELECT * 
                                 FROM Position
                                 WHERE ItemID = @ID";

            SqlParameter idParam = new SqlParameter("@ID", id);
            List<Position> result = new List<Position>();

            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sqlString, conn);
                sqlCommand.Parameters.Add(idParam);

                try
                {
                    conn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        Position position = new Position();
                        position.Latitude = Convert.ToInt64(reader[1].ToString());
                        position.Longitude = Convert.ToInt64(reader[2].ToString());
                        position.Accuracy = Convert.ToInt32(reader[3].ToString());
                        position.Date = Convert.ToDateTime(reader[4].ToString());

                        result.Add(position);
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            }

            return result;
        }
    }
}
