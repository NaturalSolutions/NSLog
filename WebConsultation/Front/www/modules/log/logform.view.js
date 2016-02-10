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
            var schema =  {
                ID:{'type':'Number','title':'ID','name':'ID'},
                JCRE:{'type':'Text','title':'JCre','name':'JCRE',options:{isInterval:true}},
                ORIGIN:{'type':'Text','title':'Origin','name':'ORIGIN'},
                USER:{'type':'Text','title':'LogUser','name':'USER'},
                MESSAGE_NUMBER:{'type':'Number','title':'MESSAGE_NUMBER','name':'MESSAGE_NUMBER'},
                LOG_MESSAGE:{'type':'Text','title':'Message','name':'LOG_MESSAGE'}
            }


            this.nsform = new NsForm({
                name: 'logForm',
                modelurl: 'http://localhost:6570/logdisplay-core/log',
                formRegion: 'LogForm',
                displayMode: 'Display',
                id: this.id,
                schema : schema
            }) ;
        },
        onRender: function () {


        }
});

module.exports = Layout;