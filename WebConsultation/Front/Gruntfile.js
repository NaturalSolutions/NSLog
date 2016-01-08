module.exports = function(grunt) {
    grunt.initConfig({
        jshint: {
            options: {
                browserify: true,
                browser: true,
                devel: true
            },
            all: [
                'Gruntfile.js',
                'www/modules/**/*.js'
            ]
        },
        simplemocha: {
            all: [
                'tests/test-suite.js',
            ]
        },
        watch: {
            options: {
                livereload: true,
                spawn: false
            },
            configFiles: {
                files: ['Gruntfile.js'],
                tasks: ['build'],
                options: {
                    reload: true
                }
            },
            js: {
                files: [
                    'www/modules/**/*.js',
                    'www/vendor/**/*'
                ],
                tasks: ['build']
            },
            tpl: {
                files: [
                    'www/modules/**/*.html'
                ],
                tasks: ['browserify:dev']
            },
            css: {
                files: ['www/modules/**/*.less', 'www/less/*'],
                tasks: ['less']
            },
        },
        less: {
            dist: {
                files: {
                    'www/style.css': 'www/modules/main/_style.less'
                },
                options: {
                    compress: false,
                    sourceMap: true,
                    sourceMapFilename: 'www/style.css.map',
                    sourceMapURL: 'style.css.map'
                }
            }
        },
        connect: {
            server: {
                options: {
                    base: 'www',
                    open: true,
                    livereload: true
                }
            }
        },
        browserify: {
            dev: {
                src: ['www/modules/**/*.js'],
                dest: 'www/index.js',
                options: {
                    browserifyOptions: {
                        debug: true // Enable (inline) source map //FIXME: source maps do not work
                    }
                }
            },
            test: {
                src: ['tests/**/*_spec.js'],
                dest: 'tests/test-suite.js'
            },
            options: {
                transform: [
                    ['node-underscorify', {
                        templateSettings: {variable: 'data'},
                        requires: [
                            {variable: '_', module: 'lodash'}
                        ]
                    }]
                ]
            }
        },
        uglify: {
            dist: {
                src: ['www/index.js'],
                dest: 'www/index.js'
            }
        },
        copy: {
            fonts: {
                expand: true,
                src: ['./node_modules/bootstrap/fonts/*'],
                dest: 'www/fonts/',
                filter: 'isFile',
                flatten: true
            }
        }
    });

    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-simple-mocha');
    grunt.loadNpmTasks('grunt-contrib-connect');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-browserify');
    grunt.loadNpmTasks('grunt-contrib-less');
    grunt.loadNpmTasks('grunt-contrib-copy');

    grunt.registerTask('test', ['jshint', 'browserify:test', 'simplemocha']);
    grunt.registerTask('build', ['less', 'browserify:dev', 'copy']);
    grunt.registerTask('dev', ['test', 'build', 'connect', 'watch']);
    grunt.registerTask('default', ['test', 'build', 'uglify']);
};
