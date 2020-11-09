using System;
using System.Collections.Generic;

namespace DigitalSimulation
{
    class QueueToCashier
        //Klasa reprezentująca kolejkę do kasjerów. Klienci w grupach trafiają do kasjerów zgodnie z kolejnością ustawienia się w kolejce (kolejka FIFO).
    {
        private List<Client> queue = new List<Client>();

        public bool Empty;
        public static double GroupsInQueue;
        public static double QueuesCount;

        public QueueToCashier()
        {
            Empty = true;
            Console.WriteLine("QueueToCashier was created!");
        }

        ~QueueToCashier()
        {
            Console.WriteLine("QueueToCashier has been removed!");
        }

        public void Push(Client client)
        {
            GroupsInQueue++;
            if (Empty)
            {
                Empty = false;
                QueuesCount++;
            }
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
