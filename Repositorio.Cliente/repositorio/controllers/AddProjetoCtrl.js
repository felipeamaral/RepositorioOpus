app.controller('AddProjetoCtrl', function ($scope, $mdDialog) {

    $scope.hide = function () {
        $mdDialog.hide();
    };
    $scope.cancel = function () {
        $mdDialog.cancel();
    };

});