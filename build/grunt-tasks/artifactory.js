var task = {
    options: {
        url: '<%= grunt.option("artifactoryServer") %>',
        repository: 'internal',
        username: '<%= grunt.option("artifactoryUsername") %>',
        password: '<%= grunt.option("artifactoryPassword") %>',
        version: '<%= grunt.option("buildNumber") %>'
    },
    artifacts: {
        files: [
            { src: ['buildOutput/Frontend/**/*'] }
        ],
        options: {
            publish: [{
                id: 'web:TaskScheduler:tgz',
                path: 'buildOutput/'
            }],
            fetch: [{
                id: 'web:TaskScheduler:tgz',
                path: 'temp'
            }]
        }
    }
};

module.exports = task;
