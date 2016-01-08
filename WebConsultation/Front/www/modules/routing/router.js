'use strict';

var Marionette = require('backbone.marionette');

var Router = Marionette.AppRouter.extend({
    appRoutes: {
        '': 'indexAction',
        'login': 'loginAction',
        'logout': 'logoutAction'
    },
});

module.exports = new Router({
    controller: new(require('./router_controller'))()
});
