using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Model;

namespace DalMongoDB
{
    public class PersonDal
    {
        private static string ConnStr = "mongodb://127.0.0.1:27017/?safe=true";
        private static MongoClient Client = new MongoClient(ConnStr);
        private static IMongoDatabase Database = Client.GetDatabase("TestDB2");
        private static IMongoCollection<Person> collection = null;
        static PersonDal()
        {
            //Database.CreateCollection("person");
            collection = Database.GetCollection<Person>("person");
        }
        /// <summary>
        /// 全查
        /// </summary>
        /// <returns></returns>
        public List<Person> SelectAllPerson()
        {
            var filer = Builders<Person>.Filter.Empty;
            return collection.Find<Person>(filer).ToList();
        }

        /// <summary>
        /// 查询一条数据
        /// </summary>
        /// <param name="id">数据的id</param>
        /// <returns></returns>
        public Person SelectSinglePerson(ObjectId id)
        {
            var filer = Builders<Person>.Filter.Eq("_id",id);
            return collection.Find<Person>(filer).FirstOrDefault();

        }


        /// <summary>
        /// 插入
        /// </summary>
        /// <returns></returns>
        public bool InsertPerson(Person person)
        {
            Person p = new Person();
            p._id = ObjectId.GenerateNewId();
            p.Name = person.Name;
            p.Salary =person.Salary;
            p.Address=new List<string>(person.Address);
            collection.InsertOne(p);
            return true;

        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public bool UpdatePerson(Person person)
        {
            var filer = Builders<Person>.Filter.Eq("_id", person._id);
            var update = Builders<Person>.Update.Set("Name",person.Name).Set("Salary",person.Salary).Set("Address",person.Address);
            collection.UpdateOne(filer, update);
            return true;

        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public bool DeletePerson(ObjectId id)
        {
            var filer = Builders<Person>.Filter.Eq("_id", id);
            collection.DeleteOne(filer);
            return true;
        }




    }
}
