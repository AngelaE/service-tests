# Generate Book Stats Client 

The Book API client is generated with autorest, see https://aka.ms/autorest for details.

Run ```npm run generate-bookstats-client``` in the root directory to generate the client

## AutoRest Client Generator Configuration
``` yaml
use-extension:
  "@microsoft.azure/autorest.modeler": "2.3.55"
  "@microsoft.azure/autorest.csharp": "2.3.82"

version: 3.0.6247 #autorest version
input-file: bookstats.yaml
output-folder: ./autorest
csharp: 
  namespace: BookStats.Autorest
  override-client-name: BookStatsClient
  client-side-validation: false
```
