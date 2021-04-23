using System.Threading.Tasks;
using Convey.CQRS.Events;
using Trill.Pusher.Channels;

namespace Trill.Pusher.Events.External.Handlers
{
    internal sealed class ActionRejectedHandler :
        IEventHandler<AdActionRejected>
    {
        private readonly ActionRejectedChannels _channels;

        public ActionRejectedHandler(ActionRejectedChannels channels)
        {
            _channels = channels;
        }

        public async Task HandleAsync(AdActionRejected @event)
        {
            await _channels.Writer.WriteAsync(new ActionRejected
            {
                Reason = @event.Reason,
                Code = @event.Code
            });
        }
    }
}