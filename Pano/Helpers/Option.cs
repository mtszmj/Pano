using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.Helpers
{
    public class Option<T>
    {
        public static readonly Option<T> None = new Option<T>();

        private T _value;
        private bool _hasValue;

        public Option() : this(default(T), false) { }
        public Option(T value) : this(value, true) { }
        public Option(T value, bool hasValue)
        {
            _value = value;
            _hasValue = hasValue;
        }

        public T Value => _hasValue ? _value : throw new InvalidOperationException("Option has no value");
        public bool HasValue => _hasValue;

        public T GetValueOrDefault() => _hasValue ? _value : default(T);
        public T GetValueOrDefault(T @default) => _hasValue ? _value : @default;

        public void Set(T value)
        {
            _value = value;
            _hasValue = true;
        }

        public void Reset()
        {
            _value = default(T);
            _hasValue = false;
        }

        public static implicit operator bool(Option<T> option) => option.HasValue;
    }
}
