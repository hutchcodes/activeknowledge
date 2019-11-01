// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API

window.ckEditorJsInterop = {
    instances: [],
    initializeCKEditor: function (params) {
        console.log(params.ckEditorId);

        for(var key in window.ckEditorJsInterop.instances){
            var editor = window.ckEditorJsInterop.instances[key];
            if (editor) {
                editor.destroy();
                delete ckEditorJsInterop.instances[key];
            }
        }

        var textArea = document.querySelector('#' + params.ckEditorId);
        ClassicEditor
            .create(textArea)
            .then(editor => {
                window.ckEditorJsInterop.instances[params.ckEditorId] = editor;
                console.log("Created CKEditor #" + params.ckEditorId);
            })
            .catch(error => {
                console.error(error);
            });

        return true;
    },
    getData: function (id) {
        var editor = window.ckEditorJsInterop.instances[id];
        if (editor) {
            return editor.getData();
        }
        return "";
    },
    destroyCKEditor: function (id) {
        var editor = window.ckEditorJsInterop.instances[id];
        if (editor) {
            editor.destroy();
            delete ckEditorJsInterop.instances[id];
        }
        console.log("Destroyed CKEditor #" + id);
        return true;
    },
    executeCKCommand: function (params) {
        var id = params.ckEditorId, command = params.commandName, data = params.data;
        var editor = window.ckEditorJsInterop.instances[id];
        if (editor) {
            editor.execute(command, data);
        }
        return "";
    }
};
