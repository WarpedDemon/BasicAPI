using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace EndProject
{
    [Table("Job")]
    public class Job
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        public bool Important { get; set; }
        [MaxLength(255)]
        public bool DisplayDelete { get; set; }
        [MaxLength(255)]
        public bool Display { get; set; }
        [MaxLength(255)]
        public string Temp { get; set; }

        public Job(int index, string name, bool important, bool displayDelete, bool display, string temp)
        {
            Id = index;
            Name = name;
            Important = important;
            DisplayDelete = displayDelete;
            Display = display;
            Temp = temp;
        }

        //Parameterless constructor needed for sqlite
        //Suspected breaks on call as no info is passed when using this to create tables
        public Job()
        {

        }
    }
}
