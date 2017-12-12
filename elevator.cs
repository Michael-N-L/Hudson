using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApp2
{
    class Elevator
    {

        private static int targetFloor = 0;//этаж, который вызывают из лифта
        private static bool isOpen = false;//открыты ли двери
        private static bool isOnFloor = false; //прибыл ли лифт 
        private static double ElapsedOpenClose = 0;//время для подсчета, закрыты ли двери
        private static double totalClose;//время для закрытия
        private static int Floor;  //случайный этаж лифта.
        private static bool isCalledCarried = true;//true вызвали лифт false ехали в лифте
        private static int userFloor = 1;//этаж пользователя, равен 1 по умолчанию
        private static double elevatorHeight; //высота положения лифта
        private static double userHeight;// высота положения пользователя
        private static int N; //число этажей в подъезде
        private static double heightOfFloor; //высота одного этажа
        private static double elevatorSpeed; //скорость лифта
        private static double openCloseTime; //время между открытием и закрытием
        private static bool Callflag = true;//флаг вызова лифта
        private static bool enterTheElevatorFlag = false; //вошел ли пользователь в лифт
        private static bool flagEscape = false; //флаг выхода false внутри
        Random rnd1 = new Random();
        public int NumberOfFloors
        {
            get { return N; }
            set
            {
                N = Convert.ToInt32(value);
            }
        }
        public double HeightOfFloor
        {
            get { return heightOfFloor; }
            set
            {
                heightOfFloor = value;
            }
        }
        public double SpeedOfElevator
        {
            get { return elevatorSpeed; }
            set
            {
                elevatorSpeed = value;
            }
        }
        public double OpenCloseTime
        {
            get { return openCloseTime; }
            set
            {
                openCloseTime = value;
            }
        }
        public static void OnTimerOpenClose(Object source, ElapsedEventArgs e)
        {
            if (totalClose > 0)
            {
                isOpen = true;

                totalClose -= 1000;
            }
        }





        public static void OnTimerToGo(Object source, ElapsedEventArgs e)
        {

            Console.WriteLine("Лифт движется");
            if (Floor > targetFloor)
            {
                elevatorHeight -= elevatorSpeed;
                Floor = Convert.ToInt32(Math.Round(elevatorHeight / heightOfFloor));
                Console.WriteLine("Лифт движется {0}", Floor);
            }
            else if (Floor < targetFloor)
            {
                elevatorHeight += elevatorSpeed;
                Floor = Convert.ToInt32(Math.Round(elevatorHeight / heightOfFloor));
                Console.WriteLine("Лифт движется {0}", Floor);
            }
        }

        public void ElevatorRunning()
        {

            Random rnd1 = new Random();
            

            Floor = rnd1.Next(1, N + 1); //случайный этаж лифта.

            Console.WriteLine("Случайный этаж лифта {0}", Floor);
            
            double startPoint = Floor * heightOfFloor; // высота положения лифта в метрах
            elevatorHeight = startPoint;//высота лифта вначале
            userHeight = (userFloor - 1) * heightOfFloor;


            Timer timerClose = new Timer(1000);//таймер для открытия закрытия дверей
            Timer timerToGo = new Timer(1000);//таймер, который считает время, которое лифт едет по этажам
            timerToGo.Elapsed += new ElapsedEventHandler(OnTimerToGo);
            //anchor1 timerclose.elapsed
            timerClose.Elapsed += new ElapsedEventHandler(OnTimerOpenClose);


            bool firstTimeflag = true; //поменять на false
            totalClose = openCloseTime * 1000;

            targetFloor = userFloor;
            string Call = "";
            while (Call != "Quit")
            {
                if (firstTimeflag == true)
                {
                    Console.WriteLine("Вы находитесь на первом этаже");
                }
                

                Console.WriteLine("Чтобы вызвать лифт, введите слово Call");
                Console.WriteLine("Чтобы завершить работу программы, введите Exit");
                Call = Console.ReadLine();
                if (Call == "Exit")
                {
                    break;
                }
                if (Call == "Call")
                {
                    flagEscape = false;
                    while ((Callflag == true && Floor != targetFloor))
                    {
                        if (isOpen == true && ElapsedOpenClose < totalClose && firstTimeflag == true)
                        {
                            timerClose.Start();

                            Console.WriteLine("Лифт поехал");
                            firstTimeflag = false;

                        }

                        if (isOpen == false && ElapsedOpenClose < totalClose && firstTimeflag == true)
                        {
                            timerToGo.Start();

                            firstTimeflag = false;
                            //Console.ReadKey();
                        }
                        if (Floor == userFloor)
                        {

                            isOnFloor = true;
                            timerToGo.Stop();

                            Console.WriteLine("Лифт приехал");

                        }



                    }
                }
                string Answer = "";
                //лифт приехал.
                string Answer1 = "";
                if (flagEscape == false)
                {
                    do
                    {
                        if (flagEscape == true)
                        {
                            Console.WriteLine("Escape");
                            break;
                        }

                        Callflag = true;
                        timerClose.Start();

                        if (totalClose > 0)
                        {

                            //Console.WriteLine("Двери открылись");
                            isOpen = true;
                        }



                        if (isOpen == true && Answer != "Y" && timerClose.Enabled == true)
                        {
                            //                    Console.WriteLine("Войти в лифт?");
                            Console.WriteLine("Двери открылись");
                            Console.WriteLine("Войти в лифт?(Y/N)");
                            Answer = Console.ReadLine();
                        }

                        if (totalClose <= 0)
                        {
                            timerClose.Stop();
                            Console.WriteLine("Двери закрылись");

                            isOpen = false;
                            break;
                        }
        



                    } while (ElapsedOpenClose <= 2 * totalClose);
                }
                Answer1 = Answer;
                totalClose = 1000 * openCloseTime;
                if (Answer1 == "Y")
                {
                    while (Answer != "Go" || Answer != "Quit")
                    {
                        if (timerClose.Enabled == false)
                        {
                            Console.WriteLine("Выйти или выбрать этаж?(Quit/Go)");
                        }
                        else
                        {
                            timerClose.Enabled = false;
                        }
                        if (flagEscape == true)
                        {
                            break;
                        }

                        Answer = "";
                        string var = "";
                        Answer = Console.ReadLine();
                        if (Answer == "Quit")
                        {
                            //Answer = "";
                            do
                            {
                                Callflag = true;
                                enterTheElevatorFlag = false;
                                isCalledCarried = false;
                                totalClose = openCloseTime * 1000;//anchor2
                                timerClose.Start();
                                Console.WriteLine("Двери открылись");
                                isOpen = true;
                                if (isOpen == true && Answer != "Y" && timerClose.Enabled == true)
                                {

                                    Console.WriteLine("Выйти из лифта (Quit)?");
                                    Answer = Console.ReadLine();
                                }

                                if (totalClose <= 0)
                                {
                                    timerClose.Stop();
                                    isOpen = false;
                                    Console.WriteLine("Двери закрылись");
                                    break;
                                }

                                if ((Answer == "Quit" && isOpen == true))
                                {

                                    //true вызвали лифт а не поехали в нем false - поехали

                                    if (totalClose <= 0)
                                    {
                                        timerClose.Stop();
                                    }

                                    Console.WriteLine("Двери закрылись");

                                    Console.WriteLine("Вы на этаже {0}", Floor);
                                    enterTheElevatorFlag = false;
                                    isCalledCarried = false;//ехали в лифте


                                    var = "Quit";
                                    flagEscape = true;
                                    Answer = "";
                                    break;
                                }

                            } while (var != "Quit");

                        }
                        else if (Answer == "Go")
                        {

                            isCalledCarried = true;
                            int d;
                            string s="";
                            do
                            {
                               
                               
                                Console.WriteLine("Введите требуемый этаж");
                                s = Console.ReadLine();

                                if(Int32.TryParse(s,out d))
                                {
                                    targetFloor = d;
                                }
                                
                            } while (targetFloor < 1 || targetFloor > N||Int32.TryParse(s,out d)==false);
                            Callflag = true;


                            while (Callflag == true && targetFloor != Floor)
                            {


                                if (isOpen == false && Answer == "Go")
                                {
                                    if (timerToGo.Enabled == false)
                                    {
                                        Console.WriteLine("Лифт поехал");
                                    }
                                    timerToGo.Start();

                                }
                                if (Floor == targetFloor)
                                {


                                    isOnFloor = true;
                                    timerToGo.Stop();

                                    Console.WriteLine("Лифт приехал");

                                }



                            }
                        }

                        if (var == "Quit")
                        {
                            break;
                        }
                    }
                }
                firstTimeflag = false;
            }


        }
        class Program
        {
            static void Main(string[] args)
            {
                Elevator t = new Elevator();

                int a;
                double b;
                string s = "";
                while ((t.NumberOfFloors < 5 || t.NumberOfFloors > 20)|| Int32.TryParse(s.ToString(),out a)==false)
                    {
                    Console.WriteLine("введите число этажей N в подъезде (Целое от 5 до 20)");
                    s = Console.ReadLine();
                    if (Int32.TryParse(s.ToString(), out a) ==true )
                    {
                        t.NumberOfFloors = Int32.Parse(s);
                    }
                    }
                s = "";

                    while (Double.TryParse(s.ToString(), out b) == false)
                    {
                        Console.WriteLine("Введите высоту этажа (м)");
                    s = Console.ReadLine();
                    if(Double.TryParse(s.ToString(),out b))
                    {
                        t.HeightOfFloor = Double.Parse(s);
                    }
                    
                    }
                s = "";

                    while (Double.TryParse(s.ToString(), out b) ==false)
                    {
                        Console.WriteLine("Введите скорость движения лифта (м/с)");
                    s = Console.ReadLine();
                    if(Double.TryParse(s,out b)==true)
                    {
                        t.SpeedOfElevator = Double.Parse(s);
                    }
                    }

                s = "";
                    while (Double.TryParse(s.ToString(), out b) ==false)
                    {
                        Console.WriteLine("Введите время между открытием и закрытием дверей лифта (c)");
                    s = Console.ReadLine();
                    if(Double.TryParse(s, out b) == true)
                    {
                        t.OpenCloseTime = Double.Parse(s);
                    }

                    }

                
                t.ElevatorRunning();

            }
        }
    }
}
