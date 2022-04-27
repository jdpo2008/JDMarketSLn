using AutoMapper;
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

namespace JdMarketSln.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<Response<GetCategoryByIdDto>>
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Response<GetCategoryByIdDto>>
    {
        private readonly ICategoryGenericRepository _CategoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(ICategoryGenericRepository categoryRepository, IMapper mapper)
        {
            _CategoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<Response<GetCategoryByIdDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var rq = _mapper.Map<GetCategoryByIdRequest>(request);
            var product = await _CategoryRepository.GetByIdAsync(rq.Id);
            var response = _mapper.Map<GetCategoryByIdDto>(product);
            return new Response<GetCategoryByIdDto>(response);
        }
    }
}
