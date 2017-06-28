'use strict';
var Marionette = require('backbone.marionette'),
    session = require('../main/session'),
    _ = require('lodash'),
    router = require('../routing/router');

var Layout = Marionette.LayoutView.extend({
    name: 'login',
    header: 'none',
    template: require('./login.tpl.html'),
    className: 'page login',
    events: {
        'submit form': 'onFormSubmit'
    },

    initialize: function() {
        
    },

});

module.exports = Layout;
