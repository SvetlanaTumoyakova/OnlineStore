using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreClient.Dto
{
    public class ProductCategoryDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
