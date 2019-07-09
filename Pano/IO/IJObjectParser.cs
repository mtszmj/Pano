﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pano.IO
{
    public interface IJObjectParser
    {
        TEnum TryParseEnum<TEnum>(JsonReader reader, string property) where TEnum : struct, IComparable, IFormattable, IConvertible;
        T ToObject<T>(JsonReader reader, Newtonsoft.Json.JsonSerializer serializer) where T : class;
    }
}
