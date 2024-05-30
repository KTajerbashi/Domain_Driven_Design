using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.MessageBus.MessageInbox
{
    public interface IMessageBusMessageInbox
    {
        void ExecuteMessageBus();
        void ExecuteMessageInbox();
    }
    public abstract class MessageBusMessageInbox
    {
        public abstract void ExecuteMessageBus();
        public abstract void ExecuteMessageInbox();
    }
}
