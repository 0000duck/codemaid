﻿#region CodeMaid is Copyright 2007-2014 Steve Cadwallader.

// CodeMaid is free software: you can redistribute it and/or modify it under the terms of the GNU
// Lesser General Public License version 3 as published by the Free Software Foundation.
//
// CodeMaid is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details <http://www.gnu.org/licenses/>.

#endregion CodeMaid is Copyright 2007-2014 Steve Cadwallader.

using EnvDTE;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SteveCadwallader.CodeMaid.IntegrationTests.Helpers;
using SteveCadwallader.CodeMaid.Logic.Reorganizing;
using SteveCadwallader.CodeMaid.Properties;

namespace SteveCadwallader.CodeMaid.IntegrationTests.Reorganizing
{
    [TestClass]
    [DeploymentItem(@"Reorganizing\Data\RegionsRemoveAndInsertWithAccessModifiers.cs", "Data")]
    [DeploymentItem(@"Reorganizing\Data\RegionsRemoveAndInsertWithAccessModifiers_Reorganized.cs", "Data")]
    public class RegionsRemoveAndInsertWithAccessModifiersTests
    {
        #region Setup

        private static CodeReorderManager _codeReorderManager;
        private ProjectItem _projectItem;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _codeReorderManager = CodeReorderManager.GetInstance(TestEnvironment.Package);
            Assert.IsNotNull(_codeReorderManager);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            TestEnvironment.CommonTestInitialize();
            _projectItem = TestEnvironment.LoadFileIntoProject(@"Data\RegionsRemoveAndInsertWithAccessModifiers.cs");
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
        public void ReorganizingRegionsRemoveAndInsertWithAccessModifiers_ReorganizesAsExpected()
        {
            Settings.Default.Reorganizing_RegionsRemoveExistingRegions = true;
            Settings.Default.Reorganizing_RegionsAutoGenerate = true;
            Settings.Default.Reorganizing_RegionsIncludeAccessLevel = true;

            TestOperations.ExecuteCommandAndVerifyResults(RunReorganize, _projectItem, @"Data\RegionsRemoveAndInsertWithAccessModifiers_Reorganized.cs");
        }

        [TestMethod]
        [HostType("VS IDE")]
        public void ReorganizingRegionsRemoveAndInsertWithAccessModifiers_DoesNothingOnSecondPass()
        {
            Settings.Default.Reorganizing_RegionsRemoveExistingRegions = true;
            Settings.Default.Reorganizing_RegionsAutoGenerate = true;
            Settings.Default.Reorganizing_RegionsIncludeAccessLevel = true;

            TestOperations.ExecuteCommandTwiceAndVerifyNoChangesOnSecondPass(RunReorganize, _projectItem);
        }

        #endregion Tests

        #region Helpers

        private static void RunReorganize(Document document)
        {
            _codeReorderManager.Reorganize(document, false);
        }

        #endregion Helpers
    }
}