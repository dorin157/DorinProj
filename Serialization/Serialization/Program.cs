using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using Models;
using DAL;

namespace Serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            Items myItems = new Items();
            SetData(myItems);
            bool mainProg = true;
            while (mainProg)
            {
                Console.WriteLine("Enter number (1) for reading or (2) for writing");
                string s = Console.ReadLine();
                int command;
                if(int.TryParse(s, out command))
                {
                    switch (command)
                    {
                        case 1:
                            myItems = Reading();
                            break;
                        case 2:
                             Writing(myItems);
                            break;
                        default:
                            continue;
                    }
                }
                bool exit = false;
                while (!exit)
                {
                    Console.WriteLine("Press (1) to exit or (2) to continue");
                    s = Console.ReadLine();
                    if (int.TryParse(s, out command))
                    {
                        switch (command)
                        {
                            case 1:
                                mainProg = false;
                                exit = true;
                                break;
                            case 2:

                                exit = true;
                                break;
                            default:
                                continue;
                        }
                    }
                }

            } 
        }

        static public void Serialize(Items items)
        {
            XmlSerializer serialiser = new XmlSerializer(typeof(Items));
            using (TextWriter writer = new StreamWriter("outputItem.xml"))
            {
                serialiser.Serialize(writer, items);
            }
        }

        static public Items Deserialize()
        {
            Items myItems = new Items();
            XmlSerializer deserializer = new XmlSerializer(typeof(Items));
            TextReader reader = new StreamReader("outputItem.xml");
            object obj = deserializer.Deserialize(reader);
            myItems = (Items)obj;
            reader.Close();
            return myItems;
        }

        static public Items Reading()
        {
            Items items = new Items();
            List<Item> itemsList = new List<Item>();
            bool b = true;
            while (b)
            {
                Console.WriteLine("Press (1) to read from *.txt, (2) - DataBase or (3) to Deserialize");
                string s = Console.ReadLine();
                int command;
                if (int.TryParse(s, out command))
                {
                    switch (command)
                    {
                        case 1:
                            using (StreamReader sr = File.OpenText("output.txt"))
                            {
                                string sRead = "";
                                List<string> stringItems = new List<string>();
                                while ((sRead = sr.ReadLine()) != null)
                                {
                                    stringItems.Add(sRead);
                                }
                                foreach (string item in stringItems)
                                {
                                    items = Items.GetItemsFromString(item);
                                }
                                Console.WriteLine(items);
                            }
                            break;
                        case 2:
                            string db1 = ConfigurationManager.AppSettings["connectionString"];
                            DataAccessManager daM = new DataAccessManager(db1);
                            items.CurItems = daM.GetItems();
                            break;
                        case 3:
                            items = Deserialize();
                            break;
                        default:
                            break;
                    }
                }

                bool exit = false;
                while (!exit)
                {
                    Console.WriteLine("Press (1) to exit from Writing or (2) to continue");
                    s = Console.ReadLine();
                    if (int.TryParse(s, out command))
                    {
                        switch (command)
                        {
                            case 1:
                                b = false;
                                exit = true;
                                break;
                            case 2:
                                exit = true;
                                break;
                            default:
                                continue;
                        }
                    }
                }
            }
            items.CurItems = itemsList;
            return items;

        }

        static public void Writing(Items items)
        {
            bool b = true;
            while (b)
            {
                Console.WriteLine("Press (1) to write in *.txt, (2) - in DataBase or (3) to Serialize");
                string s = Console.ReadLine();
                int command;
                if (int.TryParse(s, out command))
                {
                    switch (command)
                    {
                        case 1:
                            using (StreamWriter sw = File.AppendText("output.txt"))
                            {
                                sw.WriteLine(items);
                            }
                            break;
                        case 2:
                            string db1 = ConfigurationManager.AppSettings["connectionString"];
                            DataAccessManager daM = new DataAccessManager(db1);
                            foreach(Item item in items.CurItems)
                            {
                                daM.InsertItem(item);
                            }
                            break;
                        case 3:
                            Serialize(items);
                            break;
                        default:
                            break;
                    }
                }

                bool exit = false;
                while (!exit)
                {
                    Console.WriteLine("Press (1) to exit from Writing or (2) to continue");
                    s = Console.ReadLine();
                    if (int.TryParse(s, out command))
                    {
                        switch (command)
                        {
                            case 1:
                                b = false;
                                exit = true;
                                break;
                            case 2:
                                exit = true;
                                break;
                            default:
                                continue;
                        }
                    }
                }
            }
        }

        static void SetData(Items myItems)
        {
            Item myItem1 = new Item();
            Position pos1 = new Position();
            Position pos2 = new Position();
            Job job = new Job();

            pos1.Latitude = 200;
            pos1.Longitude = 300;
            pos1.Accuracy = 5;
            pos1.Date = DateTime.Now;

            pos2.Latitude = 400;
            pos2.Longitude = 500;
            pos2.Accuracy = 6;
            pos2.Date = DateTime.Now;

            job.Time = DateTime.Now;
            job.Description = "some text";
            job.Phone = "7777";
            job.UserId = 1;

            myItem1.Id = 1;
            myItem1.FirstName = "Robin";
            myItem1.LastName = "Good";
            myItem1.PositionHistory.Add(pos1);
            myItem1.PositionHistory.Add(pos2);
            myItem1.JobHistory.Add(job);

            myItems.CurItems.Add(myItem1);
            myItems.CurItems.Add(myItem1);
        }

    }
}
