using System;
using System.Collections.Generic;

namespace DigitalSimulation
{
    class QueueToBuffet
        //Klasa reprezentująca kolejkę do bufetu. Klienci trafiają do bufetu zgodnie z kolejnością ustawienia się w kolejce (kolejka FIFO).
    {
        private List<Client> queue = new List<Client>();

        public bool Empty;

        public QueueToBuffet()
        {
            Empty = true;
            Console.WriteLine("QueueToBuffet was created!");
        }

        ~QueueToBuffet()
        {
            Console.WriteLine("QueueToBuffet has been removed!");
        }

        public void Push(Client client)
        {
            Empty = false;
            queue.Add(client);
        }

        public Client Pop()
        {
            if (queue.Count == 1)
            {
                Empty = true;
            }

            var cli = queue[0];
            queue.Remove(queue[0]);
            return cli;
        }

        public Client Peak()
        {
          return  queue[0];
        }

        public int count()
        {
           return queue.Count;
        }

        public void Evacuation(int i, ref int client_id)
        {
            client_id = queue[i].ClientId;
            queue.Remove(queue[i]);
            if (queue.Count < 1)
            {
                Empty = true;
            }
        }
    }
}
