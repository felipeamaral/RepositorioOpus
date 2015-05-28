'use strict';

var app = angular.module('app', ['ngMaterial', 'ui.router', 'ui.ace', 'ngResource']);

app.config(function ($stateProvider, $urlRouterProvider, $httpProvider) {

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
            templateUrl: "Templates/Snippets.html",
            controller: 'SnippetsCtrl'
        })
        .state('adicionarSnippets', {
            url: "/Repositorio/Submeter",
            templateUrl: "Templates/Snippets_Adicionar.html"
        })
        .state('adicionaProjetos', {
            url: "/Repositorio/Projeto/Submeter",
            templateUrl: "Templates/Projetos-Adicionar.html"
        })
        .state('visualizaSnippet', {
            url: "/Repositorio/Snippet/id/Visualizar",
            templateUrl: "Templates/Visualiza_Snippet.html",
        })

        // Unmatched url
        $urlRouterProvider.otherwise('/Repositorio/Home');
});



