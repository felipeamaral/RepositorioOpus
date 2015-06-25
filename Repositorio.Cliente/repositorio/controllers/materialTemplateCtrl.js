app.controller('materialTemplateCtrl', function ($scope, $mdDialog) {

    $scope.itensMenu = ['Projetos', 'Snippets'];
    $scope.cor = 'green';

    // Fecha o modal
    $scope.cancelar = function () {
        $mdDialog.cancel();
    };

    // Gera o template com os parâmetros selecionados e faz o download do mesmo
    $scope.downloadTemplate = function () {

        var itens = "";

        // Inicia a montagem da string contendo os itens do menu a ser gerador
        if ($scope.itensMenu.length) {
            itens = "?itens=" + $scope.itensMenu[0];
        }
        // Insere os demais itens na string
        for (var i = 1; i < $scope.itensMenu.length; i++) {
            itens += "&itens=" + $scope.itensMenu[i];
        }

        // Faço o download do template, enviando pra web api os parâmetros com os quais ele deve ser gerado
        window.open('http://localhost:53412/api/template/material/' + $scope.cor + itens, '_self');
    }
});