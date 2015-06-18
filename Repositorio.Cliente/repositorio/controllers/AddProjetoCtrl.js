app.controller('AddProjetoCtrl', function ($scope, $mdDialog, apiService, nome) {

    $scope.projeto = {
        nome: nome,
        cliente: "",
        dataIni: "0000-00-00",
        dataFim: "0000-00-00",
        descricao: "",
        area: ""
    };

    $scope.cancelar = function () {
        $mdDialog.cancel();
    };

    function doisDigitos(valor) {
        if (0 <= valor && valor < 10) return "0" + valor.toString();
        return valor.toString();
    }

    //Adiciona o projeto no servidor
    $scope.adicionar = function () {

        /*Converte as datas num formato que o banco reconheça*/
        var data = $scope.dataIni;
        $scope.projeto.dataIni = data.getUTCFullYear() + "-" + doisDigitos(1 + data.getUTCMonth()) + "-" + doisDigitos(data.getUTCDate());
        data = $scope.dataFim;
        $scope.projeto.dataFim = data.getUTCFullYear() + "-" + doisDigitos(1 + data.getUTCMonth()) + "-" + doisDigitos(data.getUTCDate());

        apiService.projetos.save($scope.projeto, function (data) {
            //Envia o projeto adicionado pra página que chamou o modal
            $mdDialog.hide(data);
        });
    };
});