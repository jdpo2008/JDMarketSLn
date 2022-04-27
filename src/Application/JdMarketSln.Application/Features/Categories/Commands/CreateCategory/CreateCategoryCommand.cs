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

namespace JdMarketSln.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<Response<CreateCategoryDto>>
    {
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string CategoryDescription { get; set; }
    }


    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<CreateCategoryDto>>
    {
        private readonly ICategoryGenericRepository _CategoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryGenericRepository categoryRepository, IMapper mapper)
        {
            _CategoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<CreateCategoryDto>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            Category response = await _CategoryRepository.CreateAsync(_mapper.Map<Category>(command));

            return new Response<CreateCategoryDto>(_mapper.Map<CreateCategoryDto>(response), "Category created successfull");
        }
    }

}
