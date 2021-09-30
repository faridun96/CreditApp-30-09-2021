using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditApp
{

    class Menu
    {
        public bool Stop = false; 
        public Menu(string title)
        {
            this.Title = title;
        }

        string Title;

        
        public List<MenuItem> Items = new List<MenuItem>();

        public void Add(string title, Action click)
        {
            this.Items.Add(new MenuItem(title, click));
        }
        
        public void Draw()
        {
            
            while (!this.Stop)
            {
                
                Console.Clear();
                
                int n = 1;
                Console.WriteLine(this.Title);
                
                foreach (var item in this.Items)
                {
                    Console.WriteLine(String.Format("[{0}] {1}", n, item.Title));
                    n++;
                }
                
                string s_key = Console.ReadLine();
                if (s_key == "q")
                    break;
                int choice = 0;
                
                while (!(Int32.TryParse(s_key, out choice)&&(choice>0&&choice<=this.Items.Count)))
                {
                    Console.WriteLine("Укажите пункт меню с помощью цифры от 1 до {0}", this.Items.Count);
                    s_key = Console.ReadLine();
                }
                
                this.Items[choice - 1].Click();
            }

        }
    }

    
    class MenuItem
    {
        public MenuItem(string title, Action click)
        {
            this.Title = title;
            this.Click = click;
        }
        
        public string Title;
        
        public Action Click;

    }
}
