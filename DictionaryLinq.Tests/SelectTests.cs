// Copyright (c) 2011 Anders Gustafsson, Cureos AB.
// All rights reserved. This software and the accompanying materials
// are made available under the terms of the Eclipse Public License v1.0
// which accompanies this distribution, and is available at
// http://www.eclipse.org/legal/epl-v10.html

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Cureos.Linq
{
    [TestFixture]
    public class SelectTests
    {
        #region Unit tests

        [Test]
        public void SelectIndexlessSelector_AllPairsDifferent_ReturnsDictionary()
        {
            var instance = new[] { 1, 2, 3, 4 };
            var actual = instance.Select(i => new KeyValuePair<int, int>(i, 2 * i));
            Assert.IsInstanceOf<Dictionary<int, int>>(actual);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void SelectIndexlessSelector_SameKeyInSomePairs_Throws()
        {
            var instance = new[] { 1, 2, 3, 3 };
            var actual = instance.Select(i => new KeyValuePair<int, int>(i, 2 * i));
        }

        [Test]
        public void SelectIndexSelector_AllPairsDifferent_ReturnsDictionary()
        {
            var instance = new[] { 1, 2, 3, 4 };
            var actual = instance.Select((i, idx) => new KeyValuePair<int, int>(i, idx * i));
            Assert.IsInstanceOf<Dictionary<int, int>>(actual);
        }

        #endregion
    }
}
