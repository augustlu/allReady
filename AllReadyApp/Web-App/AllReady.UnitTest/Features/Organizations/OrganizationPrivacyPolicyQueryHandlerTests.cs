﻿using AllReady.Features.Organizations;
using System.Threading.Tasks;
using AllReady.UnitTest.Features.Campaigns;
using Xunit;

namespace AllReady.UnitTest.Features.Organizations
{
    public class OrganizationPrivacyPolicyQueryHandlerTests : InMemoryContextTest
    {
        [Fact]
        public async Task OrgWithNoPrivacyPolicy_ReturnsNullContent()
        {
            var handler = new OrganizationPrivacyPolicyQueryHandler(Context);
            var result = await handler.Handle(new OrganizationPrivacyPolicyQuery { OrganizationId = 1 });

            Assert.NotNull(result);
            Assert.Equal("Org 1", result.OrganizationName);
            Assert.Null(result.Content);
        }

        [Fact]
        public async Task OrgWithValidPrivacyPolicy_ReturnsContent()
        {
            var handler = new OrganizationPrivacyPolicyQueryHandler(Context);
            var result = await handler.Handle(new OrganizationPrivacyPolicyQuery { OrganizationId = 2 });

            Assert.NotNull(result);
            Assert.Equal("Org 2", result.OrganizationName);
            Assert.Equal("<h2>Line 1</h2><p>Line 2</p>", result.Content);
        }

        [Fact]
        public async Task NullReturnedWhenSkillIdDoesNotExists()
        {
            var handler = new OrganizationPrivacyPolicyQueryHandler(Context);
            var result = await handler.Handle(new OrganizationPrivacyPolicyQuery { OrganizationId = 100 });

            Assert.Null(result);
        }

        [Fact]
        public async Task NullReturnedWhenSkillIdNotInMessage()
        {
            var handler = new OrganizationPrivacyPolicyQueryHandler(Context);
            var result = await handler.Handle(new OrganizationPrivacyPolicyQuery());

            Assert.Null(result);
        }

        protected override void LoadTestData()
        {
            OrganizationHandlerTestHelper.LoadOrganizationHandlerTestData(Context);
        }
    }
}