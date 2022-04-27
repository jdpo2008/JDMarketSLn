using AutoMapper;
using JdMarketSln.Application.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductRequest : IMapFrom<UpdateProductCommand>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        [RegularExpression("^\\d{0,8}(\\.\\d{1,2})?$")]
        public decimal Price { get; set; }
        //[Required]
        //public int Stock { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public Guid SuplierId { get; set; }
        //[Required]
        //public Guid MarcaId { get; set; }

        //public string ImageUrl { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProductCommand, UpdateProductRequest>();
        }
    }
}
