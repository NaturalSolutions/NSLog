'use strict';

var Marionette = require('backbone.marionette');

var Router = Marionette.AppRouter.extend({
    appRoutes: {
        '': 'indexAction',
        'login': 'loginAction',
        'logout': 'logoutAction',
		'logGrid': 'loggrid',
        'logForm/:id': 'logform'
    },
	 loggrid: function () {
        this.controller.loggrid();
     },
	logform: function (id) {
        console.log(this.controller) ;
        this.controller.logform(id);
     },
	
});

module.exports = new Router({
    controller: new(require('./router_controller'))()
});
