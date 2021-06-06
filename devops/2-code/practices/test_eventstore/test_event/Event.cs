using System;
using System.Text;
using Newtonsoft.Json;

namespace test_event
{
    public class MyModel : MyBaseEvent, IEventId
    {
        /// <inheritdoc />
        public Guid Id { get; } = Guid.NewGuid();
    }

    public abstract class MyBaseEvent
    {
        public string GetTypeString() => GetType().ToString().ToLower();

        public byte[] GetByteArray() => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this));
    }

    public interface IEventId
    {
        Guid Id { get; }
    }
}