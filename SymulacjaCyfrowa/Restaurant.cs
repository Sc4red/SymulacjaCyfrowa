using System;
using System.Collections.Generic;
using System.Data;

namespace DigitalSimulation
{
    class Restaurant
        // Klasa gromadząca wszystkie pozostałe elementy systemu.
    {
        private int _deleteClients = 0;
        private int _evacuationClients = 0;


        private int _clientsToBuffet = 0;
        private int _buffetKolejka = 0;

        private int _clientsToTable = 0;
        private int _timeToTable = 0;

        private double _clientsToTableInQueueCount = 0;
        private double _queueToTableCount = 0;


        private int _sysTime = 0;

        private int _clientsInRestaurant = 0;

        private int _qA = 100;
        private int _sigmaA2 = 50;
        private int _s = 120;
        /// <summary>
        private int _qB = 3400;
        /// </summary>
        private int _sigmaB2 = 200;
        private int _lambdaN = 700;
        private int _lambdaJ = 3200;
        /// <summary>
        private int _lambdaF = 1810;
        /// </summary>
        private int _lambdaP = 400;
        private int _qAlarm = 4200;
        private int _sigmaAlarm = 50;

        private static int _clientsCount = 0;
        // Liczba Klientow
        private const int NumberOfTable2 = 20;
        // Liczba stolików 2 osobowych.
        private const int NumberOfTable3 = 12;
        // Liczba stolików 3 osobowych.
        private const int NumberOfTable4 = 5;
        // Liczba stolików 4 osobowych.
        private const int NumberOfCashier = 7;
        // Liczba kasjerów
        private const int NumberOfWaiter = 17;
        // Liczba kelnerów
        private List<Cashier> _cashier = new List<Cashier>();
        // Lista kasjerów.
        private List<Waiter> _waiter = new List<Waiter>();
        // Lista kelnerów.
        private List<Table2> _table2 = new List<Table2>(); 
        // Lista stolików 2 osobowych.
        private List<Table3> _table3 = new List<Table3>();
        // Lista stolików 3 osobowych.
        private List<Table4> _table4 = new List<Table4>();
        // Lista stolików 4 osobowych.
        private readonly DataTable _eventTable = new DataTable();

        private readonly DataTable _dataTable = new DataTable();
        // Tabela zdarzeń
        private readonly DataTable _dataTableGeneratorKlienci = new DataTable();
        private readonly DataTable _dataTableGeneratorCzasBuffet = new DataTable();
        private readonly DataTable _dataTableGeneratorCzasJedzenia = new DataTable();
        private readonly DataTable _dataTableGeneratorPrzyniesieniaJedzenia = new DataTable();
        private readonly DataTable _dataTableGeneratorPrzyniesieniePicia = new DataTable();
        private readonly DataTable _dataTableGeneratorObslugaKasjera = new DataTable();
        // Tabela zdarzeń
        private QueueToTables _queueToTables = new QueueToTables();
        // Kolejka do stolikow
        private  QueueToBuffet _queueToBuffet = new QueueToBuffet();
        // Kolejka do Bufetu
        private  QueueToCashier _queueToCashier = new QueueToCashier();
        // Kolejka do kasy
        private Buffet _buffet; 
        private Manager _manager;
        private readonly NumberGenerator _numberGeneratorClient = new NumberGenerator();

        private readonly NumberGenerator _numberGeneratorBuffet = new NumberGenerator();

        private readonly NumberGenerator _numberGeneratorWaiterDrink = new NumberGenerator();

        private readonly NumberGenerator _numberGeneratorWaiterEat = new NumberGenerator();

        private readonly NumberGenerator _numberGeneratorEatTime = new NumberGenerator();

        private readonly NumberGenerator _numberGeneratorCashierTime = new NumberGenerator();

        private readonly NumberGenerator _numberGeneratorAlarm = new NumberGenerator();

        private readonly NumberGenerator _numberGeneratorEvacuation = new NumberGenerator();

        private int _var1;
        private int _var2;
        private int _freeCashier;
        private int _freeWaiter;

        public Restaurant()
        {
            Console.WriteLine("Restaurant was created!");
            _buffet = new Buffet();
            _manager = new Manager();
            for (int i = 0; i < NumberOfCashier; i++)
                _cashier.Add(new Cashier());
            for (int i = 0; i < NumberOfWaiter; i++)
                _waiter.Add(new Waiter());
            for (int i = 0; i < NumberOfTable2; i++)
                _table2.Add(new Table2());
            for (int i = 0; i < NumberOfTable3; i++)
                _table3.Add(new Table3());
            for (int i = 0; i < NumberOfTable4; i++)
                _table4.Add(new Table4());

            Cashier._help.Columns.Add("COUNT", typeof(int));
            _dataTableGeneratorKlienci.Columns.Add("COUNT", typeof(int));
            _dataTableGeneratorCzasBuffet.Columns.Add("COUNT", typeof(int));
            _dataTableGeneratorCzasJedzenia.Columns.Add("COUNT", typeof(int));
            _dataTableGeneratorObslugaKasjera.Columns.Add("COUNT", typeof(int));
            _dataTableGeneratorPrzyniesieniaJedzenia.Columns.Add("COUNT", typeof(int));
            _dataTableGeneratorPrzyniesieniePicia.Columns.Add("COUNT", typeof(int));
            Client.GeneratorGrup.Columns.Add("COUNT", typeof(int));
            Client.GeneratorMiejsca.Columns.Add("COUNT", typeof(int));


            _dataTable.Columns.Add("COUNT", typeof(int));
            _dataTable.Columns.Add("TIME", typeof(int));

            _eventTable.Columns.Add("ID", typeof(int));
            _eventTable.Columns.Add("DATE", typeof(int));
            _eventTable.Columns.Add("VAR1", typeof(int));
            _eventTable.Columns.Add("VAR2", typeof(int));
            _eventTable.Columns.Add("NAME", typeof(string));
            _eventTable.Columns.Add("CLIENT_ID", typeof(int));
        }

        ~Restaurant()
        {
            _buffet = null;
            _manager = null;
            _cashier = null;
            _waiter = null;
            _table2 = null;
            _table3 = null;
            _table4 = null;
            Console.WriteLine("Restaurant has been removed!");
        }




        bool _tryb = false;
        bool _log = true;


        public void Simulation()
        {
         

            //Lista po zdarzeniach ulozona wg czasu
            var x = new DateTime();
           x = DateTime.Now;
           Console.WriteLine(x.ToString());
        
           // jesli falsz to ciagly

           Console.WriteLine("Aby uruchomic symulacje w trybie krokowym wybierz : 1");
           Console.WriteLine("Aby uruchomic symulacje w trybie ciaglym wybierz: 2");
           Console.WriteLine("Aby uruchomic symulacje w trybie szybkim wybierz: 3");

           var v = Int32.Parse(Console.ReadLine().ToString());
            if (v == 1)
           {
               _tryb = true;
           }else if (v == 3)
            {
                _log = false;
            }

           if (_tryb)
           {
               Console.WriteLine("Press enter to continue..");
               Console.ReadKey();
           }
            Event(1);
                  while (_eventTable.Rows.Count > 0)
                  {
                    //  if(ClientsCount>100)break;
                if (_tryb)
                {
                    Console.WriteLine("Press enter to continue..");
                    Console.ReadKey();
                }
               if(_tryb){ Console.WriteLine();}
                Event(CheckEvent());
                  }

                  if (_log)
                  {
                      for (int i = 0; i < _eventTable.Rows.Count; i++)
                      {
                          Log.Write(_eventTable.Rows[i]["NAME"].ToString());
                      }

                      Log.Write("");
                      Log.Write("Klienci: " + _clientsCount.ToString());
                      Log.Write("Usunieci Klienci: " + _clientsCount.ToString());
                      Log.Write("Sredni czas oczekiwania na stolik :" + ((double)_timeToTable / (double)_clientsToTable).ToString());
                       Log.Write("Srednia dlugosc kolejki oczekujacych na stolik :" + (_clientsToTableInQueueCount / _queueToTableCount).ToString());
                       Log.Write("Srednia dlugosc obslugi kelnera :" + (Waiter.time / Waiter.times).ToString());
                Log.Write("W restauracji:");
                      for (int i = 0; i < _dataTable.Rows.Count; i++)
                      {
                          Log.Write((_dataTable.Rows[i]["COUNT"].ToString() + "  ,  " +
                                     _dataTable.Rows[i]["TIME"].ToString()));
                      }
                  }

           

                  Console.WriteLine("Stoliki kolejka :" + (_clientsToTableInQueueCount).ToString());
                   Console.WriteLine("Zakonczono symulacje");
                  Console.WriteLine("ClientsToBuffet" + _clientsToBuffet.ToString());
                  Console.WriteLine("UsuniecieKlienci" + _deleteClients.ToString());
                Console.WriteLine("UsuniecieKlienciAlarm" + _evacuationClients.ToString());
                Console.WriteLine("ClientsToTables" + _clientsToTable.ToString());
                  Console.WriteLine("BuffetKolejka" + _buffetKolejka.ToString());
                  Console.WriteLine("Sredni czas oczekiwania na stolik :" + (_timeToTable / _clientsToTable).ToString());
                  Console.WriteLine("Srednia kolejka do stolikow :" + (QueueToTables.GroupsInQueue / QueueToTables.QueuesCount).ToString());
            Console.WriteLine("Srednia dlugosc obslugi kelnera :" + (Waiter.time / Waiter.times).ToString());
                  Console.WriteLine("Srednia kolejka do kas :" + (QueueToCashier.GroupsInQueue / QueueToCashier.QueuesCount).ToString());

            Export(_dataTable, "Dane.xlsx");
                  Export(_dataTableGeneratorKlienci, "GeneratorKlienci.xlsx");

                  Export(_dataTableGeneratorCzasBuffet, "GeneratorCzasBuffet.xlsx");

                  Export(_dataTableGeneratorCzasJedzenia, "GeneratorCzasJedzenia.xlsx");

                  Export(_dataTableGeneratorObslugaKasjera, "GeneratorObslugaKasjera.xlsx");

                  Export(_dataTableGeneratorPrzyniesieniaJedzenia, "GeneratorPrzyniesienieJedzenia.xlsx");

                  Export(_dataTableGeneratorPrzyniesieniePicia, "GeneratorPrzyniesieniePicia.xlsx");

                  Export(Client.GeneratorGrup, "GeneratorGrup.xlsx");

                  Export(Client.GeneratorMiejsca, "GeneratorMiejsca.xlsx");

                  Export(Cashier._help, "Help.xlsx");

        }

            private void AddEvent(int value, int date,string name, int x=0, int y =0, int clientId=0)
        {
            _eventTable.Rows.Add(value, date,x,y,name,clientId);
        }

        private int CheckEvent()
        {
            var x = 0;
            var y = 0;

          //  Console.WriteLine("Tabela zdarzen");
          if (_log)
          {
              Log.Write("");
              Log.Write("Tabela zdarzen");
          }

          for (int i = 0; i < _eventTable.Rows.Count; i++)
            {
             
               if(_log){ Log.Write(_eventTable.Rows[i]["NAME"].ToString()+ "  ,  "+ _eventTable.Rows[i]["DATE"].ToString() + "  ,  " + _eventTable.Rows[i]["VAR1"].ToString());}
                
                if (i == 0)
                {
                    x = (int)_eventTable.Rows[i]["DATE"];
                }
                else
                {
                    if (x >= (int)_eventTable.Rows[i]["DATE"])
                    {
                        x = (int)_eventTable.Rows[i]["DATE"];
                        y = i;
                    }
                }
            }
           // Log.Write("");
            if (_tryb)
            {
                Console.WriteLine();
                Console.WriteLine();
            }

            try {_var1 = (int)_eventTable.Rows[y]["VAR1"]; } catch { }
            try { _var2 = (int)_eventTable.Rows[y]["VAR2"]; } catch { }
            var c = (int)_eventTable.Rows[y]["ID"];
            _sysTime = (int) _eventTable.Rows[y]["DATE"];
            _eventTable.Rows[y].Delete();
            return c;
            
        }

        private void DeleteEvent(int clientId)
        {
            for (int i = 0; i < _eventTable.Rows.Count; i++)
            {
                if (clientId == (int)_eventTable.Rows[i]["CLIENT_ID"])
                {
                    if("WaiterDrinkEndOfService" == _eventTable.Rows[i]["NAME"].ToString() || "WaiterEatEndOfService" == _eventTable.Rows[i]["NAME"].ToString())
                    {
                        _waiter[(int)_eventTable.Rows[i]["VAR1"]].Evacuation();
                    }
                    _eventTable.Rows[i].Delete();
                }
            }



        }

        bool _firstAlarm = true;
        bool _work = true;
 
        private void Event(int x)
        {
            switch (x)
            {
                case 1:
                    if (_clientsCount > 1000) {
                        _work = false;
                        break;

                    }
                   if(_tryb){ Console.WriteLine("RegisteringNewGroupOfClients");}

                    var genNum = 0;

                    if (_firstAlarm)
                    {
                        genNum = (int)_numberGeneratorAlarm.Gaussian(_qAlarm, _sigmaAlarm);
                        _firstAlarm = false;
                        AddEvent(8, _sysTime + genNum, "Alarm"); //Przewidzenie pierwszego alarmu
                    }

                    genNum = (int) _numberGeneratorClient.Gaussian(_qA, _sigmaA2);
                    _dataTableGeneratorKlienci.Rows.Add(genNum);

                    AddEvent(1, _sysTime + (genNum), "RegisteringNewGroupOfClients");
                    // Zaplanowanie kolejnego klienta w takim czasie
                    Client client = new Client();
                    _clientsInRestaurant++;
                    _dataTable.Rows.Add(_clientsInRestaurant, _sysTime);
                   
                    _clientsCount++;
                   if(_tryb){ Console.WriteLine("ClientsCount: " + _clientsCount.ToString());}
                    if (client.Destination)
                    // Wycieczka stoliki
                    {
                        _clientsToTable++;
                        if (_manager.Status)
                        // Czy kierownik jest wolny
                        {
                            if (Table(client.Count))
                            // Czy istnieje stolik dla tej liczby klientow
                            {
                                _manager.Use(client);
                                // Przekazanie klienta do menagera
                                AddEvent(2, _sysTime+_s, "ManagerEndOfService",0,0,client.ClientId);
                                     // Przewidzenie zakonczenie obslugi przez managera ( w takim czasie)
                            }
                            else
                            // Dodaj do kolejki stolikowej
                            {
                                if (_queueToTables.Empty)
                                    //Powstanie nowej kolejki
                                {
                                    _queueToTableCount++;

                                }
                                _clientsToTableInQueueCount++;
                                client.TimeToTableStart = _sysTime;
                                _queueToTables.Push(client);
                            }
                        }
                        else
                        // Dodaj do kolejki stolikowej
                        {
                            if (_queueToTables.Empty)
                                //Powstanie nowej kolejki
                            {
                                _queueToTableCount++;

                            }
                            _clientsToTableInQueueCount++;
                            client.TimeToTableStart = _sysTime;
                            _queueToTables.Push(client);
                        }
                    }
                    else
                    // Wycieczka buffet
                    {
                        _clientsToBuffet++;
                        if (_buffet.Add(client,client.ClientId ))
                        {
                            genNum = (int) _numberGeneratorBuffet.Gaussian(_qB, _sigmaB2);
                            _dataTableGeneratorCzasBuffet.Rows.Add(genNum);
                            AddEvent(6, _sysTime + genNum, "BuffetEndOfService", client.ClientId,0,client.ClientId);
                            //Zaplanowanie zakonczenie obslugi bufetu do tablicy (w takim czasie)
                        }
                        // Jest miejsce w bufecie i dodano klienta
                        else
                        {
                            _buffetKolejka++;
                            _queueToBuffet.Push(client);
                        }
                        // Jesli nie ma miejsca to dodaj do kolejki bufetowej
                    }

                    break;
                case 2:
                   if(_tryb){ Console.WriteLine("ManagerEndOfService");}

                   if (_log)
                   {
                       Log.Write(("ManagerEndOfService"));
                       Log.Write("");
                   }

                   _timeToTable += _s;
                    // Przekazanie klienta do stolika i zwolnienie managera
                   client = _manager.ActualClient();
               
                    Table(client, out var mode, out var tableId);
                    // Wrzucenie grupy do stolika
                    if (CheckWaiter())
                        // Jest wolny kelner
                    {
                        _waiter[_freeWaiter].Use(mode,tableId,_sysTime);
                        // Przypisanie stolika do kelnera
                        if (mode == 2)
                        {
                            _table2[tableId].InTable = true;
                        }

                        if (mode == 3)
                        {
                            _table3[tableId].InTable = true;
                        }

                        if (mode == 4)
                        {
                            _table4[tableId].InTable = true;
                        }

                        genNum = (int) _numberGeneratorWaiterDrink.Wyk(_lambdaN);
                        _dataTableGeneratorPrzyniesieniePicia.Rows.Add(genNum);
                        AddEvent(3,_sysTime + genNum, "WaiterDrinkEndOfService",_freeWaiter,0,client.ClientId);
                        // Zaplanuj koniec przyniesienia picia
                    }
               
                   

                    if (!_queueToTables.Empty) 
                        // Jesli jest kolejka do stolikow ; Kierownik szuka w kolejce pierwszej grupy mieszczącej się w wolnych stolikach
                    {
                        //Sprawdzic wolne stoliki
                        var space = true;
                        if (ManagerCheckTable() != 0)
                        {
                          //  Log.Write("Kolejka do managera");
                            if (CheckTable2() && space)
                            {
                                // Szukam w kolejce pierwszej grupy 2 lub < os
                                // Do sprawdzenia
                               // Log.Write("Szuka klientow 2os");
                                _queueToTables.Search(2,out client,out var ok);
                                if (ok)
                                {
                                    _timeToTable += _sysTime - client.TimeToTableStart;
                                    _manager.Use(client);
                                    // Przekazanie klienta do managera

                                    AddEvent(2, _sysTime + _s, "ManagerEndOfService", 0, 0, client.ClientId);
                                    space = false;
                                }
                            }

                            if (CheckTable3() && space)
                            {
                                // Szukam w kolejce pierwszej grupy 3 lub < os
                              //  Log.Write("Szuka klientow 3os");
                                _queueToTables.Search(3, out client, out var ok);
                                if (ok)
                                {
                                    _timeToTable += _sysTime - client.TimeToTableStart;
                                    _manager.Use(client);
                                    // Przekazanie klienta do managera

                                    AddEvent(2, _sysTime + _s, "ManagerEndOfService", 0, 0, client.ClientId);
                                    space = false;
                                }
                            }

                            if (CheckTable4() && space)
                            {
                                // Szukam w kolejce pierwszej grupy 4 lub < os
                               // Log.Write("Szuka klientow 4os");
                                _queueToTables.Search(4, out client, out var ok);
                                if (ok)
                                {
                                    _timeToTable += _sysTime - client.TimeToTableStart;
                                    _manager.Use(client);
                                    // Przekazanie klienta do managera

                                    AddEvent(2, _sysTime + _s, "ManagerEndOfService", 0, 0, client.ClientId);
                                    space = false;
                                }
                            }
                        }
                    }

                    break;
                case 3:
                   if(_tryb){ Console.WriteLine("WaiterDrinkEndOfService");}

                   if (_log)
                   {
                       Log.Write(("WaiterDrinkEndOfService"));
                       Log.Write("");
                   }

                   _waiter[_var1].End(out mode, out tableId,_sysTime);
                  if(_log){  Log.Write("Zakonczenie obslugi Picie stolika " +  mode.ToString()+ " osobowego o id " + tableId.ToString());}
                    if (mode == 2)
                    {
                        _table2[tableId].Drink = true;
                        _table2[tableId].InTable = false;
                    }
                    if (mode == 3)
                    {
                        _table3[tableId].Drink = true;
                        _table3[tableId].InTable = false;
                    }
                    if (mode == 4)
                    {
                        _table4[tableId].Drink = true;
                        _table4[tableId].InTable = false;
                    }
                    // Zwolnienie kelnera

                    // Stolik ktory najdluzej czeka + przypisanie mu kelnera
                    TimeTable(out var id,out  mode);
                  if(_log){  Log.Write("Stolik ktory najdlużej czeka " + mode.ToString() + " osobowego o id " + id.ToString());}
                    if (mode == 2)
                    {
                        _waiter[_var1].Use(mode,id,_sysTime);
                        if (_table2[id].Drink)
                        {
                            genNum = (int) _numberGeneratorWaiterEat.Wyk(_lambdaJ);
                            _dataTableGeneratorPrzyniesieniaJedzenia.Rows.Add(genNum);
                            AddEvent(4, _sysTime + genNum, "WaiterEatEndOfService", _var1,0,_table2[id].Client.ClientId);
                            // Zaplanuj koniec przyniesienia jedzenie
                        }
                        else
                        {
                            genNum = (int)_numberGeneratorWaiterDrink.Wyk(_lambdaN);
                            _dataTableGeneratorPrzyniesieniePicia.Rows.Add(genNum);
                            AddEvent(3, _sysTime + genNum, "WaiterDrinkEndOfService", _var1, 0, _table2[id].Client.ClientId);
                            // Zaplanuj koniec przyniesienia picia
                        }

                    }
                    if (mode == 3)
                    {
                        _waiter[_var1].Use(mode,id,_sysTime);
                        if (_table3[id].Drink)
                        {
                            genNum = (int)_numberGeneratorWaiterEat.Wyk(_lambdaJ);
                            _dataTableGeneratorPrzyniesieniaJedzenia.Rows.Add(genNum);
                            AddEvent(4, _sysTime + genNum, "WaiterEatEndOfService", _var1, 0, _table3[id].Client.ClientId);
                            // Zaplanuj koniec przyniesienia jedzenie
                        }
                        else
                        {
                            genNum = (int)_numberGeneratorWaiterDrink.Wyk(_lambdaN);
                            _dataTableGeneratorPrzyniesieniePicia.Rows.Add(genNum);
                            AddEvent(3, _sysTime + genNum, "WaiterDrinkEndOfService", _var1, 0, _table3[id].Client.ClientId);
                            // Zaplanuj koniec przyniesienia picia
                        }
                    }
                    if (mode == 4)
                    {
                        var ac = _table4[id].Client.ClientId;
                        _waiter[_var1].Use(mode,id,_sysTime);
                        if (_table4[id].Drink)
                        {
                       
                            genNum = (int)_numberGeneratorWaiterEat.Wyk(_lambdaJ);
                            _dataTableGeneratorPrzyniesieniaJedzenia.Rows.Add(genNum);
                            AddEvent(4, _sysTime + genNum, "WaiterEatEndOfService", _var1, 0, _table4[id].Client.ClientId);
                            // Zaplanuj koniec przyniesienia jedzenie
                        }
                        else
                        {
                            genNum = (int)_numberGeneratorWaiterDrink.Wyk(_lambdaN);
                            _dataTableGeneratorPrzyniesieniePicia.Rows.Add(genNum);
                            AddEvent(3, _sysTime + genNum, "WaiterDrinkEndOfService", _var1, 0, _table4[id].Client.ClientId);
                            // Zaplanuj koniec przyniesienia picia
                        }
                    }

                    break;
                case 4:
                  if(_tryb){  Console.WriteLine("WaiterEatEndOfService");}

                  if (_log)
                  {
                      Log.Write("WaiterEatEndOfService");
                      Log.Write("");
                  }

                  _waiter[_var1].End(out mode, out tableId,_sysTime);
                   if(_log){ Log.Write("Zakonczenie obslugi jedzenie " + mode.ToString() + " osobowego o id " + tableId.ToString());}
                    genNum = (int)_numberGeneratorEatTime.Wyk(_lambdaF);
                    _dataTableGeneratorCzasJedzenia.Rows.Add(genNum);

                    if (mode == 2)
                    {
                        _table2[tableId].Eat = true;
                        AddEvent(5, _sysTime + genNum, "TableEndOfService", mode, tableId, _table2[tableId].Client.ClientId);
                        // Zaplanuj zakonczenie jedzenia dla tego stolika
                    }
                    if (mode == 3)
                    {
                        _table3[tableId].Eat = true;
                        AddEvent(5, _sysTime + genNum, "TableEndOfService", mode, tableId, _table3[tableId].Client.ClientId);
                        // Zaplanuj zakonczenie jedzenia dla tego stolika
                    }
                    if (mode == 4)
                    {
                        _table4[tableId].Eat = true;
                        AddEvent(5, _sysTime + genNum, "TableEndOfService", mode, tableId,_table4[tableId].Client.ClientId);
                        // Zaplanuj zakonczenie jedzenia dla tego stolika
                    }




                    // Stolik ktory najdluzej czeka + przypisanie mu kelnera
                    TimeTable(out id, out mode);
                    if(_log){Log.Write("Stolik ktory najdlużej czeka " + mode.ToString() + " osobowego o id " + id.ToString());}
                    if (mode == 2)
                    {
                        _waiter[_var1].Use(mode, id,_sysTime);
                        if (_table2[id].Drink)
                        {
                            genNum = (int)_numberGeneratorWaiterEat.Wyk(_lambdaJ);
                            _dataTableGeneratorPrzyniesieniaJedzenia.Rows.Add(genNum);
                            AddEvent(4, _sysTime + genNum, "WaiterEatEndOfService", _var1, 0, _table2[id].Client.ClientId);
                            // Zaplanuj koniec przyniesienia jedzenie
                        }
                        else
                        {
                            genNum = (int)_numberGeneratorWaiterDrink.Wyk(_lambdaN);
                            _dataTableGeneratorPrzyniesieniePicia.Rows.Add(genNum);
                            AddEvent(3, _sysTime + genNum, "WaiterDrinkEndOfService", _var1, 0, _table2[id].Client.ClientId);
                            // Zaplanuj koniec przyniesienia picia
                        }

                    }
                    if (mode == 3)
                    {
                        _waiter[_var1].Use(mode, id,_sysTime);
                        if (_table3[id].Drink)
                        {
                            genNum = (int)_numberGeneratorWaiterEat.Wyk(_lambdaJ);
                            _dataTableGeneratorPrzyniesieniaJedzenia.Rows.Add(genNum);
                            AddEvent(4, _sysTime + genNum, "WaiterEatEndOfService", _var1, 0, _table3[id].Client.ClientId);
                            // Zaplanuj koniec przyniesienia jedzenie
                        }
                        else
                        {
                            genNum = (int)_numberGeneratorWaiterDrink.Wyk(_lambdaN);
                            _dataTableGeneratorPrzyniesieniePicia.Rows.Add(genNum);
                            AddEvent(3, _sysTime + genNum, "WaiterDrinkEndOfService", _var1, 0, _table3[id].Client.ClientId);
                            // Zaplanuj koniec przyniesienia picia
                        }
                    }
                    if (mode == 4)
                    {
                        var ac = _table4[id].Client.ClientId;
                        _waiter[_var1].Use(mode, id,_sysTime);
                        if (_table4[id].Drink)
                        {
                            genNum = (int)_numberGeneratorWaiterEat.Wyk(_lambdaJ);
                            _dataTableGeneratorPrzyniesieniaJedzenia.Rows.Add(genNum);
                            AddEvent(4, _sysTime + genNum, "WaiterEatEndOfService", _var1, 0, _table4[id].Client.ClientId);
                            // Zaplanuj koniec przyniesienia jedzenie
                        }
                        else
                        {
                            genNum = (int)_numberGeneratorWaiterDrink.Wyk(_lambdaN);
                            _dataTableGeneratorPrzyniesieniePicia.Rows.Add(genNum);
                            AddEvent(3, _sysTime + genNum, "WaiterDrinkEndOfService", _var1, 0, _table4[id].Client.ClientId);
                            // Zaplanuj koniec przyniesienia picia
                        }
                    }

               
                    break;
                case 5:
                   if(_tryb){ Console.WriteLine("TableEndOfService");}
                    // Zwolnij stolik
                   if (_log)
                   {
                       Log.Write("TableEndOfService");
                       Log.Write("");
                   }

                   if (_var1 == 2)
                    {
                       client =  _table2[_var2].RemoveClient();
                    }
                    else if (_var1 == 3)
                    {
                        client = _table3[_var2].RemoveClient();
                    }
                    else
                    {
                        client = _table4[_var2].RemoveClient();
                    }

                    if (CheckCashier())
                        // Jesli jest wolny kasjer
                    {
                        _cashier[_freeCashier].GetClient(client);
                        // Przekazanie klienta do kasy
                        genNum = (int) _numberGeneratorCashierTime.Wyk(_lambdaP);
                        _dataTableGeneratorObslugaKasjera.Rows.Add(genNum);
                        AddEvent(7, _sysTime + genNum, "BarEndOfService",_freeCashier,0,client.ClientId);
                        // Zaplanuj zakonczenie uslugi
                    }
                    else
                        // Jesli zajety to klient do kolejki
                    {
                        Log.Write("Dodanie klkienta do kolejki do kas");
                        _queueToCashier.Push(client);
                    }


                  
                       if (!_queueToTables.Empty)
                           // Jesli jest kolejka do stolikow ; Kierownik szuka w kolejce pierwszej grupy mieszczącej się w wolnych stolikach
                       {
                        if (_manager.Status)
                        {  //Sprawdzic wolne stoliki
                            var space = true;
                           if (ManagerCheckTable() != 0)
                           {
                               //  Log.Write("Kolejka do managera");
                               if (CheckTable2() && space)
                               {
                                   // Szukam w kolejce pierwszej grupy 2 lub < os
                                   // Do sprawdzenia
                                   // Log.Write("Szuka klientow 2os");
                                   _queueToTables.Search(2, out client, out var ok);
                                   if (ok)
                                   {
                                       _timeToTable += _sysTime - client.TimeToTableStart;
                                       _manager.Use(client);
                                        // Przekazanie klienta do managera

                                        AddEvent(2, _sysTime + _s, "ManagerEndOfService", 0, 0, client.ClientId);
                                        space = false;
                                   }
                               }

                               if (CheckTable3() && space)
                               {
                                   // Szukam w kolejce pierwszej grupy 3 lub < os
                                   //  Log.Write("Szuka klientow 3os");
                                   _queueToTables.Search(3, out client, out var ok);
                                   if (ok)
                                   {
                                       _timeToTable += _sysTime - client.TimeToTableStart;
                                       _manager.Use(client);
                                        // Przekazanie klienta do managera

                                        AddEvent(2, _sysTime + _s, "ManagerEndOfService", 0, 0, client.ClientId);
                                        space = false;
                                   }
                               }

                               if (CheckTable4() && space)
                               {
                                   // Szukam w kolejce pierwszej grupy 4 lub < os
                                   // Log.Write("Szuka klientow 4os");
                                   _queueToTables.Search(4, out client, out var ok);
                                   if (ok)
                                   {
                                       _timeToTable += _sysTime - client.TimeToTableStart;
                                       _manager.Use(client);
                                        // Przekazanie klienta do managera

                                        AddEvent(2, _sysTime + _s, "ManagerEndOfService", 0, 0, client.ClientId);
                                        space = false;
                                   }
                               }
                           }
                       }
                   }



                   break;
                case 6:
                   if(_tryb){ Console.WriteLine("BuffetEndOfService");}

                   if (_log)
                   {
                       Log.Write("BuffetEndOfService");
                       Log.Write("");
                       Log.Write("aktualnie w bufecie" +_buffet.CurrentCount.ToString());
                   }

                    // Zwolij miejsce usun klienta z systemu
                   Log.Write("Id do zwolnienia" + _var1.ToString());
                    _buffet.End(_var1);
                    // Usuniecie klienta
                    if (CheckCashier())
                        // Jesli jest wolny kasjer
                    {
                        client = _buffet.RemoveFromBuffet(_var1);
                        _cashier[_freeCashier].GetClient(client);
                        // Przekazanie klienta do kasy
                        genNum = (int)_numberGeneratorCashierTime.Wyk(_lambdaP);
                        _dataTableGeneratorObslugaKasjera.Rows.Add(genNum);
                        AddEvent(7, _sysTime + genNum, "BarEndOfService", _freeCashier,0,client.ClientId);
                        // Zaplanuj zakonczenie uslugi
                    }
                    else
                    // Jesli zajety to klient do kolejki
                    {
                        
                        Log.Write("Dodanie klkienta do kolejki do kas");
                        _queueToCashier.Push(_buffet.RemoveFromBuffet(_var1));
                    }

                    // Jseli kolejka nie jest pusta
                    if (_log) { Log.Write("Kolejka do bufetu" + _queueToBuffet.count().ToString());}
                    if (!_queueToBuffet.Empty)
                        // Jesli sie zmiesci pierwsza grupa to wsaw                     
                    {
                        if (_log)
                        {
                            Log.Write("Kolejka nie jest pusta");
                            Log.Write("aktualnie w bufecie" + _buffet.CurrentCount.ToString());
                        }
                        client = _queueToBuffet.Peak();
                        if (_buffet.Add(client, client.ClientId))
                        {
                            if (_log) { Log.Write("Dodaj do bufetu");}
                            // Zaplanuj koniec
                            genNum = (int) _numberGeneratorBuffet.Gaussian(_qB, _sigmaB2);
                            _dataTableGeneratorCzasBuffet.Rows.Add(genNum);
                            AddEvent(6, _sysTime + genNum, "BuffetEndOfService",client.ClientId,0,client.ClientId);
                            _queueToBuffet.Pop();
                        } 
                    }
                    break;
                case 7:
                   if(_tryb){ Console.WriteLine("BarEndOfService");}

                   if (_log)
                   {
                       Log.Write("BarEndOfService");
                       Log.Write("");
                   }

                   _deleteClients++;
                    _clientsInRestaurant--;
                    // Zwolij miejsce usun klienta z systemu
                    _cashier[_var1].RemoveClient();
                    // Jesli kolejka nie jest pusta
                    if (!_queueToCashier.Empty)
                    {
                      if(_log){  Log.Write("BANG");}
                        client = _queueToCashier.Pop();
                        _cashier[_var1].GetClient(client);
                        // Zaplanuj dodanie grupy do stolika/bufetu
                        genNum = (int) _numberGeneratorCashierTime.Wyk(_lambdaP);
                        _dataTableGeneratorObslugaKasjera.Rows.Add(genNum);
                        AddEvent(7, _sysTime + genNum, "BarEndOfService",_var1,0,client.ClientId);
                    }
                   

                    break;
              
                    //ALARM
                case 8:

                    if (_log) { Log.Write("Wybucha alarm"); }

                    //Przewiduje kolejny alarm
                    if (_work)
                    {
                        genNum = (int)_numberGeneratorAlarm.Gaussian(_qAlarm, _sigmaAlarm);
                        AddEvent(8, _sysTime + genNum, "Alarm");
                    }

                    var clientId = 0;
                    //30% klientow usuwam + zadania dla nich
                    //Manager
                    if (!_manager.Status)
                    {
                        if (Evacuation())
                        {
                          
                            _manager.evacuation(ref clientId);
                            if (_log) { Log.Write("Alarm manager klient o id:" + clientId.ToString()); }
                            Cashier._help.Rows.Add(clientId.ToString());
                            DeleteEvent(clientId);

                        }
                    }
                    //Kolejka stoliki
                    if (!_queueToTables.Empty)
                    {
                        for (int i = 0; i < _queueToTables.count(); i++)
                        {
                            if (Evacuation())
                            {
                                _queueToTables.Evacuation(i,ref clientId);
                                if (_log) { Log.Write("warning do stolikow klient o id:" + clientId.ToString()); }
                                Cashier._help.Rows.Add(clientId.ToString());
                                DeleteEvent(clientId);
                            }
                        }
                    }
                    //kolejka kasjer
                    if (!_queueToCashier.Empty)
                    {
                        for (int i = 0; i < _queueToCashier.count(); i++)
                        {
                            if (Evacuation())
                            {
                                _queueToCashier.Evacuation(i,ref clientId);
                                if (_log) { Log.Write("warning do kas klient o id:" + clientId.ToString()); }
                                Cashier._help.Rows.Add(clientId.ToString());
                                DeleteEvent(clientId);
                            }
                        }
                    }
                    //kolejka buffet
                    if (!_queueToBuffet.Empty)
                    { 
                    for (int i = 0; i < _queueToBuffet.count(); i++)
                        {
                            if (Evacuation())
                            {
                                _queueToBuffet.Evacuation(i,ref clientId);
                                if (_log) { Log.Write("warning do bufetu klient o id:" + clientId.ToString()); }
                                Cashier._help.Rows.Add(clientId.ToString());
                                DeleteEvent(clientId);
                            }
                        }
                    }
                    //stoliki2
                    for(int i = 0; i < NumberOfTable2; i++)
                    {
                        if (!_table2[i].Status)
                        {
                            if (Evacuation())
                            {
                                _table2[i].Evacuation(ref clientId);
                                if (_log) { Log.Write("stolik 2 os klient o id:" + clientId.ToString()); }
                                Cashier._help.Rows.Add(clientId.ToString());
                                DeleteEvent(clientId);
                            }
                        }
                    }
                    //stoliki3
                    for (int i = 0; i < NumberOfTable3; i++)
                    {
                        if (!_table3[i].Status)
                        {
                            if (Evacuation())
                            {
                                _table3[i].Evacuation(ref clientId);
                                if (_log) { Log.Write("Stolik 3os klient o id:" + clientId.ToString()); }
                                Cashier._help.Rows.Add(clientId.ToString());
                                DeleteEvent(clientId);
                            }
                        }
                    }
                    //stoliki4
                    for (int i = 0; i < NumberOfTable4; i++)
                    {
                        if (!_table4[i].Status)
                        {
                            if (Evacuation())
                            {
                                _table4[i].Evacuation(ref clientId);
                                if (_log) { Log.Write("Stolik 4os klient o id:" + clientId.ToString()); }
                                Cashier._help.Rows.Add(clientId.ToString());
                                DeleteEvent(clientId);
                            }
                        }
                    }
                  //Kelner
                    //kasjer
                    for(int i = 0; i < NumberOfCashier; i++)
                    {
                        if (!_cashier[i].Status)
                        {
                            if (Evacuation())
                            {
                                _cashier[i].Evacuation(ref clientId);
                                if (_log) { Log.Write("kasa klient o id:" + clientId.ToString()); }
                                DeleteEvent(clientId);
                            }
                        }
                    }
                    //buffet
                    for (int i = 0; i < _buffet.GroupCount; i++)
                    {
                       
                            if (Evacuation())
                            {
                                _buffet.Evacuation(i,ref clientId);
                            if (_log) { Log.Write("buffet klient o id:" + clientId.ToString()); }
                            Cashier._help.Rows.Add(clientId.ToString());
                            DeleteEvent(clientId);
                            }
                        
                    }


                    break;
                default:
                    Console.WriteLine("Default case");
                    break;

            }
        }

        private bool Evacuation()
        {
            var loss = (int)_numberGeneratorEvacuation.Designate(0, 100);
            if (loss < 30)
            {
                _clientsInRestaurant--;
                _evacuationClients++;
                return true;
            }
            else
            {
                return false;
            }
        }
        

        private bool Table(int x)
        // Stolik (liczba os)
        {

            if (x == 1 || x==2)
            {
                if (CheckTable2())
                {
                    return true;
                }
                // Sprawdza stoliki 2 osobowe
                if (CheckTable3())
                {
                    return true;
                }
                // Sprawdza stoliki 3 osobowe
                if (CheckTable4())
                {
                    return true;
                }
                // Sprawdza stoliki 4 osobowe
            }
            else if (x == 3)
            {
                if (CheckTable3())
                {
                    return true;
                }
                // Sprawdza stoliki 3 osobowe
                if (CheckTable4())
                {
                    return true;
                }
                // Sprawdza stoliki 4 osobowe
            }
            else
            {
                if (CheckTable4())
                {
                    return true;
                }
                // Sprawdza stoliki 4 osobowe
            }


            return false;
        }

        private bool Table(Client client, out int mode, out int tableId)
        // Stolik (liczba os) + dodanie klienta do wolnego stolika
        {
            var x = client.Count;
            if (x == 1 || x == 2)
            {
                if (CheckTable2(client, out tableId))
                {
                    mode = 2;
                    return true;
                }
                // Sprawdza stoliki 2 osobowe
                if (CheckTable3(client,out tableId))
                {
                    mode = 3;
                    return true;
                }
                // Sprawdza stoliki 3 osobowe
                if (CheckTable4(client, out tableId))
                {
                    mode = 4;
                    return true;
                }
                // Sprawdza stoliki 4 osobowe
            }
            else if (x == 3)
            {
                if (CheckTable3(client, out tableId))
                {
                    mode = 3;
                    return true;
                }
                // Sprawdza stoliki 3 osobowe
                if (CheckTable4(client, out tableId))
                {
                    mode = 4;
                    return true;
                }
                // Sprawdza stoliki 4 osobowe
            }
            else
            {
                if (CheckTable4(client, out tableId))
                {
                    mode = 4;
                    return true;
                }
                // Sprawdza stoliki 4 osobowe
            }

            mode = 0;
            tableId = 0;
            return false;
        }

        private bool CheckTable2()
        //Sprawdzenie stolika 
        {
            for (var i = 0; i < _table2.Count; i++)
            {
                if (_table2[i].Status == true)
                {
                    return true;
                    
                }
            }

            return false;
        }

        private bool CheckTable2(Client client ,out int id)
        //Sprawdzenie stolika + dodanie klienta do stolika
        {
            for (var i = 0; i < _table2.Count; i++)
            {
                if (_table2[i].Status == true)
                {
                    _table2[i].AddClient(client);
                    id = i;
                    return true;
                    
                }
            }

            id = 0;
            return false;
        }

        private bool CheckTable3()
        //Sprawdzenie stolika 
        {
            for (var i = 0; i < _table3.Count; i++)
            {
                if (_table3[i].Status == true)
                {
                    return true;
                    
                }
            }

            return false;
        }

        private bool CheckTable3(Client client, out int id)
        //Sprawdzenie stolika + dodanie klienta do stolika
        {
            for (var i = 0; i < _table3.Count; i++)
            {
                if (_table3[i].Status == true)
                {
                    _table3[i].AddClient(client);
                    id = i;
                    return true;
                    
                }
            }

            id = 0;
            return false;
        }

        private bool CheckTable4()
        //Sprawdzenie stolika 
        {
            for (var i = 0; i < _table4.Count; i++)
            {
                if (_table4[i].Status == true)
                {
                    return true;
                    
                }
            }

            return false;
        }

        private bool CheckTable4(Client client,out int id)
        //Sprawdzenie stolika + dodanie klienta do stolika
        {
            for (var i = 0; i < _table4.Count; i++)
            {
                if (_table4[i].Status == true)
                {
                    _table4[i].AddClient(client);
                    id = i;
                    return true;
                    
                }
            }

            id = 0;
            return false;
        }

        private bool CheckCashier()
        // Sprawdzenie czy jest wolny kasjer
        {
            for (int i = 0; i < _cashier.Count; i++)
            {
                if (_cashier[i].Status)
                {
                    _freeCashier = i;
                    return true;
                }
            }

            return false;
        }

        private bool CheckWaiter()
            // Sprawdzenie czy jest wolny kasjer
        {
            for (int i = 0; i < _waiter.Count; i++)
            {
                if (_waiter[i].Status)
                {
                    _freeWaiter = i;
                    return true;
                }
            }

            return false;
        }


        private int ManagerCheckTable()
        {
            if (CheckTable2())
            {
                return 2;
            }
            if (CheckTable3())
            {
                return 3;
            }
            if (CheckTable4())
            {
                return 4;
            }

            return 0;

        }

        private void TimeTable(out int id,out int mode)
        //Stolik który najdłużej czeka id stolika / rodzaj stolika
        {
            var x = new DateTime();
            mode = 0;
            id = 0;
            bool c = true;

            for (int i = 0; i < _table2.Count; i++)
            {
                if (!_table2[i].Status && !_table2[i].Done() && !_table2[i].InTable)
                {
                    
                    if (c)
                    {
                        id = i;
                        mode = 2;
                        x = _table2[i].Start;
                        c = false;
                    }

                    if (x >_table2[i].Start)
                    {
                        mode = 2;
                        id = i;
                        x = _table2[i].Start;
                    }

                }
            }

           
            for (int i = 0; i < _table3.Count; i++)
            {
                if (!_table3[i].Status && !_table3[i].Done() && !_table3[i].InTable)
                {
                    if (c)
                    {
                        mode = 3;
                        id = i;
                        x = _table3[i].Start;
                        c = false;
                    }

                    if (x > _table3[i].Start)
                    {
                        mode = 3;
                        id = i;
                        x = _table3[i].Start;
                    }

                }
            }


            for (int i = 0; i < _table4.Count; i++)
            {
                if (!_table4[i].Status && !_table4[i].Done() && !_table4[i].InTable)
                {
                    if (c)
                    {
                        mode = 4;
                        id = i;
                        x = _table4[i].Start;
                        c = false;
                    }

                    if (x > _table4[i].Start)
                    {
                        mode = 4;
                        id = i;
                        x = _table4[i].Start;
                    }

                }
            }

            if (mode == 2)
            {
                _table2[id].InTable = true;
            }

            if (mode == 3)
            {
                _table3[id].InTable = true;
            }

            if (mode == 4)
            {
                _table4[id].InTable = true;
            }
            // Dodac jakas zmienna do tego stolika zeby go juz nie wybierac
        }




        // Eksport do excel

        private void Export(DataTable table, string name)
        {
          
            var saveFileDialog = "Dane.xlsx";
      
            {
                

                CreateFile(table, name, (System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)) + "\\Szablon.xlsx");

            }
        }

        private void CreateFile(DataTable reportdata, string fileName, string excelTemplate)
        {
            var data = new ExelData
            {
                ReportData = reportdata
            };

            new ExcelWriter(excelTemplate).Export(data, fileName);
        }
    }
}
