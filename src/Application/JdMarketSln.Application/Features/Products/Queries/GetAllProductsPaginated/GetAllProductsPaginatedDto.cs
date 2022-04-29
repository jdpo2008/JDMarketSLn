using AutoMapper;
using JdMarketSln.Application.Mappings;
using JdMarketSln.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Products.Queries.GetAllProductsPaginated
{
    public class GetAllProductsPaginatedDto : IMapFrom<Product>
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string CategoryId { get; set; }
        public string SuplierId { get; set; }
        public decimal Price { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        //public Suplier Suplier { get; set; }
        //public Category Category { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, GetAllProductsPaginatedDto>();
        }
    }
}
