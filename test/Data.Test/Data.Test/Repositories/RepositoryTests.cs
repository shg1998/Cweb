using Data.Contracts;
using Data.Repositories;
using Entities.Common;
using Entities.History;
using Entities.User;
using Microsoft.EntityFrameworkCore;
using Moq;
using NSubstitute;
using System.Net.Sockets;

namespace Data.Test.Repositories
{
    public class RepositoryTests
    {
        private Mock<ApplicationDbContext> _dbContextMock;
        private Mock<DbSet<Parameter>> _dbSetMock;

        public RepositoryTests()
        {
            this._dbContextMock = new Mock<ApplicationDbContext>();
            this._dbSetMock = new Mock<DbSet<Parameter>>();
            this._dbContextMock.Setup(db => db.Set<Parameter>()).Returns(this._dbSetMock.Object);
        }

        [Fact]
        public async Task AddAsync_AddsEntityToDbSetAndSavesChanges()
        {
            // Arrange
            var parameter = new Parameter
            {
                Id = 1,
                DateTime = DateTime.Now.Ticks,
                CentralId = 1,
                BedId = "2",
                Hr = "54",
                Awrr = "11",
                Dt = "ss",
                EtcO2 = "12",
                FiCo2 = "54",
                Ibp1Dia = "24",
                Ibp1Map = "54",
                Ibp1Sys = "dkhj",
                Ibp2Dia = "23",
                Ibp2Map = "12",
                Ibp2Sys = "87",
                NibpDia = "87",
                Spo2 = "22",
                NibpMap = "122",
                NibpSys = "22",
                Rr = "32",
                T1 = "1",
                T2 = "32"
            };
            var repo = new Repository<Parameter>(_dbContextMock.Object);
            // Act
            await repo.AddAsync(parameter, CancellationToken.None);

            // Assert
            this._dbSetMock.Verify(d => d.AddAsync(parameter, CancellationToken.None));
            this._dbContextMock.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task AddRangeAsync_AddsEntitiesToDbSetAndSavesChanges()
        {
            // Arrange
            var mockDbContext = new Mock<ApplicationDbContext>();
            var mockDbSet = new Mock<DbSet<Parameter>>();
            mockDbContext.Setup(db => db.Set<Parameter>()).Returns(mockDbSet.Object);

            var repository = new Repository<Parameter>(mockDbContext.Object);

            var entities = new List<Parameter>
            {
                new() {
                    Id = 1,
                    BedId = "2"
                },
                  new() {
                    Id = 2,
                    BedId = "33"
                },
                   new() {
                    Id = 3,
                    BedId = "12"
                }
            };

            // Act
            await repository.AddRangeAsync(entities, CancellationToken.None);

            // Assert
            mockDbSet.Verify(d => d.AddRangeAsync(entities, CancellationToken.None));
            mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task GetByIdAsync_Returns_Entity()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            var entityId = 1;
            var entity = new Mock<IEntity>();
            var dbContextMock = new Mock<ApplicationDbContext>();
            var dbSetMock = new Mock<DbSet<IEntity>>();
            dbContextMock.Setup(db => db.Set<IEntity>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(d => d.FindAsync(It.IsAny<object[]>(), cancellationToken)).ReturnsAsync(entity.Object);
            var repository = new Repository<IEntity>(dbContextMock.Object);

            // Act
            var result = await repository.GetByIdAsync(cancellationToken, entityId);

            // Assert
            Assert.NotNull(result);
            Assert.Same(entity.Object, result);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesEntityAndSavesChanges()
        {
            // Arrange
            this._dbContextMock.Setup(db => db.Set<Parameter>()).Returns(this._dbSetMock.Object);

            var repository = new Repository<Parameter>(_dbContextMock.Object);

            var existingEntity = new Parameter { Id = 1, BedId = "11", Spo2 = "25" };
            this._dbSetMock.Setup(m => m.FindAsync(1)).ReturnsAsync(existingEntity);

            var updatedEntity = new Parameter { Id = 1, BedId = "11", Spo2 = "22" };

            // Act
            await repository.UpdateAsync(updatedEntity, CancellationToken.None);

            // Assert
            this._dbSetMock.Verify(m => m.Update(updatedEntity), Times.Once);
            this._dbContextMock.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task UpdateRangeAsync_UpdatesValidEntitiesAndSavesChanges()
        {
            // Arrange
            var entities = new List<Parameter>
        {
            new() { Id = 1, BedId = "OldName" },
            new() { Id = 2, BedId = "AnotherName" }
        };

            _dbSetMock.Setup(m => m.UpdateRange(entities));
            var repository = new Repository<Parameter>(_dbContextMock.Object);
            this._dbSetMock.Setup(m => m.FindAsync(1)).ReturnsAsync(entities[0]);

            // Act
            await repository.UpdateRangeAsync(entities, CancellationToken.None);

            // Assert
            _dbSetMock.Verify(m => m.UpdateRange(entities));
            _dbContextMock.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task DeleteAsync_RemovesEntityAndSavesChanges_WhenSaveNowIsTrue()
        {
            // Arrange
            var entity = new Mock<IEntity>();
            var cancellationToken = CancellationToken.None;
            var dbContextMock = new Mock<ApplicationDbContext>();
            var dbSetMock = new Mock<DbSet<IEntity>>();
            dbContextMock.Setup(db => db.Set<IEntity>()).Returns(dbSetMock.Object);
            var repository = new Repository<IEntity>(dbContextMock.Object);

            // Act
            await repository.DeleteAsync(entity.Object, cancellationToken, saveNow: true);

            // Assert
            dbSetMock.Verify(d => d.Remove(entity.Object), Times.Once);
            dbContextMock.Verify(db => db.SaveChangesAsync(cancellationToken), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_RemovesEntityAndDoesNotSaveChanges_WhenSaveNowIsFalse()
        {
            // Arrange
            var entity = new Mock<IEntity>();
            var cancellationToken = CancellationToken.None;
            var dbContextMock = new Mock<ApplicationDbContext>();
            var dbSetMock = new Mock<DbSet<IEntity>>();
            dbContextMock.Setup(db => db.Set<IEntity>()).Returns(dbSetMock.Object);
            var repository = new Repository<IEntity>(dbContextMock.Object);

            // Act
            await repository.DeleteAsync(entity.Object, cancellationToken, saveNow: false);

            // Assert
            dbSetMock.Verify(d => d.Remove(entity.Object), Times.Once);
            dbContextMock.Verify(db => db.SaveChangesAsync(cancellationToken), Times.Never);
        }

        [Fact]
        public async Task DeleteRangeAsync_RemovesEntitiesAndSavesChanges_WhenSaveNowIsTrue()
        {
            // Arrange
            var entities = new List<IEntity>();
            var cancellationToken = CancellationToken.None;
            var dbContextMock = new Mock<ApplicationDbContext>();
            var dbSetMock = new Mock<DbSet<IEntity>>();
            dbContextMock.Setup(db => db.Set<IEntity>()).Returns(dbSetMock.Object);
            var repository = new Repository<IEntity>(dbContextMock.Object);

            // Act
            await repository.DeleteRangeAsync(entities, cancellationToken, saveNow: true);

            // Assert
            dbSetMock.Verify(d => d.RemoveRange(entities), Times.Once);
            dbContextMock.Verify(db => db.SaveChangesAsync(cancellationToken), Times.Once);
        }

        [Fact]
        public async Task DeleteRangeAsync_RemovesEntitiesAndDoesNotSaveChanges_WhenSaveNowIsFalse()
        {
            // Arrange
            var entities = new List<IEntity>();
            var cancellationToken = CancellationToken.None;
            var dbContextMock = new Mock<ApplicationDbContext>();
            var dbSetMock = new Mock<DbSet<IEntity>>();
            dbContextMock.Setup(db => db.Set<IEntity>()).Returns(dbSetMock.Object);
            var repository = new Repository<IEntity>(dbContextMock.Object);

            // Act
            await repository.DeleteRangeAsync(entities, cancellationToken, saveNow: false);

            // Assert
            dbSetMock.Verify(d => d.RemoveRange(entities), Times.Once);
            dbContextMock.Verify(db => db.SaveChangesAsync(cancellationToken), Times.Never);
        }

        [Fact]
        public void GetById_Returns_Entity()
        {
            // Arrange
            var entityId = 1;
            var entity = new Mock<IEntity>();
            var dbContextMock = new Mock<ApplicationDbContext>();
            var dbSetMock = new Mock<DbSet<IEntity>>();
            dbContextMock.Setup(db => db.Set<IEntity>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(d => d.Find(entityId)).Returns(entity.Object);

            // Setup Repository
            var repository = new Repository<IEntity>(dbContextMock.Object);

            // Act
            var result = repository.GetById(entityId);

            // Assert
            Assert.NotNull(result);
            Assert.Same(entity.Object, result);
        }

        [Fact]
        public void Add_AddsEntityAndSavesChanges_WhenSaveNowIsTrue()
        {
            // Arrange
            var entity = new Mock<IEntity>();
            var dbContextMock = new Mock<ApplicationDbContext>();
            var dbSetMock = new Mock<DbSet<IEntity>>();
            dbContextMock.Setup(db => db.Set<IEntity>()).Returns(dbSetMock.Object);
            var repository = new Repository<IEntity>(dbContextMock.Object);

            // Act
            repository.Add(entity.Object, saveNow: true);

            // Assert
            dbSetMock.Verify(d => d.Add(entity.Object), Times.Once);
            dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Add_AddsEntityAndDoesNotSaveChanges_WhenSaveNowIsFalse()
        {
            // Arrange
            var entity = new Mock<IEntity>();
            var dbContextMock = new Mock<ApplicationDbContext>();
            var dbSetMock = new Mock<DbSet<IEntity>>();
            dbContextMock.Setup(db => db.Set<IEntity>()).Returns(dbSetMock.Object);
            var repository = new Repository<IEntity>(dbContextMock.Object);

            // Act
            repository.Add(entity.Object, saveNow: false);

            // Assert
            dbSetMock.Verify(d => d.Add(entity.Object), Times.Once);
            dbContextMock.Verify(db => db.SaveChanges(), Times.Never);
        }

        [Fact]
        public void AddRange_AddsEntitiesAndSavesChanges_WhenSaveNowIsTrue()
        {
            // Arrange
            var entities = new List<IEntity>();
            var dbContextMock = new Mock<ApplicationDbContext>();
            var dbSetMock = new Mock<DbSet<IEntity>>();
            dbContextMock.Setup(db => db.Set<IEntity>()).Returns(dbSetMock.Object);
            var repository = new Repository<IEntity>(dbContextMock.Object);

            // Act
            repository.AddRange(entities, saveNow: true);

            // Assert
            dbSetMock.Verify(d => d.AddRange(entities), Times.Once);
            dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Fact]
        public void AddRange_AddsEntitiesAndDoesNotSaveChanges_WhenSaveNowIsFalse()
        {
            // Arrange
            var entities = new List<IEntity>();
            var dbContextMock = new Mock<ApplicationDbContext>();
            var dbSetMock = new Mock<DbSet<IEntity>>();
            dbContextMock.Setup(db => db.Set<IEntity>()).Returns(dbSetMock.Object);
            var repository = new Repository<IEntity>(dbContextMock.Object);

            // Act
            repository.AddRange(entities, saveNow: false);

            // Assert
            dbSetMock.Verify(d => d.AddRange(entities), Times.Once);
            dbContextMock.Verify(db => db.SaveChanges(), Times.Never);
        }

        [Fact]
        public void Update_UpdatesEntityAndSavesChanges_WhenSaveNowIsTrue()
        {
            // Arrange
            var entity = new Mock<IEntity>();
            var dbContextMock = new Mock<ApplicationDbContext>();
            var dbSetMock = new Mock<DbSet<IEntity>>();
            dbContextMock.Setup(db => db.Set<IEntity>()).Returns(dbSetMock.Object);
            var repository = new Repository<IEntity>(dbContextMock.Object);

            // Act
            repository.Update(entity.Object, saveNow: true);

            // Assert
            dbSetMock.Verify(d => d.Update(entity.Object), Times.Once);
            dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Update_UpdatesEntityAndDoesNotSaveChanges_WhenSaveNowIsFalse()
        {
            // Arrange
            var entity = new Mock<IEntity>();
            var dbContextMock = new Mock<ApplicationDbContext>();
            var dbSetMock = new Mock<DbSet<IEntity>>();
            dbContextMock.Setup(db => db.Set<IEntity>()).Returns(dbSetMock.Object);
            var repository = new Repository<IEntity>(dbContextMock.Object);

            // Act
            repository.Update(entity.Object, saveNow: false);

            // Assert
            dbSetMock.Verify(d => d.Update(entity.Object), Times.Once);
            dbContextMock.Verify(db => db.SaveChanges(), Times.Never);
        }
    }
}
