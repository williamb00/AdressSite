using System;
using System.Collections.Generic;
using Newtonsoft.Json;

class Program
{
    static List<Contact> contacts = new List<Contact>();
    static string filePath = "contacts.json";

    static void Main()
    {
        LoadContacts();

        while (true)
        {
            Console.WriteLine("Välkommen till Adressboken!");
            Console.WriteLine("1. Lägg till kontakt");
            Console.WriteLine("2. Visa alla kontakter");
            Console.WriteLine("3. Visa detaljer om kontakt");
            Console.WriteLine("4. Ta bort kontakt");
            Console.WriteLine("5. Avsluta");

            Console.Write("Vänligen välj en åtgärd (1-5): ");
            string choice = Console.ReadLine();

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
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void LoadContacts()
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            contacts = JsonConvert.DeserializeObject<List<Contact>>(json);
        }
    }

    static void SaveContacts()
    {
        string json = JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented);
        System.IO.File.WriteAllText(filePath, json);
    }

    static void AddContact()
    {
        Console.Write("Ange förnamn: ");
        string firstName = Console.ReadLine();

        Console.Write("Ange efternamn: ");
        string lastName = Console.ReadLine();

        Console.Write("Ange telefonnummer: ");
        string phoneNumber = Console.ReadLine();

        Console.Write("Ange e-postadress: ");
        string email = Console.ReadLine();

        Console.Write("Ange adressinformation: ");
        string address = Console.ReadLine();

        Contact newContact = new Contact
        {
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Email = email,
            Address = address
        };

        contacts.Add(newContact);

        Console.WriteLine("Kontakt har lagts till!");
    }

    static void ShowAllContacts()
    {
        Console.WriteLine("Alla kontakter:");
        foreach (var contact in contacts)
        {
            Console.WriteLine($"{contact.FirstName} {contact.LastName}");
        }
    }

    static void ShowContactDetails()
    {
        Console.Write("Ange index för kontakten du vill se detaljer om: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < contacts.Count)
        {
            var contact = contacts[index];
            Console.WriteLine($"Detaljer för {contact.FirstName} {contact.LastName}:");
            Console.WriteLine($"Telefonnummer: {contact.PhoneNumber}");
            Console.WriteLine($"E-postadress: {contact.Email}");
            Console.WriteLine($"Adress: {contact.Address}");
        }
        else
        {
            Console.WriteLine("Ogiltigt index. Försök igen.");
        }
    }

    static void RemoveContact()
    {
        Console.Write("Ange e-postadress för kontakten du vill ta bort: ");
        string email = Console.ReadLine();

        var contactToRemove = contacts.Find(c => c.Email == email);

        if (contactToRemove != null)
        {
            contacts.Remove(contactToRemove);
            Console.WriteLine("Kontakt har tagits bort!");
        }
        else
        {
            Console.WriteLine("Kontakt med angiven e-postadress hittades inte.");
        }
    }
}

class Contact
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}
