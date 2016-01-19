'use strict';
var Marionette = require('../../vendor/marionette-shim'),
    session = require('../main/session'),
    _ = require('lodash'),
    router = require('../routing/router'),
    NsForm = require('../../vendor/NaturalJS-Form/NsFormsModule'),
    LogModel = require('./log.model'),
    Backbone = require('backbone'),
    BackboneForm = require('backbone-forms');
    var $ = require('jquery');
    var Tpl = require('./but.tpl.html');
var Layout = Marionette.LayoutView.extend({
    name: 'LogForm',
    header: 'none',
    template: require('./logform.tpl.html'),
    className: 'LogForm',
    
    initialize: function (options) {
            Marionette.LayoutView.prototype.initialize.call(this, options);
            this.id = options.id ;
            this.nsform = new NsForm({
                name: 'logForm',
                modelurl: 'http://localhost:6570/logdisplay-core/log/',
                formRegion: 'LogForm',
                displayMode: 'Display',
                id: this.id
                //schema : 
            }) ;
        },
        onRender: function () {


        }
});

module.exports = Layout;