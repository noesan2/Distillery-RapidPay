To run the application you need to:

1 - First request a Bearer token
	* Run app "Identity.JwtToken.API"
	* As a regular user you can Create an get Balance. But you need an extra Claim in your jwt token to make a Payment, add Claim named "accountHolder" when needed to authorize a payment. 
	* You can use Postman to request a new jwt token using the below query;
	
	// Request a valid Bearer Token
	curl --location 'https://localhost:7159/api/JwtIdentity/' \
	--header 'Content-Type: application/json' \
	--data-raw '{
		"UserId": "47370384-344a-437b-8eb6-2a69bc669d53",
		"Email": "noelia.sandoval@distillery.com",
		"Claims": {
			"accountHolder": true
		}

	}'
	
2 - Run API and manage your cards and balance
	* Once with a valid Jwt Token you can start using the RapidPay app to manage Cards
	* First you, as a user needs to get a new card number, (you will obtain 20000 of credit by default to spend). Use the below request
		
		// Request new Card
		curl --location 'http://localhost:16471/api/CardManagment/create' \
		--header 'Authorization: Bearer **Paste here the Bearer token**'

	* Create an extra Card to send the first Payment
	
	* Now you can start paying using your new Card. Use the below request
		
		curl --location 'http://localhost:16471/api/CardManagment/Pay' \
		--header 'Authorization: Bearer **Paste here the Bearer token**' \
		--header 'Content-Type: application/json' \
		--data '{
			"CardNumberFrom": "1",
			"CardNumberTo": "2",
			"Amount": "10"
		}'
		
	* Request Balance using below query:


		// Request Balance
		curl --location 'http://localhost:16471/api/CardManagment/GetBalance/1' \
		--header 'Authorization: Bearer **Paste here the Bearer token**'
		
		
	* There is a background service running, simulating a UFE service. When a new payment is done, this services is queried to calculate the apropiated Fee to be applied. In the VS Output window, you can track hourly the last value 
		When process start: - PaymentFees.RecurrentEventService: Information: Timed Hosted Service running.
		Every Hour: - PaymentFees.RecurrentEventService: Information: Timed Hosted Service is working. UFE Decimal: 0.43696166797015534
		
