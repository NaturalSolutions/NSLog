'use strict';

var Marionette = require('backbone.marionette');
var router = require('./router');
var session = require('../main/session');
var Main = require('../main/main.view');

var logGrid = require('../log/loggrid.view');
var logForm = require('../log/logform.view');//require('../log/logform.view');
var $ = require('jquery');

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
       
    },

    loginAction: function() {
        
    },
	loggrid: function() {
		console.log('LogGrid') ;
		var Mymain = Main.getInstance() ;
		//console.log(Mymain) ;
        var curlogGrid = new logGrid() ;
        curlogGrid.render() ;
        Mymain.rgMain.show(curlogGrid, {
            preventDestroy: true
        });
        
    },
    logform: function(id) {
        console.log('LogGrid') ;
        var Mymain = Main.getInstance() ;
        //console.log(Mymain) ;
        var curlogForm = new logForm({id:id}) ;
        curlogForm.render() ;
        
        Mymain.rgMain.show(curlogForm, {
            preventDestroy: true
        });
        
    }

});
