using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Model;

namespace DalMongoDB
{
    public class PersonDal
    {
        private static string ConnStr = "mongodb://127.0.0.1:27017/?safe=true";
        private static MongoClient Client = new MongoClient(ConnStr);
        private static IMongoDatabase Database = Client.GetDatabase("TestDB2");
        private static IMongoCollection<Person> collection = null;
        private static IMongoCollection<BsonDocument> collectionBD = null;
        static PersonDal()
        {
            //Database.CreateCollection("person");
            collection = Database.GetCollection<Person>("person");
            collectionBD = Database.GetCollection<BsonDocument>("person"); 
        }
        #region person表的增删改查
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
            var result=  collection.UpdateOne(filer, update);
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

        #endregion 
        #region 使用BsonDocument
        /// <summary>
        /// 插入数据
        /// </summary>
        public void InsertDB()
        {
            BsonDocument bson = new BsonDocument();
            bson.Add("Name", "zq");
            bson.Add("Salary", 1000000000);
            bson.Add("Address", new BsonArray {"1","2","3" });
            collectionBD.InsertOne(bson);

        }
        /// <summary>
        /// 按照页面查询
        /// </summary>
        public void SelectPageDB(int pageSize,int pageNum)
        {
            var persons = from person in collection.AsQueryable()     
                          where person.Name == "zq"
                          orderby person.Name ascending
                          select new { Name = person.Name };

            SortDefinitionBuilder<BsonDocument> sorDefBui = Builders<BsonDocument>.Sort;
            SortDefinition<BsonDocument> sorDef = sorDefBui.Ascending("Name");
            FilterDefinitionBuilder<BsonDocument> filDefBui = Builders<BsonDocument>.Filter;
            collectionBD.Find(filDefBui.Empty).Sort(sorDef).SortBy(t=>t["Name"]);




        }
        /// <summary>
        /// 修改数据
        /// </summary>
        public void UpdateDB()
        {
            FilterDefinitionBuilder<BsonDocument> filDefBui = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filDef = filDefBui.Eq("Name", "zq");
            UpdateDefinitionBuilder<BsonDocument> updDefBui= Builders<BsonDocument>.Update;
            UpdateDefinition<BsonDocument> updDef = updDefBui.Set("Name", "zq2");
            collectionBD.UpdateMany(filDef, updDef);
        }

        /// <summary>
        /// 全部删除
        /// </summary>
        public void DeleteDB()
        {
            collectionBD.DeleteMany(Builders<BsonDocument>.Filter.Empty);
        }




        #endregion





    }
}
