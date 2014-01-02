var task = function(grunt){

    var tasklist = [
        'artifactory:artifacts:fetch'
    ];

    // when we're deploying to a Vagrant box, don't get the artifacts from JAWS
    // just package the local files
    if(!grunt.option('config') || grunt.option('config') === 'vagrant'){
        tasklist = [
            'copy:buildOutputAsPackage'
        ];
    }

    grunt.registerTask('get-artifacts', tasklist);
};

module.exports = task;
