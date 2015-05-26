var app = angular.module('app', ['ngMaterial', 'ui.router']);

app.config(function ($stateProvider, $urlRouterProvider, $httpProvider) {


     // Unmatched url
    $urlRouterProvider.otherwise('/Repositorio/Home');

    // States
    $stateProvider
        .state('home', {
            url: "/Repositorio/Home",
            templateUrl: "Templates/Snippets.html"
        })
        .state('projetos', {
            url: "/Repositorio/Projetos",
            templateUrl: "Templates/Projetos.html"
        })
        .state('snippets', {
            url: "/Repositorio/Snippets",
            templateUrl: "Templates/Snippets.html"
        })
        .state('adicionarSnippets', {
            url: "/Repositorio/Submeter",
            templateUrl: "Templates/Snippets_Adicionar.html"
        })
});



