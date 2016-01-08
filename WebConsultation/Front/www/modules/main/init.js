'use strict';

var Backbone = require('backbone');
var Marionette = require('../../vendor/marionette-shim');
var $ = require('jquery');
var bootstrap = require('bootstrap');
var main = require('./main.view');
var session = require('./session');
var NsForm = require('../../vendor/NaturalJS-Form/NsFormsModule') ;
function init() {

	console.log(' NsForm', NsForm) ;
    $('html').addClass(document.ontouchstart === undefined ? 'notouch' : 'touch');

    // Global intercept of AJAX error
    $(document).ajaxError(function(evt, jqXhr, params, err) {
        console.log('Request failed: ' + params.url, arguments);
    });

    window.addEventListener('native.keyboardshow', function() {
        $('body').addClass('keyboardshow');
    });
    window.addEventListener('native.keyboardhide', function() {
        $('body').removeClass('keyboardshow');
    });

    // FIXME: is this necessary?
    Backbone.Marionette.Renderer.render = function(template, data) {
        return template(data);
    };

    var dfdSession,
        app = new Marionette.Application();

    app.on('start', function() {
        main.render();
        Backbone.history.start();
    });

    if (localStorage.getItem('username') !== null) {
        dfdSession = session.open(localStorage.getItem('username'), localStorage.getItem('password'));
    }
    $.when(dfdSession).then(function() {
        app.start();
    });
}

$(document).ready(init);
