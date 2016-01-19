'use strict';
var Backbone = require('backbone');

var Model = Backbone.Model.extend({
    defaults: {
		id:0,
		jcre:null,
		log_level:0,
		origin:'',
		scope:'',
		loguser:'',
		domaine:0,
		message_number:500,
		log_message:'',
		otherinfo:{}
    }
});

module.exports = Model;
