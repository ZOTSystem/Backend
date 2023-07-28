using be.Models;

namespace be.Repositories.NewsRepository
{
    public class NewsRepository : INewsRepository
    {
        private readonly DbZotsystemContext _context;

        public NewsRepository()
        {
            _context = new DbZotsystemContext();
        }

        public object GetAllNews()
        {
            var data = from news in _context.News
                       select new
                       {
                           newsId = news.NewId,
                           image = news.Image,
                           title = news.Title,
                           accountName = news.Account.FullName,
                           accountId = news.AccountId,
                           createDate = news.CreateDate,
                           status = news.Status,
                           categoryName = news.NewCategory.CategoryName,
                           categoryId = news.NewCategoryId,
                       };
            if(data == null)
            {
                return new
                {
                    message = "No data to return",
                    status = 400,
                };
            }
            return new
            {
                message = "Get data successfully",
                status = 200,
                data,
            };
        }

        public object GetAllNewsCategory()
        {
            var data = _context.Newcategorys;
            if(data == null)
            {
                return new
                {
                    message = "No data to return",
                    status = 400,
                };
            }
            return new
            {
                message = "Get data successfully",
                status = 200,
                data,
            };
        }
    }
}
