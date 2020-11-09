using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;

namespace DigitalSimulation
{
    class QueueToTables
        //Klasa reprezentująca kolejkę do stolików. Klienci w grupach trafiają do stolika według strategii jaką obierze kierownik sali.
    {
        private List<Client> queue = new List<Client>();
      

        public static double GroupsInQueue;
        public static double QueuesCount;

        public bool Empty;

        public QueueToTables()
        {
            Empty = true;
            Console.WriteLine("QueueToTables was created!");
        }

        ~QueueToTables()
        {
            Console.WriteLine("QueueToTables has been removed!");
        }

        public void Push(Client client)
        {
            if (Empty)
            {
                Empty = false;
                QueuesCount++;
            }
            
            GroupsInQueue++;
            queue.Add(client);

            //Log.Write("Dodano klienta do listy");
            //for (int i = 0; i < queue.Count; i++)
            //{
            //    Log.Write("Klient id: "+ queue[i].ClientId.ToString());
            //}

        }

        public int count()
        {
            return queue.Count;
        }

        public void Evacuation(int i,ref int client_id)
        {
            client_id = queue[i].ClientId;
            queue.Remove(queue[i]);
            if (queue.Count < 1)
            {
                Empty = true;
            }
        }

        //public void Search(int x,out Client cli,out bool ok)
        //// liczba klientow, zwracany klient, jesli ok to istnieje taki klient
        //{
        //    cli = null;
        //    //1. dequeque jak pasuje to ok, jak nie to wrzucam do tablicy
        //    //2. kolejne dequque jak pasuje to ok jak nie to wrzucam do tablicy
        //    //3. jesli pasuje to wracam i usuwam z kolejku
        //    //4. zrzucam reszte kolejki
        //    //5. tworze kolejke na nowo

        //    List<Client> clientsTab = new List<Client>();
        //    Log.Write("na wejsciu + "+queue.Count.ToString());
        //    int i = 0;
        //    ok = false;
        //    while (i < queue.Count)
        //    {//cos tu nie dziaja
        //        clientsTab.Add(queue.Dequeue());

        //        cli = clientsTab[i];
        //        if (clientsTab[i].Count <= x)
        //        {
        //            ok = true;
        //            break;
        //            //jesli pierwszy sie zgadza
        //        }
        //        i++;
        //    }
        //    //jesli znnalazlo Przebudowa kolejki
        //    if (ok)
        //    {
        //        Queue<Client> queue2 = new Queue<Client>();

        //        for (int j = 0; j < clientsTab.Count-1; j++)
        //        {
        //            queue2.Enqueue(clientsTab[j]);
        //        }

        //        var a = queue.Count;
        //        for (int j = 0; j < a; j++)
        //        {
        //            queue2.Enqueue(queue.Dequeue());
        //        }
        //      try{  Log.Write("na wyjsciu + " + queue2.Count.ToString()); } catch { }
        //        Log.Write("Id klienta wychodzi z kolejki "+ cli.ClientId.ToString());
        //        queue = queue2;

        //    }
        //    else
        //    {
        //    //    queue = queue2;
        //    }
        //}

        public void Search(int x, out Client cli, out bool ok)
            // liczba klientow, zwracany klient, jesli ok to istnieje taki klient
        {
            cli = null;
            ok = false;
            for (int i = 0; i<queue.Count; i++)
            {
                if (queue[i].Count <= x)
                {
                    //  Log.Write("Wybrano klienta z listy o id: "+ queue[i].ClientId.ToString());
                 
                    cli = queue[i];
                  
                    queue.Remove(queue[i]);
                    ok = true;
                    if (queue.Count < 1)
                    {
                        Empty = true;
                    }
                    return;
                }
            }
        }

    }
}
