<TODO: Project description>

# Get started

Note: the developpement environment relies on [Git](https://git-scm.com/downloads), [NodeJS with NPM](https://nodejs.org/en/download/) and [Grunt](http://gruntjs.com/getting-started), you'll need to install these tools system-wide before starting to work on the projet.

Application is 100% Javascript, you can deploy a development environnement with:

```
git clone git@github.com:naturalsolutions/<TODO: repository>.git .
npm install
grunt build
```

The Grunt configuration includes linting, unit test, JS/CSS/templates bundling. You can also run a developement server with file watching and livereload with `grunt dev`.

# Architecture

The application is based on the [Backbone](http://backbonejs.org/)/[Marionette](http://marionettejs.com/) pair and the UI integration rely on [Bootstrap3](http://getbootstrap.com/). It is designed as a [single-page application](https://en.wikipedia.org/wiki/Single-page_application).

