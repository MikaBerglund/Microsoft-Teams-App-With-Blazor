

window.teamsBlazor = {
    initialize: function () {
        microsoftTeams.initialize();
    },

    notifySuccess: function () {
        microsoftTeams.appInitialization.notifySuccess();
    }
}