app.factory('SnippetService', function (apiService) {
    return {
        /*Retorna os snippets pertencentes a uma pagina*/
        getSnippets: function (qntd, pageNumber) {
            return apiService.getImagens.query({ qntd: qntd, pageNumber: pageNumber });
        },

        /*Retorna a quantidade de páginas no total*/
        qntdPaginas: function (qntd) {
            var qntdPages;
            var snippets =  apiService.getImagens.query(function () {
                qntdPages = snippets[0] % qntd > 0 ? snippets[0] / qntd + 1 : snippets[0] / qntd;
            });
            return qntdPages
        }
    };
});