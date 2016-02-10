'use strict';

var Marionette = require('backbone.marionette');
var MainRegion = require('./main.region');

var Layout = Marionette.LayoutView.extend({
    el: '.app',
    template: require('./main.tpl.html'),
    className: 'ns-full-height',

    regions: {
        rgMain: new MainRegion({
            el: 'main'
        })
    },
	onRender : function() {
		console.log('Render Main') ;
	},

});


var instance = null ;


module.exports = {
    getInstance:function() {
        if (instance===null) {
            instance = new Layout();
        }
        return instance ;
    }
    
};
