using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Model
{
    public class Person
    {
      
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public List<string> Address = new List<string>();
    }
}
