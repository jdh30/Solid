using System.Collections.Generic;
using Solid.Common;

namespace Solid.TrieMap
{
	internal class HashedKey<TKey>
	{
		public readonly IEqualityComparer<TKey> Comparer;
		public readonly int Hash;
		public readonly TKey Key;

		public HashedKey(TKey key, IEqualityComparer<TKey> comparer)
		{
			if (comparer == null) throw Errors.Argument_null("comparer");
			if (key == null) throw Errors.Argument_null("key");
			Comparer = comparer;
			var hash = (uint) comparer.GetHashCode(key);
			unchecked
			{
				hash *= 0x5bd1e995;
				hash ^= hash >> 24;
				hash *= 0x5bd1e995;
				hash ^= 4 ^ 0xc58f1a7b;
				Hash = (int) hash;
			}

			Key = key;
		}

		private HashedKey(TKey key, int hash, IEqualityComparer<TKey> comparer)
		{
			Hash = hash;
			Key = key;
			Comparer = comparer;
		}

		public HashedKey<TKey> Rehash()
		{
			var hash = (uint) Hash;
			unchecked
			{
				hash *= 0x5bd1e995;
				hash ^= hash >> 24;
				hash *= 0x5bd1e995;
				hash ^= 4 ^ 0xc58f1a7b;
			}
			return new HashedKey<TKey>(Key, (int) hash, Comparer);
		}
	}
}