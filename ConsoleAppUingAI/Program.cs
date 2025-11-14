using Azure;
using OpenAI.Chat;
using System.ClientModel;

var chatClient = new ChatClient(
    "gpt-4.1-mini",
    new ApiKeyCredential("your api key here"),
    new OpenAI.OpenAIClientOptions { Endpoint = new Uri("https://models.github.ai/inference") }
);

Console.WriteLine("GPT-4.1-Mini Chat - type 'exit' to quit");
Console.WriteLine();

List<ChatMessage> chatHistory = new();
while (true)
{
  Console.Write("You: ");
  string? userInput = Console.ReadLine();
  if(String.IsNullOrEmpty(userInput)){
    continue;
  }    
    if (userInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }
    chatHistory.Add(ChatMessage.CreateUserMessage(userInput));

    Console.WriteLine("Assistant: ");
    String assistantResponse = string.Empty;

    var response = await chatClient.CompleteChatAsync(chatHistory);
    assistantResponse = response.Value.Content[0].Text;
    Console.WriteLine(assistantResponse);
    
    chatHistory.Add(ChatMessage.CreateAssistantMessage(assistantResponse));
    Console.WriteLine();
}