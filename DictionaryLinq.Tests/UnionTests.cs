// Copyright (c) 2010-2011 Anders Gustafsson, Cureos AB.
// All rights reserved. Any unauthorised reproduction of this 
// material will constitute an infringement of copyright.

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Cureos.Linq
{
    [TestFixture]
    public class UnionTests
    {
        #region Unit tests

        [Test]
        public void UnionNoEqualityComparer_SameKeyInSecondDict_TakesDuplicateKeyValuePair()
        {
            var first = new Dictionary<int, int> { { 2, 3 }, { 3, 5 }, { 6, 4 } };
            var second = new SortedList<int, int> { { 3, 2 }, { 4, 7 } };

            var expected = 4;
            var actual = first.Union(second).Count;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void UnionNoEqualityComparer_SortedListFirst_ReturnsSortedList()
        {
            var first = new SortedList<int, int> { { 2, 3 }, { 3, 5 } };
            var second = new Dictionary<int, int> { { 3, 2 }, { 4, 7 } };

            var actual = first.Union(second);
            Assert.IsInstanceOf<SortedList<int, int>>(actual);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void UnionDefaultEqualityComparer_SameKeyDifferentValues_Throws()
        {
            var first = new Dictionary<int, int> { { 2, 3 }, { 3, 5 }, { 6, 4 } };
            var second = new SortedList<int, int> { { 3, 2 }, { 6, 4 } };

            var actual = first.Union(second, EqualityComparer<KeyValuePair<int, int>>.Default);
        }

        [Test]
        public void UnionDefaultEqualityComparer_SortedDictionaryFirst_ReturnsSortedDictionary()
        {
            var first = new SortedDictionary<int, int> { { 2, 3 }, { 6, 5 } };
            var second = new SortedList<int, int> { { 3, 2 }, { 4, 7 } };

            var actual = first.Union(second, EqualityComparer<KeyValuePair<int, int>>.Default);
            Assert.IsInstanceOf<SortedDictionary<int, int>>(actual);
        }

        #endregion
    }
}
