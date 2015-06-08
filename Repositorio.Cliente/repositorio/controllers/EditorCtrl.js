'use strict';

app.controller('EditorCtrl', function ($scope, apiService) {

    $scope.aceSessions = [];
    var cont = 0;

    if ($scope.idSnippet != -1) {
        // Pega os arquivos a serem exibidos no editor no formato .zip
        JSZipUtils.getBinaryContent("http://localhost:53412/api/snippet/" + $scope.idSnippet + "/files/download", function (err, data) {
            if (err) {
                throw err; // or handle err
            }

            var zip = new JSZip(data);

            // Verifica se tem o arquivo html
            if (zip.files.hasOwnProperty("arq.html")) {
                $scope.aceSessions[0].setValue(String.fromCharCode.apply(null, zip.files["arq.html"]._data.getContent()));
            }

            // Verifica se tem o arquivo css
            if (zip.files.hasOwnProperty("arq.css")) {
                $scope.aceSessions[1].setValue(String.fromCharCode.apply(null, zip.files["arq.css"]._data.getContent()));
            }

            // Verifica se tem o arquivo js
            if (zip.files.hasOwnProperty("arq.js")) {
                $scope.aceSessions[2].setValue(String.fromCharCode.apply(null, zip.files["arq.js"]._data.getContent()));
            }   
        });
    }

    // Inicializa a sessão de cada um dos editores
    $scope.aceLoaded = function (_editor, tipo) {
        _editor.$blockScrolling = Infinity;
        $scope.aceSessions[cont] = _editor.getSession();
        cont++;
    };

    // Faz download dos arquivos
    $scope.$on('download', function (event, args) {

        // Cria o arquivo a ser feito o download com o código contido no editor
        var zipDownload = new JSZip();

        if ($scope.aceSessions[0].getValue() != undefined) {
            zipDownload.file('arq.html', $scope.aceSessions[0].getValue());
        }
        if ($scope.aceSessions[1].getValue() != undefined) {
            zipDownload.file('arq.css', $scope.aceSessions[1].getValue());
        }
        console.log($scope.jsFile);
        if ($scope.aceSessions[2].getValue() != undefined) {
            zipDownload.file('arq.js', $scope.aceSessions[2].getValue());
        }

        // Faz o download do arquivo
        var blob = zipDownload.generate({ type: "blob" });
        saveAs(blob, "teste.zip");
    });

});