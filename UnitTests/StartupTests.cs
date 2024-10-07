using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;

namespace UnitTests.Pages.Startup
{
    public class StartupTests
    {
        #region TestSetup
        
        /// <summary>
        /// Initialize variables
        /// </summary>
        
        [SetUp]
        public void TestInitialize()
        {
        }

        /// <summary>
        /// Inhert Web application's Startup class for testing
        /// </summary>
        public class Startup : ContosoCrafts.WebSite.Startup
        {
            /// <summary>
            /// Intializing using Constructor with the given configuration
            /// </summary>
            /// <param name="config">The application's configuration.</param>
            public Startup(IConfiguration config) : base(config) { }

        }

        #endregion TestSetup

        #region ConfigureServices
        
        /// <summary>
        /// Tests if the Startup's ConfigureServices method runs successfully and if the web host is valid or not.
        /// </summary>
        
        [Test]
        public void Startup_ConfigureServices_Valid_Defaut_Should_Pass()
        {
            /// Creating a default web host builder and using it for testing.
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            Assert.IsNotNull(webHost);
        }

        #endregion ConfigureServices

        #region Configure
        
        /// <summary>
        /// Tests if the Startup's Configure method runs successfully and if the webhost is valid or not.
        /// </summary>
        
        [Test]
        public void Startup_Configure_Valid_Defaut_Should_Pass()
        {
            /// Creates a default web host builder and uses it for testing.
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            Assert.IsNotNull(webHost);
        }

        #endregion Configure
    }

}