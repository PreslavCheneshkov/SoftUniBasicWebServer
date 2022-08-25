using SoftUniBasicWebServer.Data;
using SoftUniBasicWebServer.HTTP;
using SoftUniBasicWebServer.MVCFramework;
using SoftUniBasicWebServer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniBasicWebServer.Controllers
{
    public class CardsController : Controller
    {
        public HttpResponse Add()
        {
            return this.View();
        }
        [HttpPost("/Cards/Add")]
        public HttpResponse DoAdd()
        {
            var dbContext = new ApplicationDbContext();
            dbContext.Cards.Add(new Card
            {
                Attack = int.Parse(this.Request.FormData["attack"]),
                Health = int.Parse(this.Request.FormData["health"]),
                Description = this.Request.FormData["description"],
                Name = this.Request.FormData["name"],
                ImageUrl = this.Request.FormData["image"],
                KeyWord = this.Request.FormData["keyword"],

            });

            dbContext.SaveChanges();

            return this.Redirect("/");
        }
        public HttpResponse All()
        {
            var db = new ApplicationDbContext();
            var cardsViewModel = db.Cards.Select(x => new CardViewModel
            {
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Type = x.KeyWord,
                Attack = x.Attack,
                Description = x.Description,
                Health = x.Health,
            }).ToList();

            return this.View(new AllCardsViewModel { Cards = cardsViewModel});
        }
        public HttpResponse Collection()
        {
            return this.View();
        }
    }
}
