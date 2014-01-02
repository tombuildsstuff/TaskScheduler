var task = function(grunt){
    grunt.registerTask('publish-artifacts', [
        'artifactory:artifacts:publish'
    ]);
};

module.exports = task;
