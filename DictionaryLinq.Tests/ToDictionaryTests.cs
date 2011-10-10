// Copyright (c) 2010-2011 Anders Gustafsson, Cureos AB.
// All rights reserved. Any unauthorised reproduction of this 
// material will constitute an infringement of copyright.

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DictionaryLinq
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
        [ExpectedException(typeof(ArgumentException))]
        public void ToDictionaryNoEqualityComparer_TwoKeysSameValuesDifferent_Throws()
        {
            var instance = new[]
                               {
                                   new KeyValuePair<string, int>("a", 1), new KeyValuePair<string, int>("b", 2),
                                   new KeyValuePair<string, int>("a", 3)
                               };
            instance.ToDictionary();
        }

        #endregion
    }
}
