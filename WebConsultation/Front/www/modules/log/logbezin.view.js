'use strict';
var Marionette = require('../../vendor/marionette-shim'),
    session = require('../main/session'),
    _ = require('lodash'),
    router = require('../routing/router'),
    NsGrid = require('../../vendor/NaturalJS-Grid/model-grid'),
    LogModel = require('./log.model'),
	Backbone = require('backbone');

    var $ = require('jquery');

var Layout = Marionette.LayoutView.extend({
    name: 'login',
    header: 'none',
    template: require('./loggrid.tpl.html'),
    className: 'LogGrid',
    
    initialize: function (options) {
            Marionette.LayoutView.prototype.initialize.call(this, options);
            var filter = [];
            var columns = [
            				{"name":"ID","label":"ID","cell":"String","renderable":true,"editable":"false","type":"String","displayFormat":"","options":null},
            				{"name":"JCRE","label":"Jcre","cell":"String","renderable":true,"editable":"false","type":"String","displayFormat":"","options":null}
            				];
            
			
            /*var CurLog = new LogModel();
            CurLog.set('id',1);
            CurLog.set('jcre','bezin');
            
            collection.add(CurLog) ;
			console.log('collection',collection);*/
            this.grid = new NsGrid ({
            	name:'logGrid',
            	url:'http://localhost:6570/logdisplay-core/log',
            	pageSize:30,
            	pagingServerSide: true,
                totalElement: 'NbElements',
                filterCriteria: filter,
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

            //this.displayNbElement();
            
            

        }
});

module.exports = Layout;
