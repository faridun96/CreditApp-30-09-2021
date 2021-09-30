using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CreditApp
{
    
    enum UserRole { Guest = 0, Client = 1, Admin = 2 }

   
    class UserManager
    {
        
        public string userName = "";

        
        public static bool CreateUser()
        {
            
            Console.Clear();
            Console.WriteLine("Номер телефона");
            string user_name = Console.ReadLine();
           
            while (!Regex.IsMatch(user_name, @"^[0-9]+$"))
            {
                Console.WriteLine("Номер телефона должен состоять только из цифр. Номер телефона");
                user_name = Console.ReadLine();
            }

            Console.WriteLine("Пароль");
            string psw = Console.ReadLine();
           
            while (String.IsNullOrWhiteSpace(psw))
            {
                Console.WriteLine("Пустой пароль");
                psw = Console.ReadLine();
            } 
            
            Console.WriteLine("Паспорт");
            string passport = Console.ReadLine();
           
            while (String.IsNullOrWhiteSpace(passport))
            {
                Console.WriteLine("Введите не пустые папортные данные");
                passport = Console.ReadLine();
            }
          


            using (SqlConnection cn = new SqlConnection(Program.connectionString))
            {
              
                cn.Open();
               
                SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM AppUsers where AppUserName=@uname", cn);
                cmd.Parameters.AddWithValue("@uname", user_name);
                var r = cmd.ExecuteScalar().ToString();
                if (r != "0")
                {
                    Console.WriteLine("Пользователь с таким номером телефона уже существует");
                    Console.ReadKey();
                    return false;
                }
                else
                {
                   
                    cmd = new SqlCommand("INSERT INTO AppUsers(appusername, psw, passport, isadmin) values(@uname, @psw, @passport, 0)", cn);
                    cmd.Parameters.AddWithValue("@uname", user_name);
                    cmd.Parameters.AddWithValue("@psw", psw);
                    cmd.Parameters.AddWithValue("@passport", passport);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Пользователь создан. Для продолжения нажмите любую клавишу");
                    Console.ReadKey();
                    return true;
                }
            }
        }

        
        public UserRole CheckUser()
        {

            Console.WriteLine("Введите имя пользователя (номер телефона)");
            string uname = Console.ReadLine();
            Console.WriteLine("Введите пароль");
            string psw = Console.ReadLine();

            this.userName = uname;
            try
            {
                using (SqlConnection cn = new SqlConnection(Program.connectionString))
                {
                    cn.Open();
                    
                    SqlCommand cmd = new SqlCommand("SELECT CASE WHEN isadmin IS NULL THEN 0 WHEN isadmin=0 THEN 1 WHEN isadmin=1 THEN 2 END as userrole FROM AppUsers where AppUserName=@uname AND psw=@psw", cn);
                    cmd.Parameters.AddWithValue("@uname", uname);
                    cmd.Parameters.AddWithValue("@psw", psw);
                    var r = cmd.ExecuteScalar();
                    
                    if (r == null)
                        return UserRole.Guest;
                    else
                        return (UserRole)r;
                }
            }
            catch
            {
                Console.WriteLine("Ошибка подключения к SQL-серверу");
                Console.ReadKey();
            
            }
            return UserRole.Guest;
        }
    }
}
