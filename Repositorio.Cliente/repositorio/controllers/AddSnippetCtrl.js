app.controller('AddSnippetCtrl', function ($timeout, $q, $scope, projetosService) {

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
        snippetEnviar.projeto = $scope.snippet.projeto;
        snippetEnviar.Keyword = [];

        var cont = 0;

        $scope.snippet.Keyword.forEach(function (key, index) {
            snippetEnviar.Keyword[cont] = { kw: key };
            cont++;
        });

        // Envia snippet pro controller do editor
        $scope.$broadcast('upload', { snippet: snippetEnviar });
    }
});
