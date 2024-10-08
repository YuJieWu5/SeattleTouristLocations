﻿using System.IO;
using NUnit.Framework;

namespace UnitTests
{
    /// <summary>
    /// This class implements a test fixture for the unit testing of the project
    /// </summary>
    [SetUpFixture]
    public class TestFixture
    {
        // Path to the Web Root
        public static string DataWebRootPath = "./wwwroot";

        // Path to the data folder for the content
        public static string DataContentRootPath = "./data/";

        /// <summary>
        /// This method sets up anything needed before the testing is started
        /// </summary>
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            // Run this code once when the test harness starts up.
            // This will copy over the latest version of the database files
            // C:\repos\5110\ClassBaseline\UnitTests\bin\Debug\net5.0\wwwroot\data
            // C:\repos\5110\ClassBaseline\src\wwwroot\data
            // C:\repos\5110\ClassBaseline\src\bin\Debug\net5.0\wwwroot\data

            var DataWebPath = "../../../../src/bin/Debug/net7.0/wwwroot/data";
            var DataUTDirectory = "wwwroot";
            var DataUTPath = DataUTDirectory + "/data";

            // Delete the Destination folder
            if (Directory.Exists(DataUTDirectory))
            {
                Directory.Delete(DataUTDirectory, true);
            }

            // Make the directory
            Directory.CreateDirectory(DataUTPath);

            // Copy over all data files
            var filePaths = Directory.GetFiles(DataWebPath);
            foreach (var filename in filePaths)
            {
                string OriginalFilePathName = filename.ToString();
                var newFilePathName = OriginalFilePathName.Replace(DataWebPath, DataUTPath);

                File.Copy(OriginalFilePathName, newFilePathName);
            }
        }

        /// <summary>
        /// This method is ran after the completion of testing, if needed
        /// </summary>
        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
        }
    }
}