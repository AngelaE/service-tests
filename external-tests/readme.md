# Component Tests with Mountebank

Check out this blog to read up on the details of the sytem under test: https://angela-evans.com/easy-api-tests-with-mountebank/

This project contains the following:

1. [HTTP requests for the book API](./http-requests)
2. [A sample e2e test](./test-e2e)
3. [Component tests using Mountebank to mock the Stats API](./test)
4. [Test support code](./src)

## Running the tests
1. Install and start [Mountebank](https://www.npmjs.com/package/mountebank)
2. Start the book API service
3. Run ```npm install``` in the current directory
4. To run the e2e tests start the book stats API, then run just the e2e tests with ```npm run test-e2e```
5. To run the component tests (everything in the ./test directory):
   1. Stop the book stats API 
   2. Run ```npm run create-proxy``` - This creates a proxy in mountebank which forwards all requests to port 5011, where the mock stats API is created. 
        This proxy also records all requests and allows 'debugging' of the traffic between Book API and Stats API.  
        Alternatively change the [port for the stats API mock](https://github.com/AngelaE/service-tests/blob/c5c71ca72c8c22b6417c6b08ad1b956e123b6a38/external-tests/src/config.ts#L5)
        to 5010
    3. Run ```npm run test``` to run just the component tests
