using System;
using System.Collections.Generic;
using System.Text;
using DataSharp.Mathematics;

namespace DataSharp.Data.Types
{
    [Serializable]
    public sealed class Metadata
    {
        private Dictionary<string, object> dictionary;

        public static Metadata Empty { get; }

        static Metadata()
        {
            Empty = new Metadata(new Dictionary<string, object>());
        }

        public Metadata(Dictionary<string, object> dictionary)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            this.dictionary = dictionary;
        }

        public bool ContainsKey(string key)
        {
            return dictionary.ContainsKey(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue<T>(string key, out T value)
        {
            object o = dictionary[key];
            if (o is T t)
            {
                value = t;
                return true;
            }
            value = default;
            return false;
        }

        public override bool Equals(object o)
        {
            if (o != null && o is Metadata other)
            {
                Dictionary<string, object> thisDictionary = this.dictionary;
                Dictionary<string, object> otherDictionary = other.dictionary;

                if (thisDictionary.Count != otherDictionary.Count)
                    return false;

                foreach (string key in thisDictionary.Keys)
                {
                    if (!otherDictionary.ContainsKey(key))
                        return false;

                    object thisValue = thisDictionary[key];
                    object otherValue = otherDictionary[key];

                    if (thisValue == null || otherValue == null)
                        return false;

                    if (!thisValue.Equals(otherValue))
                        return false;
                }

                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return dictionary.GetHashCode();
        }

        /// <summary>
        /// Computes the hash code for the types we support.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int GetHashCode(object o)
        {
            switch (o)
            {
                case Array:
                case IDictionary<string, object>:
                case Metadata:
                case long:
                case double:
                case bool:
                case string:
                case null:
                    return o.GetHashCode();
                default:
                    throw new ArgumentException($"Do not support type ${o.GetType()}");
            }
        }
    }
}
