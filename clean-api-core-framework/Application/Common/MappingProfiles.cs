using Application.DTOs;
using Application.Products.Commands;
using Application.Products.Commands.UpdateProduct;
using AutoMapper;
using Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Common
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateProductCommand, ProductCreateDto>();
            CreateMap<UpdateProductCommand, ProductUpdateDto>();
            CreateMap<Product, ProductDto>();
        }
    }
}