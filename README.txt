To run the application just F5 should be enough.
Below I leave some queries to import in Postman and test it, checkout the port where the application startups
User and password it's preloaded at the context with admin:admin

// Create a card
curl --location 'https://localhost:7145/api/Card' \
--header 'Content-Type: application/json' \
--header 'Authorization: Basic YWRtaW46YWRtaW4=' \
--data '{
	"Name": "Adrian"
}'

// Do a payment
curl --location 'https://localhost:7145/api/Payments' \
--header 'Content-Type: application/json' \
--header 'Authorization: Basic YWRtaW46YWRtaW4=' \
--data '{
	"CardId": "0306d638-a90e-4d0e-bbee-275d85410215",
	"Amount": 123.02
}'

// Get balance
curl --location 'https://localhost:7145/api/Balance/0306d638-a90e-4d0e-bbee-275d85410215' \
--header 'Authorization: Basic YWRtaW46YWRtaW4='

Things that can be improve:
* Unit test, EVERYWHERE, code it’s designed to be tested using mocks, controllers and services can be mocked using Moq and repositories can be tested using an in memory database.
* Logging: missing logging at many places for example authentication and upload this logging to a cloud service like AppInsights or New Relic.
* Error handling: should be a centralized error handling that logs exceptions that bubbles up and in low environments show them to the user.
* Swagger should have the basic authentication configured
* Models from the database can be different from domain models and mapped using Automapper.
* In case universal fees exchange has big and complex logic it should have its own project or even be a micro service.
* I like to implement health checks in the application and usually reduce the troubleshooting time.

