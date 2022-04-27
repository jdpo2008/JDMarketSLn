using AutoMapper;
using JdMarketSln.Application.Exceptions;
using JdMarketSln.Application.Interfaces.Repositories;
using JdMarketSln.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<Response<DeleteCategoryDto>>
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Response<DeleteCategoryDto>>
    {
        private readonly ICategoryGenericRepository _CategoryRepository;
        private readonly IMapper _mapper;

        public DeleteCategoryCommandHandler(ICategoryGenericRepository categoryRepository, IMapper mapper)
        {
            _CategoryRepository = categoryRepository; 
            _mapper = mapper;   
        }

        public async Task<Response<DeleteCategoryDto>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _CategoryRepository.GetByIdAsync(command.Id);
            if (category == null) throw new ApiException($"Product Not Found.");
            await _CategoryRepository.DeleteAsync(category);

            var response = _mapper.Map<DeleteCategoryDto>(category);

            return new Response<DeleteCategoryDto>(_mapper.Map<DeleteCategoryDto>(response), "Category remove successful");
        }
    }
}
