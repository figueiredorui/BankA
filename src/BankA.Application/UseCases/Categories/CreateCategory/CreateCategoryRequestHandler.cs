using BankA.Application.Interfaces;
using BankA.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Categories.CreateCategory
{
    class CreateCategoryRequestHandler : IRequestHandler<CreateCategoryRequest, CreateCategoryResponse>
    {
        private readonly IAppDataDbContext _bankADbContext;

        public CreateCategoryRequestHandler(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public async Task<CreateCategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            Category category = null;
            if (request.ParentId.HasValue && request.ParentId.Value > 0)
            {
                var parentCategory = await _bankADbContext.GetCategoryById(request.ParentId.Value);
                if (parentCategory == null)
                    throw new ApplicationException($"ParentCategory '{request.ParentId}' not found");

                category = new Category(request.CategoryName, parentCategory);
                _bankADbContext.Categories.Add(category);
            }
            else
            {
                category = new Category(request.CategoryName, request.CategoryType);
                _bankADbContext.Categories.Add(category);
            }

            await _bankADbContext.SaveChangesAsync();

            return new CreateCategoryResponse() { Id = category.Id };
        }
    }
}
