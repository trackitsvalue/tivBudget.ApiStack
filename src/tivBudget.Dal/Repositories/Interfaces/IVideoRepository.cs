using System;
using System.Collections.Generic;
using tivBudget.Dal.SimpleModels;

namespace tivBudget.Dal.Repositories.Interfaces
{
  public interface IVideoRepository
  {
    List<SimpleVideoCategory> FindAllVideos();

    SimpleVideoCategory FindCategoryVideos(Guid categoryId);

    SimpleVideoCategory FindVideo(Guid categoryId, Guid videoId);
  }
}