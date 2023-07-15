using Azure;
using Azure.Messaging.EventGrid;
using EventGrid.Moq;
using Moq;
using System.Net;

// Create some storage to analyse the events
List<EventGridEvent> events = new List<EventGridEvent>();

// Mock the event grid and response
Mock<EventGridPublisherClient> mockClient = new();
Mock<Response> mockResponse = new();
mockResponse.SetupGet(x => x.Status).Returns((int)HttpStatusCode.OK);
mockResponse.SetupGet(x => x.Content).Returns(BinaryData.FromString(""));

// Tell the mock to store the data in the list
mockClient.Setup(c => c.SendEventAsync(It.IsAny<EventGridEvent>(), It.IsAny<CancellationToken>()))
    .Callback((EventGridEvent ev, CancellationToken ct) => 
        { 
            events.Add(ev); // Or call an actual endpoint if we want to simulate that for integration purposes
        })
    .ReturnsAsync(mockResponse.Object);

// Send the mock to the class instead of the real thing
ProcessData procesData = new ProcessData(mockClient.Object);

// Start processing
await procesData.Run();

// Wait!
foreach (EventGridEvent ev in events)
{
    Console.WriteLine(ev.Data.ToString());
}

Console.ReadLine();


