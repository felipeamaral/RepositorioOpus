'use strict';

app.config(function ($stateProvider, $urlRouterProvider, $locationProvider, $httpProvider) {

    // States
    $stateProvider
        .state('home', {
            url: "/Repositorio/Home",
            templateUrl: "repositorio/templates/Snippets.html",
            controller: 'SnippetsCtrl',
            resolve: {
                valorBusca: function () {
                    return null;
                }
            }
        })
        .state('snippets', {
            url: "/Repositorio/Snippets/{valorBusca}",
            templateUrl: "repositorio/templates/Snippets.html",
            controller: 'SnippetsCtrl',
            resolve: {
                valorBusca: ['$stateParams', function ($stateParams) {
                    if ($stateParams.valorBusca) {
                        return $stateParams.valorBusca;
                    } else {
                        return "";
                    }
                }]
            }
        })
        .state('adicionarSnippets', {
            url: "/Repositorio/Snippet/Adicionar",
            templateUrl: "repositorio/templates/SnippetsAdd.html",
            controller: 'AddSnippetCtrl'
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