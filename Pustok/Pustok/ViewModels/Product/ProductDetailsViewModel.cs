using System.Collections.Generic;

namespace Pustok.ViewModels.Product
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
        public string ImageNameInFileSystem { get; set; }

        public string CategoryName { get; set; }
        public List<ColorDetailsViewModel> Colors { get; set; }
        public List<SizeDetailsViewModel> Sizes { get; set; }



        public class ColorDetailsViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public class SizeDetailsViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
