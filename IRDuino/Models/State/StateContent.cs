using System.Net.Http;
using System.Text;

namespace IRDuino.Models.State
{
    public static class StateContent
    {
        public static ByteArrayContent GetContentForTurnOff()
        {
            var buffer = Encoding.UTF8.GetBytes("{\"command\":\"gpio/write\",\"parameters\":{\"13\":\"0\"}}");
            return new ByteArrayContent(buffer);
        }
        public static ByteArrayContent GetContentForCommand()
        {
            var buffer = Encoding.UTF8.GetBytes("{\"command\":\"gpio/write\",\"parameters\":{\"13\":\"0\"}}");
            return new ByteArrayContent(buffer);
        }
    }
}
