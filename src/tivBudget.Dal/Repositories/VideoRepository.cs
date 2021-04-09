using freebyTech.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using tivBudget.Dal.SimpleModels;
using tivBudget.Dal.Models;
using tivBudget.Dal.Repositories.Interfaces;

namespace tivBudget.Dal.Repositories
{
  public class VideoRepository : GenericReadOnlyRepository<VideoCategory>, IVideoRepository
  {
    public VideoRepository(freebyTrackContext dbContext) : base(dbContext)
    {
    }

    public List<SimpleVideoCategory> FindAllVideos()
    {
      return Queryable().Select(vc => new SimpleVideoCategory()
      {
        Id = vc.Id,
        Description = vc.Description,
        Videos = vc.Videos.Select(v => new SimpleVideo
        {
          Id = v.Id,
          Title = v.Title,
          VideoEmbed = v.VideoEmbed,
          Description = v.Description,
          CategoryId = v.CategoryId
        }).OrderBy(v => v.Description).ToList(),
      }).OrderBy(vc => vc.Description).ToList();
    }

    public SimpleVideoCategory FindCategoryVideos(Guid categoryId)
    {
      return Queryable().Select(vc => new SimpleVideoCategory()
      {
        Id = vc.Id,
        Description = vc.Description,
        Videos = vc.Videos.Select(v => new SimpleVideo
        {
          Id = v.Id,
          Title = v.Title,
          VideoEmbed = v.VideoEmbed,
          Description = v.Description,
          CategoryId = v.CategoryId
        }).OrderBy(v => v.Description).ToList(),
      }).FirstOrDefault(vc => vc.Id == categoryId);
    }

    public SimpleVideoCategory FindVideo(Guid categoryId, Guid videoId)
    {
      return Queryable().Select(vc => new SimpleVideoCategory()
      {
        Id = vc.Id,
        Description = vc.Description,
        Videos = vc.Videos.Select(v => new SimpleVideo
        {
          Id = v.Id,
          Title = v.Title,
          VideoEmbed = v.VideoEmbed,
          Description = v.Description,
          CategoryId = v.CategoryId
        }).Where(v => v.Id == videoId).ToList(),
      }).FirstOrDefault(vc => vc.Id == categoryId);
    }
  }
}