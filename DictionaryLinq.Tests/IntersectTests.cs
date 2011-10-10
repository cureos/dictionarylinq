// Copyright (c) 2010-2011 Anders Gustafsson, Cureos AB.
// All rights reserved. Any unauthorised reproduction of this 
// material will constitute an infringement of copyright.

using System.Collections.Generic;
using NUnit.Framework;

namespace Cureos.Linq
{
    [TestFixture]
    public class IntersectTests
    {
        #region Unit tests

        [Test]
        public void IntersectNoEqualityComparer_SameKeyInSecondDict_TakesDuplicateKeyValuePair()
        {
            var first = new Dictionary<int, int> { { 2, 3 }, { 3, 5 }, {6, 4} };
            var second = new SortedList<int, int> { { 3, 2 }, { 4, 7 } };

            var expected = 1;
            var actual = first.Intersect(second).Count;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IntersectNoEqualityComparer_SortedListFirst_ReturnsSortedList()
        {
            var first = new SortedList<int, int> { { 2, 3 }, { 3, 5 } };
            var second = new Dictionary<int, int> { { 3, 2 }, { 4, 7 } };

            var actual = first.Intersect(second);
            Assert.IsInstanceOf<SortedList<int, int>>(actual);
        }

        [Test]
        public void IntersectDefaultEqualityComparer_SameKeyDifferentValues_OnlyKeepsMatchingKeyAndValue()
        {
            var first = new Dictionary<int, int> { { 2, 3 }, { 3, 5 }, {6, 4} };
            var second = new SortedList<int, int> { { 3, 2 }, { 6, 4 } };

            var expected = 1;
            var actual = first.Intersect(second, EqualityComparer<KeyValuePair<int, int>>.Default).Count;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IntersectDefaultEqualityComparer_SortedDictionaryFirst_ReturnsSortedDictionary()
        {
            var first = new SortedDictionary<int, int> { { 2, 3 }, { 3, 5 } };
            var second = new SortedList<int, int> { { 3, 2 }, { 4, 7 } };

            var actual = first.Intersect(second, EqualityComparer<KeyValuePair<int, int>>.Default);
            Assert.IsInstanceOf<SortedDictionary<int, int>>(actual);
        }

        #endregion
    }
}
