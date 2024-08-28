using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages.Admin.Products
{
    public class CreateModel : PageModel
    {
        private readonly IWebHostEnvironment enviroment;
        private readonly ApplictionDbContext context;

        [BindProperty]
        public ProductDto ProductDto { get; set; }=new ProductDto();

        public CreateModel(IWebHostEnvironment enviroment ,ApplictionDbContext context)
        {
            this.enviroment = enviroment;
            this.context = context;
        }
        public void OnGet()
        {

        }

        public string errorMessage = "";
        public string successMessage = "";

        public void OnPost()
        {
            if (ProductDto.ImageFile == null)
            {
                ModelState.AddModelError("ProductDto.ImageFile", "The Image File is required");
            }
            if (!ModelState.IsValid)
            {
                errorMessage = "Please provide all the required fields";
                return;

            }

            //save the imageFile

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(ProductDto.ImageFile!.FileName);

            string ImageFullPath = enviroment.WebRootPath + "/products/" + newFileName;
            using (var stream = System.IO.File.Create(ImageFullPath))
            {
                ProductDto.ImageFile.CopyTo(stream);

            }


            //save the proudct into the DB

            Product product = new Product() {
                Name = ProductDto.Name,
                Brand = ProductDto.Brand,
                Category = ProductDto.Category,
                Price = ProductDto.Price,
                Description=ProductDto.Description ?? "",
                ImageFileName= newFileName,
                CresitAt=DateTime.Now,
             
            };

            context.Products.Add(product);
            context.SaveChanges();

            //Clear the form 
            ProductDto.Name = "";
            ProductDto.Brand = "";
            ProductDto.Category = "";
            ProductDto.Price = 0;
            ProductDto.Description = "";
            ProductDto.ImageFile = null;

            ModelState.Clear();

            successMessage = "Product created successfully";
            Response.Redirect("/Admin/Products/Index");
        }
    }
}
