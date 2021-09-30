using System;

namespace CreditApp
{
    class Program
    {
        
        public static string connectionString = @"Data Source=localhost;Initial Catalog=CreditApp;Integrated Security=True";


        
        static void Main(string[] args)
        {


            char q = '0';
           
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[q] - Выход");
                Console.WriteLine("[Enter] - Продолжить");
                q = ((char)Console.ReadKey().Key);
               
                while (q != '\r')
                {
                    if (q == 'Q')
                        return;
                    q = ((char)Console.ReadKey().Key);
                }

                
                UserManager um = new UserManager();

                
                UserRole role = um.CheckUser();
                
                if (role == UserRole.Guest)
                {
                    Console.WriteLine("Неверные данные учетной записи");
                    Console.ReadKey();
                }
                
                else if (role == UserRole.Admin)
                {
                    
                    Menu adminMenu = new Menu("Меню администратора");
                    adminMenu.Add("Создать пользователя", delegate
                    {
                        UserManager.CreateUser();

                        ;
                    });
                    adminMenu.Add("Заполнить анкету", delegate
                    {
                        Quiz qz = new Quiz();
                        qz.Start();

                    });
                    adminMenu.Add("Выход", delegate
                    {
                        adminMenu.Stop = true;
                    });
                    adminMenu.Draw();
                }
                
                else if (role == UserRole.Client)
                {
                    

                    Menu clientMenu = new Menu("Меню клиента");
                    clientMenu.Add("Мои заявки", delegate
                    {
                        Quiz.GetUserApplications(um.userName);
                        
                    });
                    clientMenu.Add("График платежей", delegate
                    {
                        Quiz.GetPayments();
                    });
                    clientMenu.Add("Остаток по кредитам", delegate
                    {
                        Quiz.GetRemaining(um.userName);              
                    });
                    clientMenu.Add("Выход", delegate
                    {
                        clientMenu.Stop = true;
                    });
                    clientMenu.Draw();
                }


            }

        }
    }
}
