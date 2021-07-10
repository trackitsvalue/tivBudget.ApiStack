using freebyTech.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using tivBudget.Dal.SimpleModels;
using tivBudget.Dal.Models;
using tivBudget.Dal.Repositories.Interfaces;

namespace tivBudget.Dal.Repositories
{
  public class ReportRepository : GenericReadOnlyRepository<ReportCategory>, IReportRepository
  {
    public ReportRepository(freebyTrackContext dbContext) : base(dbContext)
    {
    }

    public List<SimpleReportCategory> FindAllReports()
    {
      return Queryable().Select(rc => new SimpleReportCategory()
      {
        Id = rc.Id,
        Name = rc.Name,
        DisplayIndex = rc.DisplayIndex,
        Reports = rc.Reports.Select(r => new SimpleReport()
        {
          Id = r.Id,
          Description = r.Description,
          StoredProcedure = r.StoredProcedure,
          ReportHelper = r.ReportHelper,
          CategoryId = r.CategoryId,
          DisplayIndex = r.DisplayIndex,
          ReportControls = r.ReportControls.Select(src => new SimpleReportControl()
          {
            Id = src.Id,
            ReportId = src.ReportId,
            Description = src.Description,
            Html = src.Html,
            DisplayIndex = src.DisplayIndex,
          }).OrderBy(src => src.DisplayIndex).ToList(),
        }).OrderBy(r => r.DisplayIndex).ToList(),
      }).OrderBy(rc => rc.DisplayIndex).ToList();
    }

    // public SimpleReportCategory FindCategoryReports(Guid categoryId)
    // {
    //   // return Queryable().Select(vc => new SimpleVideoCategory()
    //   // {
    //   //   Id = vc.Id,
    //   //   Description = vc.Description,
    //   //   Videos = vc.Videos.Select(v => new SimpleVideo
    //   //   {
    //   //     Id = v.Id,
    //   //     Title = v.Title,
    //   //     VideoEmbed = v.VideoEmbed,
    //   //     Description = v.Description,
    //   //     CategoryId = v.CategoryId
    //   //   }).OrderBy(v => v.Description).ToList(),
    //   // }).FirstOrDefault(vc => vc.Id == categoryId);
    // }
  }
}