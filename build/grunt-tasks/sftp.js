var task = {
    deploy: {
        files: {
            "./": "temp/buildOutput/Frontend/**"
        },
        options: {
            srcBasePath: "temp/buildOutput/Frontend/",
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
