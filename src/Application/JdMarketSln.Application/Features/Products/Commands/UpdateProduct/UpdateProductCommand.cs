using AutoMapper;
using JdMarketSln.Application.Exceptions;
using JdMarketSln.Application.Interfaces.Repositories;
using JdMarketSln.Application.Mappings;
using JdMarketSln.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Response<UpdateProductDto>>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
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

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<UpdateProductDto>>
    {
        private readonly IProductGenericRepository _ProductRepository;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IProductGenericRepository ProductRepository, IMapper mapper)
        {
            _ProductRepository = ProductRepository;
            _mapper = mapper;
        }
        public async Task<Response<UpdateProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateProductRequest>(request);
            var product = await _ProductRepository.GetByIdAsync(command.Id);

            if (product == null)
            {
                throw new ApiException($"Product Not Found.");
            }
            else
            {
                product.ProductName = command.ProductName;
                product.ProductDescription = command.ProductDescription;
                product.Price = command.Price;
                product.CategoryId = command.CategoryId;
                //product.Stock = command.Stock;
                //product.ImageUrl = command.ImageUrl;

                await _ProductRepository.UpdateAsync(product);

                return new Response<UpdateProductDto>(_mapper.Map<UpdateProductDto>(product), "Product Updated Successufull");
            }
        }
    }
}
