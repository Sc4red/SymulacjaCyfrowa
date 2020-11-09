using System;
using System.Runtime.InteropServices;

namespace DigitalSimulation
{
    class Manager
    // Klasa reprezentująca kierownika sali, który prowadzi grupę klientów do stolika. Czynność ta zajmuje s jednostek czasu.
    {
        public bool Status;
        private Client _client;
        public Manager()
        {
            Status = true;
            Console.WriteLine("Manager was created!");
        }

        ~Manager()
        {
            Console.WriteLine("Manager has been removed!");
        }

        public void Use(Client client)
        // Uzycie managera
        {
            Status = !Status;
            _client = client;
        }

        public Client ActualClient()
        // Zwolnik managera + oddaj klienta
        {
            Status = !Status;
            Log.Write("Klient obsłużony przez managera clientID: "+_client.ClientId.ToString());
          
            return _client;
        }

        public void evacuation(ref int id)
        {
            Status = !Status;
            id = _client.ClientId;
            Log.Write("Klient ewakuowany " + _client.ClientId.ToString());
            //usuniecie z tabeli zdarzen
        }


    }
}
