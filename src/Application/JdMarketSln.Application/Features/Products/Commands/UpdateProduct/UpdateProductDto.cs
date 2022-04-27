using AutoMapper;
using JdMarketSln.Application.Mappings;
using JdMarketSln.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductDto : IMapFrom<Product>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SuplierId { get; set; }
        //public Guid MarcaId { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? UpdateAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, UpdateProductDto>();
        }
    }
}
