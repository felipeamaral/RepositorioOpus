app.controller('AddSnippetCtrl', function ($timeout, $q, $scope) {
    
    $scope.readonly = false;
    $scope.tags = [];

    $scope.snippet = {
        Titulo: '',
        Projeto: '',
        tags: ''
    }

    $scope.salvaSnippet = function () {
        console.log($scope.snippet.Titulo);
        console.log($scope.snippet.Projeto);
        console.log($scope.tags);   
    }

    //Função que pega os dados do editor e manda em um arquivo pro servidor
    function upload(conteudo) {
        $.post("http://localhost:53412/api/snippet/3/files/upload", function () {
            var file = [conteudo];
            var blob = new Blob(file, { type: 'application/octet-binary' }); // the blob
            console.log(blob);
        });
    };

});
