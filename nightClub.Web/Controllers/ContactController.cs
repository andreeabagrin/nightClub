﻿using AutoMapper;
using nightClub.BusinessLogic.Interfaces;
using nightClub.Domain.Entities.Contact;
using nightClub.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace nightClub.Web.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IContactService _contactBL;
        public ContactController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _contactBL = bl.GetContactBL();
        }
        // GET: Contact
        public ActionResult Index()
        {
            SessionStatus();
            return View();
        }

        [HttpPost]
        public ActionResult Index(Review review)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Review, ReviewModel>();
                });
                IMapper mapper = config.CreateMapper();
                var data = mapper.Map<ReviewModel>(review);

                data.Date = DateTime.Now;

                _contactBL.AddReview(data);
                return RedirectToAction("ThankYou");
            }
            return View();
        }

        public ActionResult ThankYou()
        {
            return View();
        }

        public ActionResult Reviews()
        {
            SessionStatus();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ReviewModel, Review>();
            });
            IMapper mapper = config.CreateMapper();

            var reviews = mapper.Map<List<Review>>(_contactBL.GetReviews());
            return View(reviews);
        }
    }
}