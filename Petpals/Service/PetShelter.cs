using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Microsoft.Data.SqlClient;
using Petpals.Entity;
using Petpals.Exception;

namespace Petpals.Service
{
    public class PetShelter
    {
        private string connectionString = "Server=LAGLOP\\SQLEXPRESS;Database=petc;Trusted_Connection=True;TrustServerCertificate=True;";

        private List<Pet> availablePets;


        public PetShelter()
        {
            availablePets = new List<Pet>();
        }

        public void AddPet(Pet pet)
        {

            try
            {
                if (pet.Age <= 0 || pet.Age > 20)
                {
                    throw new InvalidPetAgeException($"Invalid age: {pet.Age}. Age must be between 1 and 20 years.");
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Pet (name, age, breed) VALUES (@Name, @Age, @Breed)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", pet.Name);
                    command.Parameters.AddWithValue("@Age", pet.Age);
                    command.Parameters.AddWithValue("@Breed", pet.Breed);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                availablePets.Add(pet);
                Console.WriteLine("Pet added successfully.");
            }
            catch (InvalidPetAgeException ex)
            {
                Console.WriteLine($"Invalid age: {ex.InvalidAge}. Age must be between 1 and 20 years.");
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

        public void RemovePet(string petName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Pet WHERE Name = @PetName";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@PetName", petName);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Pet petToRemove = availablePets.FirstOrDefault(pet => pet.Name.Equals(petName, StringComparison.OrdinalIgnoreCase));
                        if (petToRemove != null)
                        {
                            availablePets.Remove(petToRemove);
                            Console.WriteLine("Pet removed successfully.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Pet not found.");
                    }
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








        public void ListAvailablePetsFromDatabase()
        {
            string connectionString = "Server=LAGLOP\\SQLEXPRESS;Database=petc;Trusted_Connection=True;TrustServerCertificate=True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT name, age, breed FROM Pet";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    Console.WriteLine("Available pets in the shelter:");
                    Console.WriteLine();
                    while (reader.Read())
                    {
                        string name = reader["Name"].ToString();
                        int age = Convert.ToInt32(reader["Age"]);
                        string breed = reader["Breed"].ToString();
                        Console.WriteLine($"Name: {name}, Age: {age}, Breed: {breed}");
                        Console.WriteLine();
                    }

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
        public void ListCatBreedsWithColor()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT p.breed, c.cat_color,p.name FROM Pet p INNER JOIN Cat c ON p.id = c.id";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    Console.WriteLine("Cat Breeds with Colors:");
                    while (reader.Read())
                    {
                        string breed = reader["breed"].ToString();
                        string color = reader["cat_color"].ToString();
                        string name = reader["name"].ToString();
                        Console.WriteLine($"Cat Breed: {breed}, Color: {color}, Cat Name : {name}");
                        Console.WriteLine() ;
                    }

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
        public void ListDogBreeds()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT p.breed, d.dog_breed,p.name FROM Pet p INNER JOIN Dog d ON p.id = d.id";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    Console.WriteLine("Dog Breeds with Origin:");
                    while (reader.Read())
                    {
                        string breed = reader["breed"].ToString();
                        string dogBreed = reader["dog_breed"].ToString();
                        string name = reader["name"].ToString();
                        Console.WriteLine($"Breed: {breed}, Dog Origin: {dogBreed}, Dog Name : {name}");
                        Console.WriteLine();
                    }

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