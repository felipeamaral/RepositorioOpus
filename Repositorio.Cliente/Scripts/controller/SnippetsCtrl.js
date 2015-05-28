app.controller('SnippetsCtrl', function ($scope, apiService) {

    var qntd = 4;

    /*Pega os snippets da página informada*/
    $scope.goToPage = function (pageNumber) {
        var aux = apiService.getImagens.query({ qntd: qntd, pageNumber: pageNumber }, function () {
            $scope.snippets = aux;
        });
    };

    /*Pega os snippets da página 1*/
    $scope.goToPage(1);

    /*Função que faz o download dos códigos do componente*/
    $scope.download = function () { };

    /*Quantidade de páginas no total*/
    $scope.paginas = [];
    var q = apiService.getImagens.query(function () {
        var qntdPaginas = q % qntd > 0 ? Math.floor(q / qntd) + 1 : Math.floor(q / qntd);
        for (var i = 0; i < qntdPaginas; i++){
            $scope.paginas[i] = i + 1;
        }
    });
    
    
});