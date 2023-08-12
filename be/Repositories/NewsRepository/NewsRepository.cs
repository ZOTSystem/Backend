using be.DTOs;
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

        public object Addews(NewsDTO newsDTO)
        {
            var news = new News();
            var category = _context.Newcategorys.FirstOrDefault(x => x.CategoryName.Contains(newsDTO.CategoryName));  
            news.NewCategoryId = category.NewCategoryId;
            news.AccountId = newsDTO.AccountId;
            news.Title = newsDTO.Title;
            news.Subtitle = newsDTO.SubTitle;
            news.Image = newsDTO.Image;
            news.Content = newsDTO.Content;
            news.CreateDate = DateTime.Now;
            news.Status = "0";
            _context.News.Add(news);
            _context.SaveChanges();
            return new
            {
                message = "Add News Succfully",
                status = 200,
            };
        }

        public object ChangeStatusNews(int newsId, string status)
        {
            var news = _context.News.FirstOrDefault(x => x.NewId == newsId);
            if(news == null)
            {
                return new
                {
                    message = "Not found to change",
                    status = 400,
                };
            }
            news.Status = status;
            _context.SaveChanges();
            return new
            {
                message = "Change status successfully",
                status = 200,
            };
        }

        public object EditNews(NewsDTO newsDTO)
        {
            var news = _context.News.SingleOrDefault(x => x.NewId == newsDTO.NewsId);   
            if(news == null)
            {
                return new
                {
                    message = "Not found to return",
                    status = 400,
                };
            }
            var editCategory = _context.Newcategorys.FirstOrDefault(x => x.CategoryName.Contains(newsDTO.CategoryName));
            news.NewCategoryId = editCategory.NewCategoryId;
            news.Title = newsDTO.Title;
            news.Subtitle = newsDTO.SubTitle;
            news.Image = newsDTO.Image;
            news.Content = newsDTO.Content;
            _context.SaveChanges();
            return new
            {
                message = "Edit Successfully",
                status = 200,
                data = news,
            };
        }

        public object GetAllNews()
        {
            var result = from news in _context.News
                       select new
                       {
                           newsId = news.NewId,
                           image = news.Image,
                           title = news.Title,
                           subTitle = news.Subtitle,
                           accountName = news.Account.FullName,
                           accountId = news.AccountId,
                           createDate = news.CreateDate,
                           status = news.Status,
                           categoryName = news.NewCategory.CategoryName,
                           categoryId = news.NewCategoryId,
                           content = news.Content,
                       };
            var data = result.OrderByDescending(x => x.newsId);
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
