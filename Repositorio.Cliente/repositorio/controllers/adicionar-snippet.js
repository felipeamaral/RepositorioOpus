app.controller('adicionar-snippet', function ($timeout, $q, $scope) {
    var self = this;
    self.readonly = false;
    self.tags = [];

    $scope.snippet = {
        Titulo: '',
        Projeto: '',
        tags: ''
    }

    $scope.salvaSnippet = function () {
        console.log($scope.snippet.Titulo);
        console.log($scope.snippet.Projeto);
        console.log(self.tags);   
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
