using System;
using System.Collections.Generic;
using System.Text;

namespace DataSharp.Data.Types
{
    [Serializable]
    public sealed class Metadata
    {
        private Dictionary<string, object> dictionary;

        public bool ContainsKey(string key)
        {
            return dictionary.ContainsKey(key);
        }

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
                
            }

            return false;
        }
    }
}
