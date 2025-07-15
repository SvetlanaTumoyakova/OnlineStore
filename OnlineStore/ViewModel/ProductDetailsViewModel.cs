using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreClient.ViewModel
{
    public class ProductDetailsViewModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }

        public required ProductDetailsCategoryViewModel CategoryViewModel { get; set; }

    }

    public class ProductDetailsCategoryViewModel
    {
        public required int ProductCategoryId { get; set; }
        public required string NameProductCategory { get; set; }
        public required string DescriptionProductCategory { get; set; }
    }
}
