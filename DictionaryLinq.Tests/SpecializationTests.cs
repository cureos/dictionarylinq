// Copyright (c) 2010-2011 Anders Gustafsson, Cureos AB.
// All rights reserved. Any unauthorised reproduction of this 
// material will constitute an infringement of copyright.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Cureos.Linq
{
    [TestFixture]
    public class SpecializationTests
    {
        #region Unit tests

        [Test]
        public void SpecializationOfIEnumerable_ThreeSituations_CorrectInvocation()
        {
            var one = new List<KeyValuePair<int, int>>();
            var two = new SortedList<int, int>();
            var three = new Dictionary<int, int>();
            var four = new SortedDictionary<int, int>();

            Assert.IsInstanceOf<IEnumerable<KeyValuePair<int, int>>>(one.GetCode<KeyValuePair<int, int>>(four));
            Assert.IsInstanceOf<SortedList<int, int>>(two.GetCode(four));
            Assert.IsInstanceOf<Dictionary<int, int>>(three.GetCode(four));
        }

        #endregion
    }

    public static class Specialization
    {
        public static IEnumerable<TSource> GetCode<TSource>(this IEnumerable<TSource> first,
                                                              IEnumerable<TSource> second)
        {
            Console.WriteLine(1);
            return new TSource[0];
        }

        public static TDict GetCode<TDict, TKey, TValue>(this TDict first, IDictionary<TKey, TValue> second)
            where TDict : IDictionary<TKey, TValue>, new()
        {
            Console.WriteLine(2);
            return new TDict();
        }

        public static Dictionary<TKey, TValue> GetCode<TKey, TValue>(this Dictionary<TKey, TValue> first,
                                                                       IDictionary<TKey, TValue> second)
        {
            Console.WriteLine(3);
            return new Dictionary<TKey, TValue>();
        }
    }
}
