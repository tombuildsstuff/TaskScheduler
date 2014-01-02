var task = {
    deploy: {
        files: {
            "./": "temp/buildOutput/TaskScheduler/**"
        },
        options: {
            srcBasePath: "temp/buildOutput/TaskScheduler/",
            createDirectories: true
        }
    },
    copySetupScripts: {
        files: {
            "./" : "setup/**"
        },
        options: {
            createDirectories: true
        }
    }
};

module.exports = task;
