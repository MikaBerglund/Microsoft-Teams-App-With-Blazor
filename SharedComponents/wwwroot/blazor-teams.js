
window.blazorTeams = {
    getContext: function (callbackTarget, methodName) {
        console.log("Getting context...", callbackTarget, methodName);
    },

    initialize: function (callbackTarget, methodName) {
        console.log("Initializing...", callbackTarget, methodName);

        microsoftTeams.initialize(() => {
            console.log("Initialized.");

            if (callbackTarget && methodName) {
                callbackTarget.invokeMethodAsync(methodName);
            }
            else {
                microsoftTeams.appInitialization.notifySuccess();
            }
        });
    }
}