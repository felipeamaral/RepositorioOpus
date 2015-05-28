app.controller('SnippetsCtrl', function ($scope, SnippetService) {

    var qntd = 4;

    /*Pega os snippets da página 1*/
    $scope.snippets = SnippetService.getSnippets(qntd, 1);

    /*Função que faz o download dos códigos do componente*/
    $scope.download = function () { };

    /*Pega os snippets da página informada*/
    $scope.goToPage = function (pageNumber) {
        $scope.snippets = SnippetService.getSnippets(qntd, pageNumber);
    };

    $scope.pages = SnippetService.qntdPaginas(qntd);
    console.log($scope.pages);
    
});