using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class StatisticsClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void DoesThrowOnBadArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new StatisticsClient(null));
            }
        }

        public class TheGetContributorsMethod
        {
            [Fact]
            public async Task RetrievesContributorsForCorrectUrl()
            {
                var expectedEndPoint = new Uri("/repos/username/repositoryName/stats/contributors", UriKind.Relative);
                var client = Substitute.For<IApiConnection>();
                IReadOnlyList<Contributor> contributors = new ReadOnlyCollection<Contributor>(new[] { new Contributor() });
                client.GetQueuedOperation<IReadOnlyList<Contributor>>(expectedEndPoint, Args.CancellationToken)
                    .Returns(Task.FromResult(contributors));
                var statisticsClient = new StatisticsClient(client);

                var result = await statisticsClient.GetContributors("username","repositoryName");

                Assert.Equal(1, result.Count);
            }

            [Fact]
            public async Task RetrievesEmptyContributorsCollectionWhenNoContentReturned()
            {
                var expectedEndPoint = new Uri("/repos/username/repositoryName/stats/contributors", UriKind.Relative);
                var client = Substitute.For<IApiConnection>();
                IReadOnlyList<Contributor> contributors = null;
                client.GetQueuedOperation<IReadOnlyList<Contributor>>(expectedEndPoint, Args.CancellationToken)
                    .Returns(Task.FromResult(contributors));
                var statisticsClient = new StatisticsClient(client);

                var result = await statisticsClient.GetContributors("username", "repositoryName");

                Assert.Empty(result);
            }

            [Fact]
            public async Task ThrowsIfGivenNullOwner()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetContributors(null,"repositoryName"));
            }

            [Fact]
            public async Task ThrowsIfGivenNullRepositoryName()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetContributors("owner", null));
            }
        }

        public class TheGetCommitActivityForTheLastYearMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var expectedEndPoint = new Uri("/repos/username/repositoryName/stats/commit_activity", UriKind.Relative);

                var client = Substitute.For<IApiConnection>();
                var statisticsClient = new StatisticsClient(client);

                statisticsClient.GetCommitActivity("username", "repositoryName");

                client.Received().GetQueuedOperation<IReadOnlyList<WeeklyCommitActivity>>(expectedEndPoint, Args.CancellationToken);
            }

            [Fact]
            public async Task ThrowsIfGivenNullOwner()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetCommitActivity(null, "repositoryName"));
            }

            [Fact]
            public async Task ThrowsIfGivenNullRepositoryName()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetCommitActivity("owner", null));
            }
        }

        public class TheGetAdditionsAndDeletionsPerWeekMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var expectedEndPoint = new Uri("/repos/username/repositoryName/stats/code_frequency", UriKind.Relative);

                var client = Substitute.For<IApiConnection>();
                var statisticsClient = new StatisticsClient(client);

                statisticsClient.GetCodeFrequency("username", "repositoryName");

                client.Received().GetQueuedOperation<IEnumerable<long[]>>(expectedEndPoint, Args.CancellationToken);
            }

            [Fact]
            public async Task ThrowsIfGivenNullOwner()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetCodeFrequency(null, "repositoryName"));
            }

            [Fact]
            public async Task ThrowsIfGivenNullRepositoryName()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetCodeFrequency("owner", null));
            }
        }

        public class TheGetWeeklyCommitCountsMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var expectedEndPoint = new Uri("/repos/username/repositoryName/stats/participation", UriKind.Relative);

                var client = Substitute.For<IApiConnection>();
                var statisticsClient = new StatisticsClient(client);

                statisticsClient.GetParticipation("username", "repositoryName");

                client.Received().GetQueuedOperation<Participation>(expectedEndPoint, Args.CancellationToken);
            }

            [Fact]
            public async Task ThrowsIfGivenNullOwner()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetParticipation(null, "repositoryName"));
            }

            [Fact]
            public async Task ThrowsIfGivenNullRepositoryName()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetParticipation("owner", null));
            }
        }

        public class TheGetHourlyCommitCountsMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var expectedEndPoint = new Uri("/repos/username/repositoryName/stats/punch_card", UriKind.Relative);

                var client = Substitute.For<IApiConnection>();
                var statisticsClient = new StatisticsClient(client);

                statisticsClient.GetPunchCard("username", "repositoryName");

                client.Received().GetQueuedOperation<IEnumerable<int[]>>(expectedEndPoint, Args.CancellationToken);
            }

            [Fact]
            public async Task ThrowsIfGivenNullOwner()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetPunchCard(null, "repositoryName"));
            }

            [Fact]
            public async Task ThrowsIfGivenNullRepositoryName()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetPunchCard("owner", null));
            }
        }
    }
}