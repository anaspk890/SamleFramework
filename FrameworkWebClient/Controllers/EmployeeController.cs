using FrameworkWebClient.Models;
using FrameworkWebClient.ViewModels;
using GenericFramework.SystemCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace FrameworkWebClient.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            //System.Collections.Specialized.NameObjectCollectionBase forms
            //System.Collections.Specialized.NameObjectCollectionBase mvc

            //System.Collections.Specialized.NameValueCollection webforms
            //System.Collections.Specialized.NameValueCollection
            EmployeeInsertReq req = new EmployeeInsertReq();//should come via plugin
            req.DepName = collection["DepName"];
            req.EmpName = collection["EmpName"];
            EmployeeInsertResult result = new EmployeeInsertResult();//should come via plugin
            IInsertConnector connector = FrameworkWebClient.SystemConfig.coreManger.InsertConnector;
            GenericModel model = new GenericModel(connector);
            string insertCommandText = "Emps_Insert";//from xml or config
            string cmdPrefix = "@";//from xml or config
            CommandType cmdType = CommandType.StoredProcedure;//from xml or config
            List<IDbDataParameter> outParmsList = new List<IDbDataParameter>();// from xml file
            IDbDataParameter param = null;
            param = null;
            param  = new SqlParameter();
            param.ParameterName = "NewId";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;

            param = null;
            param  = new SqlParameter();
            param.ParameterName = "hasError";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;

            param = null;
            param  = new SqlParameter();
            param.ParameterName = "@Message";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.String;
            param.Size = 4000;
            outParmsList.Add(param);

            result = model.InsertItem(req, result, insertCommandText, cmdPrefix, cmdType, outParmsList, false) as EmployeeInsertResult;
            if( result.HasError==null || result.HasError == 0 )
            {
                //send success message
                ViewBag.SuccessMessage = result.Message;
            }
            else
            {
                ViewBag.ErrorMessage = result.Message;
            }
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
