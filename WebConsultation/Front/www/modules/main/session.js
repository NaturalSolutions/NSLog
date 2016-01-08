'use strict';

var Backbone = require('backbone');
var $ = require('jquery');
var _ = require('lodash');


var Session = Backbone.Model.extend({

    opened: false,

    open: function(username, password, persistent) {
        var dfd = $.Deferred(),
            self = this;
        $.ajax({
            url: '/api/login',
            data: {
                password: password,
                username: username
            }
        })
        .done(function() {
            self.opened = true;
            if (persistent) {
                localStorage.setItem('username', username);
                localStorage.setItem('password', password);
            }
        })
        .fail(function(jqXHR, textStatus, errorThrown) {
            var resp = {};

            if (jqXHR.status === 401) {
                resp.retry = true;
                resp.message = 'Wrong credentials';
            } else {
                console.log(jqXHR);
                resp.retry = false;
                resp.message = 'Network problem';
            }

            dfd.reject(resp);
        });
        return dfd.promise();
    },

    close: function() {
        this.opened = false;
        this.clear();
        localStorage.removeItem('username');
        localStorage.removeItem('password');
        this.trigger('close');
    }
});

module.exports = new Session();
