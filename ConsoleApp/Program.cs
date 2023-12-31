using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/// <summary>
/// Main class that contains the console.
/// </summary>
class Program
{
    // List of all the contacts.
    static List<Contact> contacts = new List<Contact>();

    //File path to save contacts as JSON.
    static string filePath = "contacts.json";

    /// <summary>
    /// Main method that runs when the program starts.
    /// </summary>
    static void Main()
    {
        // Load existing contacts at startup.
        LoadContacts();

        // Main menu in an infinite loop.
        while (true)
        {
            Console.WriteLine("Välkommen till Adressajten!");
            Console.WriteLine("1. Lägg till kontakt");
            Console.WriteLine("2. Visa alla kontakter");
            Console.WriteLine("3. Visa detaljer om kontakt");
            Console.WriteLine("4. Ta bort kontakt");
            Console.WriteLine("5. Avsluta");

            Console.Write("Vänligen välj en åtgärd (1-5): ");
            string choice = Console.ReadLine();

            // Manage user selection.
            switch (choice)
            {
                case "1":
                    AddContact();
                    break;
                case "2":
                    ShowAllContacts();
                    break;
                case "3":
                    ShowContactDetails();
                    break;
                case "4":
                    RemoveContact();
                    break;
                case "5":
                    SaveContacts();
                    return;
                default:
                    Console.WriteLine("Det blev ett ogiltigt val. Vänligen försök igen.");
                    break;
            }

            Console.WriteLine();
        }
    }

    /// <summary>
    /// Loads existing contacts from the JSON file at startup.
    /// </summary>
    static void LoadContacts()
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            contacts = JsonConvert.DeserializeObject<List<Contact>>(json);
        }
    }

    /// <summary>
    /// Saves all contacts to the JSON file.
    /// </summary>
    static void SaveContacts()
    {
        string json = JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented);
        System.IO.File.WriteAllText(filePath, json);
    }

    /// <summary>
    /// Adding a new contact based on user input.
    /// </summary>
    static void AddContact()
    {
        Console.Write("Ange namnet på kontakten: ");
        string fullName = Console.ReadLine();

        Console.Write("Ange Telefonnummer: ");
        string phoneNumber = Console.ReadLine();

        Console.Write("Ange E-postadress: ");
        string email = Console.ReadLine();

        Console.Write("Ange Adress: ");
        string address = Console.ReadLine();

        Console.Write("Ange Stad: ");
        string city = Console.ReadLine();

        Console.Write("Ange Personnummer: ");
        string personalNumber = Console.ReadLine();

        Contact newContact = new Contact
        {
            FirstName = fullName.Split(' ').First(), 
            LastName = fullName.Split(' ').Skip(1).FirstOrDefault(), 
            PhoneNumber = phoneNumber,
            Email = email,
            Address = address,
            City = city,
            PersonalNumber = personalNumber
        };

        contacts.Add(newContact);

        Console.WriteLine("Din kontakt har lagts till!");

    }

    /// <summary>
    /// Displays all contacts in a fine way.
    /// </summary>
    static void ShowAllContacts()
    {
        Console.WriteLine("Alla kontakter på adressajten nedan:");
        foreach (var contact in contacts)
        {
            Console.WriteLine($"{contact.FirstName} {contact.LastName}");
        }
    }

    /// <summary>
    /// Displays detailed information about a specific contact based on user input.
    /// </summary>
    static void ShowContactDetails()
    {
        Console.Write("Ange namnet på kontakten du vill hantera: ");
        string fullNameToFind = Console.ReadLine();

        var matchingContacts = contacts
            .Where(contact => (contact.FirstName + " " + contact.LastName).Equals(fullNameToFind, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (matchingContacts.Count > 0)
        {
            foreach (var contact in matchingContacts)
            {
                Console.WriteLine($"Detaljer för {contact.FirstName} {contact.LastName}:");
                Console.WriteLine($"Telefonnummer: {contact.PhoneNumber}");
                Console.WriteLine($"E-postadress: {contact.Email}");
                Console.WriteLine($"Adress: {contact.Address}");
                Console.WriteLine($"Stad: {contact.City}");
                Console.WriteLine($"Personnummer: {contact.PersonalNumber}");
            }
        }
        else
        {
            Console.WriteLine("Tyvärr hittades ingen kontakt med angivet namn...");
        }

    }

    /// <summary>
    /// Deletes a contact based on user input.
    /// </summary>
    static void RemoveContact()
    {
        Console.Write("Ange e-postadress för kontakten du vill ta bort: ");
        string email = Console.ReadLine();

        var contactToRemove = contacts.Find(c => c.Email == email);

        if (contactToRemove != null)
        {
            contacts.Remove(contactToRemove);
            Console.WriteLine("Din kontakt har tagits bort!");
        }
        else
        {
            Console.WriteLine("Tyvärr hittades inte kontakten med angiven e-postadress...");
        }
    }
}

/// <summary>
/// Represents a contact with basic information.
/// </summary>
class Contact
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string PersonalNumber { get; set; }
}
