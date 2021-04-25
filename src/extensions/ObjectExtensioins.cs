using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;

namespace toolstray
{
    public static class ObjectExtensions
    {
        public static T JSONFind<T>(this object obj, params object[] indexes) => obj._JSONFind<T>(indexes);
        static T _JSONFind<T>(this object obj, object[] indexes)
        {
            if (indexes.Length == 0)
            {
                return obj switch
                {
                    null => throw _GetEx("値が null です"),
                    T result => result,
                    var result => throw _GetEx($"型不一致です。:{result.GetType()}"),
                };
            }

            var key = indexes[0];
            var remine = indexes[1..^0];

            if (key == null)
                throw _GetEx("キーが null です");

            return obj switch
            {
                IDictionary dic => dic[key] switch
                {
                    null => throw _GetEx($"辞書の値が nullです。key:{key}"),
                    var x => x._JSONFind<T>(remine),
                },
                IList list => list[(int)key] switch
                {
                    null => throw _GetEx($"リストの値が nullです。key:{key}"),
                    var x => x._JSONFind<T>(remine),
                },
                var other => throw _GetEx($"キーが不一致です。dic:{JsonSerializer.Serialize(other)} key:{JsonSerializer.Serialize(key)}"),
            };
        }

        static Exception _GetEx(string msg) => new Exception(msg);

        public static bool JSONContainsKey(this object obj, params object[] indexes) => obj._JSONContainsKey(indexes);
        static bool _JSONContainsKey(this object obj, object[] indexes)
        {
            if (indexes.Length == 0)
            {
                return true;
            }

            var key = indexes[0];
            var remine = indexes[1..^0];

            if (key == null) return false;

            return obj switch
            {
                IDictionary dic => dic[key] switch
                {
                    null => false,
                    var x => x._JSONContainsKey(remine),
                },
                IList list => list[(int)key] switch
                {
                    null => false,
                    var x => x._JSONContainsKey(remine),
                },
                var other => false,
            };
        }
    }
}