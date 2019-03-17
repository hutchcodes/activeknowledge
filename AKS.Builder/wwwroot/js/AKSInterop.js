window.AKS = {
    SelectTopic: function (commandName) {
        
        return DotNet.invokeMethodAsync('AKS.Builder', 'SelectTopic', commandName)
            .then(data => {
                console.log(data);
            });
    }
}