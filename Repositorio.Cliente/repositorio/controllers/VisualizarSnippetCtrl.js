app.controller('VisualizarSnippetCtrl', function ($scope, apiService, id) {

    $scope.idSnippet = id;
    $scope.teste = "repositorio/templates/editor.html";

    $scope.$emit('placeholder', { place: "Busque por um snippet" });

    //Fica escutando quando deverá realizar uma busca
    $scope.$on('busca', function (event, args) {
        // realiza a busca
    });
});