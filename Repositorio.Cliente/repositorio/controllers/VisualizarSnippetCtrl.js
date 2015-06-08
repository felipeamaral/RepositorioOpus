app.controller('VisualizarSnippetCtrl', function ($scope, apiService, id) {

    $scope.idSnippet = id;
    $scope.editor = "repositorio/templates/editor.html";

    $scope.$emit('placeholder', { place: "Busque por um snippet" });

    //Fica escutando quando deverá realizar uma busca
    $scope.$on('busca', function (event, args) {
        // realiza a busca
    });

    // Função que seta que deve fazer um download
    $scope.download = function () {
        $scope.$broadcast('download', { valor: true });
    };

    // Pega o nome do snippet
    var snippet = apiService.getImagens.get({ id: id }, function () {
        $scope.nomeSnippet = snippet.nome;
    });
});