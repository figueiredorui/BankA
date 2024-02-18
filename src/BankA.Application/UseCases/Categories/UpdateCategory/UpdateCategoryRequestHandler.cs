using BankA.Application.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Categories.UpdateCategory
{
    class UpdateCategoryRequestHandler : IRequestHandler<UpdateCategoryRequest, bool>
    {
        private readonly IAppDataDbContext _bankADbContext;

        public UpdateCategoryRequestHandler(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public async Task<bool> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await _bankADbContext.GetCategoryById(request.CategoryId);
            if (category == null)
                throw new ApplicationException($"Category '{request.CategoryId}' not found");

            if (request.ParentId > 0)
            {
                var parentCategory = await _bankADbContext.GetCategoryById(request.ParentId);
                if (parentCategory == null)
                    throw new ApplicationException($"ParentCategory '{request.ParentId}' not found");

                category.Change(request.CategoryName, parentCategory);
            }
            else
            {
                category.Change(request.CategoryName, request.CategoryType);
            }

            await _bankADbContext.SaveChangesAsync();
            return true;
        }
    }
}
