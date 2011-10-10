// Copyright (c) 2011 Anders Gustafsson, Cureos AB.
// All rights reserved. This software and the accompanying materials
// are made available under the terms of the Eclipse Public License v1.0
// which accompanies this distribution, and is available at
// http://www.eclipse.org/legal/epl-v10.html

using System;
using System.Collections.Generic;
using System.Linq;

namespace Cureos.Linq
{
    public static class DictionaryMethods
    {
        public static Dictionary<TKey, TValue>
            ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            return source.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public static Dictionary<TKey, TValue>
            ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source,
                                       IEqualityComparer<TKey> comparer)
        {
            return source.ToDictionary(kv => kv.Key, kv => kv.Value, comparer);
        }

        public static Dictionary<TKey, TValue>
            Where<TKey, TValue>(this IDictionary<TKey, TValue> source, Func<KeyValuePair<TKey, TValue>, bool> predicate)
        {
            return Enumerable.Where(source, predicate).ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public static Dictionary<TKey, TValue>
            Except<TKey, TValue>(this Dictionary<TKey, TValue> first, IDictionary<TKey, TValue> second)
        {
            return
                Enumerable.Except(first, second, KeyValuePairKeyEqualityComparer<TKey, TValue>.Default).ToDictionary(
                    kv => kv.Key, kv => kv.Value);
        }

        public static TDict
            Except<TDict, TKey, TValue>(this TDict first, IDictionary<TKey, TValue> second) where TDict : IDictionary<TKey, TValue>, new()
        {
            return ToTDict<TDict, TKey, TValue>(
                Enumerable.Except(first, second, KeyValuePairKeyEqualityComparer<TKey, TValue>.Default));
        }

        public static Dictionary<TKey, TValue>
            Except<TKey, TValue>(this IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second,
                                 IEqualityComparer<KeyValuePair<TKey, TValue>> comparer)
        {
            return Enumerable.Except(first, second, comparer).ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public static Dictionary<TKey, TValue>
            Intersect<TKey, TValue>(this IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second)
        {
            return
                Enumerable.Intersect(first, second, KeyValuePairKeyEqualityComparer<TKey, TValue>.Default).ToDictionary(
                    kv => kv.Key, kv => kv.Value);
        }

        public static Dictionary<TKey, TValue>
            Intersect<TKey, TValue>(this IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second,
                                 IEqualityComparer<KeyValuePair<TKey, TValue>> comparer)
        {
            return Enumerable.Intersect(first, second, comparer).ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public static Dictionary<TKey, TValue>
            Union<TKey, TValue>(this IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second)
        {
            return
                Enumerable.Union(first, second, KeyValuePairKeyEqualityComparer<TKey, TValue>.Default).ToDictionary(
                    kv => kv.Key, kv => kv.Value);
        }

        public static Dictionary<TKey, TValue>
            Union<TKey, TValue>(this IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second,
                                 IEqualityComparer<KeyValuePair<TKey, TValue>> comparer)
        {
            return Enumerable.Union(first, second, comparer).ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public static Dictionary<TKey, TValue>
            Select<TSource, TKey, TValue>(this IEnumerable<TSource> source,
                                          Func<TSource, KeyValuePair<TKey, TValue>> selector)
        {
            return Enumerable.Select(source, selector).ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public static Dictionary<TKey, TValue>
            Select<TSource, TKey, TValue>(this IEnumerable<TSource> source,
                                          Func<TSource, int, KeyValuePair<TKey, TValue>> selector)
        {
            return Enumerable.Select(source, selector).ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        private static TDict ToTDict<TDict, TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> source) where TDict : IDictionary<TKey, TValue>, new()
        {
            var dict = new TDict();
            foreach (var kv in source) dict.Add(kv);
            return dict;
        }
    }
}