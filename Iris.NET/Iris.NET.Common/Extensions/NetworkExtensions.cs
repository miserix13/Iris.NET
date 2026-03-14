using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Iris.NET
{
    /// <summary>
    /// Extension methods for network operations.
    /// </summary>
    public static class NetworkExtensions
    {
        private static readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        /// <summary>
        /// Serializes an object into a memory stream.
        /// </summary>
        /// <param name="o">The object to serialize.</param>
        /// <returns>A memory stream containing the serialized object.</returns>
        public static MemoryStream SerializeToMemoryStream(this object o)
        {
            MemoryStream stream = new MemoryStream();
            SerializeToMemoryStream(o, stream);
            return stream;
        }

        /// <summary>
        /// Serializes an object into a given memory stream.
        /// </summary>
        /// <param name="o">The object to serialize.</param>
        /// <param name="stream">The memory stream into which serialize the object.</param>
        public static void SerializeToMemoryStream(this object o, MemoryStream stream)
        {
            var json = JsonConvert.SerializeObject(o, _jsonSettings);
            var bytes = Encoding.UTF8.GetBytes(json);
            stream.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Deserializes a memory stream into an instance of type {T}.
        /// </summary>
        /// <typeparam name="T">The type of the deserialized object.</typeparam>
        /// <param name="stream">The memory stream to deserialize.</param>
        /// <returns>The deserialized object as {T}.</returns>
        public static T DeserializeFromMemoryStream<T>(this MemoryStream stream) where T : class => DeserializeFromMemoryStream(stream) as T;

        /// <summary>
        /// Deserializes a memory stream into an object.
        /// </summary>
        /// <param name="stream">The memory stream to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        public static object DeserializeFromMemoryStream(this MemoryStream stream)
        {
            var json = Encoding.UTF8.GetString(stream.ToArray());
            return JsonConvert.DeserializeObject(json, _jsonSettings);
        }

        /// <summary>
        /// Reads the next data coming from the stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <param name="size">Maximum size to read. The default is 16 * 1024.</param>
        /// <returns></returns>
        public static MemoryStream ReadNext(this Stream input, int size = 16 * 1024)
        {
            byte[] buffer = new byte[size];
            MemoryStream ms = new MemoryStream();
            int read = input.Read(buffer, 0, buffer.Length);
            ms.Write(buffer, 0, read);
            return ms;
        }
    }
}
