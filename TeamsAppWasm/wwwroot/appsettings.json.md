# appsettings.json

To properly run your application, you need to add the `appsettings.json` file to the same folder with this documentation. This file contains all the necessary settings your applicaiton needs to run.

The sample JSON document below shows the contents of that file.

``` JSON
{
  "BlazorTeamsApp": {
    "ClientId": "<Application (client) ID>",
    "LoginUrl": "/login",
    "Scopes": [
      "user.read",
      "openid"
    ]
  }
}
```