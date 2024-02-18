using BankA.Domain.Enums;
using System;

namespace BankA.Domain
{
    public partial class Category : EntityBase
    {
        public Category Parent { get; private set; }

        public string CategoryName { get; private set; }

        public CategoryTypeEnum CategoryType { get; private set; }

        public bool IsSystem { get; private set; }

        public string CategoryFullName
        {
            get
            {
                if (Parent != null)
                    return $"{Parent.CategoryName} > {CategoryName}";
                else
                    return $"{CategoryName} > {CategoryName}";
            }
        }

        public bool IsExpense { get { return CategoryType == CategoryTypeEnum.Expense; } }
        public bool IsIncome { get { return CategoryType == CategoryTypeEnum.Income; } }
        public bool IsTransfer { get { return CategoryType == CategoryTypeEnum.Transfer; } }

        private Category()
        {

        }
        public Category(string categoryName, CategoryTypeEnum categoryType, bool isSystem)
        {
            CategoryName = categoryName;
            CategoryType = categoryType;
            IsSystem = isSystem;
            Parent = null;
        }

        public Category(string categoryName, string categoryType)
        {
            CategoryName = categoryName;
            CategoryType = ParseCategoryTypeEnum(categoryType);
        }

        public Category(string categoryName, Category parent)
        {
            if (string.IsNullOrEmpty(categoryName))
                throw new ArgumentNullException("categoryName");

            if (parent == null)
                throw new ArgumentNullException("parent");

            CategoryName = categoryName;
            Parent = parent;
            CategoryType = parent.CategoryType;
        }

        public void Change(string categoryName, Category parent)
        {
            CategoryName = categoryName;
            CategoryType = parent.CategoryType;
            Parent = parent;
        }

        public void Change(string categoryName, string categoryType)
        {
            CategoryName = categoryName;
            CategoryType = ParseCategoryTypeEnum(categoryType);
            Parent = null;
        }

        private CategoryTypeEnum ParseCategoryTypeEnum(string categoryType)
        {
            try
            {
                var categoryTypeEnum = (CategoryTypeEnum)Enum.Parse(typeof(CategoryTypeEnum), categoryType, true);
                return categoryTypeEnum;
            }
            catch (Exception)
            {
                throw new ApplicationException($"Invalid File Format");
            }
        }
    }
}
