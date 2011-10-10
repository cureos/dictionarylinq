// Copyright (c) 2011 Anders Gustafsson, Cureos AB.
// All rights reserved. This software and the accompanying materials
// are made available under the terms of the Eclipse Public License v1.0
// which accompanies this distribution, and is available at
// http://www.eclipse.org/legal/epl-v10.html

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Cureos.Linq
{
    [TestFixture]
    public class ToDictionaryTests
    {
        #region Unit tests

        [Test]
        public void ToDictionaryNoEqualityComparer_AllKeysDifferent_ReturnsDictionaryOfSourceLength()
        {
            var instance = new List<KeyValuePair<string, int>> { new KeyValuePair<string, int>("a", 1), new KeyValuePair<string, int>("b", 2) };

            var expected = 2;
            var actual = instance.ToDictionary().Count;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ToDictionaryNoEqualityComparer_TwoKeysOnlyCaseDifferent_ReturnsDictionaryOfSourceLength()
        {
            var instance = new[]
                               {
                                   new KeyValuePair<string, int>("a", 1), new KeyValuePair<string, int>("b", 2),
                                   new KeyValuePair<string, int>("A", 3)
                               };
            var expected = 3;
            var actual = instance.ToDictionary().Count;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ToDictionaryKeyEqualityComparer_CaseInsensitiveEqualityComparer_Throws()
        {
            var instance = new[]
                               {
                                   new KeyValuePair<string, int>("a", 1), new KeyValuePair<string, int>("b", 2),
                                   new KeyValuePair<string, int>("A", 3)
                               };
            var actual = instance.ToDictionary(StringComparer.InvariantCultureIgnoreCase);
        }

        #endregion
    }
}
