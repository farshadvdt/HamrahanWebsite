using FarshadTools;
using Hamrahan.Models;
using HamrahanTemplate.Application.DTOs;
using HamrahanTemplate.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HamrahanWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly IUow _uow;
        private readonly IWebHostEnvironment _webHost;

        public PostController(IUow uow)
        {
            _uow = uow;
        }

        public PostController(IUow uow, IWebHostEnvironment webHost) : this(uow)
        {
            _webHost = webHost;
        }

        //GET: PostController
        public IActionResult Index()
        {
            List<PostDTO> postDto = new();
            var posts = _uow.Post.GetAll().ToList();
            foreach (var item in posts)
            {
                postDto.Add(new PostDTO
                {
                    PersonID = item.PersonID,
                    Topic = item.Topic,
                    Content = item.Content,
                    Summary = item.Content.Substring(0,((int)(item.Content.Length/3))),
                    ImagesLink = item.ImagesLink,
                    


                }) ;
            }
            return View(postDto);
        
        }

        // GET: PostController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var posts = await _uow.Post.FindById(id).FirstOrDefaultAsync();
            PostDTO postDto = new()
            {
                PersonID = posts.PersonID,
                Topic = posts.Topic,
                Content = posts.Content,
                Summary = posts.Content.Substring(0, ((int)(posts.Content.Length / 3))),
                ImagesLink = posts.ImagesLink,
            };

            if (postDto == null)
            {
                return NotFound();
            }

            return View(postDto);
        }

        // GET: PostController/Create
        [Authorize(Roles ="Admin")]
        public  IActionResult Create()
        {
            return View();
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ImagesLink,Topic,Content,PersonID")] PostDTO postDto,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string rootPath = _webHost.WebRootPath;
                
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var upload = Path.Combine(rootPath, @"images");
                    var extension = Path.GetExtension(file.FileName);
                    using(var fileStream=new FileStream(Path.Combine(upload, fileName, extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    postDto.ImagesLink = @"\images\" + fileName + extension;

                }
            }
            try
            {
                Post post = new() 
                {
                   PersonID = postDto.PersonID,
                   Topic = postDto.Topic,
                   Content = postDto.Content,
                   ImagesLink = postDto.ImagesLink,
                   Published = DateTime.Now
                };
                _uow.Post.Insert(post);
                _uow.save();
                return RedirectToAction(nameof(Index));
                
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
}

            var post = await _uow.Post.FindById(id).FirstOrDefaultAsync();
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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

            //            if (id != post.ID)
            //            {
            //                return NotFound();
            //            }

            //            if (ModelState.IsValid)
            //            {
            //                try
            //{
            //                    _context.Update(post);
            //                    await _context.SaveChangesAsync();
            //                }
            //                catch (DbUpdateConcurrencyException)
            //{
            //                    if (!PostExists(post.ID))
            //                    {
            //                        return NotFound();
            //                    }
            //                    else
            //                    {
            //                        throw;
            //                    }
            //                }
            //                return RedirectToAction(nameof(Index));
            //            }
            //return View(post);

        }

        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

          //  if (id == null)
          //  {
          //      return NotFound();
          //  }

          //    var post = await _context.Posts
          //.FirstOrDefaultAsync(m => m.ID == id);
          //  if (post == null)
          //  {
          //      return NotFound();}

            //return View(post);

// POST: PostController/Delete/5
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

        //var post = await _context.Posts.FindAsync(id);
        //_context.Posts.Remove(post);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));}

    //private bool PostExists(int id)
    //{
    //    return _context.Posts.Any(e => e.ID == id);
    //}
}
}
