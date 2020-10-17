using System;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Trill.Pusher.Channels;

namespace Trill.Pusher.Events.External.Handlers
{
    internal sealed class StorySentHandler : IEventHandler<StorySent>
    {
        private readonly StorySentChannels _channels;

        public StorySentHandler(StorySentChannels channels)
        {
            _channels = channels;
        }
        
        public async Task HandleAsync(StorySent @event)
        {
            if (@event.Visibility.From <= DateTime.UtcNow)
            {
                await _channels.Writer.WriteAsync(@event);
            }
        }
    }
}