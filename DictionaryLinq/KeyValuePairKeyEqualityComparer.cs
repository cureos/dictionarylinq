// Copyright (c) 2011 Anders Gustafsson, Cureos AB.
// All rights reserved. This software and the accompanying materials
// are made available under the terms of the Eclipse Public License v1.0
// which accompanies this distribution, and is available at
// http://www.eclipse.org/legal/epl-v10.html

using System.Collections.Generic;

namespace Cureos.Linq
{
    public class KeyValuePairKeyEqualityComparer<TKey, TValue> : EqualityComparer<KeyValuePair<TKey, TValue>>
    {
        #region CONSTRUCTORS

        public KeyValuePairKeyEqualityComparer()
        {
            KeyComparer = EqualityComparer<TKey>.Default;
        }

        public KeyValuePairKeyEqualityComparer(EqualityComparer<TKey> keyComparer)
        {
            KeyComparer = keyComparer;
        }

        static KeyValuePairKeyEqualityComparer()
        {
            Default = new KeyValuePairKeyEqualityComparer<TKey, TValue>();
        }

        #endregion

        #region AUTO-IMPLEMENTED PROPERTIES

        public EqualityComparer<TKey> KeyComparer { get; private set; }

        public new static KeyValuePairKeyEqualityComparer<TKey, TValue> Default { get; private set; }

        #endregion

        #region Overrides of EqualityComparer<KeyValuePair<TKey,TValue>>

        /// <summary>
        /// Determines whether two key-value pairs are equal based on the key equality comparer.
        /// </summary>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        /// <param name="x">The first object to compare.</param><param name="y">The second object to compare.</param>
        public override bool Equals(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
        {
            return KeyComparer.Equals(x.Key, y.Key);
        }

        /// <summary>
        /// When overridden in a derived class, serves as a hash function for the specified object for hashing algorithms and data structures, such as a hash table.
        /// </summary>
        /// <returns>
        /// A hash code for the specified object.
        /// </returns>
        /// <param name="obj">The object for which to get a hash code.</param><exception cref="T:System.ArgumentNullException">The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is null.</exception>
        public override int GetHashCode(KeyValuePair<TKey, TValue> obj)
        {
            return KeyComparer.GetHashCode(obj.Key);
        }

        #endregion
    }
}