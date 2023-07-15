using Azure.Messaging.EventGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventGrid.Moq
{
    public class ProcessData
    {
        private EventGridPublisherClient _publisherClient;

        public ProcessData(EventGridPublisherClient publisherClient) 
        {
            _publisherClient = publisherClient;
        }

        public async Task Run()
        {
            await _publisherClient.SendEventAsync(new EventGridEvent("subject", "type", "1.1", new BinaryData("")), new CancellationToken());
        }

    }
}
