using BankA.Application.Interfaces;
using BankA.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Categories.GetCategory
{
    class GetCategoryRequestHandler : IRequestHandler<GetCategoryRequest, GetCategoryResponse>
    {
        private readonly IAppDataDbContext _bankADbContext;

        public GetCategoryRequestHandler(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public async Task<GetCategoryResponse> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await GetCategory(request);

            return new GetCategoryResponse()
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                CategoryFullName = category.CategoryFullName,
                ParentId = category.Parent?.Id,
                ParentCategoryName = category.Parent?.CategoryName,
                CategoryType = category.CategoryType.ToString()
            };
        }

        private async Task<Category> GetCategory(GetCategoryRequest request)
        {
            var category = await _bankADbContext.GetCategoryById(request.CategoryId);
            if (category == null)
                throw new ApplicationException($"Category '{request.CategoryId}' not found");
            return category;
        }
    }
}
