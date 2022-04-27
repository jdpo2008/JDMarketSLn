using AutoMapper;
using JdMarketSln.Application.Mappings;
using JdMarketSln.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductRequest : IMapFrom<CreateProductCommand>
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SuplierId { get; set; }
        // public Guid MarcaId { get; set; }
        public decimal Price { get; set; }
        // public int Stock { get; set; }
        // public string ImageUrl { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProductCommand, Product>();
        }
    }
}
