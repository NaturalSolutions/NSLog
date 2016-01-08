'use strict';

var Marionette = require('backbone.marionette');
var router = require('./router');
var session = require('../main/session');
var main = require('../main/main.view');
var Login = require('../user/login.view');


module.exports = Marionette.Object.extend({
    initialize: function(options) {},

    goToLogin: function() {
        router.navigate('#login', {
            trigger: true
        });
    },

    indexAction: function() {
        console.log('Route to index');
    },

    logoutAction: function() {
        session.close();
        this.goToLogin();
    },

    loginAction: function() {
        main.getInstance().rgMain.show(new Login(), {
            preventDestroy: true
        });
    }
});
