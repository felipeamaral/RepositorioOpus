app.controller('VisualizarSnippetCtrl', function ($scope, apiService, $state, id) {

    $scope.idSnippet = id;
    $scope.editor = "repositorio/templates/editor.html";

    $scope.$emit('placeholder', { place: "Busque por um snippet" });

    //Fica escutando quando deverá realizar uma busca
    $scope.$on('busca', function (event, args) {
        $state.go('snippets', { valorBusca: $scope.valorBusca });
    });

    // Função que seta que deve fazer um download
    $scope.download = function () {
        $scope.$broadcast('download', {nomeSnippet: $scope.nomeSnippet});
    };

    // Pega o nome do snippet
    var snippet = apiService.getImagens.get({ id: id }, function () {
        $scope.nomeSnippet = snippet.nome;
        $scope.kws = snippet.keyword;
    });
});