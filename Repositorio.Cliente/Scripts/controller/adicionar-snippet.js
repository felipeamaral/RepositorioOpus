app.controller('chipsCtrl', DemoCtrl);
function DemoCtrl($timeout, $q) {
    var self = this;
    self.readonly = false;
    self.tags = [];
   
}