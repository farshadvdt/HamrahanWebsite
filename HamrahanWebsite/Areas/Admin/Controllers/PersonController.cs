using Microsoft.AspNetCore.Http;

using HamrahanTemplate.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contexts;
using HamrahanTemplate.Infrastructure.UnitOfWork;
using HamrahanTemplate.Application.DTOs;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hamrahan.Models;
using Microsoft.Win32;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace HamrahanWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PersonController : Controller
    {

        private readonly IUow _uow;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Person> _userManager;
        public PersonController(IUow uow, RoleManager<IdentityRole> roleManager, UserManager<Person> userManager)
        {
            _uow = uow;
            _roleManager = roleManager;
            _userManager = userManager;

        }

        // GET: PersonController
        public ActionResult Index()
        {
            List<PersonDTO> personDTO = new();
            var person = _uow.Person.GetAll().ToList();
            
            foreach (var item in person)
            {
                personDTO.Add(new PersonDTO {

                    FirstName = item.FirstName,
                    Lastname = item.Lastname,
                    TelePhone = item.TelePhone,
                    PhoneNumber = item.PhoneNumber,
                    EducationGradeName=_uow.Grade.Find(t=>t.Code == item.EducationGradeCode).FirstOrDefault().Grade,
                    Age = item.Age,
                    Gender = item.Gender,
                    UserName = item.Email,


                });
            }
            return View(personDTO);
        }

        // GET: PersonController/Details/5
        public ActionResult Details(string? id)
        {

            var person = _uow.Person.FindById(id).FirstOrDefault();
            //var roles = new List<SelectListItem>(

            //_roleManager.Roles.Select(p =>

            //    new SelectListItem
            //    {
            //        Text = p.Name,
            //        Value = p.Id,

            //    }).ToList());

            var personDto = new PersonDTO
            {

                FirstName = person.FirstName,
                Lastname = person.Lastname,
                TelePhone = person.TelePhone,
                PhoneNumber = person.PhoneNumber,
                Age = person.Age,
                UserName = person.Email,
                Birthdate = person.Birthdate,
                NationalCode = person.NationalCode,
                Address = person.Address,
                //Roles = roles



            };

         

            return View(personDto);
        }

        // GET: PersonController/Create
        public ActionResult Create()
        {
            //var educationGrade = new SelectList(
            //    _uow.Grade.GetAll(), "Code", "Grade" );
            ViewData["EducationGradeCode"] = new SelectList(_uow.Grade.GetAll(), "Code", "Grade");
            ViewData["Role"] = new SelectList(_roleManager.Roles.AsEnumerable(), "Id", "Name");


            //var role = new List<SelectListItem>(

            //          _roleManager.Roles.Select(p => new SelectListItem
            //          {
            //              Text = p.Name,
            //              Value = p.Name
            //          }
            //              ).ToList());


            return View(
                //new PersonDTO { Roles = role,
                //EducationGrades = educationGrade}
                //
                );
            
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create( PersonDTO personDTO)
        {
            try
            {
               

                Person person = new()
                {
                    FirstName = personDTO.FirstName,
                    Lastname = personDTO.Lastname,
                    TelePhone = personDTO.TelePhone,
                    PhoneNumber = personDTO.PhoneNumber,
                    EducationGradeCode=personDTO.EducationGradeCode,
                    UserName = personDTO.Email,
                    Birthdate = personDTO.Birthdate,
                    NationalCode = personDTO.NationalCode,
                    Email=personDTO.Email,
                    Address = personDTO.Address,
                    Gender=personDTO.Gender,
                    
                };

               // ViewData["EducationGradeCode"] = new SelectList(_uow.Grade.GetAll(), "Code", "Grade", person.EducationGradeCode);
                var result = _userManager.CreateAsync(person, personDTO.Password).Result;
                var findPerson = _userManager.FindByIdAsync(person.Id).Result;
                var addRole = _userManager.AddToRoleAsync(findPerson, personDTO.Role).Result;

                
                
                if (result.Succeeded && addRole.Succeeded)
                {
                    return RedirectToAction("Index", "Person", new { area = "Admin" });
                    TempData["msg"] = "ثبت شد";
                }

                string message = "";
                foreach (var item in result.Errors.ToList())
                {
                    message += item.Description + Environment.NewLine;
                }
                TempData["Message"] = message;
                return View(personDTO);
            }
            catch
            {
                return View(personDTO);
            }
        }

        // GET: PersonController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("Admin")]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        //public IActionResult AddUserRole(string id)
        //{
        //    var roles = new List<SelectListItem>(

        //        _roleManager.Roles.Select(p =>

        //            new SelectListItem
        //            {
        //                Text = p.Name,
        //                Value = p.Id,

        //            }).ToList());


        //    return View(new AddUSerRoleDTO
        //    {
        //        Id = id,
        //        Roles = roles
        //    });
        //}
    }
}
