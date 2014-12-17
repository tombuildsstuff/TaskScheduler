'use strict';

module.exports = function(grunt) {

    var taskObject = {
        pkg: grunt.file.readJSON('package.json')
    };

    // Loop through the tasks in the 'grunt-tasks' folder, ignore any with an underscore at
    // the beginning, and add them to the taskObject
    // or invoke if they are functions
    grunt.file.expand('build/grunt-tasks/**/*.js', '!build/grunt-tasks/**/_*.js').forEach(function(file) {
        var task = require('./'+ file);

        if(grunt.util._.isFunction(task)){
            task(grunt);
        } else {
            var name = file.split('/');
            taskObject[name[name.length - 1].replace('.js', '')] = task;
        }
    });

    grunt.initConfig(taskObject);

    // Automatically load in all Grunt npm tasks
    require('matchdep').filterDev('grunt-*').forEach(grunt.loadNpmTasks);

    // Front end tasks
    grunt.registerTask('dev', ['watch']);

    // Build & Test tasks
    grunt.registerTask('default', ['build', 'test']);
    grunt.registerTask('build', ['clean:build', 'xbuild:build', 'copy:buildOutput']);
    grunt.registerTask('unitTest', ['watch-unitTests', 'exec:runUnitTests']);
    grunt.registerTask('integrationTest', ['watch-integrationTests', 'exec:runIntegrationTests']);
    // grunt.registerTask('test', ['unitTest', 'integrationTest']);
    grunt.registerTask('test', ['unitTest']);

    // Deployment task
    grunt.registerTask('deploy', [
        'sshexec:take-app-offline',
        'get-artifacts',
        'sshexec:stop',
        'sshexec:remove-mono-cache',
        'sshexec:make-release-dir',
        'sshexec:update-symlinks',
        'sftp:deploy',
        'sshexec:set-config',
        'sshexec:start',
        'wait',
        'sshexec:put-app-online',
        'http:warmup',
        'clean:afterDeploy'
    ]);
};
