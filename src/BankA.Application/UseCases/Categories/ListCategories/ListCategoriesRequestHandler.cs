using BankA.Application.Interfaces;
using BankA.Application.Models;
using BankA.Application.UseCases.Categories.Models;
using BankA.Domain;
using BankA.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Categories.ListCategories
{
    class ListCategoriesRequestHandler : IRequestHandler<ListCategoriesRequest, ListCategoriesResponse>
    {
        private readonly IAppDataDbContext _bankADbContext;

        public ListCategoriesRequestHandler(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public async Task<ListCategoriesResponse> Handle(ListCategoriesRequest request, CancellationToken cancellationToken)
        {
            var query = _bankADbContext.Categories.Include(i => i.Parent).AsNoTracking();

            query = FilterQuery(query, request);

            var result = query
                            .Select(s => new CategoryModel()
                            {
                                Id = s.Id,
                                CategoryName = s.CategoryName,
                                CategoryFullName = s.CategoryFullName,
                                ParentId = s.Parent.Id,
                                ParentCategoryName = s.Parent.CategoryName,
                                CategoryType = s.CategoryType.ToString(),
                            })
                            .OrderBy(a => a.ParentCategoryName).ThenBy(s => s.CategoryName)
                            .PagedList(request.PageIndex, request.PageSize);

            return await Task.FromResult(new ListCategoriesResponse(result));
        }

        private IQueryable<Category> FilterQuery(IQueryable<Category> query, ListCategoriesRequest request)
        {
            if (!string.IsNullOrEmpty(request.CategoryType))
            {
                query = query.Where(x => x.CategoryType == Enum.Parse<CategoryTypeEnum>(request.CategoryType, true));
            }

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(q => q.CategoryName.Contains(request.Search, System.StringComparison.InvariantCultureIgnoreCase)
                            || q.Parent.CategoryName.Contains(request.Search, System.StringComparison.InvariantCultureIgnoreCase));
            }

            if (request.IsParent)
            {
                query = query.Where(q => q.Parent == null);
            }
            else
            {
                query = query.Where(q => q.Parent != null);
            }

            return query;
        }
    }
}
