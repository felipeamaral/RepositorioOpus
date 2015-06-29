app.controller('materialTemplateCtrl', function ($scope, $mdDialog) {

    $scope.itensMenu = ['Projetos', 'Snippets'];
    $scope.cor = 'green';

    // Paletta de cores possíveis
    $scope.palette = ['#F44336', '#E91E63', '#9C27B0', '#673AB7', '#3F51B5', '#2196F3', '#03A9F4', '#00BCD4',
                        '#009688', '#4CAF50', '#8BC34A', '#CDDC39', '#FFEB3B', '#FFC107', '#FF9800', '#FF5722',
                        '#795548', '#9E9E9E', '#607D8B'];
    $scope.colorSelected = '#3F51B5';

    // Fecha o modal
    $scope.cancelar = function () {
        $mdDialog.hide();
    };

    $scope.selectColor = function (cor) {
        $(".selected").removeClass('selected');
        $scope.colorSelected = cor;
    }

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