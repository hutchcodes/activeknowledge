window.AKS = {
    SelectTopic: function (commandName) {

        return DotNet.invokeMethodAsync('AKS.App.Core', 'SelectTopic', commandName)
            .then(data => {
                console.log(data);
            });
    }
};