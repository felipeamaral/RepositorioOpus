app.factory('apiService', function ($resource) {
    return {
        getImagens: $resource('http://localhost:53412/api/snippet/:qntd/:pageNumber'),
        downloadFiles: $resource('http://localhost:53412/api/snippet/:id/files/download'),
        buscaSnippetNome: $resource('http://localhost:53412/api/snippet/busca/:nome/:qntd/:pageNumber'),
        buscaSnippetProjeto: $resource('http://localhost:53412/api/snippet/busca/:qntd/:pageNumber/:projetos'),
        buscaSnippetNomeProj: $resource('http://localhost:53412/api/snippet/busca/:nome/:projetos/:qntd/:pageNumber'),
        projetos: $resource('http://localhost:53412/api/projeto'),
        clientes: $resource('http://localhost:53412/api/cliente'),
        areas: $resource('http://localhost:53412/api/area'),
        templates: $resource('http://localhost:53412/api/template/material')
    };
});