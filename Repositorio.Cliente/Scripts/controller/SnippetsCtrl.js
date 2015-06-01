app.controller('SnippetsCtrl', function ($scope, apiService) {

    var qntd = 2;
    var qntdPaginasMostra = 5;

    $scope.qntdPaginas = 0;
    $scope.paginaAtual = 1;

    /*Pega os snippets da página informada*/
    $scope.goToPage = function (pageNumber, primeira, last) {
        var aux = apiService.getImagens.query({ qntd: qntd, pageNumber: pageNumber }, function () {

            /*Verifica se clicou pra ir direto pra primeira página*/
            if (primeira || $scope.paginaAtual != pageNumber) {
                for (i = 0; i < qntdPaginasMostra && i < $scope.qntdPaginas; i++) {
                    $scope.paginas[i] = i+1;
                }
            }

            /*Verifica se clicou pra ir direto pra ultima pagina*/
            if (last && $scope.paginaAtual != pageNumber) {
                cont = 0;
                for (i = $scope.qntdPaginas - qntdPaginasMostra >= 0 ? $scope.qntdPaginas - qntdPaginasMostra + 1 : 1; i <= $scope.qntdPaginas; i++) {
                    $scope.paginas[cont] = i;
                    cont++;
                }
            }

            $scope.snippets = aux;
            $scope.paginaAtual = pageNumber;
        });
    };

    /*Pega os snippets da página 1*/
    $scope.goToPage(1);

    /*Vetor com numeração das páginas a serem mostradas*/
    $scope.paginas = [];

    /*Função que faz o download dos códigos do componente*/
    $scope.download = function () { };
    

    /*Inicializa as páginas a serem mostradas*/
    var q = apiService.getImagens.query(function () {
        $scope.qntdPaginas  = q % qntd > 0 ? Math.floor(q / qntd) + 1 : Math.floor(q / qntd);
        for (var i = 0; i < qntdPaginasMostra && i < $scope.qntdPaginas; i++){
            $scope.paginas[i] = i + 1;
        }
    });

    /*Vai pra próxima pagina*/
    $scope.goToNextPage = function () {

        /*Verifica se existem páginas a serem mostradas*/
        if ($scope.paginaAtual < $scope.qntdPaginas) {
            $scope.goToPage($scope.paginaAtual + 1);

            /*Ta na ultima pagina que ta sendo mostrada*/
            if ($scope.paginaAtual == $scope.paginas[qntdPaginasMostra - 1]) {
                
                for (i = 0; i < qntdPaginasMostra; i++) {
                    $scope.paginas[i] = $scope.paginas[i] + 1;
                }
            }
        }
    }

    /*Vai para a pagina anterior*/
    $scope.goToPreviousPage = function () {

        /*Verifica se existem páginas a serem mostradas*/
        if ($scope.paginaAtual > 1) {
            $scope.goToPage($scope.paginaAtual - 1);

            /*Ta na ultima primeira que ta sendo mostrada*/
            if ($scope.paginaAtual == $scope.paginas[0]) {

                for (i = 0; i < qntdPaginasMostra; i++) {
                    $scope.paginas[i] = $scope.paginas[i] - 1;
                }
            }
        }
    }
});