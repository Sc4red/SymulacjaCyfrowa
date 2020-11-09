using System;
using System.Collections.Generic;

namespace DigitalSimulation
{
   class Buffet
       //Klasa reprezentująca bufet. Ilość miejsc w bufecie ‘b’.
   {
       public const int Count = 25;
       public int CurrentCount = 0;
       private Client _client;
       private readonly List<KeyValuePair<int, Client>> _clients = new List<KeyValuePair<int, Client>>();
        public int GroupCount = 0;

       public Buffet()
        {
            Console.WriteLine("Buffet was created!");
        }

        ~Buffet()
        {
            Console.WriteLine("Buffet has been removed!");
        }

        public bool Add(Client client,int id)
        {
            if (CurrentCount + client.Count <= Count)
            {
                CurrentCount = CurrentCount + client.Count;
                // Dodanie liczby klientow
                _clients.Add(new KeyValuePair<int, Client>(id, client));
                GroupCount++;
                return true;
                // Pomyslnie dodano do bufetu
            }
            else
            {
                return false;
                // Nie dodano do bufetu
            }
          
        }

        public void End(int clientId)
        // Zwolnienie miejsca w bufecie
        {
            for (int i = 0; i < _clients.Count; i++)
            {
//                Log.Write("Id w bufecie" + _clients[i].Value.ClientId.ToString());
                if (_clients[i].Value.ClientId == clientId)
                {
                    CurrentCount = CurrentCount - _clients[i].Value.Count;
                    // usuniecie klientow z baru
                  //  _clients.Remove(new KeyValuePair<int, Client>(clientId,_clients[i].Value));
                    //// usuniecie elementu
                }
            }


        }

        public int CurCount()
        {
            return CurrentCount;
        }

        public Client RemoveFromBuffet(int clientId)
        // Usuniecie klienta z bufetu
        {
            
            for (int i = 0; i < _clients.Count; i++)
            {
                if (_clients[i].Value.ClientId == clientId)
                {
                    _client = _clients[i].Value;
                    _clients.Remove(new KeyValuePair<int, Client>(clientId, _clients[i].Value));
                    GroupCount--;
                    // usuniecie elementu
                 
                }
            }
            return _client;
        }

        public void Evacuation(int id,ref int clientId)
        {
            
            GroupCount--;
            _client = _clients[id].Value;
            clientId = _client.ClientId;
            End(clientId);
            _clients.Remove(new KeyValuePair<int, Client>(clientId, _clients[id].Value));
        }

   }
}
