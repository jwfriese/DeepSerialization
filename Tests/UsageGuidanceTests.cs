using Newtonsoft.Json;
using PickyJson;
using Xunit;

namespace Tests
{
    public class UsageGuidanceTests
    {
        class DoesNotActuallyImplementTheInterface
        {
        }

        [JsonConverter(typeof(PickySerializer))]
        class PretendObject
        {
            [UseNullReplacementFactory(typeof(DoesNotActuallyImplementTheInterface))]
            public string ShouldNotWork { get; set; }
        }

        [Fact]
        public void ThrowsExceptionWhenUserFalsyClaimsATypeImplementsNullReplacementFactory()
        {
            var obj = new PretendObject()
            {
                ShouldNotWork = null
            };

            Assert.Throws<MissingInterfaceImplException>(() => JsonConvert.SerializeObject(obj));
        }
    }
}