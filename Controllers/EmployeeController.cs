using DSCC_CW1_7244_2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DSCC_CW1_7244_2.Controllers
{
    public class EmployeeController : Controller
    {
        string Baseurl = "https://localhost:44362/";
        public async Task<ActionResult> Index()
        {
            //Hosted web API REST Service base url
            
            List<Employee> EmpInfo = new List<Employee>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Employee");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Product list
                    EmpInfo = JsonConvert.DeserializeObject<List<Employee>>(PrResponse);
                }
                //returning the Product list to view
                return View(EmpInfo);
            }
        }

        // GET: Employee/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Employee employee = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                HttpResponseMessage Res = await client.GetAsync(string.Format("api/Employee/{0}", id));

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Product list  
                    employee = JsonConvert.DeserializeObject<Employee>(PrResponse);

                }
                else
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            }
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                // TODO: Add update logic here
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync(string.Format("api/Employee", employee), employee);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Edit");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View();
            }
        }

        // GET: Employee/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Employee employee = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                HttpResponseMessage Res = await client.GetAsync(string.Format("api/Employee/{0}", id));

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Product list  
                    employee = JsonConvert.DeserializeObject<Employee>(PrResponse);

                }
                else
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            }
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Employee employee)
        {
            try
            {
                // TODO: Add update logic here
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    HttpResponseMessage Res = await client.GetAsync(string.Format("api/Employee/{0}", id));
                    Employee serverEmp = null;
                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var PrResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Product list  
                        serverEmp = JsonConvert.DeserializeObject<Employee>(PrResponse);
                    }
                    employee.EmployeeDepartment = serverEmp.EmployeeDepartment;
                    //HTTP POST
                    var postTask = client.PutAsJsonAsync<Employee>(string.Format("api/Employee/{0}", employee.Id), employee);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View();
            }
        }

        // GET: Employee/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Employee employee = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                HttpResponseMessage Res = await client.GetAsync(string.Format("api/Employee/{0}", id));

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Product list  
                    employee = JsonConvert.DeserializeObject<Employee>(PrResponse);

                }
                else
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Employee employee)
        {
            try
            {// TODO: Add update logic here
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    var deleteTask = client.DeleteAsync(string.Format("api/Employee/{0}", id));
                    deleteTask.Wait();
                    //Checking the response is successful or not which is sent using HttpClient  
                    var Res = deleteTask.Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }


                }
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View();
            }
        }
    }
}
