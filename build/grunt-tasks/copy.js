var task = {
    buildOutput: {
        files: [
            {
                expand: true,
                src: ['Frontend/**', '!Frontend/**/*.cs', '!Frontend/obj/**'],
                dest: 'buildOutput/'
            }
        ]
    },
    buildOutputAsPackage: { // used when deploying locally to vagrant
        files: [
            {
                expand: true,
                cwd: "buildOutput/TaskScheduler",
                src: ["**"],
                dest: "temp/buildOutput/TaskScheduler"
            }
        ]
    }
};

module.exports = task;

