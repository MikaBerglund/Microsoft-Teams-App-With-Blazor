
window.blazorTeams = {
    config: {
        /*
        This object only contains placeholders for the configuration items
        required by this code. You should add a separate script file to your
        appplication that replaces this object with the actual values
        for your application.

        The values here should be considered as default values.
        */

        // The client ID of your application (as registered in Azure AD)
        clientId: "00000000-0000-0000-0000-000000000000",

        redirectUri: "/",

        // The relative path to your page that initializes authentication.
        loginUrl: "/login",

        // An array of scopes (permissions) that your application requires
        // on behalf of the user.
        scopes: [
            "user.read",
            "openid"
        ]
    },

    getConfig: function () {
        return blazorTeams.config;
    },

    authenticate: function () {
        microsoftTeams.authentication.authenticate({
            url: window.location.origin + this.config.loginUrl,
            successCallback: function (result) {
                console.log("Authentication success", result);
            },
            failureCallback: function (reason) {
                console.error("Authentication fail", reason);
            }
        });
    },

    redirectToAuthority: function (nonce, state) {
        microsoftTeams.getContext((ctx) => {
            let url = "https://login.microsoftonline.com/" + ctx.tid + "/oauth2/v2.0/authorize?client_id=" + blazorTeams.config.clientId + "&response_type=id_token token&response_mode=fragment&redirect_uri=" + window.location.origin + blazorTeams.config.loginUrl + "&scope=" + blazorTeams.config.scopes.join(" ") + "&nonce=" + nonce + "&state=" + state + "&login_hint=" + ctx.loginHint;

            console.log("Redirecting to authorization endpoint.", url);

            window.location.assign(url);
        });
    },

    getContext: function (callbackTarget, methodName) {
        microsoftTeams.getContext((ctx) => {
            if (callbackTarget && methodName) {
                callbackTarget.invokeMethodAsync(methodName, ctx);
            }
        });
    },

    initialize: function (callbackTarget, methodName) {
        microsoftTeams.initialize(() => {
            if (callbackTarget && methodName) {
                callbackTarget.invokeMethodAsync(methodName);
            }
            else {
                microsoftTeams.appInitialization.notifySuccess();
            }
        });
    },

    aad: {
        getToken: function (username, callbackTarget, methodName) {
            console.log("Getting token...");


            let msalConfig = this.getMsalConfig();
            console.log("MSAL config", msalConfig);

            let msalApp = new msal.PublicClientApplication(msalConfig);
            console.log("MSAL App", msalApp)

            msalApp.acquireTokenSilent({
                scopes: blazorTeams.config.scopes,
                account: msalApp.getAccountByUsername(username)
            }).then(response => {
                console.log("Token silent", response);
            }).catch(err => {
                console.error("Token silent", err);
                if (callbackTarget && methodName) {
                    callbackTarget.invokeMethodAsync(methodName);
                }
            })
        },

        login: function (callbackTarget, methodName) {
            console.log("Logging in...");

            let config = this.getMsalConfig();
            let app = new msal.PublicClientApplication(config);
            app
                .loginPopup({ scopes: blazorTeams.config.scopes })
                .then((response) => {
                    console.log("Login", response);

                })
                .catch((err) => {
                    console.log("Login error", err);
                })
                ;
        },

        getMsalConfig: function () {
            return {
                auth: {
                    clientId: blazorTeams.config.clientId,
                    authority: blazorTeams.config.authority,
                    redirectUri: window.location.origin + blazorTeams.config.redirectUri
                },
                cache: {
                    cacheLocation: "localStorage"
                }
            };
        }
    }
};
