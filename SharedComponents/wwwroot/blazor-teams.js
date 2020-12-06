
window.blazorTeams = {
    getContext: function (callbackTarget, methodName) {
        console.log("Getting context...", callbackTarget, methodName);

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
    }
}