using System;
using System.Data;

namespace DigitalSimulation
{
    class Cashier
        //Klasa reprezentująca kasjera. Ilość kasjerów na sali  ‘c’.
    {
        public bool Status;
        private Client _client;
        public static  DataTable _help= new DataTable();
        public Cashier()
        {
            Status = true;
            Console.WriteLine("Cashier was created!");
        }

        ~Cashier()
        {
            Console.WriteLine("Cashier has been removed!");
        }

        public void GetClient(Client client)
        {
            Status = !Status;
            _client = client;
         //   Log.Write("dodanie klienta o nr id do kasjera " + _client.ClientId);
         
        }

        public void RemoveClient()
        {
            Status = !Status;
            Log.Write("usuniecie klienta o nr id:" + _client.ClientId);
          _help.Rows.Add(_client.ClientId.ToString());
            _client = null;
        }

        public void Evacuation(ref int client_id)
        {
            Status = !Status;
            client_id = _client.ClientId;
            Log.Write("usuniecie klienta o nr id:" + _client.ClientId);
              _help.Rows.Add(_client.ClientId.ToString());
            _client = null;
        }
    }
}
