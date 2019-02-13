using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace tivBudget.Dal.Models
{
    public partial class freebyTrackContext : DbContext
    {
        public freebyTrackContext(DbContextOptions<freebyTrackContext> options)
            : base(options)
        {

        }
    }
}
