app.controller('LeftCtrl', function ($scope) {
    $scope.iconSnippets = "add";
    $scope.iconTemas = "add";
    $scope.iconProjs = "add";

    $scope.toggleTemas = function () {
        $scope.iconTemas = $scope.iconTemas == "add" ? "close" : "add";
    };
    
    $scope.toggleSnippets = function () {
        $scope.iconSnippets = $scope.iconSnippets == "add" ? "close" : "add";
    };

    $scope.toggleProjs = function () {
        $scope.iconProjs = $scope.iconProjs == "add" ? "close" : "add";
    };

});