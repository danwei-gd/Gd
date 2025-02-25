using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;

var client = new HttpClient();
client.Timeout = TimeSpan.FromSeconds(3600);

var customerToken = Guid.NewGuid().ToString();
var clientRequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:7071/api/programs/sec/customers")
{
	Content = new StringContent($@"
	{{
		""customerToken"": ""{customerToken}"",
		""firstName"": ""wei"",
		""lastName"": ""dan"",
		""middleName"": ""string"",
		""email"": ""{Guid.NewGuid()}"",
		""phoneNumber"": ""string"",
		""dateOfBirth"": ""string"",
		""address"": {{
			""addressLine1"": ""string"",
			""addressLine2"": ""string"",
			""city"": ""string"",
			""state"": ""st"",
			""zipCode"": ""string""
		}}
	}}", System.Text.Encoding.UTF8, "application/json")
};
clientRequestMessage.Headers.Add("X-GD-RequestId", customerToken);
Console.WriteLine($"customerToken: {customerToken}");
Console.WriteLine("------------------------------------------------------------------------------------");
var clientResponse = await client.SendAsync(clientRequestMessage);
Console.WriteLine(await clientResponse.Content.ReadAsStringAsync());




var cardRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:7071/api/programs/sec/customers/{customerToken}/paymentInstruments")
{
	Content = new StringContent($@"		
	{{
	  ""customerToken"": ""{customerToken}"",
	  ""firstName"": ""string"",
	  ""middleName"": ""string"",
	  ""lastName"": ""string"",
	  ""cardAccount"": {{
	    ""accountNumber"": ""{4234567890123456}"",
	    ""expiryMonth"": ""12"",
	    ""expiryYear"": ""2025"",
	    ""last4Pan"": ""1234""
	  }},
	  ""nickName"": ""string"",
	  ""address"": {{
	    ""addressLine1"": ""string"",
	    ""addressLine2"": ""string"",
	    ""city"": ""string"",
	    ""state"": ""AL"",
	    ""zipCode"": ""12345""
	  }}
	}}		
	", System.Text.Encoding.UTF8, "application/json")
};
cardRequestMessage.Headers.Add("X-GD-RequestId", customerToken);
var cardResponse = await client.SendAsync(cardRequestMessage);
var cardResult = await cardResponse.Content.ReadAsStringAsync();
JsonNode node = JsonNode.Parse(cardResult);

var profileId = node["paymentInstrument"]["paymentInstrumentId"].GetValue<string>(); 
Console.WriteLine($"profileId: { profileId }");
Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
Console.WriteLine(cardResult);


var transferId = Guid.NewGuid().ToString();
var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:7076/api/programs/sec/transfers")
{
	Content = new StringContent($@"
{{
	""transferId"": ""{transferId}"",
	""transferType"": 9,
	""initiator"": ""1a42172b-4aaf-4a9c-abe2-c4fe059707c2"",
	""transferRoute"": {{
		""transactionAmount"": 1,
		""sourceTransferEndpoint"": {{
			""transferEndpointType"": 2,
			""identifier"": ""{ profileId }"",
                        ""cardHolderAuthenticationVerification"":""AAABBCYNFYSNFXmQEjRWAAAAAA=="",
                        ""ecommTransactionIndicator"":""05"",
			""currency"": ""USD""
		}},
		""targetTransferEndpoint"": {{
			""transferEndpointType"": 3,
			""currency"": ""USD"",
			""paymentInstrument"": {{
				""cardAccount"": {{
					""accountNumber"": ""5102550000000044"",
					""expiryMonth"": ""10"",
					""expiryYear"": ""2027""
				}}
			}}
		}}
	}},
        ""fraudData"": {{""AuthenticationType"": ""Fingerprint""}}
}}
			
	", System.Text.Encoding.UTF8, "application/json"),
};
httpRequestMessage.Headers.Add("X-GD-RequestId", transferId);
Console.WriteLine($"transferId: { transferId }");
Console.WriteLine("---------------------------------------------------------------------------------");
var response = await client.SendAsync(httpRequestMessage);

Console.WriteLine(await response.Content.ReadAsStringAsync());


