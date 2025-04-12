using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _repository;
        private readonly IFileProcessingHandler _fileProcessingHandler;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(
            IProductRepository repository,
            IFileProcessingHandler fileProcessingHandler,
            IMapper mapper)
        {
            _repository = repository;
            _fileProcessingHandler = fileProcessingHandler;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // validate or business logic
            var isUnique = await _repository.IsProductNameUniqueAsync(request.Name, cancellationToken);
            if (!isUnique)
                throw new Exception("Product name already exists");

            // Ensure the Image is not null before processing
            if (request.Image == null)
                throw new ArgumentNullException(nameof(request.Image), "Image file cannot be null");

            // Process the iamge here.
            //var imageData = await _fileProcessingHandler.ConvertFormFileToBlobAsync(request.Image);
            var imageData = await _fileProcessingHandler.StreamFileToTempAsync(request.Image);

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                //ImageData = imageData.data,
                ImageTempPath = imageData.tempFilePath,
                ImageContentType = imageData.contentType,
                ImageName = imageData.fileName
            };

            var response = await _repository.AddAsync(product, cancellationToken);
            // Delete the temp file.
            if (File.Exists(product.ImageTempPath))
                File.Delete(product.ImageTempPath);
            // Log the deletion or handle it as per your requirement
            // Console.WriteLine($"Temporary file {product.ImageTempPath} deleted.");
            return response;
        }

    }

}