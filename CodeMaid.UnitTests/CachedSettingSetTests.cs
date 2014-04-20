﻿#region CodeMaid is Copyright 2007-2014 Steve Cadwallader.

// CodeMaid is free software: you can redistribute it and/or modify it under the terms of the GNU
// Lesser General Public License version 3 as published by the Free Software Foundation.
//
// CodeMaid is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details <http://www.gnu.org/licenses/>.

#endregion CodeMaid is Copyright 2007-2014 Steve Cadwallader.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SteveCadwallader.CodeMaid.Helpers;
using SteveCadwallader.CodeMaid.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SteveCadwallader.CodeMaid.UnitTests
{
    [TestClass]
    public class CachedSettingSetTests
    {
        private int _lookupCount;
        private int _parseCount;
        private CachedSettingSet<string> _cachedSettingSet;

        [TestInitialize]
        public void TestInitialize()
        {
            Settings.Default.Reset();

            _lookupCount = 0;
            _parseCount = 0;
            _cachedSettingSet = new CachedSettingSet<string>(
               () =>
               {
                   _lookupCount++;
                   return Settings.Default.Cleaning_ExclusionExpression;
               },
               x =>
               {
                   _parseCount++;
                   return x.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries)
                           .Select(y => y.Trim().ToLower())
                           .Where(z => !string.IsNullOrEmpty(z))
                           .ToList();
               });

            Assert.AreEqual(0, _lookupCount);
            Assert.AreEqual(0, _parseCount);
            Assert.IsNotNull(_cachedSettingSet);
        }

        [TestMethod]
        public void CachedSettingSetCanLookupAndParse()
        {
            var cleanupExclusions = _cachedSettingSet.Value;

            Assert.IsNotNull(cleanupExclusions);
            Assert.AreEqual(1, _lookupCount);
            Assert.AreEqual(1, _parseCount);
        }

        [TestMethod]
        public void CachedSettingSetUsesCacheOnSecondLookup()
        {
            var cleanupExclusions = _cachedSettingSet.Value;

            Assert.IsNotNull(cleanupExclusions);
            Assert.AreEqual(1, _lookupCount);
            Assert.AreEqual(1, _parseCount);

            var cleanupExclusions2 = _cachedSettingSet.Value;

            Assert.IsNotNull(cleanupExclusions2);
            Assert.AreEqual(2, _lookupCount);
            Assert.AreEqual(1, _parseCount);
        }

        [TestMethod]
        public void CachedSettingSetReParsesOnChange()
        {
            var cleanupExclusions = _cachedSettingSet.Value;

            Assert.IsNotNull(cleanupExclusions);
            Assert.AreEqual(1, _lookupCount);
            Assert.AreEqual(1, _parseCount);

            var cleanupExclusion2 = new List<string>(cleanupExclusions) { ".*Test.*" };
            var serializedCleanupExclusions = string.Join("||", cleanupExclusion2);

            Settings.Default.Cleaning_ExclusionExpression = serializedCleanupExclusions;

            var memberTypeSetting2 = _cachedSettingSet.Value;

            Assert.IsNotNull(memberTypeSetting2);
            Assert.AreEqual(2, _lookupCount);
            Assert.AreEqual(2, _parseCount);
        }
    }
}