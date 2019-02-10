using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List_methods
{

    class Program
    {
        public static List<object> list;
        public static Random rnd;
        //public static List<int> list_2;


        static void Main(string[] args)
        {
            list = new List<object>();
            rnd = new Random();
            for (int i = 0; i < 30; i++)
            {
                list.Add(rnd.Next(0, 10));
                Console.WriteLine(list.Last());
            }

            Console.WriteLine(list.Count);

            //list_2 = new List<int>();
            //list_2.AddRange(list);

            List<object> unique = new List<object>();
            bool duplicate = new bool();

            int n = 0;

            for (int i = 0; i < list.Count; i++)
            {
                duplicate = false;
                for (int j = 0; j < unique.Count; j++)
                {
                    duplicate = Equals(list[i], unique[j]);
                    if (duplicate == true) break;
                }
                if (duplicate == false)
                {
                    unique.Add(list[i]);
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (Equals(list[i],list[j])) n++;
                    }
                    Console.WriteLine("Число: " + list[i] + "      Колчество: " + n);
                    n = 0;
                }
            }

            Console.ReadLine();
        }

    }
}
