﻿app.controller('SnippetsCtrl', function ($scope, apiService) {

    var qntd = 2;
    var qntdPaginasMostra = 5;

    $scope.qntdPaginas = 0;
    $scope.paginaAtual = 0;
    $scope.proj = "";

    /*Inicializa as páginas a serem mostradas*/
    var setaPaginas = function (qntdPaginas) {
        $scope.qntdPaginas = qntdPaginas;
        $scope.paginas = [];
        for (var i = 0; i < qntdPaginasMostra && i < qntdPaginas; i++) {
            $scope.paginas[i] = i + 1;
        }
    };

    //Fica escutando quando deverá realizar uma busca
    $scope.$on('busca', function (event, args) {
        $scope.paginaAtual = 0;
        $scope.goToPage(1, true, false);
    });

    $scope.$emit('placeholder', {place: "Busque por um snippet"});

    /*Pega os snippets da página informada*/
    $scope.goToPage = function (pageNumber, primeira, last) {
        
        var params = {};
        /*Define os parâmetros que serão passados*/
        if (($scope.valorBusca == "" || $scope.valorBusca == undefined || $scope.valorBusca == null) &&
                ($scope.proj == "" || $scope.proj == undefined || $scope.proj == null)) {
            params = { qntd: qntd, pageNumber: pageNumber };
            url = "getImagens";
        } else if ($scope.proj == "" || $scope.proj == undefined || $scope.proj == null) {
            params = { nome: $scope.valorBusca, qntd: qntd, pageNumber: pageNumber };
            url = "buscaSnippetNome";
        } else if ($scope.valorBusca == "" || $scope.valorBusca == undefined || $scope.valorBusca == null) {
            params = { idProjeto: $scope.proj, qntd: qntd, pageNumber: pageNumber };
            url = "buscaSnippetProjeto";
        } else {
            params = { nome: $scope.valorBusca, idProjeto: $scope.proj, qntd: qntd, pageNumber: pageNumber };
            url = "buscaSnippetNomeProj";
        }
        
        var aux = apiService[url].query(params, function () {

            /*Verifica se clicou pra ir direto pra ultima pagina*/
            if (last && $scope.paginaAtual != pageNumber) {
                cont = 0;
                for (i = $scope.qntdPaginas - qntdPaginasMostra >= 0 ? $scope.qntdPaginas - qntdPaginasMostra + 1 : 1; i <= $scope.qntdPaginas; i++) {
                    $scope.paginas[cont] = i;
                    cont++;
                }
            }

            $scope.snippets = aux;

            /*Verifica se clicou pra ir direto pra primeira página*/
            if (primeira && $scope.paginaAtual != pageNumber) {
                setaPaginas($scope.snippets[0].qntdPages);
            }

            $scope.paginaAtual = pageNumber;
        });
    };

    /*Pega os snippets da página 1*/
    $scope.goToPage(1, true, false);

    /*Vetor com numeração das páginas a serem mostradas*/
    $scope.paginas = [];

    /*Função que faz o download dos códigos do componente, em formato zip*/
    $scope.download = function (snippet) {
        window.open("http://localhost:53412/api/snippet/" + snippet.idComponente + "/files/download", '_self', '');
    };

    /*Vai pra próxima pagina*/
    $scope.goToNextPage = function () {

        /*Verifica se existem páginas a serem mostradas*/
        if ($scope.paginaAtual < $scope.qntdPaginas) {
            $scope.goToPage($scope.paginaAtual + 1, false, false);

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
            $scope.goToPage($scope.paginaAtual - 1, false, false);

            /*Ta na ultima primeira que ta sendo mostrada*/
            if ($scope.paginaAtual == $scope.paginas[0]) {

                for (i = 0; i < qntdPaginasMostra; i++) {
                    $scope.paginas[i] = $scope.paginas[i] - 1;
                }
            }
        }
    }
});