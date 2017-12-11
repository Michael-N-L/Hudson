using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace elevator
{
    class Program
    {
        static int targetFloor = 0;//этаж, который вызывают из лифта
        static bool isOpen = false;//открыты ли двери
        static bool isReady = false;//готов ли лифт
        static bool isOnFloor = false; //прибыл ли лифт 
        static double ElapsedOpenClose = 0;//время для подсчета, закрыты ли двери
        static double totalClose;//время для закрытия
        static int Floor;  //случайный этаж лифта.
        static bool isCalledCarried = true;//true вызвали лифт false ехали в лифте
        static int userFloor = 1;//этаж пользователя, равен 1 по умолчанию
        static double elevatorHeight; //высота положения лифта
        static double userHeight;// высота положения пользователя
        static int N; //число этажей в подъезде
        static double heightOfFloor; //высота одного этажа
        static double elevatorSpeed; //скорость лифта
        static double openCloseTime; //время между открытием и закрытием
        static bool Callflag = true;//флаг вызова лифта
        static bool enterTheElevatorFlag = false; //вошел ли пользователь в лифт
        static void Main(string[] args)
        {

            Random rnd1 = new Random();
            Console.WriteLine("введите число этажей N в подъезде");
            N = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите высоту этажа");
            heightOfFloor = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите скорость движения лифта");
            elevatorSpeed = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите время между открытием и закрытием дверей лифта");
            openCloseTime = double.Parse(Console.ReadLine());

            Floor = rnd1.Next(1, N + 1); //случайный этаж лифта.

            Console.WriteLine("random {0}", Floor);
            Console.ReadLine();
            double startPoint = Floor * heightOfFloor; // высота положения лифта в метрах
            elevatorHeight = startPoint;//высота лифта вначале
            userHeight = (userFloor - 1) * heightOfFloor;


            Timer timerClose = new Timer(1000 * openCloseTime);//таймер для открытия закрытия дверей
            Timer timerToGo = new Timer(1000);//таймер, который считает время, которое лифт едет по этажам
            timerToGo.Elapsed += new ElapsedEventHandler(OnTimerToGo);
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
                Console.WriteLine("Чтобы завершить работу программы, введите Quit");
                Call = Console.ReadLine();
                if (Call == "Quit")
                {
                    break;
                }
                if (Call == "Call")
                {
                    while ((Callflag == true && Floor != targetFloor))
                    {
                        if (isOpen == true && ElapsedOpenClose < totalClose && firstTimeflag == true)
                        {
                            timerClose.Start();

                            //Console.Write("Press any key to exit... ");
                            //Console.WriteLine("Go, Baby, go");
                            Console.WriteLine("Timer started");
                            firstTimeflag = false;
                            //Console.ReadKey();
                        }
                        /* if(ElapsedOpenClose>=totalClose)
                         {
                             isOpen = false;
                             timerClose.Stop();
                             timerToGo.Start();
                             //Console.WriteLine("Timer Stoped");
                         }*/
                        if (isOpen == false && ElapsedOpenClose < totalClose && firstTimeflag == true)
                        {
                            timerToGo.Start();
                            // timerClose.Start();

                            //Console.Write("Press any key to exit... ");
                            //Console.WriteLine("Go, Baby, go");
                            Console.WriteLine("Timer started");
                            firstTimeflag = false;
                            //Console.ReadKey();
                        }
                        if (Floor == userFloor)
                        {
                            //isOnFloor = true;
                            //timerToGo.Stop();
                            //Console.WriteLine("Лифт приехал");

                            isOnFloor = true;
                            timerToGo.Stop();

                            Console.WriteLine("Лифт приехал");

                        }



                    }
                }
                string Answer = "";
                //лифт приехал.

                do
                {
                    Callflag = true;

                    timerClose.Start();

                    if (isOpen == true && Answer != "Y")
                    {
                        //                    Console.WriteLine("Войти в лифт?");
                        Answer = Console.ReadLine();
                    }
                    if ((Answer == "Y" && isOpen == false))//(ElapsedOpenClose>=2*totalClose&&isReady==true)
                    {
                        isCalledCarried = true;//true вызвали лифт а не поехали в нем false - поехали
                        timerClose.Stop();
                        enterTheElevatorFlag = true;
                        Console.WriteLine("Timer Stoped");
                        break;
                    }
                    if (Callflag == false)
                    {

                        timerClose.Stop();

                        Console.WriteLine("Timer Stoped2");
                        break;
                    }


                } while (ElapsedOpenClose <= 2 * totalClose);

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
                            timerClose.Start();


                            if ((Answer == "Quit" && isOpen == true))//(ElapsedOpenClose>=2*totalClose&&isReady==true)
                            {

                                //true вызвали лифт а не поехали в нем false - поехали
                                timerClose.Stop();
                                Console.WriteLine("Timer Stoped out elevator");
                                Console.WriteLine("Вы на этаже {0}", Floor);
                                var = "Quit";
                                break;
                            }


                            // while ( Answer!="Quit" enterTheElevatorFlag = false);
                        } while (var != "Quit");

                    }
                    else if (Answer == "Go")
                    {
                        Console.WriteLine("Go thread");
                        isCalledCarried = true;
                        do
                        {
                            Console.WriteLine("Введите требуемый этаж");
                            targetFloor = Int32.Parse(Console.ReadLine());
                        } while (targetFloor < 1 || targetFloor > N);
                        Callflag = true;


                        while (Callflag == true && targetFloor != Floor)
                        {


                            if (isOpen == false && Answer == "Go")
                            {
                                if (timerToGo.Enabled == false)
                                {
                                    Console.WriteLine("Timer started");
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


        }

        public static void OnTimerOpenClose(Object source, ElapsedEventArgs e)
        {

            if (isOpen == false && enterTheElevatorFlag == false && Callflag == true)
            {
                Console.WriteLine("двери открылись");
                if (isCalledCarried == true)
                {
                    Console.WriteLine("Войти в лифт?(Y/N)");
                }
                else if (isCalledCarried == false)
                {
                    Console.WriteLine("Выйти из лифта?");
                }
                isOpen = true;




            }
            else if (isOpen == true && enterTheElevatorFlag == false && Callflag == true)
            {
                Console.WriteLine("двери закрылись");
                isOpen = false;
                Callflag = false;
                //Callflag = false;//флаг вызова на этаж
                return;

            }

            ElapsedOpenClose += totalClose;


        }
        public static void OnTimerToGo(Object source, ElapsedEventArgs e)
        {
            //Console.WriteLine("DateTime: " + DateTime.Now);
            //System.Timers.Timer theTimer = (System.Timers.Timer)source;
            //ElapsedOpenClose += 1000;
            //theTimer.Interval += 1000;
            //theTimer.Enabled = true;
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
    }
}
