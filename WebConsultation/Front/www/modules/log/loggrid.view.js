'use strict';
var Marionette = require('../../vendor/marionette-shim'),
    session = require('../main/session'),
    _ = require('lodash'),
    router = require('../routing/router'),
    NsGrid = require('../../vendor/NaturalJS-Grid/model-grid'),
    NsFilter = require('../../vendor/NaturalJS-Filter/model-filter'),
    NsCom = require('../../vendor/NaturalJS-Com/ns_com'),
    LogModel = require('./log.model'),
    Backbone = require('backbone');

    var $ = require('jquery');
    Backbone.$ = $ ;
    console.log('LayoutGrid',Layout);
    var Layout = Marionette.LayoutView.extend({
    name: 'login',
    header: 'none',
    template: require('./loggrid.tpl.html'),
    className: 'LogGrid',
    events: {
        'click #update': 'update',
        'click #Clear': 'clear'
    },
    initialize: function (options) {
            Marionette.LayoutView.prototype.initialize.call(this, options);
            this.com = new NsCom();
            var filter = [];
            var columns = [
            				{"name":"ID","label":"ID","cell":"String","renderable":true,"editable":"false","type":"String","displayFormat":"","options":null},
            				{"name":"JCRE","label":"Jcre","cell":"String","renderable":true,"editable":"false","type":"String","displayFormat":"","options":null},
                            {"name":"LOG_LEVEL","label":"Log_Level","cell":"String","renderable":true,"editable":"false","type":"String","displayFormat":"","options":null},
                            {"name":"ORIGIN","label":"Origin","cell":"String","renderable":true,"editable":"false","type":"String","displayFormat":"","options":null},
                            {"name":"SCOPE","label":"Scope","cell":"String","renderable":true,"editable":"false","type":"String","displayFormat":"","options":null},
                            {"name":"LOGUSER","label":"User","cell":"String","renderable":true,"editable":"false","type":"String","displayFormat":"","options":null},
                            {"name":"DOMAINE","label":"Domaine","cell":"String","renderable":true,"editable":"false","type":"String","displayFormat":"","options":null},
                            {"name":"MESSAGE_NUMBER","label":"Message Number","cell":"String","renderable":true,"editable":"false","type":"String","displayFormat":"","options":null},
                            {"name":"LOG_MESSAGE","label":"Message Message","cell":"String","renderable":true,"editable":"false","type":"String","displayFormat":"","options":null}
            				];

            this.grid = new NsGrid ({
            	name:'logGrid',
            	url:'http://localhost:6570/logdisplay-core/log',
            	pageSize:30,
            	pagingServerSide: true,
                totalElement: 'NbElements',
                filterCriteria: filter,
                com: this.com,
                columns:columns
            });


            //console.log('initialize',this,this.template);
        },
         getSelectedIndex: function (id) {
            for (var i = 0; i < this.selectedSamples.length; i++) {
                if (this.selectedSamples[i].sample == id) {
                    return i;
                }
            }
            return -1;
        },
        onRender: function () {
            $('.spinner').attr('style', 'display:visible');
            // add icon in grid
            
            var Grid = this.grid.displayGrid();
            console.log('Grid',Grid);
            this.$el.find('#grid').html(Grid);
            this.$el.find('#paginator').html(this.grid.displayPaginator());
        },
         update: function () {
            $('.spinner').attr('style', 'display:visible');
            this.filter.update();
        },
        clear: function () {
            this.filter.reset() ;
            //this.createFilters();
            this.update();
        },
        createFilters() {

            var myfilters = [
                {'type':'Number','title':'ID','ID':'toto'},
                {'type':'Text','title':'JCRE','name':'Jcre'},
                {'type':'Text','title':'ORIGIN','name':'Origin'},
                {'type':'Text','title':'LOGUSER','name':'User'},
                {'type':'Number','title':'MESSAGE_NUMBER','name':'Message Number'},
                {'type':'Text','title':'LOG_MESSAGE','name':'Message'}
            ];

            this.filter = new NsFilter ({
                name:'myFilter',
                filters : myfilters,
                com: this.com,
                filterContainer: 'filters',
            });
        },
        onShow:function() {
           this.createFilters() ;
        }
});

module.exports = Layout;
