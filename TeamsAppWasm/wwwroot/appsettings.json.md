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
    ],
    "TenantId": "common"
  }
}
```

These properties are:

- `ClientId` - The application ID aka client ID of your application. You need to register your application in Azure AD to get this.
- `LoginUrl` - The URL in your application that implements the login functionality. This is a page in your application that can use the `LoginDialogHandler` component, which takes care of all necessary things related to logging in. You just need to have this page in your application.
- `Scopes` - The scopes that your application requires to function. Defaults to `user.read` and `openid`. These scopes allow the application to sign the user in and read basic profile information.
- `TenantId` - The ID or name of your tenant. This can be either the GUID or name (`[tenant].onmicrosoft.com`) of your tenant. It can also be configured to `common` (default), `organizations` or `consumers`. [Read more](https://docs.microsoft.com/en-us/azure/active-directory/develop/msal-client-application-configuration)...
