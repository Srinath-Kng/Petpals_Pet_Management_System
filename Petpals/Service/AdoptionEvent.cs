using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Petpals.Service
{
    public class AdoptionEvent : IAdoptable
    {
        private string connectionString = "Server=LAGLOP\\SQLEXPRESS;Database=petc;Trusted_Connection=True;TrustServerCertificate=True;";

        public AdoptionEvent()
        {

        }

        public void Adopt()
        {
            Console.WriteLine("Pet Adopted Successfully");
        }


        public void RegisterParticipant(string participantName, int eventId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO participants (event_id, participant_name) VALUES (@EventId, @ParticipantName)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EventId", eventId);
                    command.Parameters.AddWithValue("@ParticipantName", participantName);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("Inserted Successfully !");
                    Console.WriteLine($"{rowsAffected} Participant(s) registered for the event.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public void ListAllEvents()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT event_id, event_name, event_date, event_location FROM events";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    Console.WriteLine("All Events:");
                    Console.WriteLine("Event ID | Event Name | Event Date | Event Location");
                    

                    while (reader.Read())
                    {
                        int eventId = Convert.ToInt32(reader["event_id"]);
                        string eventName = reader["event_name"].ToString();
                        DateTime eventDate = Convert.ToDateTime(reader["event_date"]);
                        string eventLocation = reader["event_location"].ToString();

                        Console.WriteLine($"{eventId,-9} | {eventName,-10} | {eventDate.ToShortDateString(),-10} | {eventLocation}");
                    }

                    Console.WriteLine("-----------------------------------------------------------------");

                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
