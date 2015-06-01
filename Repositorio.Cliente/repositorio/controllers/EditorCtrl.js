'use strict';

app.controller('EditorCtrl', function ($scope, apiService) {

    if ($scope.idSnippet != -1) {
        // Pega os arquivos a serem exibidos no editor no formato .zip
        JSZipUtils.getBinaryContent("http://localhost:53412/api/snippet/" + $scope.idSnippet + "/files/download", function (err, data) {
            if (err) {
                throw err; // or handle err
            }

            var zip = new JSZip(data);

            // Verifica se tem o arquivo html
            if (zip.files.hasOwnProperty("arq.html")) {
                $scope.htmlFile = String.fromCharCode.apply(null, zip.files["arq.html"]._data.getContent());
            }

            // Verifica se tem o arquivo css
            if (zip.files.hasOwnProperty("arq.css")) {
                $scope.cssFile = String.fromCharCode.apply(null, zip.files["arq.css"]._data.getContent());
            }

            // Verifica se tem o arquivo js
            if (zip.files.hasOwnProperty("arq.js")) {
                $scope.jsFile = String.fromCharCode.apply(null, zip.files["arq.js"]._data.getContent());
            }
        });
    }

});