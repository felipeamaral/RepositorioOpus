app.controller('AddSnippetCtrl', function ($timeout, $q, $scope, projetosService, $scope, $mdDialog) {

    $scope.editor = "repositorio/templates/editor.html";

    $scope.$emit('placeholder', { place: "Busque por um snippet" });

    $scope.snippet = {
        nome: '',
        usuario: 'camila@gmail.com',
        projeto: '',
        Keyword: []
    }

    //Função que salva um novo snippet
    $scope.salvaSnippet = function () {

        //Verifica se um nome foi informado
        if ($scope.snippet.nome != "" && $scope.snippet.nome != undefined && $scope.snippet.nome != null) {

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
        } else {
            //Faz com que o erro de que é obrigatório apareça no campo nome
            $scope.addSnippetForm.nome.$touched = true;
        }
    };

    /*Fica escutando pra exibir uma mensagem caso o usuário tenha tentado
        fazer upload sem informar um código html*/
    $scope.$on('html', function (event, args) {
        //Exibe a mensagem de que é necessário informar o código html
    });

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
    
    /* FUNÇÕES REFERENTES AO AUTOCOMPLETE*/

    // Pega os projetos cadastrados no banco
    var projetosCadastrados = projetosService.getProjetos();

    $scope.projs = [];
    projetosCadastrados.$promise.then(function (data) {
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
        return results;
    }
    
    function createFilterFor(query) {
        var lowercaseQuery = angular.lowercase(query);
        return function filterFn(proj) {
            return (proj.display.indexOf(lowercaseQuery) === 0);
        };
    }

    /* CHAMA O MODAL DE CADASTRAR UM NOVO PROJETO*/

    $scope.addProjeto = function (ev) {
        $mdDialog.show({
            controller: 'AddProjetoCtrl',
            templateUrl: 'repositorio/templates/AddProjetoModal.html',
            parent: angular.element(document.body),
            targetEvent: ev,
        })
        .then(function (projeto) {
            $scope.projs[$scope.projs.length] = { value: projeto.idProjeto, display: projeto.nome.toLowerCase() };
        });
    };
});
