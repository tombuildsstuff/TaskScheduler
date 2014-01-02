var task = {
    build: {
        src: [
            "**/bin/**",
            "**/obj/**",
            "buildOutput/**",
            "!node_modules/**",
            "!build/**",
            "!packages/**"
        ]
    },
    afterDeploy: {
        src: ["temp"]
    }
};

module.exports = task;
