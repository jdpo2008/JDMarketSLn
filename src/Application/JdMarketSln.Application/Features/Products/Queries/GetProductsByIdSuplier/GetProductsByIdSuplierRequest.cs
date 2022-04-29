using AutoMapper;
using JdMarketSln.Application.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Products.Queries.GetProductsByIdSuplier
{
    public class GetProductsByIdSuplierRequest : IMapFrom<GetProductsByIdSuplierQuery>
    {
        public Guid SuplierId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetProductsByIdSuplierQuery, GetProductsByIdSuplierRequest>();

        }
    }
}
