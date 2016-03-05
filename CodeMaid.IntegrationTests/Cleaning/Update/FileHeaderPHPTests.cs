﻿#region CodeMaid is Copyright 2007-2015 Steve Cadwallader.

// CodeMaid is free software: you can redistribute it and/or modify it under the terms of the GNU
// Lesser General Public License version 3 as published by the Free Software Foundation.
//
// CodeMaid is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details <http://www.gnu.org/licenses/>.

#endregion CodeMaid is Copyright 2007-2015 Steve Cadwallader.

using EnvDTE;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SteveCadwallader.CodeMaid.IntegrationTests.Helpers;
using SteveCadwallader.CodeMaid.Logic.Cleaning;
using SteveCadwallader.CodeMaid.Properties;

namespace SteveCadwallader.CodeMaid.IntegrationTests.Cleaning.Update
{
    [Ignore]
    [TestClass]
    [DeploymentItem(@"Cleaning\Update\Data\FileHeaderPHP.php", "Data")]
    [DeploymentItem(@"Cleaning\Update\Data\FileHeaderPHP_Cleaned.php", "Data")]
    public class FileHeaderPHPTests
    {
        #region Setup

        private static FileHeaderLogic _fileHeaderLogic;
        private ProjectItem _projectItem;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _fileHeaderLogic = FileHeaderLogic.GetInstance(TestEnvironment.Package);
            Assert.IsNotNull(_fileHeaderLogic);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            TestEnvironment.CommonTestInitialize();
            _projectItem = TestEnvironment.LoadFileIntoProject(@"Data\FileHeaderPHP.php");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            TestEnvironment.RemoveFromProject(_projectItem);
        }

        #endregion Setup

        #region Tests

        [TestMethod]
        [HostType("VS IDE")]
        public void CleaningUpdateFileHeaderPHP_CleansAsExpected()
        {
            Settings.Default.Cleaning_UpdateFileHeaderPHP =
@"<?php
/*
 * This is a sample php class file
 */";

            TestOperations.ExecuteCommandAndVerifyResults(RunUpdateFileHeader, _projectItem, @"Data\FileHeaderPHP_Cleaned.php");
        }

        [TestMethod]
        [HostType("VS IDE")]
        public void CleaningUpdateFileHeaderPHP_DoesNothingOnSecondPass()
        {
            Settings.Default.Cleaning_UpdateFileHeaderPHP =
@"<?php
/*
 * This is a sample php class file
 */";

            TestOperations.ExecuteCommandTwiceAndVerifyNoChangesOnSecondPass(RunUpdateFileHeader, _projectItem);
        }

        [TestMethod]
        [HostType("VS IDE")]
        public void CleaningUpdateFileHeaderPHP_DoesNothingWhenSettingIsDisabled()
        {
            Settings.Default.Cleaning_UpdateFileHeaderPHP = null;

            TestOperations.ExecuteCommandAndVerifyNoChanges(RunUpdateFileHeader, _projectItem);
        }

        #endregion Tests

        #region Helpers

        private static void RunUpdateFileHeader(Document document)
        {
            var textDocument = TestUtils.GetTextDocument(document);

            _fileHeaderLogic.UpdateFileHeader(textDocument);
        }

        #endregion Helpers
    }
}