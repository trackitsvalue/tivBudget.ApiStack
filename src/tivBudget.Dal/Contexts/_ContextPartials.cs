using freebyTech.Common.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static tivBudget.Dal.Models.ModelExtensions;

namespace tivBudget.Dal.Models
{
    public static class ModelExtensions
    {
        /// <summary>
        /// Applies general configruation to model classes that derive from IEditableModel.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        public class EditableModelConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEditableModel
        {
            public void Configure(EntityTypeBuilder<TEntity> builder)
            {
                builder.Ignore(p => p.IsNew);
                builder.Ignore(p => p.IsDirty);

                builder.Property(p => p.CreatedOn)
                    .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
                builder.Property(p => p.CreatedBy)
                    .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            }
        }

        public static void ApplyEditableModelConfigurations(this ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes()
                .Where(t => typeof(IEditableModel).IsAssignableFrom(t.ClrType));
            foreach (var entityType in entityTypes)
            {
                var configurationType = typeof(EditableModelConfiguration<>)
                    .MakeGenericType(entityType.ClrType);
                modelBuilder.ApplyConfiguration(
                    (dynamic)Activator.CreateInstance(configurationType));
            }
        }
    }

    public partial class freebyTrackContext : DbContext
    {
        partial void OnModelCreatingExt(ModelBuilder modelBuilder)
        {
            


        }
    }
}
