using System;
using System.Data;
using System.Security.Cryptography;

namespace DigitalSimulation
{
    class Client
        //Klasa reprezentująca klienta. Klienci pojawiają się w restauracji jako grupy 1-, 2-, 3- lub 4-osobowe. Odstęp czasu rozdzielający pojawienie się kolejnych grup klientów jest zmienną losową o rozkładzie normalnym. Klienci ustawiają się w kolejkach według swoich preferencji (ok 50% klientów wybiera bufet)
    {
        public int Count;
        // Ilo osobowa grupa
        public bool Destination;
        // If true Stolik else Buffet
        private const double p1 = 0.11;
        // Prawdopodobienstwo wystapienia grupy jednoosobowej
        private const double p2 = 0.33;
        // Prawdopodobienstwo wystapienia grupy dwuosobowej
        private const double p3 = 0.33;

        // Prawdopodobienstwo wystapienia grupy czteroosobowej
        private static NumberGenerator _placeGenerator = new NumberGenerator();
        private static NumberGenerator _groupGenerator = new NumberGenerator();

        public static DataTable GeneratorGrup = new DataTable();
        public static DataTable GeneratorMiejsca = new DataTable();
        // Generator liczb
        public int ClientId = 0;
        public static int Cid=0;
        public int TimeToTableStart = 0;

        public Client()
        {
            Cid++;
            ClientId = Cid;
            Count = Group();
            Destination = Place();
          //  Console.WriteLine("Client was created!");
           if(!Destination){ Log.Write("Stworzony klienkt id:" + ClientId.ToString() + " BUFET");}
           else{ Log.Write("Stworzony klienkt id:" + ClientId.ToString() + " STOLIK");}


        }

        ~Client()
        {
          //  Console.WriteLine("Client has been removed!");
        }

        private int Group()
        // Losowanie z prawdopodobienstwami grup
        {
            var a = _groupGenerator.Designate(0,100);
            GeneratorGrup.Rows.Add(a);
            if (a <= p1 * 100) return 1;
            else if (a <= p1 * 100 + p2 * 100) return 2;
            else if (a <= p1 * 100 + p2 * 100 + p3 * 100) return 3;
            else return 4;
        }

        private bool Place()
        {
            var a = _placeGenerator.Designate(0, 100);
         //   GeneratorMiejsca.Rows.Add(a);
            if (a < 50)
            {
                GeneratorMiejsca.Rows.Add(1);
                return true;
            }
            else
            {
                GeneratorMiejsca.Rows.Add(2);
                return false;
            }
        }
    }
}
