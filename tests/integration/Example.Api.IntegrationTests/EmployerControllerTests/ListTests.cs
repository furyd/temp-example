using Example.Api.Constants;
using Example.Domain.Queries.Interfaces;
using Example.Domain.Queries.Models;
using Example.Domain.Shared.Interfaces;
using Example.Domain.Shared.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace Example.Api.IntegrationTests.EmployerControllerTests
{
    public class ListTests
    {
        [Fact]
        public async Task List_ReturnsCorrectHeadersAndStatucCode_WhenReturningRecords()
        {
            const int pageNumber = 1;
            const int pageSize = 20;

            var results = new List<FieldModel>
                    {
                        new FieldModel(Guid.NewGuid(), "A", "B", new List<FieldOptionModel>()),
                        new FieldModel(Guid.NewGuid(), "C", "D", new List<FieldOptionModel>())
                    };

            using var webApplicationFactory = Setup(pageNumber, pageSize, results);
            using var client = webApplicationFactory.CreateClient();

            var sut = await client.GetAsync($"/{Employer.Root}/{Guid.NewGuid():D}");

            sut.IsSuccessStatusCode.Should().BeTrue();

            sut.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            sut.Headers.TryGetValues(Headers.Page, out var pageHeader).Should().BeTrue();
            sut.Headers.TryGetValues(Headers.PageSize, out var pageSizeHeader).Should().BeTrue();
            sut.Headers.TryGetValues(Headers.Records, out var recordsHeader).Should().BeTrue();

            pageHeader?.Contains(pageNumber.ToString()).Should().BeTrue();
            pageSizeHeader?.Contains(pageSize.ToString()).Should().BeTrue();
            recordsHeader?.Contains(results.Count.ToString()).Should().BeTrue();
        }

        [Fact]
        public async Task List_ReturnsCorrectHeadersAndStatucCode_WhenReturningNoRecords()
        {
            const int pageNumber = 10;
            const int pageSize = 200;

            var results = Array.Empty<FieldModel>();

            using var webApplicationFactory = Setup(pageNumber, pageSize, results);
            using var client = webApplicationFactory.CreateClient();

            var sut = await client.GetAsync($"/{Employer.Root}/{Guid.NewGuid():D}");

            sut.IsSuccessStatusCode.Should().BeTrue();

            sut.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

            sut.Headers.TryGetValues(Headers.Page, out var pageHeader).Should().BeTrue();
            sut.Headers.TryGetValues(Headers.PageSize, out var pageSizeHeader).Should().BeTrue();
            sut.Headers.TryGetValues(Headers.Records, out var recordsHeader).Should().BeTrue();

            pageHeader?.Contains(pageNumber.ToString()).Should().BeTrue();
            pageSizeHeader?.Contains(pageSize.ToString()).Should().BeTrue();
            recordsHeader?.Contains(results.Length.ToString()).Should().BeTrue();
        }

        private static WebApplicationFactory<Program> Setup(int pageNumber, int pageSize, ICollection<FieldModel> results)
        {
            return new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(serviceCollection =>
                {
                    var fieldQueries = serviceCollection.Single(serviceDescriptor => serviceDescriptor.ServiceType == typeof(IFieldQueries));

                    serviceCollection.Remove(fieldQueries);

                    var mockFieldQueries = Substitute.For<IFieldQueries>();

                    mockFieldQueries
                    .List(Arg.Any<PagedEmployerFilter>())
                    .Returns(Task.FromResult((IPager<FieldModel>)new Pager<FieldModel>(pageNumber, pageSize, results)));

                    serviceCollection.AddSingleton(mockFieldQueries);
                });
            });
        }
    }
}