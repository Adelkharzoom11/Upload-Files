using System.ComponentModel.DataAnnotations.Schema;

namespace API.FileProcessing.Models
{
    public class product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Double Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        [NotMapped]
        public IFormFile formFile { get; set; }

    }
}
