using Example.Domain.Queries.Implementation;
using Example.Domain.Repositories.Interfaces;
using Example.Domain.Repositories.Models;
using Example.Domain.Shared.Interfaces;
using Example.Domain.Shared.Models;
using FluentAssertions;
using NSubstitute;

namespace Example.Domain.UnitTests.Queries
{
    public class DefaultFieldQueriesTests
    {
        [Fact]
        public async Task List_ShouldReturnEmptyList_WhenRepositoryReturnsNoResults()
        {
            const int pageSize = 10;
            const int pageNumber = 2;

            var results = new List<ReadOnlyFieldModel>();

            var repository = Substitute.For<IFieldRepository>();

            repository.List(Arg.Any<PagedEmployerFilter>()).Returns(Task.FromResult((IPager<ReadOnlyFieldModel>)new Pager<ReadOnlyFieldModel>(pageNumber, pageSize, results)));

            var queries = new DefaultFieldQueries(repository);

            var sut = await queries.List(new PagedEmployerFilter());

            sut.Records.Should().BeEmpty();
            sut.Page.Should().Be(pageNumber);
            sut.PageSize.Should().Be(pageSize);
            sut.ActualPageSize.Should().Be(results.Count);
        }

        [Fact]
        public async Task List_ShouldReturnEmptyList_WhenRepositoryReturnsResults()
        {
            const int pageSize = 10;
            const int pageNumber = 2;

            var results = new List<ReadOnlyFieldModel>
            {
                new ReadOnlyFieldModel(Guid.NewGuid(), "A", "B"),
                new ReadOnlyFieldModel(Guid.NewGuid(), "C", "D")
            };

            var repository = Substitute.For<IFieldRepository>();

            repository
                .List(Arg.Any<PagedEmployerFilter>())
                .Returns(Task.FromResult((IPager<ReadOnlyFieldModel>)new Pager<ReadOnlyFieldModel>(pageNumber, pageSize, results)));

            var queries = new DefaultFieldQueries(repository);

            var sut = await queries.List(new PagedEmployerFilter());

            sut.Records.Should().NotBeEmpty();
            sut.Page.Should().Be(pageNumber);
            sut.PageSize.Should().Be(pageSize);
            sut.ActualPageSize.Should().Be(results.Count);

            foreach(var item in results)
            {
                sut.Records.Should().Contain(model => model.Id == item.Id && model.Name.Equals(item.Name, StringComparison.Ordinal) &&  model.Type.Equals(item.Type, StringComparison.Ordinal));
            }
        }
    }
}
