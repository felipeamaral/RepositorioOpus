app.controller('AddSnippetCtrl', function ($timeout, $q, $scope, projetosService, $scope, $mdDialog) {

    $scope.editor = "repositorio/templates/editor.html";

    $scope.snippet = {
        nome: '',
        usuario: 'camila@gmail.com',
        projeto: '',
        Keyword: []
    }

    // Pega os projetos cadastrados no banco
    $scope.projetosCadastrados = projetosService.getProjetos();

    //Função que salva um novo snippet
    $scope.salvaSnippet = function () {

        //Monta o snippet a ser enviado pro servidor
        var snippetEnviar = {};

        snippetEnviar.nome = $scope.snippet.nome;
        snippetEnviar.usuario = $scope.snippet.usuario;
        snippetEnviar.projeto = $scope.snippet.projeto.value;
        snippetEnviar.Keyword = [];

        var cont = 0;

        $scope.snippet.Keyword.forEach(function (key, index) {
            snippetEnviar.Keyword[cont] = { kw: key };
            cont++;
        });

        // Envia snippet pro controller do editor
        $scope.$broadcast('upload', { snippet: snippetEnviar });
    };

    // Chama o modal de adição de um novo projeto
    $scope.addProjeto = function (ev) {
        $mdDialog.show({
            controller: 'AddProjetoCtrl',
            templateUrl: 'repositorio/templates/AddProjetoModal.html',
            targetEvent: ev,
        }).then(function (nomeProj) {
            // Adiciona projeto a lista e seta como o selecionado
        }, function () {
            // Fechou o modal
        });
    };


    // AUTOCOMPLETE

    $scope.projs = [];
    $scope.projetosCadastrados.$promise.then(function (data) {

        var cont = 0;
        data.forEach(function (proj) {
            $scope.projs[cont] = { value: proj.idProjeto, display: proj.nome.toLowerCase() };
            cont++;
        });
    });
    $scope.searchText = null;
    $scope.querySearch = querySearch;

    function querySearch(query) {
        var results = query ? $scope.projs.filter(createFilterFor(query)) : [];
        console.log(results);
        return results;
    }
    
    function createFilterFor(query) {
        var lowercaseQuery = angular.lowercase(query);
        return function filterFn(proj) {
            return (proj.display.indexOf(lowercaseQuery) === 0);
        };
    }
});
