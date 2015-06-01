app.controller('searchCtrl', function ($scope) {

    $scope.openSearch = function () {
        $("#menuToolBar").addClass("hidden");
        $("#searchToolBar").removeClass("hidden");

    }

    $scope.closeSearch = function () {
        $("#searchToolBar").addClass("hidden");
        $("#menuToolBar").removeClass("hidden");
    }

});