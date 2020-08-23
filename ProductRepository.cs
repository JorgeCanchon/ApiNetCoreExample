using System;

namespace APIExample
{
    public class ProductRepository : RepositoryBase<ProductViewModel>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
