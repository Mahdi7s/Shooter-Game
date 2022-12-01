using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ES3Internal
{
	[System.Serializable]
	public abstract class ES3SerializableDictionary<TKey,TVal> : System.Collections.IEnumerable, IEnumerable<KeyValuePair<TKey, TVal>>
	{
		[SerializeField]
		private List<TKey> _Keys = new List<TKey>();
		[SerializeField]
		private List<TVal> _Values = new List<TVal>();

		protected abstract bool KeysAreEqual(TKey a, TKey b);
		protected abstract bool ValuesAreEqual(TVal a, TVal b);

		public List<TKey> Keys
		{
			get{ return _Keys; }
		}

		public List<TVal> Values
		{
			get{ return _Values; }
		}

		public int Count
		{
			get{ return _Keys.Count; }
		}

		public void Add(TKey key, TVal value)
		{
			if(FindKeyIndex(key) != -1)
				throw new ArgumentException("An element with the same key already exists in the Dictionary");
			Insert(key, value);
		}

		public TVal this[TKey key]
		{
			get
			{
				int index = FindKeyIndex(key);
				if (index >= 0)
					return _Values[index];
				throw new KeyNotFoundException(key.ToString());
			}
			set 
			{
				int index = FindKeyIndex(key);
				if(index != -1)
					_Values[index] = value;
				else
					Insert(key, value);
			}
		}

		public bool TryGetValue(TKey key, out TVal val)
		{
			int index = FindKeyIndex(key);
			if(index == -1)
			{
				val = default(TVal);
				return false;
			}
			val = _Values[index];
			return true;
		}

		public bool TryGetKey(TVal value, out TKey key)
		{
			int index = FindValueIndex(value);
			if(index == -1)
			{
				key = default(TKey);
				return false;
			}
			key = _Keys[index];
			return true;
		}

		public bool ContainsKey(TKey key)
		{
			return (FindKeyIndex(key) > -1);
		}

		public bool ContainsValue(TVal value)
		{
			for(int i=0; i<_Values.Count; i++)
				if(ValuesAreEqual(value, _Values[i]))
					return true;
			return false;
		}

		public void Remove(TKey key)
		{
			int index = FindKeyIndex(key);
			if(index == -1)
				return;
			_Keys.RemoveAt(index);
			_Values.RemoveAt(index);
		}

		public int RemoveNullKeys()
		{
			int index = 0;
			int removedCount = 0;
			while(index < _Keys.Count)
			{
				if(_Keys[index] == null)
				{
					_Keys.RemoveAt(index);
					_Values.RemoveAt(index);
					removedCount++;
				}
				else
					index++;
			}
			return removedCount;
		}

		public int RemoveNullValues()
		{
			int index = 0;
			int removedCount = 0;
			while(index < _Keys.Count)
			{
				if(_Values[index] == null)
				{
					_Keys.RemoveAt(index);
					_Values.RemoveAt(index);
					removedCount++;
				}
				else
					index++;
			}
			return removedCount;
		}

		// Changes the key of a value without changing it's position in the underlying Lists.
		// Mainly used in the Editor where position might otherwise change while the user is editing it.
		// Returns true if a change was made.
		public bool ChangeKey(TKey oldId, TKey newId)
		{
			if(KeysAreEqual(oldId, newId))
				return false;
			int oldKeyIndex = FindKeyIndex(oldId);
			if(oldKeyIndex == -1)
				return false;
			// Check that the change won't result in a duplicate key.
			if(FindKeyIndex(newId) != -1)
				return false;
			_Keys[oldKeyIndex] = newId;
			return true;
		}

		private int FindKeyIndex(TKey key)
		{
			for(int i=0; i<_Keys.Count; i++)
				if(KeysAreEqual(_Keys[i], key))
					return i;
			return -1;
		}

		private int FindValueIndex(TVal value)
		{
			for(int i=0; i<_Values.Count; i++)
				if(ValuesAreEqual(_Values[i], value))
					return i;
			return -1;
		}

		// Only call this method if you are certain the key and value do not already exist in the Dictionary.
		private void Insert(TKey key, TVal value)
		{
			if(key == null)
				throw new System.ArgumentNullException("key");

			_Keys.Add(key);
			_Values.Add(value);
		}

		public Enumerator GetEnumerator()
		{
			return new Enumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator<KeyValuePair<TKey, TVal>> IEnumerable<KeyValuePair<TKey, TVal>>.GetEnumerator()
		{
			return GetEnumerator();
		}

		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TVal>>
		{
			private readonly ES3SerializableDictionary<TKey, TVal> _Dictionary;
			private int _Index;
			private KeyValuePair<TKey, TVal> _Current;

			public KeyValuePair<TKey, TVal> Current
			{
				get { return _Current; }
			}

			public Enumerator(ES3SerializableDictionary<TKey, TVal> dictionary)
			{
				_Dictionary = dictionary;
				_Current = default(KeyValuePair<TKey, TVal>);
				_Index = 0;
			}

			public bool MoveNext()
			{
				while (_Index < _Dictionary.Count)
				{
					_Current = new KeyValuePair<TKey, TVal>(_Dictionary._Keys[_Index], _Dictionary._Values[_Index]);
					_Index++;
					return true;
				}

				_Index = _Dictionary.Count + 1;
				_Current = default(KeyValuePair<TKey, TVal>);
				return false;
			}

			void IEnumerator.Reset()
			{
				_Index = 0;
				_Current = default(KeyValuePair<TKey, TVal>);
			}

			object IEnumerator.Current
			{
				get { return Current; }
			}

			public void Dispose()
			{
			}
		}
	}
}