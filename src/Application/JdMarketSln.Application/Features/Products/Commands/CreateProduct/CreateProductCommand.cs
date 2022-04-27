using AutoMapper;
using JdMarketSln.Application.Interfaces.Repositories;
using JdMarketSln.Application.Wrappers;
using JdMarketSln.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Response<CreateProductDto>>
    {
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

    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<CreateProductDto>>
    {
        private readonly IProductGenericRepository _ProductRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductGenericRepository ProductRepository, IMapper mapper)
        {
            _ProductRepository = ProductRepository;
            _mapper = mapper;
        }
        public async Task<Response<CreateProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            Product response = await _ProductRepository.CreateAsync(_mapper.Map<Product>(request));

            return new Response<CreateProductDto>(_mapper.Map<CreateProductDto>(response), "Product created successfull");
        }
    }
}
