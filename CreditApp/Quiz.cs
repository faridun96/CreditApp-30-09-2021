using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditApp
{
    
    class Quiz
    {
        
        public int Start()
        {
            
            Console.Clear();
            
            int user_mark = 0;
            Console.WriteLine("Имя пользователя:");
            
            string user_name = Console.ReadLine();
            try
            {
                using (SqlConnection cn = new SqlConnection(Program.connectionString))
                {

                    cn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT appuserid FROM AppUsers where AppUserName=@uname", cn);
                    cmd.Parameters.AddWithValue("@uname", user_name);
                    var r = cmd.ExecuteScalar();
                    string appuser_id = "";
                    
                    if (r == null)
                    {
                        Console.WriteLine("Пользователь с таким номером телефона не существует");
                        Console.ReadKey();
                        return 0;

                    }

                    
                    appuser_id = r.ToString();




                    Console.WriteLine("Срок кредита (месяцев) 1-100:");
                    
                    int duration = 0;
                    while (!(Int32.TryParse(Console.ReadLine(), out duration) && duration > 0 && duration < 100))
                        Console.WriteLine("Введите правильное число из диапазона. Срок кредита (месяцев) 1-100:");
                    if (duration > 12)
                        user_mark++;
                    else
                        user_mark++;

                    Console.WriteLine("Сумма кредита:");
                    
                    int sum = 0;
                    while (!(Int32.TryParse(Console.ReadLine(), out sum) && sum > 0))
                        Console.WriteLine("Введите положительную сумму кредита:");


                    Console.WriteLine("Возраст:");
                    
                    int age = 0;
                    while (!(Int32.TryParse(Console.ReadLine(), out age) && age >= 18 && age < 120))
                        Console.WriteLine("Введите правильное число из диапазона. Введите возраст от 18 до 120 лет:");
                    if (age <= 25)
                        user_mark = user_mark;
                    else if (age <= 26 && age <= 35)
                        user_mark++;
                    else if (age >= 36 && age <= 62)
                        user_mark += 2;
                    else if (age >= 63)
                        user_mark++;
                    
                    string s_gender = "";
                   
                    int gender = 0;
                    Console.WriteLine("Выберите пол: 1 - мужчина, 2 - женщина");
                    while (!(Int32.TryParse(Console.ReadLine(), out gender) && gender >= 1 && gender <= 2))
                        Console.WriteLine("Введите правильный номер. Выберите пол: 1 - мужчина, 2 - женщина");
                    if (gender == 1)
                    {
                        user_mark += 1;
                        s_gender = "мужчина";
                    }
                    else if (gender == 2)
                    {
                        user_mark += 2;
                        s_gender = "женщина";
                    }




                    Console.WriteLine("Страна");
                    
                    string country = Console.ReadLine();
                    while (String.IsNullOrWhiteSpace(country))
                    {
                        Console.WriteLine("Страна (непустая строка)");              
                         country = Console.ReadLine();
                    }
                    if (country.ToLower() == "Таджикистан".ToLower())
                        user_mark++;

                    
                    int loan_number = -1;
                    Console.WriteLine("Введите количество уже выданных займов (кредитная история) (от 0 до 1000):");
                    while (!(Int32.TryParse(Console.ReadLine(), out loan_number) && loan_number >= 0 && loan_number < 1000))
                        Console.WriteLine("Введите количество уже выданных займов (кредитная история) (от 0 до 1000):");
                    if (loan_number == 0)
                        user_mark--;
                    else if (loan_number == 1 || loan_number == 2)
                        user_mark++;
                    else if (loan_number >= 3)
                        user_mark += 2;

                   
                    int late_number = 0;
                    Console.WriteLine("Введите количество просрочек (от 0 до 1000):");
                    while (!(Int32.TryParse(Console.ReadLine(), out late_number) && late_number >= 0 && late_number < 1000))
                        Console.WriteLine("Введите правильное число из диапазона. Введите количество просрочек (от 0 до 1000):");
                    if (late_number > 7)
                        user_mark -= 3;
                    else if (late_number >= 5 && loan_number <= 7)
                        user_mark -= 2;
                    else if (late_number == 4)
                        user_mark--;


                    
                    int income = 0;
                    Console.WriteLine("Введите доход (от 0 до 1000000000):");
                    while (!(Int32.TryParse(Console.ReadLine(), out income) && income >= 0 && income < 1000000000))
                        Console.WriteLine("Введите правильное число из диапазона. Введите доход (от 0 до 1000000000):");
                    int rate = 0;
                    if (income > 0)
                        rate = sum / income;
                    else
                        rate = 10000000;
                    if (rate < 80)
                        user_mark += 4;
                    else if (rate >= 80 && rate < 150)
                        user_mark += 3;
                    else if (rate >= 150 && rate < 250)
                        user_mark += 2;
                    else if (rate > 250)
                        user_mark++;

                    
                    int family_status = 0;
                    
                    string family = "";
                    Console.WriteLine("Введите семейное положение: 1- холост, 2 - семьянин, 3 - в разводе, 4 - вдовец/вдова");
                    while (!(Int32.TryParse(Console.ReadLine(), out family_status) && family_status >= 1 && family_status <= 4))
                        Console.WriteLine("Введите правильный номер. Введите семейное положение: 1- холост, 2 - семьянин, 3 - в разводе, 4 - вдовец/вдова");
                    if (family_status == 1)
                    {
                        user_mark++;
                        family = "холост";
                    }
                    else if (family_status == 3)
                    {
                        user_mark++;
                        family = "в разводе";
                    }
                    else if (family_status == 2)
                    {
                        user_mark += 2;
                        family = "семьянин";
                    }
                    else if (family_status == 4)
                    {
                        user_mark += 2;
                        family = "вдовец/вдова";
                    }

                    
                    string s_goal = "";
                    
                    int goal = 0;
                    Console.WriteLine("Введите цель кредита: 1- бытовая техника, 2 - ремонт, 3 - телефон, 4 - прочее");
                    while (!(Int32.TryParse(Console.ReadLine(), out goal) && goal >= 1 && goal <= 4))
                        Console.WriteLine("Введите правильный номер. Введите цель кредита: 1- бытовая техника, 2 - ремонт, 3 - телефон, 4 - прочее");
                    if (goal == 1)
                    {
                        user_mark += 2;
                        s_goal = "бытовая техника";
                    }
                    else if (goal == 2)
                    {
                        user_mark += 1;
                        s_goal = "ремонт";
                    }
                    else if (goal == 3)
                    {
                        user_mark += 0;
                        s_goal = "телефон";
                    }
                    else if (goal == 4)
                    {
                        user_mark += -1;
                        s_goal = "прочее";
                    }
                 
                    int isCompleted = 0;
                    Console.WriteLine("Количество баллов - {0}", user_mark);
                    if (user_mark <= 11)
                    {
                        Console.WriteLine("Набрано недостаточное количество баллов для выдачи кредита");
                        Console.ReadKey();

                    }
                    else
                    {
                        Console.WriteLine("Кредит оформлен");
                        isCompleted = 1;
                        Console.ReadKey();
                    }
                   
                    cmd = new SqlCommand("INSERT INTO Applications(appuserid, CreditSum, income, age, country, goal, family, Score, duration, gender, isCompleted) " +
                        "values(@appuserid, @sum, @income, @age, @country, @goal, @family, @score,   @duration, @gender, @iscompleted)", cn);
                    cmd.Parameters.AddWithValue("@appuserid", appuser_id);
                    cmd.Parameters.AddWithValue("@sum", sum);
                    cmd.Parameters.AddWithValue("@income", income);
                    cmd.Parameters.AddWithValue("@age", age);
                    cmd.Parameters.AddWithValue("@country", country);
                    cmd.Parameters.AddWithValue("@goal", s_goal);
                    cmd.Parameters.AddWithValue("@family", family);
                    cmd.Parameters.AddWithValue("@gender", s_gender);
                    cmd.Parameters.AddWithValue("@duration", duration);
                    cmd.Parameters.AddWithValue("@score", user_mark);
                    cmd.Parameters.AddWithValue("@iscompleted", isCompleted);

                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                Console.WriteLine("Ошибка подключения к SQL-серверу");
                Console.ReadKey();

            }

            return 0;
        }



      
        public static void GetUserApplications(string userName)
        {
           
            Console.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Program.connectionString))
                {

                    cn.Open();
                    
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Applications  where AppUserid=(select appuserid from appusers where appusername=@uname)", cn);
                    cmd.Parameters.AddWithValue("@uname", userName);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Console.WriteLine("Номер заявки\t Дата\t Статус");
                   
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["ApplicationId"] + "\t" + reader["Posted"] + "\t" + ((bool)reader["IsCompleted"] ? "Одобрено" : "Отклонено"));
                    }

                }
                Console.ReadKey();
            }
            catch
            {
                Console.WriteLine("Ошибка подключения к SQL-серверу");
                Console.ReadKey();

            }
        }


       
        public static void GetPayments()
        {
            Console.Clear();
            int applicationId = 0;
            Console.WriteLine("Введите номер заявки");
            while (!Int32.TryParse(Console.ReadLine(), out applicationId))
            {
                Console.WriteLine("Введите номер заявки");
            }
            try
            {
                using (SqlConnection cn = new SqlConnection(Program.connectionString))
                {

                    cn.Open();
                
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Applications  where applicationId=@applicationId", cn);
                    cmd.Parameters.AddWithValue("@applicationId", applicationId);
                    SqlDataReader reader = cmd.ExecuteReader();


                    

                    if (reader.Read())
                    {
                        if ((bool)reader["IsCompleted"])
                        {
                            
                            int sum = (int)reader["CreditSum"];
                            double rate = 0.20;
                            double ratedCreditSum = sum * (1 + (int)reader["Duration"] * rate / 12.0);
                            double monthPayment = ratedCreditSum / (int)reader["Duration"];
                            Console.WriteLine();
                            DateTime time = (DateTime)reader["Posted"];
                            for (int i = 1; i <= (int)reader["Duration"]; i++)
                            {
                                Console.WriteLine("Платеж № {0} в размере {1} до {2}", i, String.Format("{0:0.00}", monthPayment), time.AddMonths(i).ToShortDateString());
                            }
                        }
                        else
                        {
                            Console.WriteLine("Заявка не одобрена. График платежей отсутствует");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Заявка с указанным номером не найдена");
                    }

                }
                Console.ReadKey();
            }
            catch
            {
                Console.WriteLine("Ошибка подключения к SQL-серверу");
                Console.ReadKey();
            }
        }

        public static void GetRemaining(string userName)
        {
            
            Console.Clear();
            try
            {
                using (SqlConnection cn = new SqlConnection(Program.connectionString))
                {

                    cn.Open();
                   
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Applications  where iscompleted=1 and dateadd(month, duration, posted)> getdate() and AppUserid=(select appuserid from appusers where appusername=@uname)", cn);
                    cmd.Parameters.AddWithValue("@uname", userName);
                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                      
                        int sum = (int)reader["CreditSum"];
                        double rate = 0.20;
                        double ratedCreditSum = sum * (1 + (int)reader["Duration"] * rate / 12.0);
                        double monthPayment = ratedCreditSum / (int)reader["Duration"];
                       
                        DateTime d1 = ((DateTime)reader["Posted"]).AddMonths((int)reader["Duration"]);
                        DateTime d2 = DateTime.Now;
                        int monthRemaining = ((d1.Year - d2.Year) * 12) + d1.Month - d2.Month;
                        if (d1.Day > d2.Day && DateTime.DaysInMonth(d1.Year, d1.Month) != d1.Day)
                            monthRemaining--;
                        Console.WriteLine();
                        Console.WriteLine("Кредит № {0}  остаток: {1}", reader["ApplicationId"], String.Format("{0:0.00}", monthRemaining * monthPayment));


                    }
                    Console.ReadKey();
                }
            }
            catch
            {
                Console.WriteLine("Ошибка подключения к SQL-серверу");
                Console.ReadKey();
            }
        }
    }
}
