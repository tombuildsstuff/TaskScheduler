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
                cwd: "buildOutput/Frontend",
                src: ["**"],
                dest: "temp/buildOutput/Frontend"
            }
        ]
    }
};

module.exports = task;

