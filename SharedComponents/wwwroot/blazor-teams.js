
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

    authenticate: function (callbackTarget, methodName) {
        microsoftTeams.authentication.authenticate({
            url: window.location.origin + this.config.loginUrl,
            successCallback: function (result) {
                if (callbackTarget && methodName) {
                    callbackTarget.invokeMethodAsync(methodName);
                }
            },
            failureCallback: function (reason) {
                console.error("Authentication fail", reason);
            }
        });
    },

    redirectToAuthority: function (nonce, state) {
        microsoftTeams.getContext((ctx) => {
            let url = "https://login.microsoftonline.com/" + ctx.tid + "/oauth2/v2.0/authorize?client_id=" + blazorTeams.config.clientId + "&response_type=id_token token&response_mode=fragment&redirect_uri=" + window.location.origin + blazorTeams.config.loginUrl + "&scope=" + blazorTeams.config.scopes.join(" ") + "&nonce=" + nonce + "&state=" + state + "&login_hint=" + ctx.loginHint;
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
};
