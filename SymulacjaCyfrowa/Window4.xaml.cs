using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OxyPlot;
using Microsoft.Win32;
using System.Data;

namespace DigitalSimulation
{
    /// <summary>
    /// Logika interakcji dla klasy Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
//Deklaracja obiektu

        public Window4()
        {
            InitializeComponent();
            //W konstruktorze
            this.Plot1.Model = new OxyPlot.PlotModel();

            draw();
         //   beforeDraw();
            PodajDaneDoWykresu();


        }

        private bool dysty = false;
        public Window4(int c)
        {
            InitializeComponent();
            dysty = true;
            //W konstruktorze
            this.Plot1.Model = new OxyPlot.PlotModel();
            beforeDraw();
            PodajDaneDoWykresu();

        }

     

        OxyPlot.Series.LineSeries[] punktySerii= new OxyPlot.Series.LineSeries[1];  //zakres do - zakres od
        private DataTable Dystrybuanta = new DataTable();

        int liczba_probek = 1000;
        private double[] values;
    


        private void draw()
        {
            NumberGenerator gen = new NumberGenerator();
       
            values = new double[10]; //os y

            for (var j = 0; j < liczba_probek; j++) // os x
            {
               
               var x = gen.Wyk(0.3, out double c);//trend;
           //    double x = gen.Gaussian(0.5,0.2);
                if (x < 0.1)
                {
                    values[0]++;
                }
                else if (x < 0.2)
                {
                    values[1]++;
                }
                else if (x < 0.3)
                {
                    values[2]++;
                }
                else if (x < 0.4)
                {
                    values[3]++;
                }
                else if (x < 0.5)
                {
                    values[4]++;
                }
                else if (x < 0.6)
                {
                    values[5]++;
                }
                else if (x < 0.7)
                {
                    values[6]++;
                }
                else if (x < 0.8)
                {
                    values[7]++;
                }
                else if (x < 0.9)
                {
                    values[8]++;
                }
                else if (x < 1)
                {
                    values[9]++;
                }
            }
        }

        private void beforeDraw()
        {
            Dystrybuanta.Columns.Add("Wylosowana", typeof(int));
            Dystrybuanta.Columns.Add("Ile", typeof(int));
            NumberGenerator gen = new NumberGenerator();

 
            values = new double[liczba_probek]; //os y

            for (var j = 0; j < liczba_probek; j++) // os x
            {

                values[j] = gen.Wyk(400,out double x);//trend;
            }


            Dystrybuanta.Clear();


            int DystrybuantaRows = 0;
            int prevSmallestNumber = 0;
            var prevSmallestNumberCount = 1;
            bool frst = true;
            for (int i = 0; i < liczba_probek; i++)
                //dodawanie liczb do wykresu od najmniejszej do najwiekszej
            {
                var smallestNumber = values[0];
                var numberToDell = 0;
                bool check = true;

                for (int x = 0; x < values.Length; x++)
                {

                    if (smallestNumber > values[x])
                    {
                        smallestNumber = values[x];
                        numberToDell = x;
                    }
                }

                if (check)
                {
                    values[numberToDell] = 9999999;
                }
                if (prevSmallestNumber == smallestNumber && !frst)
                    // kolejne wystapienia
                {
                    prevSmallestNumberCount++;
                    Dystrybuanta.Rows[DystrybuantaRows - 1]["ILE"] = prevSmallestNumberCount;
                }
                else
                {
                    frst = false;
                    Dystrybuanta.Rows.Add(smallestNumber, 1);
                    prevSmallestNumber = (int)smallestNumber;
                    prevSmallestNumberCount = 1;
                    DystrybuantaRows++;
                }


            }
        }

        public void PodajDaneDoWykresu()//Lista X i Y podana jako parametr metody
        {
            

            this.Plot1.Model = new OxyPlot.PlotModel();
            //Usunięcie ustawionych parametrów z poprzedniego uruchomienia metody
            this.Plot1.Model.Series = new System.Collections.ObjectModel.Collection<OxyPlot.Series.Series> { };
            this.Plot1.Model.Axes = new System.Collections.ObjectModel.Collection<OxyPlot.Axes.Axis> { };

            punktySerii = new OxyPlot.Series.LineSeries[1];  //zakres do - zakres od

            //Graficzne ustawienia wykresów
            for (int i = 0; i < (1); i++)
            {
             
                punktySerii[i] = new OxyPlot.Series.LineSeries
                {
                    MarkerType = ksztaltPunktowWykresu[4], //oznaczenie punktów - definicja poniżej
                    MarkerSize = 4, //wielkość punktów
                    MarkerStroke = koloryWykresow[3], //Kolor linii wykresu - definicja poniżej
                    Title = "Seria nr: " + (i).ToString() //tytuł serii
                };
            }
   
            //Uzupełnianie danych

            // pierwszy
            {
                List<double> listaXX = new List<double>();

                List<double> listaYY = new List<double>();

                if (dysty)
                {
                    for (int i = 0; i < Dystrybuanta.Rows.Count; i++)
                    {
                        listaXX.Add(int.Parse(Dystrybuanta.Rows[i]["Wylosowana"].ToString()));
                        listaYY.Add(int.Parse(Dystrybuanta.Rows[i]["Ile"].ToString()));
                    }
                }
                else
                {
                    var b = 0.1;
                    listaYY.Add(values[0]);
                    listaXX.Add(0);
                    for (int i = 0; i < values.Length-1; i++)
                    {
                      
                        listaYY.Add(values[i]);
                        listaXX.Add(b);
                        listaYY.Add(values[i+1]);
                        listaXX.Add(b);
                        b += 0.1;
                    }
                    listaYY.Add(values[9]);
                    listaXX.Add(1);


                }
             
              

                for (int n = 0; n < listaXX.Count; n++)
                    punktySerii[0].Points.Add(new OxyPlot.DataPoint(listaXX[n], listaYY[n]));//dodanie wszystkich serii do wykresu

                this.Plot1.Model.Series.Add(punktySerii[0]);
            }

 

            //Opis i parametry osi wykresu
            var xAxis = new OxyPlot.Axes.LinearAxis(OxyPlot.Axes.AxisPosition.Bottom, "X") { MajorGridlineStyle = OxyPlot.LineStyle.Solid, MinorGridlineStyle = OxyPlot.LineStyle.Dot };
            this.Plot1.Model.Axes.Add(xAxis);
            var yAxis = new OxyPlot.Axes.LinearAxis(OxyPlot.Axes.AxisPosition.Left, "Y") { MajorGridlineStyle = OxyPlot.LineStyle.Solid, MinorGridlineStyle = OxyPlot.LineStyle.Dot };
            this.Plot1.Model.Axes.Add(yAxis);
            
        }


  
        //Wypisane po to, by zmieniać kolor i kształt wraz z numerem klasy
        private readonly List<OxyPlot.OxyColor> koloryWykresow = new List<OxyPlot.OxyColor>
                                    {
                                        OxyPlot.OxyColors.Green,
                                        OxyPlot.OxyColors.IndianRed,
                                        OxyPlot.OxyColors.Coral,
                                        OxyPlot.OxyColors.Chartreuse,
                                        OxyPlot.OxyColors.Peru
                                    };
        private readonly List<OxyPlot.MarkerType> ksztaltPunktowWykresu = new List<OxyPlot.MarkerType>
                                            {
                                                OxyPlot.MarkerType.Plus,
                                                OxyPlot.MarkerType.Star,
                                                OxyPlot.MarkerType.Cross,
                                                OxyPlot.MarkerType.Custom,
                                                OxyPlot.MarkerType.Square
                                            };

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }




}
