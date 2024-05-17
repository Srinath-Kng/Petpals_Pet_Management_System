using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Petpals.Exception;
using Petpals.Exceptions;

namespace Petpals.Entity
{
    public class ItemDonation : Donation
    {
        public string ItemType { get; set; }

        public ItemDonation(string donorName, decimal amount, string itemType) : base(donorName, amount)
        {
            ItemType = itemType;
        }

        public override void RecordDonation()
        {
            try
            {
                string connectionString = "Server=LAGLOP\\SQLEXPRESS;Database=petc;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Donation (donor_name, amount) VALUES (@DonorName, @Amount); SELECT SCOPE_IDENTITY();";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@DonorName", DonorName);
                    command.Parameters.AddWithValue("@Amount", Amount);

                    connection.Open();
                    int donationId = Convert.ToInt32(command.ExecuteScalar());

                    query = "INSERT INTO ItemDonation (id, item_type) VALUES (@DonationId, @ItemType)";
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@DonationId", donationId);
                    command.Parameters.AddWithValue("@ItemType", ItemType);

                    command.ExecuteNonQuery();
                }
                Console.WriteLine();
                Console.WriteLine("Donation Successfull");
                Console.WriteLine($"Item donation of {ItemType} recorded");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch(InvalidInputException ex)
            {
                Console.WriteLine("Error"+ex.Message);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
