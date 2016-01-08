'use strict';

var Marionette = require('backbone.marionette');
var _ = require('lodash');
var $ = require('jquery');

var Region = Marionette.Region.extend({
    attachHtml: function(view) {
        //TODO: another place for that ?
        if ( this.$el.children('div').length && this.currentView ) {
            var last = this.currentView;
            var $last = last.$el;
            $last.on('transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd', function(e) {
                if ( $last.hasClass('animate-close') ) {
                    $('body').alterClass('section-*', 'section-'+ view.name);
                    $last.removeClass('animate animate-close');
                    last.destroy();
                }
            });
            $last.addClass('animate animate-close');
        } else
            $('body').alterClass('section-*', 'section-'+ view.name);

        this.$el.prepend(view.el);
    }
});

module.exports = Region;
