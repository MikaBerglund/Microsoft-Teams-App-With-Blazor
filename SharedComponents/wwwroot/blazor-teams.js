
window.blazorTeams = {
    authentication: {
        authenticate: function (options, callback) {
            console.log("Authenticating...", options);

            microsoftTeams.authentication.authenticate({
                url: window.location.origin + options.loginUrl,
                successCallback: function (result) {
                    if (callback && callback.target && callback.methodName) {
                        callback.target.invokeMethodAsync(callback.methodName);
                    }
                },
                failureCallback: function (reason) {
                    console.error("Authentication fail", reason);
                }
            });
        },

        redirectToAuthority: function (options, nonce, state) {
            microsoftTeams.getContext((ctx) => {
                let url = "https://login.microsoftonline.com/" + ctx.tid + "/oauth2/v2.0/authorize?client_id=" + options.clientId + "&response_type=id_token token&response_mode=fragment&redirect_uri=" + window.location.origin + options.loginUrl + "&scope=" + options.scopes.join(" ") + "&nonce=" + nonce + "&state=" + state + "&login_hint=" + ctx.loginHint;
                window.location.assign(url);
            });
        },
    },

    getContext: function (callback) {
        microsoftTeams.getContext((ctx) => {
            if (callback && callback.target && callback.methodName) {
                callback.target.invokeMethodAsync(callback.methodName, ctx);
            }
            else {
                microsoftTeams.appInitialization.notifySuccess();
            }
        });
    },

    initialize: function (callback) {
        microsoftTeams.initialize((args) => {
            if (callback && callback.target && callback.methodName) {
                callback.target.invokeMethodAsync(callback.methodName);
            }
            else {
                microsoftTeams.appInitialization.notifySuccess();
            }
        });
    }

};
