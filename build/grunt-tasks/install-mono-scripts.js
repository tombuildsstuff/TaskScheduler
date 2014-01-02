var task = function(grunt){
    grunt.registerTask('installMonoScripts', [
        'sftp:copySetupScripts',
        'sshexec:installMonoScript'
    ]);
};

module.exports = task;
