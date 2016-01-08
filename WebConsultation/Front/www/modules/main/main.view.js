'use strict';

var Marionette = require('backbone.marionette');
var MainRegion = require('./main.region');

var Layout = Marionette.LayoutView.extend({
    el: 'body',
    template: require('./main.tpl.html'),
    className: 'ns-full-height',

    regions: {
        rgMain: new MainRegion({
            el: 'main'
        })
    }
});

module.exports = new Layout();
