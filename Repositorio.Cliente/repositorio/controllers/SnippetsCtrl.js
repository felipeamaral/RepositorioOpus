app.controller('SnippetsCtrl', function ($scope, apiService, $timeout, $mdSidenav, $mdUtil, $log) {

    var qntd = 3;
    var qntdPaginasMostra = 5;
    var valorBusca;
    var projBusca = "";

    $scope.qntdPaginas = 0;
    $scope.paginaAtual = 0;

    //Pega os projetos contidos no banco para o filtro por projeto
    var projetos = apiService.projetos.query(function () {
        $scope.projetosCadastrados = projetos;
        $scope.projSelecionado = [];

        /*seta todos como não selecionados*/
        projetos.forEach(function (proj, index) {
            $scope.projSelecionado[index] = false;
        });
    });

    $scope.toggleRight = buildToggler('right');

    /*Função que realiza a abertura da barra alteral direita*/
    function buildToggler(navID) {
        var debounceFn = $mdUtil.debounce(function () {
            $mdSidenav(navID)
              .toggle()
              .then(function () {
                  $log.debug("toggle " + navID + " is done");
              });
        }, 300);
        return debounceFn;
    };

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

        // Verifica se já não é a busca cujo os resultados estão sendo mostrados
        if ($scope.valorBusca.toLowerCase() !== valorBusca) {
            $scope.paginaAtual = 0;
            valorBusca = $scope.valorBusca.toLowerCase();
            $scope.goToPage(1, true, false);
        } else {
            // Exibe mensagem de que essa é a busca atual
        }
    });

    $scope.$emit('placeholder', {place: "Busque por um snippet"});

    /*Pega os snippets da página informada*/
    $scope.goToPage = function (pageNumber, primeira, last) {

        var params = {};
        /*Define os parâmetros que serão passados*/
        if ((valorBusca == "" || valorBusca == undefined || valorBusca == null) &&
                (projBusca.length == 0 || projBusca == undefined || projBusca == null)) {
            params = { qntd: qntd, pageNumber: pageNumber };
            url = "getImagens";
        } else if (projBusca.length == 0 || projBusca == undefined || projBusca == null) {
            params = { nome: valorBusca, qntd: qntd, pageNumber: pageNumber };
            url = "buscaSnippetNome";
        } else if (valorBusca == "" || valorBusca == undefined || valorBusca == null) {
            params = { projetos: projBusca, qntd: qntd, pageNumber: pageNumber };
            url = "buscaSnippetProjeto";
        } else {
            params = { nome: valorBusca, projetos: projBusca, qntd: qntd, pageNumber: pageNumber };
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
            if (primeira) {
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

    // Faz o filtro pelos projetos
    $scope.filtrar = function () {

        //Limpa a variavel que contera os projetos a serem filtrados
        projBusca = "";

        /*Monta o parâmetro de busca de projetos e chama a função de busca*/
        $scope.projSelecionado.forEach(function (projeto, index) {
            if (projeto) {
                if (projBusca === "") {
                    projBusca = projBusca + $scope.projetosCadastrados[index].idProjeto;
                } else {
                    projBusca = projBusca + "0" + $scope.projetosCadastrados[index].idProjeto;
                }
            }
        });

        //Chama a busca
        $scope.goToPage(1, true, false);

        //Fecha o menu lateral
        $scope.toggleRight();
    }
});