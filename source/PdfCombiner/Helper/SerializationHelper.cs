using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PdfCombiner.Helper
{
    class AllPropertiesResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            property.Ignored = false;
            return property;
        }
    }

    class AllPropertiesExceptIgnoredResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);
            prop.Ignored = false;

            var property = member as PropertyInfo;
            if (property != null)
            {
                var a = property.GetCustomAttribute(typeof(IgnoreDataMemberAttribute));
                if (a != null)
                {
                    prop.Ignored = true;
                }
            }

            return prop;
        }
    }

    /// <summary>
    ///     Helper class for handling JSON serializing
    /// </summary>
    public static class JsonSerializer
    {
        /// <summary>
        ///     Serialized a object to a JSON string
        /// </summary>
        /// <param name="obj">Object that should be serialized</param>
        /// <param name="indented">True if the JSON format should be intended. Default = False</param>
        /// <param name="excludeType">True if the objects types should be excluded. Default = False</param>
        /// <param name="excludePropertiesWithIgnoreDataMemberAttribute">True, if properties with "IgnoreDataMember" attribute should be excluded. Default = False</param>
        public static string SerializeObject(object obj, bool indented = false, bool excludeType = false, bool excludePropertiesWithIgnoreDataMemberAttribute = false)
        {
            DefaultContractResolver contractResolver;
            if (excludePropertiesWithIgnoreDataMemberAttribute) contractResolver = new AllPropertiesExceptIgnoredResolver();
            else contractResolver = new AllPropertiesResolver();

            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, ContractResolver = contractResolver, ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            if (indented) settings.Formatting = Formatting.Indented;
            if (excludeType) settings.TypeNameHandling = TypeNameHandling.None;

            return JsonConvert.SerializeObject(obj, settings);
        }

        /// <summary>
        ///     Deserializes a JSON string to a generic object
        /// </summary>
        /// <typeparam name="T">Type the object should have after deserializing</typeparam>
        /// <param name="json">String that represents the object</param>
        public static T DeserializeObject<T>(string json)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ContractResolver = new AllPropertiesResolver(),
                NullValueHandling = NullValueHandling.Include
            };

            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        /// <summary>
        ///     Deserializes an object, where the string has to be exactly the same as the target object
        /// </summary>
        /// <typeparam name="T">Type the object should have after deserializing</typeparam>
        /// <param name="json">String that represents the object</param>
        /// <param name="onError">EventHandler for handling deserializing errors</param>
        public static T DeserializeObjectExact<T>(string json, EventHandler<ErrorEventArgs> onError)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ContractResolver = new AllPropertiesResolver(),
                NullValueHandling = NullValueHandling.Include,
                Error = onError,
                MissingMemberHandling = MissingMemberHandling.Error
            };
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
    }

    /// <summary>
    ///     Helper class for handling JSON file operations
    /// </summary>
    public static class JsonFileHelper
    {
        /// <summary>
        ///     Loading a JSON file from the file system
        /// </summary>
        /// <typeparam name="T">Type the object should have after deserializing</typeparam>
        /// <param name="path">Path where the file is located</param>
        public static T LoadJsonFile<T>(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path));

            try
            {
                var configFileContent = File.ReadAllText(path);
                return JsonSerializer.DeserializeObject<T>(configFileContent);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        ///     Saves a object as JSON file
        /// </summary>
        /// <param name="obj">Object that should be serialized</param>
        /// <param name="path">File path where the objects JSON should be saved</param>
        /// <param name="excludeType">True if the objects types should be excluded. Default = False</param>
        public static void SaveJsonFile(object obj, string path, bool excludeType = false)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path));

            try
            {
                var json = JsonSerializer.SerializeObject(obj, true, excludeType);
                new FileInfo(path).Directory?.Create();

                File.WriteAllText(path, json);
            }
            catch
            {
                // ignored
            }
        }
    }
}
