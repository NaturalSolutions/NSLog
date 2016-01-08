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
        this.loginSuccess = _.bind(this.loginSuccess, this);
        this.loginFailure = _.bind(this.loginFailure, this);
    },

    loginSuccess: function() {
        router.navigate('', {trigger:true});
    },

    loginFailure: function(resp) {
        alert(resp.message);
    },

    onFormSubmit: function(e) {
        e.preventDefault();
        this.$el.find('button[type="submit"]').prop('disabled', true);
        // TODO: visual feedback (start)

        var self = this,
            username = this.$el.find('input[name="login"]').val(),
            password = this.$el.find('input[name="password"]').val(),
            persistent = this.$el.find('input[name="persistent"]').prop('checked');

        session.open(username, password, persistent)
            .done(this.loginSuccess)
            .fail(this.loginFailure)
            .always(function() {
                self.$el.find('button[type="submit"]').prop('disabled', false);
                // TODO: visual feedback (stop)
            });
    }
});

module.exports = Layout;
