using System;

namespace DigitalSimulation
{
    class Table3
        //Klasa reprezentująca stół 3-osobowy. Klienci, którzy wybrali posiłek przy stoliku i są grupą nie większą niż 3-osobową trafiają do stolika 3-osobowego.
    {
        public bool Status;
        public Client Client;
        public DateTime Start;
        public bool Drink;
        public bool Eat;
        public bool InTable;

        public Table3()
        {
            Status = true;
            Drink = false;
            Eat = false;
            InTable = false;
            Console.WriteLine("Table3 was created!");
        }

        ~Table3()
        {
            Console.WriteLine("Table3 has been removed!");
        }

        public void AddClient(Client client)
        {
            Start = DateTime.Now;
            Client = client;
            Status = false;
        }

        public bool Done()
        {
            if (Drink && Eat)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Client RemoveClient()
        {
            Status = true;
            Drink = false;
            Eat = false;
            InTable = false;
            Cashier._help.Rows.Add(Client.ClientId);
            //Log.Write("Usuniecie ze stolika klient o id: " + Client.ClientId.ToString());
            return Client;
        }

        public void Evacuation(ref int client_id)
        {
            Status = true;
            Drink = false;
            Eat = false;
            InTable = false;
            // Log.Write("Usuniecie ze stolika klient o id: " + Client.ClientId.ToString());
            client_id = Client.ClientId;
            Client = null;
        }
    }
}
