﻿using AutoMapper;
using JdMarketSln.Application.Mappings;
using JdMarketSln.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdDto : IMapFrom<Product>
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        //public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SuplierId { get; set; }
        //public Guid MarcaId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, GetProductByIdDto>();
        }
    }
}
