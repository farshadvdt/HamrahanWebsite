using FarshadTools;
using Hamrahan.Models;
using HamrahanTemplate.Application.DTOs;
using HamrahanTemplate.Application.Pagination;
using HamrahanTemplate.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public ActionResult Index([FromQuery] PersonPaginationParameters parameters)
        {
            if (parameters.PageNumber < 1)
            {
                parameters.PageNumber = 1;
            }

            var person = _uow.Person.FindAllByPagination(parameters);
            ViewBag.CurrentPage = person.CurrentPage;
            ViewBag.TotalPages = person.TotalPages;






            ///only in case if bad page entered

            List<PersonDTO> personDTO = new();

       
            
            foreach (var item in person)
            {
                personDTO.Add(new PersonDTO
                {

                    FirstName = item.FirstName,
                    Lastname = item.Lastname,
                    TelePhone = item.TelePhone,
                    PhoneNumber = item.PhoneNumber,
                    EducationGradeName = _uow.Grade.Find(t => t.Code == item.EducationGradecode).FirstOrDefault().Grade,
                    Age = item.Age,
                    Gender = item.Gender,
                    UserName = item.Email,
                    

                }) ;
            }
            return View(personDTO);

        }

        // GET: PersonController/Details/5
        public ActionResult Details(string? id)
        {

            var person = _uow.Person.FindById(id).FirstOrDefault();
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
      
            ViewData["EducationGradeCode"] = new SelectList(_uow.Grade.GetAll(), "Code", "Grade");

           


            return View();
            
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
                    EducationGradecode=personDTO.EducationGradeCode,
                    UserName = personDTO.Email,
                    Birthdate = personDTO.Birthdate,
                    NationalCode = personDTO.NationalCode,
                    Email=personDTO.Email,
                    Address = personDTO.Address,
                    Gender=personDTO.Gender,
                    
                    
                };

                var result = _userManager.CreateAsync(person, personDTO.Password).Result;
                var addRole = _userManager.AddToRoleAsync(person,Roles.Customer).Result;
               

                if (result.Succeeded && addRole.Succeeded)
                {
                    return RedirectToAction("Index", "Person", new { area = "Admin" });
                    
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

    }
}
