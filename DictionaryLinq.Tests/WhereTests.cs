// Copyright (c) 2011 Anders Gustafsson, Cureos AB.
// All rights reserved. This software and the accompanying materials
// are made available under the terms of the Eclipse Public License v1.0
// which accompanies this distribution, and is available at
// http://www.eclipse.org/legal/epl-v10.html

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Cureos.Linq
{
    [TestFixture]
    public class WhereTests
    {
        #region Unit tests

        [Test]
        public void Where_OnDictionary_ReturnsDictionary()
        {
            var instance = new Dictionary<int, int>
                           { { 1, 1 }, { 2, 1 }, { 3, 2 }, { 4, 3 }, { 5, 5 }, { 6, 8 }, { 7, 13 } };

            Assert.IsInstanceOf<Dictionary<int, int>>(instance.Where(kv => kv.Value > 3));
        }

        [Test]
        public void Where_OnDictionary_ContainsKeyValuePairFromSourceDict()
        {
            var instance = new Dictionary<int, int> { { 1, 1 }, { 2, 1 }, { 3, 2 }, { 4, 3 }, { 5, 5 }, { 6, 8 }, { 7, 13 } };

            CollectionAssert.Contains(instance.Where(kv => kv.Value > 3), new KeyValuePair<int, int>(6, 8));
        }

        [Test]
        public void Where_OnDictionary_DoesNotContainPairWhereOnlyKeyIsSame()
        {
            var instance = new Dictionary<int, int> { { 1, 1 }, { 2, 1 }, { 3, 2 }, { 4, 3 }, { 5, 5 }, { 6, 8 }, { 7, 13 } };

            CollectionAssert.DoesNotContain(instance.Where(kv => kv.Value > 3), new KeyValuePair<int, int>(6, 10));
        }

        [Test]
        public void Where_OnDictionaryCastToIEnumerable_ReturnsIEnumerable()
        {
            var instance = new Dictionary<int, int>
                               { { 1, 1 }, { 2, 1 }, { 3, 2 }, { 4, 3 }, { 5, 5 }, { 6, 8 }, { 7, 13 } };

            Assert.IsInstanceOf(typeof(IEnumerable<KeyValuePair<int, int>>),
                                    ((IEnumerable<KeyValuePair<int, int>>)instance).Where(kv => kv.Key > 3));
        }

        #endregion
    }
}
