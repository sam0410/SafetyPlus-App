using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafetyPlus1
{
    class DataBaseController
    {
        public static void createTable()
        {
            using (var connection = new SQLiteConnection("Storage.db"))
            {
                using (var statement = connection.Prepare(@"CREATE TABLE IF NOT EXISTS Student (
                                                ID INTEGER,
                                                VALUE NVARCHAR(10));"))
                {
                    statement.Step();
                }
            }
        }
        public static void insertData(long param1, string param2)
        {
            try
            {
                using (var connection = new SQLiteConnection("Storage.db"))
                {
                    using (var statement = connection.Prepare(@"INSERT INTO Student (ID,VALUE)
                                            VALUES(?, ?);"))
                    {
                        statement.Bind(1, param1);
                        statement.Bind(2, param2);
                       

                        // Inserts data.
                        statement.Step();


                        statement.Reset();
                        statement.ClearBindings();


                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception\n" + ex.ToString());
            }
        }
        public static ObservableCollection<Student> getValues()
        {
            ObservableCollection<Student> list = new ObservableCollection<Student>();

            using (var connection = new SQLiteConnection("Storage.db"))
            {
                using (var statement = connection.Prepare(@"SELECT * FROM Student;"))
                {

                    while (statement.Step() == SQLiteResult.ROW)
                    {

                        list.Add(new Student()
                        {
                            Id = (long)statement[0],
                            Value = (string)statement[1],
                           
                        });

                        Debug.WriteLine(statement[0] + " ---" + statement[1] + " ---" + statement[2]);
                    }
                }
            }
            return list;
        }

    }
    
}
