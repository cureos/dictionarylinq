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
        /// <summary>
        /// Creates a <see cref="Dictionary{TKey,TValue}"/> from an <see cref="IEnumerable{T}"/> consisting of
        /// elements of type <see cref="KeyValuePair{TKey,TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the collection.</typeparam>
        /// <typeparam name="TValue">The type of values in the collection.</typeparam>
        /// <param name="source">An enumerable of key-value pairs to create the dictionary from.</param>
        /// <returns>A <see cref="Dictionary{TKey,TValue}"/> directly constructed from the enumerable of key-value pairs.</returns>
        public static Dictionary<TKey, TValue>
            ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            return source.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        /// <summary>
        /// Creates a <see cref="Dictionary{TKey,TValue}"/> from an <see cref="IEnumerable{T}"/> consisting of
        /// elements of type <see cref="KeyValuePair{TKey,TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the collection.</typeparam>
        /// <typeparam name="TValue">The type of values in the collection.</typeparam>
        /// <param name="source">An enumerable of key-value pairs to create the dictionary from.</param>
        /// <param name="comparer">An <see cref="IEqualityComparer{T}"/> to compare keys.</param>
        /// <returns>A <see cref="Dictionary{TKey,TValue}"/> directly constructed from the collection of key-value pairs.</returns>
        public static Dictionary<TKey, TValue>
            ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source,
                                       IEqualityComparer<TKey> comparer)
        {
            return source.ToDictionary(kv => kv.Key, kv => kv.Value, comparer);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="TDict">Dictionary type.</typeparam>
        /// <typeparam name="TKey">The type of keys in the collection.</typeparam>
        /// <typeparam name="TValue">The type of values in the collection.</typeparam>
        /// <param name="source">A dictionary of key-value pairs to filter.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A dictionary that contains elements from the input sequence that satisfy the condition.</returns>
        /// <remarks>
        /// In its present form, this method is "hidden" by the <see cref="Where{TKey,TValue}"/> method unless 
        /// the types are explicitly inferred. The reason for this is to ensure that at least some kind of <see cref="Dictionary{TKey,TValue}"/>
        /// is returned. If the type of the first argument in <see cref="Where{TKey,TValue}"/> is 
        /// changed to <see cref="Dictionary{TKey,TValue}"/> this method will instead be "hidden" by the general
        /// <see cref="Enumerable.Where{TSource}(System.Collections.Generic.IEnumerable{TSource},System.Func{TSource,bool})"/> 
        /// method which returns a generic <see cref="IEnumerable{T}"/> of <see cref="KeyValuePair{TKey,TValue}"/>.
        /// </remarks>
        public static TDict
            Where<TDict, TKey, TValue>(this TDict source, Func<KeyValuePair<TKey, TValue>, bool> predicate) where TDict : IDictionary<TKey, TValue>, new()
        {
            return ToTDict<TDict, TKey, TValue>(Enumerable.Where(source, predicate));
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the collection.</typeparam>
        /// <typeparam name="TValue">The type of values in the collection.</typeparam>
        /// <param name="source">A dictionary of key-value pairs to filter.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A dictionary that contains elements from the input sequence that satisfy the condition.</returns>
        public static Dictionary<TKey, TValue>
            Where<TKey, TValue>(this IDictionary<TKey, TValue> source, Func<KeyValuePair<TKey, TValue>, bool> predicate)
        {
            return Enumerable.Where(source, predicate).ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public static TDict
            Except<TDict, TKey, TValue>(this TDict first, IDictionary<TKey, TValue> second) where TDict : IDictionary<TKey, TValue>, new()
        {
            return ToTDict<TDict, TKey, TValue>(
                Enumerable.Except(first, second, KeyValuePairKeyEqualityComparer<TKey, TValue>.Default));
        }

        public static Dictionary<TKey, TValue>
            Except<TKey, TValue>(this Dictionary<TKey, TValue> first, IDictionary<TKey, TValue> second)
        {
            return
                Enumerable.Except(first, second, KeyValuePairKeyEqualityComparer<TKey, TValue>.Default).ToDictionary(
                    kv => kv.Key, kv => kv.Value);
        }

        public static TDict
            Except<TDict, TKey, TValue>(this TDict first, IDictionary<TKey, TValue> second,
                                 IEqualityComparer<KeyValuePair<TKey, TValue>> comparer) where TDict : IDictionary<TKey, TValue>, new()
        {
            return ToTDict<TDict, TKey, TValue>(Enumerable.Except(first, second, comparer));
        }

        public static Dictionary<TKey, TValue>
            Except<TKey, TValue>(this Dictionary<TKey, TValue> first, IDictionary<TKey, TValue> second,
                                 IEqualityComparer<KeyValuePair<TKey, TValue>> comparer)
        {
            return Enumerable.Except(first, second, comparer).ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public static TDict
            Intersect<TDict, TKey, TValue>(this TDict first, IDictionary<TKey, TValue> second) where TDict : IDictionary<TKey, TValue>, new()
        {
            return ToTDict<TDict, TKey, TValue>(
                Enumerable.Intersect(first, second, KeyValuePairKeyEqualityComparer<TKey, TValue>.Default));
        }

        public static Dictionary<TKey, TValue>
            Intersect<TKey, TValue>(this Dictionary<TKey, TValue> first, IDictionary<TKey, TValue> second)
        {
            return
                Enumerable.Intersect(first, second, KeyValuePairKeyEqualityComparer<TKey, TValue>.Default).ToDictionary(
                    kv => kv.Key, kv => kv.Value);
        }

        public static TDict
            Intersect<TDict, TKey, TValue>(this TDict first, IDictionary<TKey, TValue> second,
                                 IEqualityComparer<KeyValuePair<TKey, TValue>> comparer) where TDict : IDictionary<TKey, TValue>, new()
        {
            return ToTDict<TDict, TKey, TValue>(Enumerable.Intersect(first, second, comparer));
        }

        public static Dictionary<TKey, TValue>
            Intersect<TKey, TValue>(this Dictionary<TKey, TValue> first, IDictionary<TKey, TValue> second,
                                 IEqualityComparer<KeyValuePair<TKey, TValue>> comparer)
        {
            return Enumerable.Intersect(first, second, comparer).ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public static TDict
            Union<TDict, TKey, TValue>(this TDict first, IDictionary<TKey, TValue> second) where TDict : IDictionary<TKey, TValue>, new()
        {
            return ToTDict<TDict, TKey, TValue>(
                Enumerable.Union(first, second, KeyValuePairKeyEqualityComparer<TKey, TValue>.Default));
        }

        public static Dictionary<TKey, TValue>
            Union<TKey, TValue>(this Dictionary<TKey, TValue> first, IDictionary<TKey, TValue> second)
        {
            return
                Enumerable.Union(first, second, KeyValuePairKeyEqualityComparer<TKey, TValue>.Default).ToDictionary(
                    kv => kv.Key, kv => kv.Value);
        }

        public static TDict
            Union<TDict, TKey, TValue>(this IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second,
                                 IEqualityComparer<KeyValuePair<TKey, TValue>> comparer) where TDict : IDictionary<TKey, TValue>, new()
        {
            return ToTDict<TDict, TKey, TValue>(Enumerable.Union(first, second, comparer));
        }

        public static Dictionary<TKey, TValue>
            Union<TKey, TValue>(this Dictionary<TKey, TValue> first, IDictionary<TKey, TValue> second,
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