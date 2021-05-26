# Generate Book API Client 

The Book API client is generated with autorest, see https://aka.ms/autorest for details.

## AutoRest Client Generator Configuration
``` yaml
use-extension:
  "@microsoft.azure/autorest.modeler": "2.3.55"
  "@microsoft.azure/autorest.typescript": "4.2.4"

#version: 3.0.6247 #autorest version
input-file: http://localhost:5000/swagger/v1/swagger.json
output-folder: .
typescript: 
  source-code-folder-path: ./autorest
  override-client-name: BookApiClient
  client-side-validation: false # disable client side validation of constraints
  generate-metadata: false
```
