using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using MimeKit;

namespace ConsoleApp1
{
    public class DBHelper
    {
        private static string connectionString = "Server={Enter Your Server};Database=mailDB;Integrated Security=true;";

        // using bool returnResults so user has flexibility if user wants to return read values
        public static List<Dictionary<string, object>> ExecuteStoredProcedure(string storedProcedureName, bool returnResults)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = storedProcedureName;
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            List<Dictionary<string, object>> readerList = null;

                            if (returnResults)
                            {
                                readerList = new List<Dictionary<string, object>>();

                                while (reader.Read())
                                {
                                    var dictionary = new Dictionary<string, object>();
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        dictionary[reader.GetName(i)] = reader.GetValue(i);
                                    }
                                    readerList.Add(dictionary);
                                }
                            }

                            return readerList;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public static void SaveMailToDB(MimeMessage message, bool hasSuccess)
        {
            var sender = message.From.ToString().Replace('<', ' ').Replace('>', ' ').Trim();
            var recipient = message.To.ToString();
            var subject = message.Subject.ToString();
            var body = message.TextBody;
            var date = DateTime.Parse(message.Date.ToString());
            var success = 0;
            var attempt = 1;

            if (hasSuccess)
            {
                success = 1;
            }

            string connectionString = "Server=DESKTOP-UDUUQVM;Database=mailDB;Integrated Security=true;";
            string queryStatement = "INSERT INTO Mail ([Sender], [Recipient], [Subject], [Body], [Date], [Success], [Attempt]) VALUES (@sender, @recipient, @subject, @body, @date, @success, @attempt)";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(queryStatement, con))
                    {
                        cmd.Parameters.AddWithValue("@sender", sender);
                        cmd.Parameters.AddWithValue("@recipient", recipient);
                        cmd.Parameters.AddWithValue("@subject", subject);
                        cmd.Parameters.AddWithValue("@body", body);
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@success", success);
                        cmd.Parameters.AddWithValue("@attempt", attempt);

                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void UpdateMailToDB(string mailID, bool hasSuccess)
        {
            var success = 0;

            if (hasSuccess)
            {
                success = 1;
            }

            string connectionString = "Server=DESKTOP-UDUUQVM;Database=mailDB;Integrated Security=true;";
            string queryStatement = "UPDATE Mail SET Success = @success, Attempt = Attempt+1 WHERE MailID = @mailID";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(queryStatement, con))
                    {
                        cmd.Parameters.AddWithValue("@success", success);
                        cmd.Parameters.AddWithValue("@mailID", mailID);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
