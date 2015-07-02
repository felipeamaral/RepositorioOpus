app.controller('angularTemplateCtrl', function ($scope, $mdDialog) {

    $scope.nomeApp = "";
    $scope.controllers = [];
    $scope.services = [];

    // Faz o download do template
    $scope.downloadTemplate = function () {

        //Verifica se um nome para o módulo foi informado
        if ($scope.nomeApp !== "") {

            var ctrls = "";
            var servs = "";

            // Monta a string com os nomes dos controllers
            if ($scope.controllers.length) {
                ctrls += "/?controllers=" + $scope.controllers[0];
                for (var i = 1; i < $scope.controllers.length; i++) {
                    ctrls += "&controllers=" + $scope.controllers[i];
                }
            }

            // Monta a string com os nomes dos services
            for (var i = 0; i < $scope.services.length; i++) {
                servs += "&services=" + $scope.services[i];
            }

            window.open('http://localhost:53412/api/template/angular/' + $scope.nomeApp + ctrls + servs, '_self');
        } else {
            $scope.angularTemplateForm.nome.$touched = true;
        }
    }

    // Fecha o modal
    $scope.cancelar = function () {
        $mdDialog.hide();
    }

});