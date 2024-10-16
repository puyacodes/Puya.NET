﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puya.Collections
{
    public class CaseInsensitiveDictionary<T> : IDictionary<string, T>
    {
        protected Dictionary<string, T> items;
        public bool IgnoreNotExistingKeys { get; set; }
        public CaseInsensitiveDictionary(bool ignoreNotExistingKeys)
        {
            IgnoreNotExistingKeys = ignoreNotExistingKeys;

            items = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);
        }
        public CaseInsensitiveDictionary()
        {
            items = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);
        }
        public void Add(string key, T value)
        {
            if (items.ContainsKey(key))
            {
                items[key] = value;
            }
            else
            {
                items.Add(key, value);
            }
        }

        public bool ContainsKey(string key)
        {
            return items.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return items.Keys; }
        }

        public bool Remove(string key)
        {
            if (ContainsKey(key))
            {
                return items.Remove(key);
            }
            else
            {
                return false;
            }
        }

        public bool TryGetValue(string key, out T value)
        {
            return items.TryGetValue(key, out value);
        }

        public ICollection<T> Values
        {
            get { return items.Values; }
        }

        public T this[string key]
        {
            get
            {
                if (ContainsKey(key))
                    return items[key];
                else
                {
                    return default(T);
                }
            }
            set
            {
                if (items.ContainsKey(key))
                {
                    items[key] = value;
                }
                else
                {
                    if (IgnoreNotExistingKeys)
                        items.Add(key, value);
                    else
                        throw new KeyNotFoundException();

                }
            }
        }

        public void Add(KeyValuePair<string, T> item)
        {
            if (items.ContainsKey(item.Key))
            {
                items[item.Key] = item.Value;
            }
            else
            {
                items.Add(item.Key, item.Value);
            }
        }

        public void Clear()
        {
            items.Clear();
        }

        public bool Contains(KeyValuePair<string, T> item)
        {
            var _item = items.FirstOrDefault(x =>
            {
                var result = string.Compare(x.Key, item.Key, StringComparison.OrdinalIgnoreCase) == 0;

                if (result)
                {
                    IComparable x1 = x.Value as IComparable;

                    if (x1 != null)
                    {
                        result &= x1.CompareTo(item.Value) == 0;
                    }
                    else
                    {
                        IComparable<T> x2 = x.Value as IComparable<T>;

                        if (x2 != null)
                        {
                            result &= x2.CompareTo(item.Value) == 0;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                }

                return result;
            });

            return !string.IsNullOrEmpty(_item.Key);
        }

        public void CopyTo(KeyValuePair<string, T>[] array, int arrayIndex)
        {
            if (array != null && array.Length > 0)
            {
                if (arrayIndex < 0 || arrayIndex >= array.Length)
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    var i = arrayIndex;

                    foreach (var item in items)
                    {
                        array[i++] = item;
                    }
                }
            }
        }

        public int Count
        {
            get { return items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<string, T> item)
        {
            if (!items.ContainsKey(item.Key))
                return false;

            var x = items[item.Key];

            if ((object)x == null && (object)item.Value == null)
                return items.Remove(item.Key);

            if (x.Equals(item.Value))
                return items.Remove(item.Key);
            else
                return false;
        }

        public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public virtual T Get(string key, T defaultValue = default(T))
        {
            var result = defaultValue;

            if (items.ContainsKey(key))
            {
                var value = items[key];
            }

            return result;
        }
    }
}
