'use strict';

var app = angular.module('app', ['ngMaterial', 'ui.router', 'ui.ace', 'ngResource']);

app.config(function ($stateProvider, $urlRouterProvider, $httpProvider) {

    // States
    $stateProvider
        .state('home', {
            url: "/Repositorio/Home",
            templateUrl: "repositorio/templates/Snippets.html"
        })
        .state('projetos', {
            url: "/Repositorio/Projetos",
            templateUrl: "repositorio/templates/Projetos.html"
        })
        .state('snippets', {
            url: "/Repositorio/Snippets",
            templateUrl: "repositorio/templates/Snippets.html",
            controller: 'SnippetsCtrl'
        })
        .state('adicionarSnippets', {
            url: "/Repositorio/Submeter",
            templateUrl: "repositorio/templates/Snippets_Adicionar.html"
        })
        .state('adicionaProjetos', {
            url: "/Repositorio/Projeto/Submeter",
            templateUrl: "repositorio/templates/Projetos-Adicionar.html"
        })
        .state('visualizaSnippet', {
            url: "/Repositorio/Snippet/{id:int}/Visualizar",
            templateUrl: "repositorio/templates/Visualiza_Snippet.html",
            resolve:{
                id: ['$stateParams', function($stateParams){
                    return $stateParams.id;
                }]
            }
        })

        // Unmatched url
        $urlRouterProvider.otherwise('/Repositorio/Home');
});



