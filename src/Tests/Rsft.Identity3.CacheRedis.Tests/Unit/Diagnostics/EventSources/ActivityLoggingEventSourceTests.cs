// <copyright file="ActivityLoggingEventSourceTests.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Tests.Unit.Diagnostics.EventSources
{
    using CacheRedis.Diagnostics.EventSources;
    using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Utility;
    using NUnit.Framework;

    [TestFixture]
    public class ActivityLoggingEventSourceTests : TestBase
    {
        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {

        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {

        }

        /// <summary>
        /// The should validate event source.
        /// </summary>
        [Test]
        public void ShouldValidateEventSource()
        {
            EventSourceAnalyzer.InspectAll(ActivityLoggingEventSource.Log);
        }
    }

}