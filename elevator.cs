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
        // Это класс лифта и всего, что характеризует его поведение
        // Он представляет собой объект лифт со свойствами, которые задает пользователь
        
            
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
        private static bool firstTimeflag = true; //флаг, вызываем ли в первый раз лифт
        private string Answer = "";//переменная для хранения ответа пользователя
        Random rnd1 = new Random();
        Timer timerClose = new Timer(1000);//таймер для открытия закрытия дверей
        Timer timerToGo = new Timer(1000);//таймер, который считает время, которое лифт едет по этажам
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
        /// <summary>
        /// процедура OnTimerOpenClose
        /// Обрабатывает событие таймера timerClose 
        /// и отсчитывает время, в течение которого лифт открыт
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public static void OnTimerOpenClose(Object source, ElapsedEventArgs e)
        {
            if (totalClose > 0)
            {
                isOpen = true;

                totalClose -= 1000;
            }
        }


        private static void ElevatorRidesTheFloor()
        {

        }
       
        /// <summary>
        /// процедура TimerInitialization
        /// Инициализирует методы, которые будут при 
        /// прохождении заданного интервала времени
        /// </summary>
        private void TimerInitialization()
        {
            timerToGo.Elapsed += new ElapsedEventHandler(OnTimerToGo);//метод, который считает высоту лифта и этаж
            timerClose.Elapsed += new ElapsedEventHandler(OnTimerOpenClose); // метод, который считает время до закрытия дверей
        }

       /// <summary>
       /// Процедура ChangeFloor
       /// Запускает, а затем останавливает таймер,
       /// который служит для вычисления высоты лифта 
       /// при движении. 
       /// Когда лифт приехал, таймер останавливается.
       /// </summary>
        private void ChangeFloor()
        {
           
            while ((Callflag == true && Floor != targetFloor))
            {
                if (isOpen == true && ElapsedOpenClose < totalClose && firstTimeflag == true) //Если вызвали лифт в первый раз
                {
                    timerClose.Start();

                    Console.WriteLine("Лифт поехал");
                    firstTimeflag = false;

                }

                if (isOpen == false && ElapsedOpenClose < totalClose && firstTimeflag == true)
                {
                    timerToGo.Start();
                    Console.WriteLine("Лифт поехал");

                    firstTimeflag = false;

                }
                if (Floor == targetFloor && Answer != "Go")
                {

                    isOnFloor = true;
                    timerToGo.Stop();

                    Console.WriteLine("Лифт приехал");
                }
                
                if (isOpen == false && Answer == "Go") //Если пользователь внутри лифта, ответ Answer равен "Go"
                {
                    if (timerToGo.Enabled == false)
                    {
                        Console.WriteLine("Лифт поехал");
                    }
                    timerToGo.Start();

                }
                if (Floor == targetFloor && Answer == "Go")
                {


                    isOnFloor = true;
                    timerToGo.Stop();

                    Console.WriteLine("Лифт приехал");

                }
            }

        }

        /// <summary>
        /// процедура OnTimerToGo
        /// Обрабатывает событие таймера timerToGo
        /// Увеличивает или уменьшает высоту лифта на заданное значение,
        /// вычисляет текущий этаж и выводит сообщение о том, какой этаж проезжает лифт
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public static void OnTimerToGo(Object source, ElapsedEventArgs e)
        {

            Console.WriteLine("Лифт движется");
            if (Floor > targetFloor)
            {
                elevatorHeight -= elevatorSpeed;
                Floor = Convert.ToInt32(Math.Round(elevatorHeight / heightOfFloor));
                Console.WriteLine("Лифт проезжает {0} этаж", Floor);
            }
            else if (Floor < targetFloor)
            {
                elevatorHeight += elevatorSpeed;
                Floor = Convert.ToInt32(Math.Round(elevatorHeight / heightOfFloor));
                Console.WriteLine("Лифт проезжает {0} этаж", Floor);
            }
        }
        /// <summary>
        /// Метод ElevatorRunning 
        /// Запускает цикл работы лифта
        /// </summary>
        public void ElevatorRunning()
        {

            Random rnd1 = new Random();


            Floor = rnd1.Next(1, N + 1); //случайный этаж лифта.

            Console.WriteLine("Случайный этаж лифта {0}", Floor);

            double startPoint = Floor * heightOfFloor; // высота положения лифта в метрах
            elevatorHeight = startPoint;//высота лифта вначале
            userHeight = (userFloor - 1) * heightOfFloor; //высота, на которой пользователь


            TimerInitialization();


            
            totalClose = openCloseTime * 1000;

            targetFloor = userFloor;
            string Call = "";
            while (Call != "Quit")
            {
                if (firstTimeflag == true) // если в первый раз вызываем лифт, то находимся на первом этаже
                {
                    Console.WriteLine("Вы находитесь на первом этаже"); 
                }

                    Console.WriteLine("Чтобы вызвать лифт, введите слово Call");
                    Console.WriteLine("Чтобы завершить работу программы, введите Exit");
                    Call = Console.ReadLine();
                
            
                if (Call == "Exit") // Если ответ пользователя "Exit", выходим из цикла
                {
                    break;
                }
                if (Call == "Call") //Если ответ пользователя "Call", вызываем лифт на этаж
                {
                    flagEscape = false;
                    ChangeFloor();
                }
                Answer = "";
                //лифт приехал.
                string Answer1 = "";
                if (flagEscape == false) //Если флаг выхода flagEscape = false
                {
                    do
                    {
                       
                        Callflag = true; 
                        timerClose.Start(); //Когда лифт приехал, открываем двери и запускаем таймер отсчета времени до закрытия дверей

                        if (totalClose > 0)
                        {

                            
                            isOpen = true;//Если флаг isOpen=true, двери открылись
                        }



                        if (isOpen == true && Answer != "Y" && timerClose.Enabled == true) 
                        {
                           
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
                        if (timerClose.Enabled == false) // Если пользователь едет внутри лифта, спрашиваем, хочет ли он выйти, когда лифт остановился
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
                            
                            do
                            {
                                Callflag = true;
                                enterTheElevatorFlag = false;
                                isCalledCarried = false;
                                totalClose = openCloseTime * 1000;
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

                                    

                                    if (totalClose <= 0)
                                    {
                                        timerClose.Stop();
                                    }

                                    Console.WriteLine("Двери закрылись");

                                    Console.WriteLine("Вы на этаже {0}", Floor);
                                    enterTheElevatorFlag = false;
                                    isCalledCarried = false; //Если isCalledCarried = false, значит ехали в лифте, иначе вызвали лифт


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
                            string s = "";
                            do
                            {


                                Console.WriteLine("Введите требуемый этаж");
                                s = Console.ReadLine();

                                if (Int32.TryParse(s, out d))
                                {
                                    targetFloor = d;
                                }

                            } while (targetFloor < 1 || targetFloor > N || Int32.TryParse(s, out d) == false);
                            Callflag = true;

                            ChangeFloor();
                         
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
                while ((t.NumberOfFloors < 5 || t.NumberOfFloors > 20) || Int32.TryParse(s.ToString(), out a) == false)
                {
                    Console.WriteLine("введите число этажей N в подъезде (Целое от 5 до 20)");
                    s = Console.ReadLine();
                    if (Int32.TryParse(s.ToString(), out a) == true)
                    {
                        t.NumberOfFloors = Int32.Parse(s);
                    }
                }
                s = "";

                while (Double.TryParse(s.ToString(), out b) == false)
                {
                    Console.WriteLine("Введите высоту этажа (м)");
                    s = Console.ReadLine();
                    if (Double.TryParse(s.ToString(), out b))
                    {
                        t.HeightOfFloor = Double.Parse(s);
                    }

                }
                s = "";

                while (Double.TryParse(s.ToString(), out b) == false)
                {
                    Console.WriteLine("Введите скорость движения лифта (м/с)");
                    s = Console.ReadLine();
                    if (Double.TryParse(s, out b) == true)
                    {
                        t.SpeedOfElevator = Double.Parse(s);
                    }
                }

                s = "";
                while (Double.TryParse(s.ToString(), out b) == false)
                {
                    Console.WriteLine("Введите время между открытием и закрытием дверей лифта (c)");
                    s = Console.ReadLine();
                    if (Double.TryParse(s, out b) == true)
                    {
                        t.OpenCloseTime = Double.Parse(s);
                    }

                }


                t.ElevatorRunning();

            }
        }
    }
}
