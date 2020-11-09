using System;

namespace DigitalSimulation
{
    class Waiter
        //Klasa reprezentująca kelnera. Ilość kelnerów na sali  ‘k’.
    {
        public bool Status;
        private int _mode;
        private int _tableId;
        public static int time;
        public static int times =0;
        private int startTime;
        public Waiter()
        {
            Status = true;
            Console.WriteLine("Waiter was created!");
        }

        ~Waiter()
        {
            Console.WriteLine("Waiter has been removed!");
        }

        public void Use(int mode, int tableID,int StartTime)
        {
            Status = !Status;
            _mode = mode;
            _tableId = tableID;
            startTime = StartTime;
            times++;
        }

        public void End(out int mode, out int tableID, int EndTime)
        {
            Status = !Status;
           mode =  _mode;
           tableID = _tableId;
           time += EndTime - startTime;
        }

        public void Evacuation()
        {
            Status = !Status;
        }
    }
}
