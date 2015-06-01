'use strict';

app.config(function ($stateProvider, $urlRouterProvider, $locationProvider, $httpProvider) {

    // States
    $stateProvider
        .state('home', {
            url: "/Repositorio/Home",
            templateUrl: "repositorio/templates/Snippets.html",
            controller: 'SnippetsCtrl',
        })
        .state('projetos', {
            url: "/Repositorio/Projetos",
            templateUrl: "repositorio/templates/Projetos.html"
        })
        .state('snippets', {
            url: "/Repositorio/Snippets",
            templateUrl: "repositorio/templates/Snippets.html",
            controller: 'SnippetsCtrl',
            /*resolve: {
                busca: ['$stateParams', function ($stateParams) {
                    if ($stateParams) {
                        return $stateParams.busca;
                    } else {
                        return "";
                    }
                }]
            }*/
        })
        .state('adicionarSnippets', {
            url: "/Repositorio/Submeter",
            templateUrl: "repositorio/templates/Snippets_Adicionar.html",
            controller: 'AddSnippetCtrl'
        })
        .state('adicionaProjetos', {
            url: "/Repositorio/Projeto/Submeter",
            templateUrl: "repositorio/templates/Projetos-Adicionar.html"
        })
        .state('visualizaSnippet', {
            url: "/Repositorio/Snippet/{id:int}/Visualizar",
            templateUrl: "repositorio/templates/Visualiza_Snippet.html",
            controller: 'VisualizarSnippetCtrl',
            resolve: {
                id: ['$stateParams', function ($stateParams) {
                    return $stateParams.id;
                }]
            }
        })

    // Unmatched url
    $urlRouterProvider.otherwise('/Repositorio/Home');
});