using System;

namespace DigitalSimulation
{
    class Table2
        //Klasa reprezentująca stół 2-osobowy. Klienci, którzy wybrali posiłek przy stoliku i są grupą nie większą niż 2-osobową trafiają do stolika 2-osobowego.
    {
        public bool Status;
        public Client Client;
        public DateTime Start;
        public bool Drink;
        public bool Eat;
        public bool InTable;

        public Table2()
        {

            Status = true;
            Drink = false;
            Eat = false;
            InTable = false;
            Console.WriteLine("Table2 was created!");
        }

        ~Table2()
        {
            Console.WriteLine("Table2 has been removed!");
        }

        public void AddClient(Client client)
        {
            Start = DateTime.Now;
            Client = client;
            Status = false;
        }

        public Client RemoveClient()
        {
            Status = true;
            Drink = false;
            Eat = false;
            InTable = false;
            // Log.Write("Usuniecie ze stolika klient o id: " + Client.ClientId.ToString());
            Cashier._help.Rows.Add(Client.ClientId);
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

       
    }
}
