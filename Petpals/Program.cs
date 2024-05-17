using Petpals.Entity;
using Petpals.Service;
using System;
class Program
{
    static void Main(string[] args)
    {

        int choice;

        do
        {
            Console.WriteLine();
            Console.WriteLine("                  Choose an Option:                   ");
            Console.WriteLine();
            Console.WriteLine("1. List available pets for adoption");
            Console.WriteLine("2. Add pets to the database");
            Console.WriteLine("3. Donate");
            Console.WriteLine("4. Register for an event");
            Console.WriteLine("5. List all events");
            Console.WriteLine("6. Remove a pet");
            Console.WriteLine("7. List cats Alone");
            Console.WriteLine("8. List dogs with breed origin");
            Console.WriteLine("9. Exit");
            Console.WriteLine("════════════════════════════════════════════════════");
            Console.Write("Enter your choice: ");


            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid choice. Please enter a valid number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    PetShelter shelter = new PetShelter();
                    shelter.ListAvailablePetsFromDatabase();
                    break;
                case 2:
                    {
                        try
                        {
                            Console.WriteLine("Enter pet details:");
                            Console.Write("Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Age: ");
                            int age = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Breed: ");
                            string breed = Console.ReadLine();

                            PetShelter shelte = new PetShelter();
                            shelte.AddPet(new Pet(name, age, breed));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;
                    }
                case 7:
                    {
                        PetShelter shelte = new PetShelter();
                        shelte.ListCatBreedsWithColor();
                        break;
                    }
                case 8:
                    {
                        PetShelter shelte = new PetShelter();
                        shelte.ListDogBreeds();
                        break;
                    }

                
                case 3:
                    {
                        try
                        {
                            Console.WriteLine("Enter donor information:");
                            Console.Write("Name: ");
                            string donorName = Console.ReadLine();
                            

                            Console.WriteLine("Select donation type:");
                            Console.WriteLine("1. Cash donation");
                            Console.WriteLine("2. Item donation");
                            Console.Write("Enter choice: ");
                            int donationType = Convert.ToInt32(Console.ReadLine());

                            Donation donation;
                            if (donationType == 1)
                            {
                                Console.Write("Amount: ");
                                decimal amount = Convert.ToDecimal(Console.ReadLine());
                                Console.Write("Enter donation date (YYYY-MM-DD HH:mm:ss): ");
                                DateTime donationDate = Convert.ToDateTime(Console.ReadLine());
                                donation = new CashDonation(donorName, amount, donationDate);
                            }
                            else if (donationType == 2)
                            {
                                Console.Write("Amount: ");
                                decimal amount = Convert.ToDecimal(Console.ReadLine());
                                Console.Write("Enter item type: ");
                                string itemType = Console.ReadLine();
                                donation = new ItemDonation(donorName, amount, itemType);
                            }
                            else
                            {
                                Console.WriteLine("Invalid donation type.");
                                return;
                            }

                            donation.RecordDonation();
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input format. Please enter valid input.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred: {ex.Message}");
                        }
                        break;
                    }
                case 4:
                    try
                    {
                        Console.WriteLine("Enter participant information:");
                        Console.Write("Participant Name: ");
                        string participantName = Console.ReadLine();

                        Console.Write("Event ID: ");
                        int eventId = Convert.ToInt32(Console.ReadLine());

                        AdoptionEvent adoptionEven = new AdoptionEvent();
                        adoptionEven.RegisterParticipant(participantName, eventId);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input format. Please enter valid input.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                    break;
                case 5:
                    AdoptionEvent adoptionEvent = new AdoptionEvent();
                    adoptionEvent.ListAllEvents();
                    break;
                case 6:
                    {
                        try
                        {
                            Console.WriteLine("Enter the name of the pet to remove:");
                            string petName = Console.ReadLine();

                            PetShelter shelte = new PetShelter();
                            shelte.RemovePet(petName);
                            Console.WriteLine("---Deleted Successfully---");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;
                    }
                case 9:
                    Console.WriteLine("Exiting...");
                    break;



                default:
                    Console.WriteLine("Invalid choice. Please enter a valid number.");
                    break;
            }
        } while (choice != 9);
    }
}