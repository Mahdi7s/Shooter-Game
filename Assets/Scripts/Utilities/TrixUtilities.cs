using System;
using System.Collections;
using Models.Constants;
using UnityEngine;

namespace Utilities
{
    public static class TrixUtilities
    {
        public static string GetDataSetPath(Type type)
        {
            return $"{StaticValues.DirectoryResources}{type.Name}{(type.Name.Contains("DataSet") ? string.Empty : "DataSet")}.asset";
        }
    }
}
