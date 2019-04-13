using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DalMongoDB;
using Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace TestProjectMongodb.Controllers
{
    public class PersonController : Controller
    {
        PersonDal pDal = new PersonDal();
        // GET: Person

        public ActionResult LookPersons()
        {
            List<Person> persons = pDal.SelectAllPerson();
            return View(persons);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SeekOnePerson(string _id)
        {
            //string _id = Request.QueryString["id"].ToString();
            if (string.IsNullOrEmpty(_id))
            {
                return View(new Person() { _id = default(ObjectId), Name = "", Salary = default(double), Address = new List<string>() });
            }
            ObjectId __id = new ObjectId();
            ObjectId.TryParse(_id, out __id);
            Person person = pDal.SelectSinglePerson(__id);
            return View(person);

            //return View();
        }

        /// <summary>
        /// 增加或者修改
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SeekOnePerson(string _id, Person person,string[] Address)
        {
            ObjectId __id;
            ObjectId.TryParse(_id,out __id);
            person._id = __id;
            if (Address != null)
            {
                List<string> adresList = Address.ToList();
                person.Address = adresList;
            }
            else
            {
                person.Address = new List<string>();
            }
            if (person._id == default(ObjectId))
            {
                pDal.InsertPerson(person);
            }
            else
                pDal.UpdatePerson(person);
             return  RedirectToAction("LookPersons");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeletePerson(string _id)
        {
            ObjectId __id;
            ObjectId.TryParse(_id, out __id);
            bool result= pDal.DeletePerson(__id);
            return Json(new { Result = result },JsonRequestBehavior.AllowGet);
            
        }





         
        





    }
}