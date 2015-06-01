app.controller('ListCtrl', function ($scope, $mdDialog) {
    $scope.project = [
      { name: 'Projeto 1', wanted: true },
      { name: 'Projeto 2', wanted: false },
      { name: 'Projeto 3', wanted: true },
      { name: 'Projeto 4', wanted: false }
    ];
});
