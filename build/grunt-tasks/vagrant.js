var shell = require('shelljs');

module.exports = function(grunt){

    grunt.registerTask('vagrant-setopt', function(){
        grunt.option('config', 'vagrant');
    });

    // Vagrant tasks
    grunt.registerTask('vagrant-up', function(){
        shell.exec('vagrant up --provider vmware_fusion');
        grunt.task.run(['build', 'vagrant-setopt', 'deploy']);
    });

    grunt.registerTask('vagrant-destroy', function(){
        shell.exec('vagrant destroy -f');
    });

    grunt.registerTask('vagrant-test', [ 'mocha' ]);
};