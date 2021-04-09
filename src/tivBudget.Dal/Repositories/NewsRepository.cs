using freebyTech.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using tivBudget.Dal.SimpleModels;
using tivBudget.Dal.Models;
using tivBudget.Dal.Repositories.Interfaces;

namespace tivBudget.Dal.Repositories
{
  public class NewsRepository : GenericReadOnlyRepository<News>, INewsRepository
  {
    public NewsRepository(freebyTrackContext dbContext) : base(dbContext)
    {
    }

    public List<SimpleNews> FindAllNews()
    {
      return Queryable().Where(n => n.IsPublished).OrderByDescending(n => n.PublishedOn).Select(n => new SimpleNews
      {
        Id = n.Id,
        Title = n.Title,
        Text = n.Text,
        OwnerId = n.OwnerId,
        ItemType = n.ItemType,
        PublishedOn = n.PublishedOn
      }).ToList();
    }
  }
}