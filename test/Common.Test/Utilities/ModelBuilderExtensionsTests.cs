using Common.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Test.Utilities
{
    public class Entity1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Entity2> Entities2 { get; set; }
    }

    public class Entity2
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int YourEntity1Id { get; set; }
        public Entity1 YourEntity1 { get; set; }
    }

    public class ModelBuilderExtensionsTests
    {
        [Fact]
        public void AddSingularizingTableNameConvention_SetsSingularizedTableName()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();

            // Act
            modelBuilder.AddSingularizingTableNameConvention();

            // Assert
            var entityTypes = modelBuilder.Model.GetEntityTypes();
            foreach (var entityType in entityTypes)
            {
                var tableName = entityType.GetTableName();
                Xunit.Assert.False(tableName.EndsWith("s"));
            }
        }

        [Fact]
        public void AddSequentialGuidForIdConvention_SetsSequentialGuidForId()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();

            // Act
            modelBuilder.AddSequentialGuidForIdConvention();

            // Assert
            var entityTypes = modelBuilder.Model.GetEntityTypes();
            foreach (var entityType in entityTypes)
            {
                var properties = entityType.GetProperties();
                foreach (var property in properties)
                {
                    if (property.Name == "Id" && property.ClrType == typeof(Guid))
                    {
                        var defaultValueSql = property.GetDefaultValueSql();
                        Xunit.Assert.Equal("NEWSEQUENTIALID()", defaultValueSql);
                        return;
                    }
                }
            }
        }

        [Fact]
        public void AddRestrictDeleteBehaviorConvention_SetsRestrictForCascadeDeleteBehaviors()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();
            modelBuilder.Entity<Entity1>().HasOne<Entity2>().WithMany().OnDelete(DeleteBehavior.Cascade);

            // Act
            modelBuilder.AddRestrictDeleteBehaviorConvention();

            // Assert
            var entityType = modelBuilder.Model.FindEntityType(typeof(Entity1));
            var foreignKey = entityType.GetForeignKeys().FirstOrDefault();
            Xunit.Assert.Equal(DeleteBehavior.Restrict, foreignKey?.DeleteBehavior);
        }

        [Fact]
        public void RegisterEntityTypeConfiguration_RegistersConfigurations()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();
            var assembly = Assembly.GetExecutingAssembly();

            // Act
            modelBuilder.RegisterEntityTypeConfiguration(assembly);

            // Assert
            Xunit.Assert.Empty(modelBuilder.Model.GetEntityTypes());
        }

        [Fact]
        public void RegisterAllEntities_RegistersEntities()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();
            var assembly = Assembly.GetExecutingAssembly();

            // Act
            modelBuilder.RegisterAllEntities<Entity1>(assembly);

            // Assert
            Xunit.Assert.NotEmpty(modelBuilder.Model.GetEntityTypes());
        }
    }
}
