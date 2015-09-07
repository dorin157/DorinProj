using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Task08LINQ
{
    class Program
    {
        const string TABLENAME = "myTable";
        const string ID_COLUMN = "id";
        const string NAME_COLUMN = "name";
        const string PRICE_COLUMN = "price";
        static void Main(string[] args)
        {
            DataTable dataTable = CreateTable();
            InsertRecords(dataTable);

            //all records with id > 3;
            var query1 = from rec in dataTable.AsEnumerable()
                         where rec.Field<Int32>(ID_COLUMN) > 3
                         select rec;

            //all prices for records with id > 4;
            var query2 = from rec in dataTable.AsEnumerable()
                         where rec.Field<Int32>(ID_COLUMN) > 4
                         select rec.Field<Double>(PRICE_COLUMN);

            //all prices in ascending order;
            var query3 = from rec in dataTable.AsEnumerable()
                         orderby rec.Field<Double>(PRICE_COLUMN)
                         select rec.Field<Double>(PRICE_COLUMN);

            //anonymous types (classes w/o names) with following field names IdField,
            //NameField, PriceField (mapped on corresponding columns);
            var query4 = from rec in dataTable.AsEnumerable()
                         select new
                         {
                             ID = rec.Field<Int32>(ID_COLUMN),
                             NAME = rec.Field<String>(NAME_COLUMN),
                             PRICE_COLUMN = rec.Field<Double>(PRICE_COLUMN)
                         };

            //all records with id > 2 and id < 8 ordered by price descending.
            var query5 = from rec in dataTable.AsEnumerable()
                         where rec.Field<Int32>(ID_COLUMN) > 2 &&
                         rec.Field<Int32>(ID_COLUMN) < 8
                         select rec;

            Console.WriteLine("\nall records with id > 3");
            foreach (var res in query1)
            {
                Console.WriteLine("id - {0}, Name {1}, Price {2}", res[ID_COLUMN],
                res[NAME_COLUMN], res[PRICE_COLUMN]);
            }
            Console.WriteLine("\nall prices for records with id > 4");
            foreach (var res in query2)
            {
                Console.WriteLine(" Price {0}", res);
            }
            Console.WriteLine("\nall prices in ascending order");
            foreach (var res in query3)
            {
                Console.WriteLine(" Price {0}", res);
            }
            Console.WriteLine("\nanonymous types (classes w/o names) with following field names IdField,\n"+
            "NameField, PriceField (mapped on corresponding columns)");
            foreach (var res in query4)
            {
                Console.WriteLine("id - {0}, Name {1}, Price {2}", res.ID,
                res.NAME, res.PRICE_COLUMN);
            }
            Console.WriteLine("\nall records with id > 2 and id < 8 ordered by price descending");
            foreach (var res in query5)
            {
                Console.WriteLine("id - {0}, Name {1}, Price {2}", res[ID_COLUMN],
                res[NAME_COLUMN], res[PRICE_COLUMN]);
            }
            Console.ReadLine();

        }

        public static DataTable CreateTable()
        {
            DataTable dataTable = new DataTable(TABLENAME);

            DataColumn[] cols = {
                                    new DataColumn(ID_COLUMN, typeof(Int32)),
                                    new DataColumn(NAME_COLUMN, typeof(String)),
                                    new DataColumn(PRICE_COLUMN, typeof(Double))
                                };
            dataTable.Columns.AddRange(cols);
            return dataTable;
        }

        public static void InsertRecords(DataTable dataTable)
        {
            Random random = new Random();
            
            for (int i = 1; i <= 10; i++)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow[ID_COLUMN] = i;
                dataRow[NAME_COLUMN] = i + "name";
                dataRow[PRICE_COLUMN] = random.NextDouble() * (100.00 - 0.01) + 0.01;
                dataTable.Rows.Add(dataRow);
            }

        }
        
    }
}
