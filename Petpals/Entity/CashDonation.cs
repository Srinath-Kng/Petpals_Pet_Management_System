using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Petpals.Entity
{
    public class CashDonation : Donation
    {
        public DateTime DonationDate { get; set; }

        public CashDonation(string donorName, decimal amount, DateTime donationDate) : base(donorName, amount)
        {
            DonationDate = donationDate;
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

                    query = "INSERT INTO CashDonation (id, donation_date) VALUES (@DonationId, @DonationDate)";
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@DonationId", donationId);
                    command.Parameters.AddWithValue("@DonationDate", DonationDate);

                    command.ExecuteNonQuery();
                }
                Console.WriteLine();
                Console.WriteLine("Donation Successfull");
                Console.WriteLine($"Cash donation of ${Amount} recorded on {DonationDate}");
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
