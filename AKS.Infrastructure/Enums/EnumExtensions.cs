﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AKS.Infrastructure.Enums
{
    public static class EnumExtensions
    {
        public static string? GetStringValue(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());

            StringValueAttribute[]? attribs = fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

            return attribs?.Length > 0 ? attribs[0]?.StringValue : null;
        }
    }

    public sealed class StringValueAttribute : Attribute
    {
        public string StringValue { get; private set; }
        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }
    }
}
