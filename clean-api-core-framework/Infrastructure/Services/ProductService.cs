using Application.DTOs;
using Application.Interfaces;
using Application.Products.Commands;
using AutoMapper;
using MediatR;

namespace Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<int> CreateProductAsync(ProductCreateDto dto, CancellationToken cancellationToken)
        {
            // Convert ProductCreateDto to CreateProductCommand
            var command = new CreateProductCommand
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                Image = dto.Image
            };

            // Call the handler with the command
            return await _mediator.Send(command, cancellationToken);
        }

        public Task<IEnumerable<ProductCreateDto>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsProductNameUniqueAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}