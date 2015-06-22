app.controller('LoginCtrl', function ($scope, $mdDialog) {

    $scope.cancelar = function () {
        $mdDialog.cancel();
    };

    $scope.entrar = function () {
        //Faz a autenticação
    }

});