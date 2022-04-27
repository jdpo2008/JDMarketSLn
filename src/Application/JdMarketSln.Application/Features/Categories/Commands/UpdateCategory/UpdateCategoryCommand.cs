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

namespace JdMarketSln.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<Response<UpdateCategoryDto>>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string CategoryDescription { get; set; }
    }


    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Response<UpdateCategoryDto>>
    {

        private readonly ICategoryGenericRepository _CategoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryGenericRepository categoryRepository, IMapper mapper)
        {
            _CategoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<UpdateCategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateCategoryRequest>(request);
            var category = await _CategoryRepository.GetByIdAsync(command.Id);

            if (category == null)
            {
                throw new ApiException($"Category Not Found.");
            }
            else
            {
                category.CategoryName = command.CategoryName;
                category.CategoryDescription = command.CategoryDescription;

                await _CategoryRepository.UpdateAsync(category);

                return new Response<UpdateCategoryDto>(_mapper.Map<UpdateCategoryDto>(category), "Category Updated Successufull");
            };
        }
    }

}
