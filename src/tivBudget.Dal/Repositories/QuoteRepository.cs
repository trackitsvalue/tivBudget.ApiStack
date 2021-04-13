using freebyTech.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using tivBudget.Dal.SimpleModels;
using tivBudget.Dal.Models;
using tivBudget.Dal.Repositories.Interfaces;

namespace tivBudget.Dal.Repositories
{
  public class QuoteRepository : GenericReadOnlyRepository<Quote>, IQuoteRepository
  {
    public QuoteRepository(freebyTrackContext dbContext) : base(dbContext)
    {
    }

    public IEnumerable<SimpleQuote> FindAllQuotes()
    {
      return Queryable().Select(q => new SimpleQuote
      {
        Id = q.Id,
        Source = q.Source,
        Text = q.Text,
      });
    }
  }
}