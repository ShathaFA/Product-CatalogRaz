using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;
using WebApplication1.Models;
using System.Linq;
namespace WebApplication1.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {
        private readonly ApplictionDbContext context;
        public List<Product> Products { get; set; } = new List<Product>();
        public IndexModel(ApplictionDbContext context)
        {
            this.context = context;
        }
        public void OnGet()
        {
            Products = context.Products.OrderByDescending(p=>p.Id).ToList();


        }
    }
}
