using Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Products.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile? Image { get; set; }
    }

}