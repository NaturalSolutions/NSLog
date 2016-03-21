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
        'click #Clear': 'clear',
        //'click tr': 'rowClicked',
        'dblclick tr': 'rowDblClicked'
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
            	pageSize:25,
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
        createFilters:function() {

            var myfilters = [
                {'type':'Number','title':'ID','name':'ID'},
                {'type':'Text','title':'JCre','name':'JCRE',options:{isInterval:true}},
                {'type':'Text','title':'Origin','name':'ORIGIN'},
                {'type':'Text','title':'LogUser','name':'USER'},
                {'type':'Number','title':'MESSAGE_NUMBER','name':'MESSAGE_NUMBER'},
                {'type':'Text','title':'Message','name':'LOG_MESSAGE'}
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
        },
        rowDblClicked: function (e) {
                var nId = -1;
                // get the index of ID if exists
               var routeur = require('../routing/router');
               routeur.navigate('#logForm/' + $(e.currentTarget).children().eq(0).text(), {trigger: true});
                //window.location.href = this.getBaseURL() + '#logForm/' + $(e.currentTarget).children().eq(0).text();
        },
        getBaseURL: function () {
            return window.location.origin + window.location.pathname;

        },
});

module.exports = Layout;
